using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.CustomControls.WarningsForm;
using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application.Musics
{
    public partial class Frm_Musics_Main
    {
        private string OpenSaveAsDialog(TreeView TreeView_File, Dictionary<uint, EXMusic> StreamSoundsList, ProjectFile FileProperties)
        {
            string SavePath = GenericFunctions.SaveFileBrowser("EuroSound Files (*.esf)|*.esf|All files (*.*)|*.*", 1, true, FileProperties.FileName);
            if (!string.IsNullOrEmpty(SavePath))
            {
                if (Directory.Exists(Path.GetDirectoryName(SavePath)))
                {
                    SerializeInfo.SaveMusics(TreeView_File, StreamSoundsList, SavePath, FileProperties);

                    //Add file to recent list
                    RecentFilesMenu.AddFile(SavePath);
                }
            }
            return SavePath;
        }

        private void RemoveMusicSelectedNode(TreeNode NodeToRemove)
        {
            //Show warning
            if (GlobalPreferences.ShowWarningMessagesBox)
            {
                EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox(string.Join(" ", new string[] { "Delete Music:", TreeView_MusicData.SelectedNode.Text }), "Warning", true);
                if (WarningDialog.ShowDialog() == DialogResult.OK)
                {
                    GlobalPreferences.ShowWarningMessagesBox = WarningDialog.ShowWarningAgain;
                    RemoveMusic(NodeToRemove);
                }
                WarningDialog.Dispose();
            }
            else
            {
                RemoveMusic(NodeToRemove);
            }
        }

        private void RemoveMusic(TreeNode SelectedNode)
        {
            if (TreeView_MusicData.SelectedNode != null)
            {
                EXMusic MusicToSave = MusicsList[uint.Parse(SelectedNode.Name)];
                SaveSnapshot(uint.Parse(SelectedNode.Name), MusicToSave, SelectedNode);

                EXMusicsFunctions.RemoveMusic(SelectedNode.Name, MusicsList);
                TreeNodeFunctions.TreeNodeDeleteNode(TreeView_MusicData, SelectedNode, SelectedNode.Tag.ToString());
            }
        }

        internal void OpenMusicPropertiesForm(TreeNode SelectedNode)
        {
            string MusicKeyInDictionary = SelectedNode.Name;
            EXMusic SelectedMusic = EXMusicsFunctions.GetMusicByName(Convert.ToUInt32(MusicKeyInDictionary), MusicsList);
            if (SelectedMusic != null)
            {
                Frm_Musics_Properties FormAudioProps = new Frm_Musics_Properties(SelectedMusic, MusicKeyInDictionary, SelectedNode.Text)
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

        private void RemoveFolderSelectedNode(TreeNode SelectedNode)
        {
            //Check we are not trying to delete a root folder
            if (!(SelectedNode == null || SelectedNode.Tag.Equals("Root")))
            {
                //Show warning
                if (GlobalPreferences.ShowWarningMessagesBox)
                {
                    EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox(string.Join(" ", new string[] { "Delete Folder:", SelectedNode.Text }), "Warning", true);
                    if (WarningDialog.ShowDialog() == DialogResult.OK)
                    {
                        GlobalPreferences.ShowWarningMessagesBox = WarningDialog.ShowWarningAgain;
                        RemoveRecursivelyFolder();
                    }
                    WarningDialog.Dispose();
                }
                else
                {
                    RemoveRecursivelyFolder();
                }
            }
        }

        private void RemoveRecursivelyFolder()
        {
            //Remove child nodes sounds and samples
            IList<TreeNode> ChildNodesCollection = new List<TreeNode>();
            foreach (TreeNode ChildNode in TreeNodeFunctions.GetNodesInsideFolder(TreeView_MusicData, TreeView_MusicData.SelectedNode, ChildNodesCollection))
            {
                EXMusicsFunctions.RemoveMusic(ChildNode.Name, MusicsList);
            }
            TreeNodeFunctions.TreeNodeDeleteNode(TreeView_MusicData, TreeView_MusicData.SelectedNode, TreeView_MusicData.SelectedNode.Tag.ToString());
        }

        private void UpdateIMAData()
        {
            EngineXImaAdpcm.ImaADPCM_Functions ImaADPCM = new EngineXImaAdpcm.ImaADPCM_Functions();

            UpdateImaData = new Thread(() =>
            {
                try
                {
                    foreach (KeyValuePair<uint, EXMusic> SoundToUpdate in MusicsList)
                    {
                        //Get IMA ADPCM Data Left Channel
                        short[] ShortArrayPCMData_LeftChannel = AudioLibrary.ConvertPCMDataToShortArray(SoundToUpdate.Value.PCM_Data_LeftChannel);
                        SoundToUpdate.Value.IMA_ADPCM_DATA_LeftChannel = ImaADPCM.EncodeIMA_ADPCM(ShortArrayPCMData_LeftChannel, SoundToUpdate.Value.PCM_Data_LeftChannel.Length / 2);

                        //Get IMA ADPCM Data Right Channel
                        short[] ShortArrayPCMData_RightChannel = AudioLibrary.ConvertPCMDataToShortArray(SoundToUpdate.Value.PCM_Data_RightChannel);
                        SoundToUpdate.Value.IMA_ADPCM_DATA_RightChannel = ImaADPCM.EncodeIMA_ADPCM(ShortArrayPCMData_RightChannel, SoundToUpdate.Value.PCM_Data_RightChannel.Length / 2);

                        //Update Status Bar
                        GenericFunctions.ParentFormStatusBar.ShowProgramStatus(string.Join(" ", new string[] { "Checking:", SoundToUpdate.Key.ToString() }));
                    }

                    MessageBox.Show(GenericFunctions.ResourcesManager.GetString("StreamSoundsUpdatedSuccess"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    //Update Status Bar
                    GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
                    MessageBox.Show(GenericFunctions.ResourcesManager.GetString("StreamSoundsUpdatedError"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //Update Status Bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));

            })
            {
                IsBackground = true
            };
            UpdateImaData.Start();
        }

        private void UpdateWavDataList()
        {
            TreeNode NodeToCheck;

            UpdateWavList = new Thread(() =>
            {
                try
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
                    Button_UpdateProperties.BeginInvoke((MethodInvoker)delegate
                    {
                        Button_UpdateProperties.Enabled = false;
                    });

                    //Add data to list
                    foreach (KeyValuePair<uint, EXMusic> item in MusicsList)
                    {
                        NodeToCheck = TreeView_MusicData.Nodes.Find(item.Key.ToString(), true)[0];

                        //Left Channel
                        ListViewItem LeftChannelInfo = new ListViewItem(new[]
                        {
                            NodeToCheck.Text.ToString() + " L",
                            item.Value.Frequency_LeftChannel.ToString(),
                            item.Value.Channels_LeftChannel.ToString(),
                            item.Value.Bits_LeftChannel.ToString(),
                            item.Value.PCM_Data_LeftChannel.Length.ToString(),
                            item.Value.Encoding_LeftChannel.ToString(),
                            item.Value.Duration_LeftChannel.ToString(),
                            item.Value.StartMarkers.Count.ToString(),
                            item.Value.Markers.Count.ToString(),
                        })
                        {
                            UseItemStyleForSubItems = false
                        };
                        LeftChannelInfo.Tag = NodeToCheck.Name;
                        GenericFunctions.AddItemToListView(LeftChannelInfo, ListView_WavHeaderData);
                        GenericFunctions.ParentFormStatusBar.ShowProgramStatus(string.Join(" ", new string[] { "Checking sound:", item.Value.WAVFileName_LeftChannel.ToString() }));

                        //RightChannel
                        ListViewItem RightChannelInfo = new ListViewItem(new[]
                        {
                            NodeToCheck.Text.ToString() + " R",
                            item.Value.Frequency_RightChannel.ToString(),
                            item.Value.Channels_RightChannel.ToString(),
                            item.Value.Bits_RightChannel.ToString(),
                            item.Value.PCM_Data_RightChannel.Length.ToString(),
                            item.Value.Encoding_RightChannel.ToString(),
                            item.Value.Duration_RightChannel.ToString(),
                            item.Value.StartMarkers.Count.ToString(),
                            item.Value.Markers.Count.ToString(),
                        })
                        {
                            UseItemStyleForSubItems = false
                        };
                        RightChannelInfo.Tag = NodeToCheck.Name;
                        GenericFunctions.AddItemToListView(RightChannelInfo, ListView_WavHeaderData);

                        GenericFunctions.ParentFormStatusBar.ShowProgramStatus(string.Join(" ", new string[] { "Checking sound:", item.Value.WAVFileName_RightChannel.ToString() }));
                        Thread.Sleep(85);
                    }

                    //Enable List
                    ListView_WavHeaderData.BeginInvoke((MethodInvoker)delegate
                    {
                        ListView_WavHeaderData.Enabled = true;
                        Button_UpdateIMAData.Enabled = true;
                    });

                    //Enable update ima button
                    Button_UpdateIMAData.BeginInvoke((MethodInvoker)delegate
                    {
                        Button_UpdateIMAData.Enabled = true;
                    });

                    //Enable Update button
                    Button_UpdateProperties.BeginInvoke((MethodInvoker)delegate
                    {
                        Button_UpdateProperties.Enabled = true;
                    });

                    //Update Counter
                    Textbox_DataCount.BeginInvoke((MethodInvoker)delegate
                    {
                        Textbox_DataCount.Text = ListView_WavHeaderData.Items.Count.ToString();
                    });
                }
                catch (ObjectDisposedException)
                {

                }
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
            })
            {
                IsBackground = true
            };
            UpdateWavList.Start();
        }


        //*===============================================================================================
        //* UNDO AND REDO
        //*===============================================================================================
        private void SaveSnapshot(uint ItemKey, EXMusic MusicToSave, TreeNode NodeToSave)
        {
            //Save the snapshot.
            UndoListMusics.Push(new KeyValuePair<uint, EXMusic>(ItemKey, MusicToSave));
            UndoListNodes.Push(NodeToSave);

            //Enable or disable the Undo and Redo menu items.
            EnableUndo();
        }

        //Enable or disable the Undo and Redo menu items.
        private void EnableUndo()
        {
            MenuItem_Edit_Undo.Enabled = (UndoListMusics.Count > 0);
        }
    }
}
