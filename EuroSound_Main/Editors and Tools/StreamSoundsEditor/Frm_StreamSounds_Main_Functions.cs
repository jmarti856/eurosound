using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.Clases;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.Editors_and_Tools.ApplicationTargets;
using EuroSound_Application.HashCodesFunctions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application.StreamSounds
{
    public partial class Frm_StreamSounds_Main
    {
        internal void OpenSoundPropertiesForm(TreeNode SelectedNode)
        {
            string SoundKeyInDictionary = SelectedNode.Name;
            EXSoundStream SelectedSound = EXStreamSoundsFunctions.GetSoundByName(Convert.ToUInt32(SoundKeyInDictionary), StreamSoundsList);
            if (SelectedSound != null)
            {
                GenericFunctions.SetCurrentFileLabel(SelectedNode.Text, "SBPanel_LastFile");
                Frm_StreamSounds_Properties FormAudioProps = new Frm_StreamSounds_Properties(SelectedSound, SoundKeyInDictionary, SelectedNode.Text, ProjectInfo)
                {
                    Text = GenericFunctions.TruncateLongString(SelectedNode.Text, 25) + " - Properties",
                    Tag = Tag,
                    Owner = this,
                    ShowInTaskbar = false
                };
                FormAudioProps.ShowDialog();
                FormAudioProps.Dispose();
            }
        }

        private string OpenSaveAsDialog(TreeView TreeView_File, Dictionary<uint, EXSoundStream> StreamSoundsList, ProjectFile FileProperties)
        {
            string SavePath = BrowsersAndDialogs.SaveFileBrowser("EuroSound Files (*.esf)|*.esf|All files (*.*)|*.*", 1, true, FileProperties.FileName);
            if (!string.IsNullOrEmpty(SavePath))
            {
                if (Directory.Exists(Path.GetDirectoryName(SavePath)))
                {
                    EuroSoundFilesFunctions.SaveEuroSoundFile(TreeView_File, StreamSoundsList, null, OutputTargets, SavePath, FileProperties);

                    //Add file to recent list
                    RecentFilesMenu.AddFile(SavePath);
                }
            }
            return SavePath;
        }

        internal void OpenTargetProperties(TreeNode SelectedNode)
        {
            EXAppTarget outTarget = OutputTargets[Convert.ToUInt32(SelectedNode.Name)];
            using (Frm_ApplicationTarget newOutTarget = new Frm_ApplicationTarget(outTarget) { Owner = this })
            {
                newOutTarget.ShowDialog();

                if (newOutTarget.DialogResult == DialogResult.OK)
                {
                    //File has been modified
                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }

        private void UpdateIMAData()
        {
            UpdateImaData = new Thread(() =>
            {
                try
                {
                    EngineXImaAdpcm.ImaADPCM_Functions ImaADPCM = new EngineXImaAdpcm.ImaADPCM_Functions();
                    foreach (KeyValuePair<uint, EXSoundStream> SoundToUpdate in StreamSoundsList)
                    {
                        //Get IMA ADPCM Data
                        short[] ShortArrayPCMData = AudioLibrary.ConvertPCMDataToShortArray(SoundToUpdate.Value.PCM_Data);
                        SoundToUpdate.Value.IMA_ADPCM_DATA = ImaADPCM.EncodeIMA_ADPCM(ShortArrayPCMData, SoundToUpdate.Value.PCM_Data.Length / 2);

                        //Update Status Bar
                        GenericFunctions.ParentFormStatusBar.ShowProgramStatus(string.Join(" ", new string[] { "Checking:", SoundToUpdate.Key.ToString() }));
                    }

                    MessageBox.Show(GenericFunctions.resourcesManager.GetString("StreamSoundsUpdatedSuccess"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    //Update Status Bar
                    GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
                    MessageBox.Show(GenericFunctions.resourcesManager.GetString("StreamSoundsUpdatedError"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //Update Status Bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));

            })
            {
                IsBackground = true
            };
            UpdateImaData.Start();
        }

        private void UpdateWavDataList()
        {
            int Index = 1;

            UpdateWavList = new Thread(() =>
            {
                //Clear List
                ListView_WavHeaderData.BeginInvoke((MethodInvoker)delegate
                {
                    ListView_WavHeaderData.Items.Clear();
                    ListView_WavHeaderData.Enabled = false;
                });

                //Disable Update IMA data button
                Button_UpdateIMAData.BeginInvoke((MethodInvoker)delegate
                {
                    Button_UpdateIMAData.Enabled = false;
                });

                //Disable Update button
                Button_UpdateList_WavData.BeginInvoke((MethodInvoker)delegate
                {
                    Button_UpdateList_WavData.Enabled = false;
                });

                //Add data to list
                foreach (KeyValuePair<uint, EXSoundStream> item in StreamSoundsList)
                {
                    TreeNode NodeToCheck = TreeView_StreamData.Nodes.Find(item.Key.ToString(), true)[0];
                    ListViewItem Hashcode = new ListViewItem(new[]
                    {
                        NodeToCheck.Text.ToString(),
                        Index.ToString(),
                        item.Value.Frequency.ToString(),
                        item.Value.Channels.ToString(),
                        item.Value.Bits.ToString(),
                        item.Value.PCM_Data.Length.ToString(),
                        item.Value.Encoding.ToString(),
                        item.Value.Duration.ToString(),
                        item.Value.StartMarkers.Count.ToString(),
                        item.Value.Markers.Count.ToString(),
                    })
                    {
                        Tag = NodeToCheck.Name,
                        UseItemStyleForSubItems = false
                    };

                    GenericFunctions.AddItemToListView(Hashcode, ListView_WavHeaderData);
                    Index++;

                    GenericFunctions.ParentFormStatusBar.ShowProgramStatus(string.Join(" ", new string[] { "Checking sound:", item.Value.WAVFileName.ToString() }));
                    Thread.Sleep(85);
                }

                //Enable List
                ListView_WavHeaderData.BeginInvoke((MethodInvoker)delegate
                {
                    ListView_WavHeaderData.Enabled = true;
                });

                //Enable update ima button
                Button_UpdateIMAData.BeginInvoke((MethodInvoker)delegate
                {
                    Button_UpdateIMAData.Enabled = true;
                });

                //Enable Update button
                Button_UpdateList_WavData.BeginInvoke((MethodInvoker)delegate
                {
                    Button_UpdateList_WavData.Enabled = true;
                });
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
            })
            {
                IsBackground = true
            };
            UpdateWavList.Start();
        }

        private void Button_StopUpdate_Click(object sender, EventArgs e)
        {
            if (UpdateWavList != null)
            {
                UpdateWavList.Abort();
                ListView_WavHeaderData.Items.Clear();
                ListView_WavHeaderData.Enabled = true;
                Button_UpdateIMAData.Enabled = true;
                Button_UpdateList_WavData.Enabled = true;
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
            }
        }

        private void UpdateStatusBarLabels()
        {
            //Update File name label
            GenericFunctions.SetCurrentFileLabel(ProjectInfo.FileName, "SBPanel_File");

            //Update Hashcode name label
            GenericFunctions.SetCurrentFileLabel(Hashcodes.GetHashcodeLabel(Hashcodes.SB_Defines, ProjectInfo.Hashcode), "SBPanel_Hashcode");
        }

        private void ClearStatusBarLabels()
        {
            //Update File name label
            GenericFunctions.SetCurrentFileLabel(string.Empty, "SBPanel_File");

            //Update Hashcode name label
            GenericFunctions.SetCurrentFileLabel(string.Empty, "SBPanel_Hashcode");
        }

        private void MoveDown(TreeNode node)
        {
            TreeNode parent = node.Parent;
            TreeView view = node.TreeView;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index < parent.Nodes.Count - 1)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index + 1, node);
                }
            }
            else if (view != null && view.Nodes.Contains(node)) //root node
            {
                int index = view.Nodes.IndexOf(node);
                if (index < view.Nodes.Count - 1)
                {
                    view.Nodes.RemoveAt(index);
                    view.Nodes.Insert(index + 1, node);
                }
            }
        }

        private void MoveUp(TreeNode node)
        {
            TreeNode parent = node.Parent;
            TreeView view = node.TreeView;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index > 0)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index - 1, node);
                }
            }
            else if (node.TreeView.Nodes.Contains(node)) //root node
            {
                int index = view.Nodes.IndexOf(node);
                if (index > 0)
                {
                    view.Nodes.RemoveAt(index);
                    view.Nodes.Insert(index - 1, node);
                }
            }
        }
    }
}
