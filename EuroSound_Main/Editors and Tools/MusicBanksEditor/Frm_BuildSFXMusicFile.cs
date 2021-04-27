using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.Classes.SFX_Files;
using EuroSound_Application.CurrentProjectFunctions;
using NAudio.Wave;
using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application.Musics
{
    public partial class Frm_BuildSFXMusicFile : Form
    {
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private BinaryStream BWriter;
        private ProjectFile CurrentFileProperties;
        private List<string> Reports = new List<string>();
        private string FileName;
        private int DebugFlags;
        private WaveOut outputSound;

        public Frm_BuildSFXMusicFile(ProjectFile FileProperties, string MusicBankFinalName, int CheckedDebugFlags)
        {
            InitializeComponent();
            CurrentFileProperties = FileProperties;
            FileName = MusicBankFinalName;
            DebugFlags = CheckedDebugFlags;
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_BuildSFXStreamFile_Load(object sender, EventArgs e)
        {
            //Run Background Worker
            if (!BackgroundWorker_BuildSFX.IsBusy)
            {
                BackgroundWorker_BuildSFX.RunWorkerAsync();
            }
        }

        //*===============================================================================================
        //* BACKGROUND WORKER
        //*===============================================================================================
        private void BackgroundWorker_BuildSFX_DoWork(object sender, DoWorkEventArgs e)
        {
            Reports.Clear();
            if (Directory.Exists(GlobalPreferences.MusicOutputPath))
            {
                Dictionary<uint, EXMusic> FinalMusicsDict;
                GenerateSFXMusicBank SFXCreator = new GenerateSFXMusicBank();
                Form ParentForm = GenericFunctions.GetFormByName("Frm_Musics_Main", Tag.ToString());
                SFX_ChecksBeforeGeneration SFX_Check = new SFX_ChecksBeforeGeneration();
                bool CanOutputFile = true;
                string CurrentObjectName;
                int TotalProgress = 1;

                BWriter = new BinaryStream(File.Open(GlobalPreferences.MusicOutputPath + "\\" + FileName + ".SFX", FileMode.Create, FileAccess.Write), null, Encoding.ASCII);

                //Check For Cancelation;
                if (BackgroundWorker_BuildSFX.CancellationPending == true)
                {
                    BWriter.Close();
                    BWriter.Dispose();
                    e.Cancel = true;
                }
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                //*===============================================================================================
                //* STEP 1: DISCARD SFX THAT WILL NOT BE OUTPUTED (20%)
                //*===============================================================================================
                GenericFunctions.ProgressBarSetMaximum(ProgressBar_CurrentTask, 1);
                GenericFunctions.ProgressBarValue(ProgressBar_CurrentTask, 0);

                //Update Label
                GenericFunctions.SetLabelText(Label_CurrentTask, "Getting SFX To export");

                //Update Progress Bar
                GenericFunctions.ProgressBarSetMaximum(ProgressBar_CurrentTask, ((Frm_Musics_Main)ParentForm).MusicsList.Keys.Count);

                //Discard SFXs that has checked as "no output"
                FinalMusicsDict = SFXCreator.GetFinalMusicsDictionary(((Frm_Musics_Main)ParentForm).MusicsList, ProgressBar_CurrentTask, Label_CurrentTask);
                GenericFunctions.ProgressBarValue(ProgressBar_CurrentTask, 1);

                //*===============================================================================================
                //* STEP 2: CHECK DATA THAT WILL BE OUTPUTED (30%)
                //*===============================================================================================
                //Update Label
                GenericFunctions.SetLabelText(Label_CurrentTask, "Checking data");

                //Update Progress Bar
                GenericFunctions.ProgressBarSetMaximum(ProgressBar_CurrentTask, FinalMusicsDict.Count);
                GenericFunctions.ProgressBarValue(ProgressBar_CurrentTask, 0);

                //Check Data
                foreach (KeyValuePair<uint, EXMusic> MusicToCheck in FinalMusicsDict)
                {
                    CurrentObjectName = ((Frm_Musics_Main)ParentForm).TreeView_MusicData.Nodes.Find(MusicToCheck.Key.ToString(), true)[0].Text;
                    CanOutputFile = SFX_Check.ValidateMusics(MusicToCheck.Value, CurrentObjectName, Reports);
                    if (CanOutputFile == false)
                    {
                        break;
                    }
                    else
                    {
                        CanOutputFile = SFX_Check.ValidateMarkers(MusicToCheck.Value.Markers, CurrentObjectName, Reports);
                        if (CanOutputFile == false)
                        {
                            break;
                        }
                    }
                    GenericFunctions.ProgressBarAddValue(ProgressBar_CurrentTask, 1);
                }

                //Update Total Progress
                TotalProgress += 20;
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                //*===============================================================================================
                //* STEP 3: START WRITTING (70%)
                //*===============================================================================================
                if (CanOutputFile)
                {
                    if (FinalMusicsDict.Count == 1)
                    {
                        TotalProgress += 20;
                        BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                        //Check For Cancelation;
                        if (BackgroundWorker_BuildSFX.CancellationPending == true)
                        {
                            BWriter.Close();
                            BWriter.Dispose();
                            e.Cancel = true;
                        }

                        //--------------------------------------[WRITE FILE HEADER]--------------------------------------
                        //Update Label
                        GenericFunctions.SetLabelText(Label_CurrentTask, "Writting File Header");

                        //Write Header
                        SFXCreator.WriteFileHeader(BWriter, CurrentFileProperties.Hashcode, ProgressBar_CurrentTask);

                        //Update Total Progress
                        TotalProgress += 10;
                        BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                        //--------------------------------------[Write SECTIONS]--------------------------------------
                        //Update Label
                        GenericFunctions.SetLabelText(Label_CurrentTask, "Writting File Sections");

                        //Write Sections
                        SFXCreator.WriteFileSections(BWriter, ProgressBar_CurrentTask);

                        //Update Total Progress
                        TotalProgress += 10;
                        BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                        //--------------------------------------[SECTION File Section 1]--------------------------------------
                        //Update Label
                        GenericFunctions.SetLabelText(Label_CurrentTask, "Writting Markers");

                        //Write Table
                        SFXCreator.WriteFileSection1(BWriter, FinalMusicsDict, ProgressBar_CurrentTask);

                        //Update Total Progress
                        TotalProgress += 10;
                        BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                        //--------------------------------------[SECTION File Section 2]--------------------------------------
                        //Update Label
                        GenericFunctions.SetLabelText(Label_CurrentTask, "Writting IMA ADPCM Data");

                        //Write Data
                        SFXCreator.WriteFileSection2(BWriter, FinalMusicsDict, ProgressBar_CurrentTask);

                        //Update Total Progress
                        TotalProgress += 10;
                        BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                        //Update Total Progress
                        TotalProgress += 10;
                        BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                        //*===============================================================================================
                        //* STEP 4: WRITE FINAL DATA (80%)
                        //*===============================================================================================
                        //Check For Cancelation
                        if (BackgroundWorker_BuildSFX.CancellationPending == true)
                        {
                            BWriter.Close();
                            BWriter.Dispose();
                            e.Cancel = true;
                        }

                        //Update Label
                        GenericFunctions.SetLabelText(Label_CurrentTask, "WrittingFinalOffsets");

                        //Write Offsets
                        SFXCreator.WriteFinalOffsets(BWriter, ProgressBar_CurrentTask);
                    }

                    //Close Writer
                    BWriter.Close();
                    BWriter.Dispose();

                    //*===============================================================================================
                    //* STEP 5: CREATE DEBUG FILE IF REQUIRED
                    //*===============================================================================================
                    if (CanOutputFile)
                    {
                        if (DebugFlags > 0)
                        {
                            MusicBanks_DebugWriter DBGWritter = new MusicBanks_DebugWriter();

                            //Update Label
                            GenericFunctions.SetLabelText(Label_CurrentTask, "Creating debug file");

                            //Create file
                            DBGWritter.CreateDebugFile(GlobalPreferences.MusicOutputPath + "\\" + FileName + ".SFX", DebugFlags);
                        }
                    }

                    //*===============================================================================================
                    //* STEP 6: BUILD FILELIST
                    //*===============================================================================================
                    //Update Label
                    GenericFunctions.SetLabelText(Label_CurrentTask, "Building Filelist");

                    GenericFunctions.BuildSphinxFilelist();

                    //Update Label
                    GenericFunctions.SetLabelText(Label_CurrentTask, "Output Completed");

                    //Update Total Progress
                    TotalProgress += 9;
                    BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);
                }
                else
                {
                    Reports.Add("0The music has unchecked the flag \"Output this music\"");
                }
            }
            else
            {
                Reports.Add("0Unable to open " + GlobalPreferences.MusicOutputPath + "\\" + FileName + ".SFX");
            }
        }

        private void BackgroundWorker_BuildSFX_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar_Total.Value = e.ProgressPercentage;
        }

        private void BackgroundWorker_BuildSFX_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Reports.Add("2" + GenericFunctions.ResourcesManager.GetString("OutputSFXSoundbankCancelled"));
            }
            else if (e.Error != null)
            {
                Reports.Add("0" + GenericFunctions.ResourcesManager.GetString("OutputSFXSoundbankErrors"));
            }

            if (Reports.Count > 0)
            {
                GenericFunctions.ShowErrorsAndWarningsList(Reports, FileName + ".SFX Output Errors", this);
            }

            //Play Sound
            if (GlobalPreferences.PlaySoundWhenOutput)
            {
                if (File.Exists(GlobalPreferences.OutputSoundPath))
                {
                    Wave16ToFloatProvider provider = new Wave16ToFloatProvider(new WaveFileReader(GlobalPreferences.OutputSoundPath));
                    outputSound = new WaveOut();
                    if (outputSound.PlaybackState == PlaybackState.Stopped)
                    {
                        outputSound.Init(provider);
                        outputSound.PlaybackStopped += new EventHandler<StoppedEventArgs>(AudioOutput_PlaybackStopped);
                        outputSound.Play();
                    }
                }
            }
            else
            {
                //Close Form
                Close();
                Dispose();
            }
        }

        private void AudioOutput_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            //Close Form
            Close();
            Dispose();
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_Abort_Click(object sender, EventArgs e)
        {
            if (BackgroundWorker_BuildSFX.IsBusy)
            {
                BackgroundWorker_BuildSFX.CancelAsync();
            }
        }

        private void Frm_BuildSFXMusicFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BWriter != null)
            {
                BWriter.Close();
                BWriter.Dispose();
            }
            if (BackgroundWorker_BuildSFX.IsBusy)
            {
                BackgroundWorker_BuildSFX.Dispose();
            }

            //Stop sound playing
            if (outputSound != null)
            {
                if (outputSound.PlaybackState == PlaybackState.Playing)
                {
                    outputSound.Stop();
                }
                outputSound.Dispose();
            }
        }
    }
}
