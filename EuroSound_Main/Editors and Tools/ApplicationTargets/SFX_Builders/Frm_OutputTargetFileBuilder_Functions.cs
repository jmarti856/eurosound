using EuroSound_Application.Classes.SFX_Files;
using EuroSound_Application.Editors_and_Tools.StreamSoundsEditor.Debug_Writer;
using EuroSound_Application.GenerateSoundBankSFX;
using EuroSound_Application.Musics;
using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.SoundBanksEditor.Debug_Writer;
using EuroSound_Application.StreamSounds;
using Syroot.BinaryData;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application.Editors_and_Tools.ApplicationTargets
{
    public partial class Frm_OutputTargetFileBuilder
    {
        private void BuildSFXSoundBank_Sphinx(string directoryPath, string fullFilePath, string parentFormTag, string target, DoWorkEventArgs e)
        {
            if (Directory.Exists(directoryPath))
            {
                //*===============================================================================================
                //* GLOBAL VARS
                //*===============================================================================================
                Dictionary<uint, EXSound> finalSoundsDict;
                Dictionary<string, EXAudio> finalAudioDataDict;
                List<uint> soundsHashcodes = new List<uint>();
                GenerateSFXSoundBank SFXCreator = new GenerateSFXSoundBank();
                Form parentForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", parentFormTag);
                SFX_ChecksBeforeGeneration SFX_Check = new SFX_ChecksBeforeGeneration();

                int totalProgress = 1;

                binaryWriter = new BinaryStream(File.Open(fullFilePath, FileMode.Create, FileAccess.Write), null, Encoding.ASCII);
                //Check For Cancelation;
                if (BackgroundWorker_BuildSFX.CancellationPending == true)
                {
                    binaryWriter.Close();
                    binaryWriter.Dispose();
                    e.Cancel = true;
                }

                BackgroundWorker_BuildSFX.ReportProgress(totalProgress);

                //*===============================================================================================
                //* STEP 1: DISCARD SFX THAT WILL NOT BE OUTPUTED (20%)
                //*===============================================================================================
                GenericFunctions.ProgressBarValue(ProgressBar_CurrentTask, 0);

                //Update Label
                GenericFunctions.SetLabelText(Label_CurrentTask, "Getting SFX To export");

                //Update Progress Bar
                GenericFunctions.ProgressBarSetMaximum(ProgressBar_CurrentTask, ((Frm_Soundbanks_Main)parentForm).SoundsList.Keys.Count);

                //Discard SFXs that has checked as "no output"
                finalSoundsDict = SFXCreator.GetFinalSoundsDictionary(((Frm_Soundbanks_Main)parentForm).SoundsList, ProgressBar_CurrentTask, Label_CurrentTask);

                totalProgress += 20;
                BackgroundWorker_BuildSFX.ReportProgress(totalProgress);

                //*===============================================================================================
                //* STEP 2: DISCARD AUDIO DATA THAT SHOULD HAVE BEEN PURGED (40%)
                //*===============================================================================================
                //Get Final Audios To Export
                GenericFunctions.ProgressBarValue(ProgressBar_CurrentTask, 0);

                //Update Label
                GenericFunctions.SetLabelText(Label_CurrentTask, "Getting Audio Data To export");
                IEnumerable<string> usedAudiosList = EXSoundbanksFunctions.GetAudiosToExport(finalSoundsDict);

                //Add data
                finalAudioDataDict = SFXCreator.GetFinalAudioDictionaryPCMData(usedAudiosList, ((Frm_Soundbanks_Main)parentForm).AudioDataDict, ProgressBar_CurrentTask, target);

                //Update Total Progress
                totalProgress += 20;
                BackgroundWorker_BuildSFX.ReportProgress(totalProgress);

                //*===============================================================================================
                //* STEP 3: CHECK DATA THAT WILL BE OUTPUTED (50%)
                //*===============================================================================================
                //Update Label
                GenericFunctions.SetLabelText(Label_CurrentTask, "Checking data");

                //Update Progress Bar
                GenericFunctions.ProgressBarSetMaximum(ProgressBar_CurrentTask, finalSoundsDict.Count + finalAudioDataDict.Count);
                GenericFunctions.ProgressBarValue(ProgressBar_CurrentTask, 0);

                //Check Data
                bool canOutputFile = true;
                foreach (KeyValuePair<uint, EXSound> soundToCheck in finalSoundsDict)
                {
                    canOutputFile = SFX_Check.ValidateSFX(soundToCheck.Value, finalSoundsDict, soundsHashcodes, ((Frm_Soundbanks_Main)parentForm).TreeView_File.Nodes.Find(soundToCheck.Key.ToString(), true)[0].Text, Reports);
                    ToolsCommonFunctions.ProgressBarAddValue(ProgressBar_CurrentTask, 1);
                }

                if (canOutputFile)
                {
                    foreach (KeyValuePair<string, EXAudio> audioToCheck in finalAudioDataDict)
                    {
                        canOutputFile = SFX_Check.ValidateAudios(audioToCheck.Value, ((Frm_Soundbanks_Main)parentForm).TreeView_File.Nodes.Find(audioToCheck.Key, true)[0].Text, Reports);
                        if (canOutputFile == false)
                        {
                            break;
                        }
                        ToolsCommonFunctions.ProgressBarAddValue(ProgressBar_CurrentTask, 1);
                    }
                }

                //Update Total Progress
                totalProgress += 10;
                BackgroundWorker_BuildSFX.ReportProgress(totalProgress);

                //*===============================================================================================
                //* STEP 4: START WRITTING (70%)
                //*===============================================================================================
                if (canOutputFile)
                {
                    //Check For Cancelation;
                    if (BackgroundWorker_BuildSFX.CancellationPending == true)
                    {
                        binaryWriter.Close();
                        binaryWriter.Dispose();
                        e.Cancel = true;
                    }

                    //--------------------------------------[WRITE FILE HEADER]--------------------------------------
                    //Update Label
                    GenericFunctions.SetLabelText(Label_CurrentTask, "Writting File Header");

                    //Write Data
                    SFXCreator.WriteFileHeader(binaryWriter, CurrentFileProperties.Hashcode, ProgressBar_CurrentTask);

                    //Update Total Progress
                    totalProgress += 10;
                    BackgroundWorker_BuildSFX.ReportProgress(totalProgress);


                    //--------------------------------------[Write SECTIONS]--------------------------------------
                    //Update Label
                    GenericFunctions.SetLabelText(Label_CurrentTask, "Writting File Sections");

                    //Write Data
                    SFXCreator.WriteFileSections(binaryWriter, GenericFunctions.CountNumberOfSamples(finalSoundsDict), ProgressBar_CurrentTask);

                    //Update Total Progress
                    totalProgress += 10;
                    BackgroundWorker_BuildSFX.ReportProgress(totalProgress);


                    //--------------------------------------[SECTION SFX elements]--------------------------------------
                    //Write Data
                    SFXCreator.WriteSFXSection(binaryWriter, finalSoundsDict, finalAudioDataDict, ProgressBar_CurrentTask, Label_CurrentTask);

                    //Update Total Progress
                    totalProgress += 10;
                    BackgroundWorker_BuildSFX.ReportProgress(totalProgress);


                    //--------------------------------------[SECTION Sample info elements]--------------------------------------
                    //Write Data
                    SFXCreator.WriteSampleInfoSection(binaryWriter, finalAudioDataDict, ProgressBar_CurrentTask, Label_CurrentTask);

                    //Update Total Progress
                    totalProgress += 5;
                    BackgroundWorker_BuildSFX.ReportProgress(totalProgress);

                    //--------------------------------------[SECTION Sample data]--------------------------------------
                    //Write Data
                    int blockAlign = 4;
                    if (target.Equals("PS2", System.StringComparison.OrdinalIgnoreCase))
                    {
                        blockAlign = 128;
                    }
                    SFXCreator.WriteSampleDataSectionPC(binaryWriter, finalAudioDataDict, blockAlign, ProgressBar_CurrentTask, Label_CurrentTask);

                    //Update Total Progress
                    totalProgress += 5;
                    BackgroundWorker_BuildSFX.ReportProgress(totalProgress);


                    //*===============================================================================================
                    //* STEP 5: WRITE FINAL DATA (80%)
                    //*===============================================================================================
                    //Check For Cancelation
                    if (BackgroundWorker_BuildSFX.CancellationPending == true)
                    {
                        binaryWriter.Close();
                        binaryWriter.Dispose();
                        e.Cancel = true;
                    }

                    //Update Label
                    GenericFunctions.SetLabelText(Label_CurrentTask, "WrittingFinalOffsets");

                    //Write Data
                    SFXCreator.WriteFinalOffsets(binaryWriter, ProgressBar_CurrentTask, Label_CurrentTask);

                }
                //Close Binary Writter
                binaryWriter.Close();
                binaryWriter.Dispose();

                //Clear Temporal Dictionaries
                finalSoundsDict.Clear();
                finalAudioDataDict.Clear();

                //*===============================================================================================
                //* STEP 6: CREATE DEBUG FILE IF REQUIRED
                //*===============================================================================================
                if (canOutputFile)
                {
                    if (DebugFlags > 0)
                    {
                        SoundBanks_DebugWriter DBGWritter = new SoundBanks_DebugWriter();

                        //Update Label
                        GenericFunctions.SetLabelText(Label_CurrentTask, "Creating debug file");

                        //Create file
                        DBGWritter.CreateDebugFile(fullFilePath, DebugFlags);
                    }
                }
                //Update Label
                GenericFunctions.SetLabelText(Label_CurrentTask, "Output Completed");

                //Update Total Progress
                totalProgress += 9;
                BackgroundWorker_BuildSFX.ReportProgress(totalProgress);
            }
            else
            {
                Reports.Add("0Unable to open " + fullFilePath);
            }
        }

        private void BuildSFXStreamBank_Sphinx(string directoryPath, string fullFilePath, string parentFormTag, DoWorkEventArgs e)
        {
            if (Directory.Exists(directoryPath))
            {
                Dictionary<uint, EXSoundStream> FinalSoundsDict;
                GenerateSFXStreamedSounds SFXCreator = new GenerateSFXStreamedSounds();
                SFX_ChecksBeforeGeneration SFX_Check = new SFX_ChecksBeforeGeneration();
                Form ParentForm = GenericFunctions.GetFormByName("Frm_StreamSounds_Main", parentFormTag);
                int TotalProgress = 1;

                BinaryStream BWriter = new BinaryStream(File.Open(fullFilePath, FileMode.Create, FileAccess.Write), null, Encoding.ASCII);

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
                bool CanOutputFile = true;
                foreach (KeyValuePair<uint, EXSoundStream> SoundToCheck in FinalSoundsDict)
                {
                    string CurrentObjectName = ((Frm_StreamSounds_Main)ParentForm).TreeView_StreamData.Nodes.Find(SoundToCheck.Key.ToString(), true)[0].Text;
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
                    ToolsCommonFunctions.ProgressBarAddValue(ProgressBar_CurrentTask, 1);
                }

                //Update Total Progress
                TotalProgress += 20;
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
                        DBGWritter.CreateDebugFile(fullFilePath, DebugFlags);
                    }
                }
                //Update Label
                GenericFunctions.SetLabelText(Label_CurrentTask, "Output Completed");

                //Update Total Progress
                TotalProgress += 9;
                BackgroundWorker_BuildSFX.ReportProgress(TotalProgress);
            }
            else
            {
                Reports.Add("0Unable to open " + fullFilePath);
            }
        }

        private void BuildMusicBank_Sphinx(string directoryPath, string fullFilePath, string parentFormTag, string target, DoWorkEventArgs e)
        {
            if (Directory.Exists(directoryPath))
            {
                Dictionary<uint, EXMusic> FinalMusicsDict;
                GenerateSFXMusicBank SFXCreator = new GenerateSFXMusicBank();
                Form ParentForm = GenericFunctions.GetFormByName("Frm_Musics_Main", parentFormTag);
                SFX_ChecksBeforeGeneration SFX_Check = new SFX_ChecksBeforeGeneration();
                int TotalProgress = 1;

                BinaryStream BWriter = new BinaryStream(File.Open(fullFilePath, FileMode.Create, FileAccess.Write), null, Encoding.ASCII);

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
                FinalMusicsDict = SFXCreator.GetFinalMusicsDictionary(((Frm_Musics_Main)ParentForm).MusicsList, ProgressBar_CurrentTask, Label_CurrentTask, target);
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
                bool CanOutputFile = true;
                foreach (KeyValuePair<uint, EXMusic> MusicToCheck in FinalMusicsDict)
                {
                    string CurrentObjectName = ((Frm_Musics_Main)ParentForm).TreeView_MusicData.Nodes.Find(MusicToCheck.Key.ToString(), true)[0].Text;
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
                    ToolsCommonFunctions.ProgressBarAddValue(ProgressBar_CurrentTask, 1);
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
                        SFXCreator.WriteFileSection2(BWriter, FinalMusicsDict, ProgressBar_CurrentTask, target);

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
                            DBGWritter.CreateDebugFile(fullFilePath, DebugFlags);
                        }
                    }

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
                Reports.Add("0Unable to open " + fullFilePath);
            }
        }
    }
}
