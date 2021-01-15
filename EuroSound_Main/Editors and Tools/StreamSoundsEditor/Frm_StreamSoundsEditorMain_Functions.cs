﻿using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.CustomControls.WarningsForm;
using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application.StreamSounds
{
    public partial class Frm_StreamSoundsEditorMain
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
                Frm_StreamSounds_Properties FormAudioProps = new Frm_StreamSounds_Properties(SelectedSound, SoundKeyInDictionary)
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

        private string SaveDocument(string LoadedFile, TreeView TreeView_File, Dictionary<uint, EXSoundStream> StreamSoundsList, ProjectFile ProjectProperties)
        {
            string NewFilePath;

            if (!string.IsNullOrEmpty(LoadedFile))
            {
                NewFilePath = SerializeInfo.SaveStreamedSoundsBank(TreeView_File, StreamSoundsList, LoadedFile, ProjectProperties);
            }
            else
            {
                NewFilePath = OpenSaveAsDialog(TreeView_File, StreamSoundsList, ProjectProperties);
            }

            return NewFilePath;
        }

        private string OpenSaveAsDialog(TreeView TreeView_File, Dictionary<uint, EXSoundStream> StreamSoundsList, ProjectFile FileProperties)
        {
            string SavePath = GenericFunctions.SaveFileBrowser("EuroSound Files (*.esf)|*.esf|All files (*.*)|*.*", 1, true, "StreamFile");
            if (!string.IsNullOrEmpty(SavePath))
            {
                if (Directory.Exists(Path.GetDirectoryName(SavePath)))
                {
                    SerializeInfo.SaveStreamedSoundsBank(TreeView_File, StreamSoundsList, SavePath, FileProperties);
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
                    string ImaPath;

                    foreach (KeyValuePair<uint, EXSoundStream> SoundToUpdate in StreamSoundsList)
                    {
                        string AudioPath = Path.Combine(new string[] { Path.GetTempPath(), @"EuroSound\", SoundToUpdate.Key.ToString() + ".wav" });
                        AudioLibrary.CreateWavFile((int)SoundToUpdate.Value.Frequency, (int)SoundToUpdate.Value.Bits, (int)SoundToUpdate.Value.Channels, SoundToUpdate.Value.PCM_Data, AudioPath);

                        //Get IMA ADPCM Data
                        ImaPath = AudioLibrary.ConvertWavToIMAADPCM(AudioPath, Path.GetFileNameWithoutExtension(AudioPath));
                        if (!string.IsNullOrEmpty(ImaPath))
                        {
                            SoundToUpdate.Value.IMA_ADPCM_DATA = File.ReadAllBytes(ImaPath);
                        }

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
            UpdateWavList = new Thread(() =>
            {
                try
                {
                    int Index = 1;
                    TreeNode NodeToCheck;

                    //Clear List
                    ListView_WavHeaderData.BeginInvoke((MethodInvoker)delegate
                    {
                        ListView_WavHeaderData.Items.Clear();
                        ListView_WavHeaderData.Enabled = false;
                    });

                    //Disable update button
                    Button_UpdateIMAData.BeginInvoke((MethodInvoker)delegate
                    {
                        Button_UpdateIMAData.Enabled = false;
                    });

                    //Add data to list
                    foreach (KeyValuePair<uint, EXSoundStream> item in StreamSoundsList)
                    {
                        NodeToCheck = TreeView_StreamData.Nodes.Find(item.Key.ToString(), true)[0];
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
                            item.Value.MarkerDataCounterID.ToString(),
                            item.Value.MarkerID.ToString()
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

                    //Enable update button
                    Button_UpdateIMAData.BeginInvoke((MethodInvoker)delegate
                    {
                        Button_UpdateIMAData.Enabled = true;
                    });
                }
                catch
                {
                    //Cancel and clear list
                    ListView_WavHeaderData.BeginInvoke((MethodInvoker)delegate
                    {
                        ListView_WavHeaderData.Items.Clear();
                    });
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