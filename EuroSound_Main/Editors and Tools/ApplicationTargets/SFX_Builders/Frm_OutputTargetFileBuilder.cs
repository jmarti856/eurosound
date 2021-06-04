using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.CurrentProjectFunctions;
using NAudio.Wave;
using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.Editors_and_Tools.ApplicationTargets
{
    public partial class Frm_OutputTargetFileBuilder : Form
    {
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private BinaryStream binaryWriter;
        private ProjectFile CurrentFileProperties;
        private List<string> Reports = new List<string>();
        private EXAppTarget selectedTarget;
        private Dictionary<uint, EXAppTarget> currentProjectTargets;
        private int DebugFlags;
        private string parentFormTag;
        private WaveOut outputSound;

        public Frm_OutputTargetFileBuilder(ProjectFile FileProperties, EXAppTarget outputTarget, Dictionary<uint, EXAppTarget> projectTargets, int CheckedDebugFlags, string formTag)
        {
            InitializeComponent();
            CurrentFileProperties = FileProperties;
            selectedTarget = outputTarget;
            DebugFlags = CheckedDebugFlags;
            currentProjectTargets = projectTargets;
            parentFormTag = formTag;
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_OutputTargetFileBuilder_Load(object sender, EventArgs e)
        {
            //Put info in the labels
            Label_ProjectAndTarget.Text = string.Join("", GlobalPreferences.SelectedProfileName, " : ", selectedTarget.Name);
            Label_CurrentTask.Text = string.Empty;

            //Run Background Worker
            if (!BackgroundWorker_BuildSFX.IsBusy)
            {
                BackgroundWorker_BuildSFX.RunWorkerAsync();
            }
        }

        private void Frm_OutputTargetFileBuilder_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (binaryWriter != null)
            {
                binaryWriter.Close();
                binaryWriter.Dispose();
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

        //*===============================================================================================
        //* BACKGROUND WORKER
        //*===============================================================================================
        private void BackgroundWorker_BuildSFX_DoWork(object sender, DoWorkEventArgs e)
        {
            Reports.Clear();
            if (GlobalPreferences.SelectedProfileName.Equals("Sphinx", StringComparison.OrdinalIgnoreCase))
            {
                if (selectedTarget.Name.Equals("All", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (EXAppTarget storedTarget in currentProjectTargets.Values)
                    {
                        string directoryPath = Path.Combine(storedTarget.OutputDirectory, string.Join("", "_bin_", storedTarget.Name));
                        OutputTarget(selectedTarget, directoryPath, e);
                    }
                }
                else
                {
                    string directoryPath = Path.Combine(selectedTarget.OutputDirectory, string.Join("", "_bin_", selectedTarget.Name));
                    OutputTarget(selectedTarget, directoryPath, e);
                }
            }

        }

        private void OutputTarget(EXAppTarget storedTarget, string directoryPath, DoWorkEventArgs e)
        {
            if (string.IsNullOrEmpty(storedTarget.BinaryName))
            {
                Reports.Add("0File name is empty");
            }
            else
            {
                switch (CurrentFileProperties.TypeOfData)
                {
                    case (int)Enumerations.ESoundFileType.SoundBanks:
                        string filePath = Path.Combine(directoryPath, "_Eng", selectedTarget.BinaryName);
                        BuildSFXSoundBank_Sphinx(directoryPath, filePath, parentFormTag, storedTarget.Name, e);
                        break;
                    case (int)Enumerations.ESoundFileType.StreamSounds:
                        filePath = Path.Combine(directoryPath, "_Eng", selectedTarget.BinaryName);
                        BuildSFXStreamBank_Sphinx(directoryPath, filePath, parentFormTag, e);
                        break;
                    case (int)Enumerations.ESoundFileType.MusicBanks:
                        filePath = Path.Combine(directoryPath, "Music", selectedTarget.BinaryName);
                        BuildMusicBank_Sphinx(directoryPath, filePath, parentFormTag, storedTarget.Name, e);
                        break;
                }

                if (selectedTarget.UpdateFileList)
                {
                    GenericFunctions.BuildSphinxFilelist();
                }
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
                Reports.Add("2" + GenericFunctions.resourcesManager.GetString("OutputSFXSoundbankCancelled"));
            }
            else if (e.Error != null)
            {
                Reports.Add("0" + GenericFunctions.resourcesManager.GetString("OutputSFXSoundbankErrors"));
            }

            if (Reports.Count > 0)
            {
                //Show Errors
                GenericFunctions.ShowErrorsAndWarningsList(Reports, selectedTarget.BinaryName + ".SFX Output Errors", this);
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
    }
}
