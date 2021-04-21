using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.Classes.SFX_Files;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.GenerateSoundBankSFX;
using EuroSound_Application.SoundBanksEditor.Debug_Writer;
using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application.SoundBanksEditor.BuildSFX
{
    public partial class Frm_BuildSFXFile : Form
    {
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private BinaryStream BWriter;
        private ProjectFile CurrentFileProperties;
        private List<string> Reports = new List<string>();
        private string FileName;
        private int DebugFlags;

        public Frm_BuildSFXFile(ProjectFile FileProperties, string SoundBankFinalName, int CheckedDebugFlags)
        {
            InitializeComponent();
            CurrentFileProperties = FileProperties;
            FileName = SoundBankFinalName;
            DebugFlags = CheckedDebugFlags;
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_BuildSFXFile_Load(object sender, EventArgs e)
        {
            //Run Background Worker
            if (!BackgroundWorker_BuildSFX.IsBusy)
            {
                BackgroundWorker_BuildSFX.RunWorkerAsync();
            }
        }

        private void Frm_BuildSFXFile_FormClosing(object sender, FormClosingEventArgs e)
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

        private void BackgroundWorker_BuildSFX_DoWork(object sender, DoWorkEventArgs e)
        {
            Reports.Clear();
            if (Directory.Exists(GlobalPreferences.SFXOutputPath))
            {
                //*===============================================================================================
                //* GLOBAL VARS
                //*===============================================================================================
                Dictionary<uint, EXSound> FinalSoundsDict;
                Dictionary<string, EXAudio> FinalAudioDataDict;
                List<uint> SoundsHashcodes = new List<uint>();
                GenerateSFXSoundBank SFXCreator = new GenerateSFXSoundBank();
                Form ParentForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", Tag.ToString());
                SFX_ChecksBeforeGeneration SFX_Check = new SFX_ChecksBeforeGeneration();
                bool CanOutputFile = true;

                int TotalProgress = 1;

                BWriter = new BinaryStream(File.Open(GlobalPreferences.SFXOutputPath + "\\" + FileName + ".SFX", FileMode.Create, FileAccess.Write), null, Encoding.ASCII);
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
                GenericFunctions.ProgressBarValue(ProgressBar_CurrentTask, 0);

                //Update Label
                GenericFunctions.SetLabelText(Label_CurrentTask, "Getting SFX To export");

                //Update Progress Bar
                GenericFunctions.ProgressBarSetMaximum(ProgressBar_CurrentTask, ((Frm_Soundbanks_Main)ParentForm).SoundsList.Keys.Count);

                //Discard SFXs that has checked as "no output"
                FinalSoundsDict = SFXCreator.GetFinalSoundsDictionary(((Frm_Soundbanks_Main)ParentForm).SoundsList, ProgressBar_CurrentTask, Label_CurrentTask);

                TotalProgress += 20;
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                //*===============================================================================================
                //* STEP 2: DISCARD AUDIO DATA THAT SHOULD HAVE BEEN PURGED (40%)
                //*===============================================================================================
                //Get Final Audios To Export
                GenericFunctions.ProgressBarValue(ProgressBar_CurrentTask, 0);

                //Update Label
                GenericFunctions.SetLabelText(Label_CurrentTask, "Getting Audio Data To export");
                IEnumerable<string> UsedAudios = EXSoundbanksFunctions.GetAudiosToExport(FinalSoundsDict);

                //Add data
                FinalAudioDataDict = SFXCreator.GetFinalAudioDictionaryPCMData(UsedAudios, ((Frm_Soundbanks_Main)ParentForm).AudioDataDict, ProgressBar_CurrentTask);

                //Update Total Progress
                TotalProgress += 20;
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                //*===============================================================================================
                //* STEP 3: CHECK DATA THAT WILL BE OUTPUTED (50%)
                //*===============================================================================================
                //Update Label
                GenericFunctions.SetLabelText(Label_CurrentTask, "Checking data");

                //Update Progress Bar
                GenericFunctions.ProgressBarSetMaximum(ProgressBar_CurrentTask, FinalSoundsDict.Count + FinalAudioDataDict.Count);
                GenericFunctions.ProgressBarValue(ProgressBar_CurrentTask, 0);

                //Check Data
                foreach (KeyValuePair<uint, EXSound> SoundToCheck in FinalSoundsDict)
                {
                    CanOutputFile = SFX_Check.ValidateSFX(SoundToCheck.Value, SoundsHashcodes, ((Frm_Soundbanks_Main)ParentForm).TreeView_File.Nodes.Find(SoundToCheck.Key.ToString(), true)[0].Text, Reports);
                    if (CanOutputFile == false)
                    {
                        break;
                    }
                    GenericFunctions.ProgressBarAddValue(ProgressBar_CurrentTask, 1);
                }

                if (CanOutputFile)
                {
                    foreach (KeyValuePair<string, EXAudio> AudioToCheck in FinalAudioDataDict)
                    {
                        CanOutputFile = SFX_Check.ValidateAudios(AudioToCheck.Value, ((Frm_Soundbanks_Main)ParentForm).TreeView_File.Nodes.Find(AudioToCheck.Key, true)[0].Text, Reports);
                        if (CanOutputFile == false)
                        {
                            break;
                        }
                        GenericFunctions.ProgressBarAddValue(ProgressBar_CurrentTask, 1);
                    }
                }

                //Update Total Progress
                TotalProgress += 10;
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                //*===============================================================================================
                //* STEP 4: START WRITTING (70%)
                //*===============================================================================================
                if (CanOutputFile)
                {
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

                    //Write Data
                    SFXCreator.WriteFileHeader(BWriter, CurrentFileProperties.Hashcode, ProgressBar_CurrentTask);

                    //Update Total Progress
                    TotalProgress += 10;
                    BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);


                    //--------------------------------------[Write SECTIONS]--------------------------------------
                    //Update Label
                    GenericFunctions.SetLabelText(Label_CurrentTask, "Writting File Sections");

                    //Write Data
                    SFXCreator.WriteFileSections(BWriter, GenericFunctions.CountNumberOfSamples(FinalSoundsDict), ProgressBar_CurrentTask);

                    //Update Total Progress
                    TotalProgress += 10;
                    BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);


                    //--------------------------------------[SECTION SFX elements]--------------------------------------
                    //Write Data
                    SFXCreator.WriteSFXSection(BWriter, FinalSoundsDict, FinalAudioDataDict, ProgressBar_CurrentTask, Label_CurrentTask);

                    //Update Total Progress
                    TotalProgress += 10;
                    BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);


                    //--------------------------------------[SECTION Sample info elements]--------------------------------------
                    //Write Data
                    SFXCreator.WriteSampleInfoSection(BWriter, FinalAudioDataDict, ProgressBar_CurrentTask, Label_CurrentTask);

                    //Update Total Progress
                    TotalProgress += 5;
                    BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                    //--------------------------------------[SECTION Sample data]--------------------------------------
                    //Write Data
                    SFXCreator.WriteSampleDataSection(BWriter, FinalAudioDataDict, ProgressBar_CurrentTask, Label_CurrentTask);

                    //Update Total Progress
                    TotalProgress += 5;
                    BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);


                    //*===============================================================================================
                    //* STEP 5: WRITE FINAL DATA (80%)
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

                    //Write Data
                    SFXCreator.WriteFinalOffsets(BWriter, ProgressBar_CurrentTask, Label_CurrentTask);

                }
                //Close Binary Writter
                BWriter.Close();
                BWriter.Dispose();

                //Clear Temporal Dictionaries
                FinalSoundsDict.Clear();
                FinalAudioDataDict.Clear();

                //*===============================================================================================
                //* STEP 6: CREATE DEBUG FILE IF REQUIRED
                //*===============================================================================================
                if (CanOutputFile)
                {
                    if (DebugFlags > 0)
                    {
                        SoundBanks_DebugWriter DBGWritter = new SoundBanks_DebugWriter();

                        //Update Label
                        GenericFunctions.SetLabelText(Label_CurrentTask, "Creating debug file");

                        //Create file
                        DBGWritter.CreateDebugFile(GlobalPreferences.SFXOutputPath + "\\" + FileName + ".SFX", DebugFlags);
                    }
                }

                //*===============================================================================================
                //* STEP 7: BUILD FILELIST (100%)
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
                Reports.Add("0Unable to open " + GlobalPreferences.SFXOutputPath + "\\" + FileName + ".SFX");
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
                //Show Errors
                GenericFunctions.ShowErrorsAndWarningsList(Reports, FileName + ".SFX Output Errors", this);
            }

            //Close Form
            Close();
            Dispose();
        }
    }
}