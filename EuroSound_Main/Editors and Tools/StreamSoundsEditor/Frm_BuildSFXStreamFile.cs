using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.CurrentProjectFunctions;
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
                BinaryStream BWriter = new BinaryStream(File.Open(GlobalPreferences.SFXOutputPath + "\\" + FileName + ".SFX", FileMode.Create, FileAccess.Write), null, Encoding.ASCII);
                StreamWriter DebugFileWritter = null;

                Form ParentForm = GenericFunctions.GetFormByName("Frm_StreamSoundsEditorMain", Tag.ToString());
                int TotalProgress = 1;

                //Check For Cancelation;
                if (BackgroundWorker_BuildSFX.CancellationPending == true)
                {
                    BWriter.Close();
                    BWriter.Dispose();
                    if (DebugFileWritter != null)
                    {
                        DebugFileWritter.Close();
                    }
                    e.Cancel = true;
                }
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                //Check if user wants a debug file
                if (DebugFlags != 0)
                {
                    DebugFileWritter = new StreamWriter(GlobalPreferences.SFXOutputPath + "\\" + FileName + ".dbg");
                    DebugFileWritter.WriteLine(new String('/', 70));
                    DebugFileWritter.WriteLine("// EngineX Output: " + GlobalPreferences.SFXOutputPath + "\\" + FileName + ".SFX");
                    DebugFileWritter.WriteLine("// Soundbank: " + CurrentFileProperties.FileName);
                    DebugFileWritter.WriteLine("// Output By: " + Environment.UserName);
                    DebugFileWritter.WriteLine("// Output Date: " + DateTime.Now);
                    DebugFileWritter.WriteLine(new String('/', 70) + "\n");
                }

                //*===============================================================================================
                //* STEP 1: DISCARD SFX THAT WILL NOT BE OUTPUTED (20%)
                //*===============================================================================================
                ProgressBarValue(ProgressBar_CurrentTask, 0);

                //Update Label
                SetLabelText(Label_CurrentTask, "Getting SFX To export");

                //Update Progress Bar
                ProgressBarSetMaximum(ProgressBar_CurrentTask, ((Frm_StreamSoundsEditorMain)ParentForm).StreamSoundsList.Keys.Count);

                //Discard SFXs that has checked as "no output"
                FinalSoundsDict = SFXCreator.GetFinalSoundsDictionary(((Frm_StreamSoundsEditorMain)ParentForm).StreamSoundsList, ProgressBar_CurrentTask, Label_CurrentTask);

                TotalProgress += 20;
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                //*===============================================================================================
                //* STEP 3: START WRITTING (80%)
                //*===============================================================================================
                //Check For Cancelation;
                if (BackgroundWorker_BuildSFX.CancellationPending == true)
                {
                    BWriter.Close();
                    BWriter.Dispose();
                    if (DebugFileWritter != null)
                    {
                        DebugFileWritter.Close();
                    }
                    e.Cancel = true;
                }

                //--------------------------------------[WRITE FILE HEADER]--------------------------------------
                //Update Label
                SetLabelText(Label_CurrentTask, "Writting File Header");

                //Write Header
                SFXCreator.WriteFileHeader(BWriter, CurrentFileProperties.Hashcode, ProgressBar_CurrentTask);

                //Update Total Progress
                TotalProgress += 10;
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);


                //--------------------------------------[Write SECTIONS]--------------------------------------
                //Update Label
                SetLabelText(Label_CurrentTask, "Writting File Sections");

                //Write Sections
                SFXCreator.WriteFileSections(BWriter, ProgressBar_CurrentTask);

                //Update Total Progress
                TotalProgress += 10;
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);


                //--------------------------------------[SECTION Look up Table]--------------------------------------
                //Update Label
                SetLabelText(Label_CurrentTask, "Writting Look Up Table");

                //Write Table
                SFXCreator.WriteLookUptable(BWriter, FinalSoundsDict, ProgressBar_CurrentTask);

                //Update Total Progress
                TotalProgress += 10;
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                //--------------------------------------[SECTION Sample info elements]--------------------------------------
                //Update Label
                SetLabelText(Label_CurrentTask, "Writting Markers");

                //Write Data
                SFXCreator.WriteStreamFile(BWriter, FinalSoundsDict, DebugFlags, DebugFileWritter, ProgressBar_CurrentTask);

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
                    if (DebugFileWritter != null)
                    {
                        DebugFileWritter.Close();
                    }
                    e.Cancel = true;
                }

                //Update Label
                SetLabelText(Label_CurrentTask, "WrittingFinalOffsets");

                //Write Offsets
                SFXCreator.WriteFinalOffsets(BWriter, CurrentFileProperties.Hashcode, DebugFlags, DebugFileWritter, ProgressBar_CurrentTask);

                //Close Writer
                BWriter.Close();
                BWriter.Dispose();

                //Close DebugFile
                if (DebugFileWritter != null)
                {
                    DebugFileWritter.Close();
                }

                //Clear Temporal Dictionary
                FinalSoundsDict.Clear();

                //*===============================================================================================
                //* STEP 5: BUILD FILELIST
                //*===============================================================================================
                //Update Label
                SetLabelText(Label_CurrentTask, "Building Filelist");

                GenericFunctions.BuildSphinxFilelist();

                //Update Label
                SetLabelText(Label_CurrentTask, "Output Completed");

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

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void ProgressBarSetMaximum(ProgressBar BarToChange, int Maximum)
        {
            BarToChange.Invoke((MethodInvoker)delegate
            {
                BarToChange.Maximum = Maximum;
            });
        }

        private void ProgressBarValue(ProgressBar BarToChange, int value)
        {
            BarToChange.Invoke((MethodInvoker)delegate
            {
                BarToChange.Value = value;
            });
        }

        private void SetLabelText(Label LabelToChange, string TextToShow)
        {
            LabelToChange.Invoke((MethodInvoker)delegate
            {
                LabelToChange.Text = TextToShow;
            });
        }
    }
}
