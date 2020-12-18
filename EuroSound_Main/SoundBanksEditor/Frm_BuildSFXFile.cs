using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_BuildSFXFile : Form
    {
        private ProjectFile CurrentFileProperties;
        private string FileName;
        private List<string> Reports = new List<string>();
        public Frm_BuildSFXFile(ProjectFile FileProperties, string SoundBankFinalName)
        {
            InitializeComponent();
            CurrentFileProperties = FileProperties;
            FileName = SoundBankFinalName;
        }

        private void BackgroundWorker_BuildSFX_DoWork(object sender, DoWorkEventArgs e)
        {
            Reports.Clear();
            if (Directory.Exists(GlobalPreferences.SFXOutputPath))
            {
                //*===============================================================================================
                //* GLOBAL VARS
                //*===============================================================================================
                Dictionary<int, EXSound> FinalSoundsDict = new Dictionary<int, EXSound>();
                Dictionary<string, EXAudio> FinalAudioDataDict = new Dictionary<string, EXAudio>();
                EXBuildSFX SFXCreator = new EXBuildSFX();
                BinaryWriter BWriter = new BinaryWriter(File.Open(GlobalPreferences.SFXOutputPath + "\\" + FileName + ".SFX", FileMode.Create, FileAccess.Write), Encoding.ASCII);

                Form ParentForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", this.Tag.ToString());
                int TotalProgress = 1;

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
                ProgressBarValue(ProgressBar_CurrentTask, 0);

                //Update Label
                SetLabelText(Label_CurrentTask, "Getting SFX To export");

                //Update Progress Bar
                ProgressBarSetMaximum(ProgressBar_CurrentTask, ((Frm_Soundbanks_Main)ParentForm).SoundsList.Keys.Count);

                //Discard SFXs that has checked as "no output"
                foreach (KeyValuePair<int, EXSound> Sound in ((Frm_Soundbanks_Main)ParentForm).SoundsList)
                {
                    if (Sound.Value.OutputThisSound)
                    {
                        FinalSoundsDict.Add(Sound.Key, Sound.Value);
                    }
                    SetLabelText(Label_CurrentTask, "Checking SFX: " + Sound.Value.DisplayName);
                    ProgressBar_CurrentTask.Invoke((MethodInvoker)delegate
                    {
                        ProgressBar_CurrentTask.Value += 1;
                    });
                    //Thread.Sleep(1);
                }
                TotalProgress *= 20;
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                //*===============================================================================================
                //* STEP 2: DISCARD AUDIO DATA THAT SHOULD HAVE BEEN PURGED (40%)
                //*===============================================================================================
                //Check For Cancelation;
                if (BackgroundWorker_BuildSFX.CancellationPending == true)
                {
                    BWriter.Close();
                    BWriter.Dispose();
                    e.Cancel = true;
                }

                //Get Final Audios To Export
                ProgressBarValue(ProgressBar_CurrentTask, 0);

                //Update Label
                SetLabelText(Label_CurrentTask, "Getting Audio Data To export");
                List<string> UsedAudios = EXObjectsFunctions.GetUsedAudios(FinalSoundsDict, true);

                //Update Progress Bar
                ProgressBarSetMaximum(ProgressBar_CurrentTask, UsedAudios.Count);

                //Add data
                foreach (string Key in UsedAudios)
                {
                    KeyValuePair<string, EXAudio> ObjectToAdd;
                    if (((Frm_Soundbanks_Main)ParentForm).AudioDataDict.ContainsKey(Key))
                    {
                        ObjectToAdd = new KeyValuePair<string, EXAudio>(Key, ((Frm_Soundbanks_Main)ParentForm).AudioDataDict[Key]);
                        FinalAudioDataDict.Add(ObjectToAdd.Key, ObjectToAdd.Value);
                    }
                    ProgressBar_CurrentTask.Invoke((MethodInvoker)delegate
                    {
                        ProgressBar_CurrentTask.Value += 1;
                    });
                    //Thread.Sleep(1);
                }

                //Reset maximum
                ProgressBarSetMaximum(ProgressBar_CurrentTask, 100);

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
                    e.Cancel = true;
                }

                //Get Final Audios To Export
                ProgressBarValue(ProgressBar_CurrentTask, 0);
                //--------------------------------------[WRITE FILE HEADER]--------------------------------------
                /*Update Label*/
                SetLabelText(Label_CurrentTask, "Writting File Header");

                /*Write Data*/
                SFXCreator.WriteFileHeader(BWriter, CurrentFileProperties.Hashcode);

                /*Update Progress Bar*/
                ProgressBar_CurrentTask.Invoke((MethodInvoker)delegate
                {
                    ProgressBar_CurrentTask.Value += 20;
                });

                //--------------------------------------[Write SECTIONS]--------------------------------------
                /*Update Label*/
                SetLabelText(Label_CurrentTask, "Writting File Sections");

                /*Write Data*/
                SFXCreator.WriteFileSections(BWriter, CountNumberOfSamples(FinalSoundsDict));

                /*Update Progress Bar*/
                ProgressBar_CurrentTask.Invoke((MethodInvoker)delegate
                {
                    ProgressBar_CurrentTask.Value += 20;
                });

                //--------------------------------------[SECTION SFX elements]--------------------------------------
                /*Update Label*/
                SetLabelText(Label_CurrentTask, "Writting SFX Section");

                /*Write Data*/
                SFXCreator.WriteSFXSection(BWriter, FinalSoundsDict, FinalAudioDataDict);

                /*Update Progress Bar*/
                ProgressBar_CurrentTask.Invoke((MethodInvoker)delegate
                {
                    ProgressBar_CurrentTask.Value += 20;
                });

                //--------------------------------------[SECTION Sample info elements]--------------------------------------
                /*Update Label*/
                SetLabelText(Label_CurrentTask, "Writting Sample Info Section");

                /*Write Data*/
                SFXCreator.WriteSampleInfoSection(BWriter, FinalAudioDataDict);

                /*Update Progress Bar*/
                ProgressBar_CurrentTask.Invoke((MethodInvoker)delegate
                {
                    ProgressBar_CurrentTask.Value += 20;
                });

                //--------------------------------------[SECTION Sample data]--------------------------------------
                /*Update Label*/
                SetLabelText(Label_CurrentTask, "Writting Sample Data Section");

                /*Write Data*/
                SFXCreator.WriteSampleDataSection(BWriter, FinalAudioDataDict);

                /*Update Progress Bar*/
                ProgressBar_CurrentTask.Invoke((MethodInvoker)delegate
                {
                    ProgressBar_CurrentTask.Value += 20;
                });

                /*Update Total Progress*/
                TotalProgress += 40;
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                //*===============================================================================================
                //* STEP 4: WRITE FINAL DATA (80%)
                //*===============================================================================================
                //Check For Cancelation;
                if (BackgroundWorker_BuildSFX.CancellationPending == true)
                {
                    BWriter.Close();
                    BWriter.Dispose();
                    e.Cancel = true;
                }

                //Update Label
                SetLabelText(Label_CurrentTask, "Writting final offsets");

                /*Write Data*/
                SFXCreator.WriteFinalOffsets(BWriter);
                BWriter.Close();
                BWriter.Dispose();

                //Update Label
                SetLabelText(Label_CurrentTask, "Output Completed");

                /*Update Progress Bar*/
                ProgressBar_CurrentTask.Invoke((MethodInvoker)delegate
                {
                    ProgressBar_CurrentTask.Value = 100;
                });

                /*Update Total Progress*/
                TotalProgress += 20;
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
                Reports.Add("2Output cancelled by user");
            }
            else if (e.Error != null)
            {
                Reports.Add("0An error occurred in the output and the file might be corrupt");
            }

            if (Reports.Count > 0)
            {
                //Show Errors
                EuroSound_ErrorsAndWarningsList ImportResults = new EuroSound_ErrorsAndWarningsList(Reports)
                {
                    Text = FileName + ".SFX Output Errors",
                    ShowInTaskbar = false,
                    TopMost = true
                };
                ImportResults.ShowDialog();
                ImportResults.Dispose();
            }

            //Close Form
            this.Close();
            this.Dispose();
        }

        private void Button_Abort_Click(object sender, EventArgs e)
        {
            BackgroundWorker_BuildSFX.CancelAsync();
        }

        private int CountNumberOfSamples(Dictionary<int, EXSound> SoundsList)
        {
            int Counter = 0;
            foreach (KeyValuePair<int, EXSound> Sound in SoundsList)
            {
                foreach (EXSample Sample in Sound.Value.Samples)
                {
                    if (!Sample.IsStreamed)
                    {
                        Counter++;
                    }
                }
            }
            return Counter;
        }

        private void Frm_BuildSFXFile_Load(object sender, EventArgs e)
        {
            //Run Background Worker
            if (!BackgroundWorker_BuildSFX.IsBusy)
            {
                BackgroundWorker_BuildSFX.RunWorkerAsync();
            }
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