using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.Classes.SFX_Files;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.Editors_and_Tools.StreamSoundsEditor.Debug_Writer;
using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application.StreamSounds.BuildSFX
{
    public partial class Frm_BuildSFXStreamFile : Form
    {
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private ProjectFile CurrentFileProperties;
        private List<string> Reports = new List<string>();
        private string FileName;
        private int DebugFlags;

        public Frm_BuildSFXStreamFile(ProjectFile FileProperties, string SoundBankFinalName, int CheckedDebugFlags)
        {
            InitializeComponent();
            CurrentFileProperties = FileProperties;
            FileName = SoundBankFinalName;
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
            if (Directory.Exists(GlobalPreferences.SFXOutputPath))
            {
                Dictionary<uint, EXSoundStream> FinalSoundsDict;
                GenerateSFXStreamedSounds SFXCreator = new GenerateSFXStreamedSounds();
                SFX_ChecksBeforeGeneration SFX_Check = new SFX_ChecksBeforeGeneration();
                Form ParentForm = GenericFunctions.GetFormByName("Frm_StreamSoundsEditorMain", Tag.ToString());
                bool CanOutputFile = true;
                string CurrentObjectName;
                int TotalProgress = 1;

                BinaryStream BWriter = new BinaryStream(File.Open(GlobalPreferences.SFXOutputPath + "\\" + FileName + ".SFX", FileMode.Create, FileAccess.Write), null, Encoding.ASCII);

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
                GenericFunctions.ProgressBarSetMaximum(ProgressBar_CurrentTask, ((Frm_StreamSounds_Main)ParentForm).StreamSoundsList.Keys.Count);

                //Discard SFXs that has checked as "no output"
                FinalSoundsDict = SFXCreator.GetFinalSoundsDictionary(((Frm_StreamSounds_Main)ParentForm).StreamSoundsList, ProgressBar_CurrentTask, Label_CurrentTask);

                TotalProgress += 20;
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                //*===============================================================================================
                //* STEP 2: CHECK DATA THAT WILL BE OUTPUTED (30%)
                //*===============================================================================================
                //Update Label
                GenericFunctions.SetLabelText(Label_CurrentTask, "Checking data");

                //Update Progress Bar
                GenericFunctions.ProgressBarSetMaximum(ProgressBar_CurrentTask, FinalSoundsDict.Count);
                GenericFunctions.ProgressBarValue(ProgressBar_CurrentTask, 0);

                //Check Data
                foreach (KeyValuePair<uint, EXSoundStream> SoundToCheck in FinalSoundsDict)
                {
                    CurrentObjectName = ((Frm_StreamSounds_Main)ParentForm).TreeView_StreamData.Nodes.Find(SoundToCheck.Key.ToString(), true)[0].Text;
                    CanOutputFile = SFX_Check.ValidateStreamingSounds(SoundToCheck.Value, CurrentObjectName, Reports);
                    if (CanOutputFile == false)
                    {
                        break;
                    }
                    else
                    {
                        CanOutputFile = SFX_Check.ValidateMarkers(SoundToCheck.Value.Markers, CurrentObjectName, Reports);
                        if (CanOutputFile == false)
                        {
                            break;
                        }
                    }
                    GenericFunctions.ProgressBarAddValue(ProgressBar_CurrentTask, 1);
                }

                //Update Total Progress
                TotalProgress += 10;
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                //*===============================================================================================
                //* STEP 3: START WRITTING (70%)
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


                    //--------------------------------------[SECTION Look up Table]--------------------------------------
                    //Update Label
                    GenericFunctions.SetLabelText(Label_CurrentTask, "Writting Look Up Table");

                    //Write Table
                    SFXCreator.WriteLookUptable(BWriter, FinalSoundsDict, ProgressBar_CurrentTask);

                    //Update Total Progress
                    TotalProgress += 10;
                    BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                    //--------------------------------------[SECTION Sample info elements]--------------------------------------
                    //Update Label
                    GenericFunctions.SetLabelText(Label_CurrentTask, "Writting Markers");

                    //Write Data
                    SFXCreator.WriteStreamFile(BWriter, FinalSoundsDict, ProgressBar_CurrentTask);

                    //Update Total Progress
                    TotalProgress += 5;
                    BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                    //Update Total Progress
                    TotalProgress += 5;
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

                //Clear Temporal Dictionary
                FinalSoundsDict.Clear();

                //*===============================================================================================
                //* STEP 5: CREATE DEBUG FILE IF REQUIRED
                //*===============================================================================================
                if (CanOutputFile)
                {
                    if (DebugFlags > 0)
                    {
                        StreamSounds_DebugWriter DBGWritter = new StreamSounds_DebugWriter();

                        //Update Label
                        GenericFunctions.SetLabelText(Label_CurrentTask, "Creating debug file");

                        //Create file
                        DBGWritter.CreateDebugFile(GlobalPreferences.SFXOutputPath + "\\" + FileName + ".SFX", DebugFlags);
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
                GenericFunctions.ShowErrorsAndWarningsList(Reports, FileName + ".SFX Output Errors", this);
            }

            //Close Form
            Close();
            Dispose();
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_Abort_Click(object sender, EventArgs e)
        {
            BackgroundWorker_BuildSFX.CancelAsync();
        }
    }
}
