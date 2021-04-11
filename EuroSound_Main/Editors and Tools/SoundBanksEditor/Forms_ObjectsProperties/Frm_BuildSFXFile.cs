﻿using EuroSound_Application.ApplicationPreferences;
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
            BackgroundWorker_BuildSFX.CancelAsync();
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
                GenerateSFXSoundBank SFXCreator = new GenerateSFXSoundBank();
                StreamWriter DebugFileWritter = null;

                Form ParentForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", Tag.ToString());
                int TotalProgress = 1;

                BWriter = new BinaryStream(File.Open(GlobalPreferences.SFXOutputPath + "\\" + FileName + ".SFX", FileMode.Create, FileAccess.Write), null, Encoding.ASCII);
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

                //*===============================================================================================
                //* STEP 1: DISCARD SFX THAT WILL NOT BE OUTPUTED (20%)
                //*===============================================================================================
                ProgressBarValue(ProgressBar_CurrentTask, 0);

                //Update Label
                SetLabelText(Label_CurrentTask, "Getting SFX To export");

                //Update Progress Bar
                ProgressBarSetMaximum(ProgressBar_CurrentTask, ((Frm_Soundbanks_Main)ParentForm).SoundsList.Keys.Count);

                //Discard SFXs that has checked as "no output"
                FinalSoundsDict = SFXCreator.GetFinalSoundsDictionary(((Frm_Soundbanks_Main)ParentForm).SoundsList, ProgressBar_CurrentTask, Label_CurrentTask);

                TotalProgress += 20;
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                //*===============================================================================================
                //* STEP 2: DISCARD AUDIO DATA THAT SHOULD HAVE BEEN PURGED (40%)
                //*===============================================================================================
                //Get Final Audios To Export
                ProgressBarValue(ProgressBar_CurrentTask, 0);

                //Update Label
                SetLabelText(Label_CurrentTask, "Getting Audio Data To export");
                IEnumerable<string> UsedAudios = EXSoundbanksFunctions.GetAudiosToExport(FinalSoundsDict);

                //Add data
                FinalAudioDataDict = SFXCreator.GetFinalAudioDictionaryPCMData(UsedAudios, ((Frm_Soundbanks_Main)ParentForm).AudioDataDict, ProgressBar_CurrentTask);

                //Update Total Progress
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

                //Write Data
                SFXCreator.WriteFileHeader(BWriter, CurrentFileProperties.Hashcode, ProgressBar_CurrentTask);

                //Update Total Progress
                TotalProgress += 10;
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);


                //--------------------------------------[Write SECTIONS]--------------------------------------
                //Update Label
                SetLabelText(Label_CurrentTask, "Writting File Sections");

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
                TotalProgress += 10;
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);

                //--------------------------------------[SECTION Sample data]--------------------------------------
                //Write Data
                SFXCreator.WriteSampleDataSection(BWriter, FinalAudioDataDict, ProgressBar_CurrentTask, Label_CurrentTask);

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

                //Write Data
                SFXCreator.WriteFinalOffsets(BWriter, ProgressBar_CurrentTask, Label_CurrentTask);

                //Close Binary Writter
                BWriter.Close();
                BWriter.Dispose();

                //Close Text Writer
                if (DebugFileWritter != null)
                {
                    DebugFileWritter.Close();
                    DebugFileWritter.Dispose();
                }

                //Clear Temporal Dictionaries
                FinalSoundsDict.Clear();
                FinalAudioDataDict.Clear();

                //*===============================================================================================
                //* STEP 5: BUILD FILELIST
                //*===============================================================================================
                //Update Label
                SetLabelText(Label_CurrentTask, "Building Filelist");

                GenericFunctions.BuildSphinxFilelist();

                //*===============================================================================================
                //* STEP 6: CREATE DEBUG FILE IF REQUIRED
                //*===============================================================================================
                if (DebugFlags > 0)
                {
                    SoundBanks_DebugWriter DBGWritter = new SoundBanks_DebugWriter();

                    //Update Label
                    SetLabelText(Label_CurrentTask, "Creating debug file");

                    //Create file
                    DBGWritter.CreateDebugFile(GlobalPreferences.SFXOutputPath + "\\" + FileName + ".SFX", DebugFlags);
                }

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
                //Show Errors
                GenericFunctions.ShowErrorsAndWarningsList(Reports, FileName + ".SFX Output Errors", this);
            }

            //Close Form
            Close();
            Dispose();
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