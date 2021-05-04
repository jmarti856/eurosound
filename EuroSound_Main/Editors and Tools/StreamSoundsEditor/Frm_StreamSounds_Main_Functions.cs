using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.Clases;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.CustomControls.WarningsForm;
using EuroSound_Application.HashCodesFunctions;
using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application.StreamSounds
{
    public partial class Frm_StreamSounds_Main
    {
        private void RemoveStreamSoundSelectedNode(TreeNode NodeToRemove)
        {
            //Show warning
            if (GlobalPreferences.ShowWarningMessagesBox)
            {
                EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox(string.Join(" ", new string[] { "Delete Stream Sound:", TreeView_StreamData.SelectedNode.Text }), "Warning", true);
                if (WarningDialog.ShowDialog() == DialogResult.OK)
                {
                    GlobalPreferences.ShowWarningMessagesBox = WarningDialog.ShowWarningAgain;
                    RemoveSound(NodeToRemove);
                }
                WarningDialog.Dispose();
            }
            else
            {
                RemoveSound(NodeToRemove);
            }
        }

        private void RemoveSound(TreeNode SelectedNode)
        {
            if (TreeView_StreamData.SelectedNode != null)
            {
                EXSoundStream SoundtoSave = StreamSoundsList[uint.Parse(SelectedNode.Name)];
                SaveSnapshot(uint.Parse(SelectedNode.Name), SoundtoSave, SelectedNode);

                EXStreamSoundsFunctions.RemoveStreamedSound(SelectedNode.Name, StreamSoundsList);
                TreeNodeFunctions.TreeNodeDeleteNode(TreeView_StreamData, SelectedNode, SelectedNode.Tag.ToString());
            }
        }

        internal void OpenSoundPropertiesForm(TreeNode SelectedNode)
        {
            string SoundKeyInDictionary = SelectedNode.Name;
            EXSoundStream SelectedSound = EXStreamSoundsFunctions.GetSoundByName(Convert.ToUInt32(SoundKeyInDictionary), StreamSoundsList);
            if (SelectedSound != null)
            {
                GenericFunctions.SetCurrentFileLabel(SelectedNode.Text, "LastFile");
                Frm_StreamSounds_Properties FormAudioProps = new Frm_StreamSounds_Properties(SelectedSound, SoundKeyInDictionary, SelectedNode.Text)
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
                    EuroSoundFilesFunctions.SaveStreamedSoundsBank(TreeView_File, StreamSoundsList, SavePath, FileProperties);

                    //Add file to recent list
                    RecentFilesMenu.AddFile(SavePath);
                }
            }
            return SavePath;
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
            int Index = 1;

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
                            UseItemStyleForSubItems = false
                        };
                        Hashcode.Tag = NodeToCheck.Name;
                        GenericFunctions.AddItemToListView(Hashcode, ListView_WavHeaderData);
                        Index++;

                        GenericFunctions.ParentFormStatusBar.ShowProgramStatus(string.Join(" ", new string[] { "Checking sound:", item.Value.WAVFileName.ToString() }));

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
                    Button_UpdateList_WavData.BeginInvoke((MethodInvoker)delegate
                    {
                        Button_UpdateList_WavData.Enabled = true;
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

        private void Button_StopUpdate_Click(object sender, EventArgs e)
        {
            if (UpdateWavList != null)
            {
                UpdateWavList.Abort();
                ListView_WavHeaderData.Items.Clear();
                ListView_WavHeaderData.Enabled = true;
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
            }
        }

        private void UpdateStatusBarLabels()
        {
            //Update File name label
            GenericFunctions.SetCurrentFileLabel(ProjectInfo.FileName, "File");

            //Update Hashcode name label
            GenericFunctions.SetCurrentFileLabel(Hashcodes.GetHashcodeLabel(Hashcodes.SB_Defines, ProjectInfo.Hashcode), "Hashcode");
        }

        private void ClearStatusBarLabels()
        {
            //Update File name label
            GenericFunctions.SetCurrentFileLabel(string.Empty, "File");

            //Update Hashcode name label
            GenericFunctions.SetCurrentFileLabel(string.Empty, "Hashcode");
        }

        //*===============================================================================================
        //* UNDO AND REDO
        //*===============================================================================================
        private void SaveSnapshot(uint ItemKey, EXSoundStream SoundToSave, TreeNode NodeToSave)
        {
            //Save the snapshot.
            UndoListSounds.Push(new KeyValuePair<uint, EXSoundStream>(ItemKey, SoundToSave));
            UndoListNodes.Push(NodeToSave);

            //Enable or disable the Undo and Redo menu items.
            EnableUndo();
        }

        //Enable or disable the Undo and Redo menu items.
        private void EnableUndo()
        {
            MenuItem_Edit_Undo.Enabled = (UndoListSounds.Count > 0);
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
