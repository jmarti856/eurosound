using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.CustomControls.WarningsForm;
using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application.SoundBanksEditor
{
    public partial class Frm_Soundbanks_Main
    {
        private string SaveDocument(string LoadedFile, TreeView TreeView_File, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudioDataDict, ProjectFile ProjectProperties)
        {
            string NewFilePath;

            if (!string.IsNullOrEmpty(LoadedFile))
            {
                NewFilePath = EuroSoundFilesFunctions.SaveSoundBanksDocument(TreeView_File, SoundsList, AudioDataDict, LoadedFile, ProjectProperties);
            }
            else
            {
                NewFilePath = OpenSaveAsDialog(TreeView_File, SoundsList, AudioDataDict, ProjectProperties);
            }

            return NewFilePath;
        }

        private string OpenSaveAsDialog(TreeView TreeView_File, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudioDataDict, ProjectFile FileProperties)
        {
            string SavePath = GenericFunctions.SaveFileBrowser("EuroSound Files (*.esf)|*.esf|All files (*.*)|*.*", 1, true, Hashcodes.GetHashcodeLabel(Hashcodes.SB_Defines, FileProperties.Hashcode));
            if (!string.IsNullOrEmpty(SavePath))
            {
                if (Directory.Exists(Path.GetDirectoryName(SavePath)))
                {
                    EuroSoundFilesFunctions.SaveSoundBanksDocument(TreeView_File, SoundsList, AudioDataDict, SavePath, FileProperties);
                }
            }
            return SavePath;
        }

        internal void OpenAudioProperties(TreeNode SelectedNode)
        {
            EXAudio SelectedSound = TreeNodeFunctions.GetSelectedAudio(SelectedNode.Name, AudioDataDict);
            if (SelectedSound != null)
            {
                Frm_AudioProperties FormAudioProps = new Frm_AudioProperties(SelectedSound, SelectedNode.Name)
                {
                    Text = GenericFunctions.TruncateLongString(SelectedNode.Text, 25) + " - Properties",
                    Tag = Tag,
                    Owner = this,
                    ShowInTaskbar = false
                };
                FormAudioProps.ShowDialog();
                FormAudioProps.Dispose();
                ProjectInfo.FileHasBeenModified = true;
            }
        }

        internal void OpenSampleProperties(TreeNode SelectedNode)
        {
            string Section = TreeNodeFunctions.FindRootNode(SelectedNode).Name;
            EXSound ParentSound = EXSoundbanksFunctions.ReturnSoundFromDictionary(uint.Parse(SelectedNode.Parent.Name), SoundsList);
            EXSample SelectedSample = EXSoundbanksFunctions.ReturnSampleFromSound(ParentSound, uint.Parse(SelectedNode.Name));

            if (Section.Equals("StreamedSounds"))
            {
                using (Frm_NewStreamSound AddStreamSound = new Frm_NewStreamSound(SelectedSample))
                {
                    AddStreamSound.Text = GenericFunctions.TruncateLongString(SelectedNode.Text, 25) + " - Properties";
                    AddStreamSound.Tag = Tag;
                    AddStreamSound.Owner = this;
                    AddStreamSound.ShowInTaskbar = false;
                    AddStreamSound.ShowDialog();

                    if (AddStreamSound.DialogResult == DialogResult.OK)
                    {
                        SelectedSample.FileRef = (short)AddStreamSound.SelectedSound;
                    }
                };
            }
            else
            {
                Frm_SampleProperties FormSampleProps = new Frm_SampleProperties(SelectedSample, EXSoundbanksFunctions.SubSFXFlagChecked(ParentSound.Flags))
                {
                    Text = GenericFunctions.TruncateLongString(SelectedNode.Text, 25) + " - Properties",
                    Tag = Tag,
                    Owner = this,
                    ShowInTaskbar = false
                };
                FormSampleProps.ShowDialog();
                FormSampleProps.Dispose();
            }
            ProjectInfo.FileHasBeenModified = true;
        }

        private void OpenSelectedNodeSampleProperties(TreeNode SelectedNode)
        {
            if (SelectedNode != null)
            {
                if (SelectedNode.Tag.Equals("Sound"))
                {
                    OpenSoundProperties(SelectedNode);
                }
                else if (SelectedNode.Tag.Equals("Sample"))
                {
                    OpenSampleProperties(SelectedNode);
                }
            }
        }

        internal void OpenSoundProperties(TreeNode SelectedNode)
        {
            string SoundSection = TreeNodeFunctions.FindRootNode(SelectedNode).Name;
            EXSound SelectedSound = EXSoundbanksFunctions.ReturnSoundFromDictionary(uint.Parse(SelectedNode.Name), SoundsList);
            Frm_EffectProperties FormSoundProps = new Frm_EffectProperties(SelectedSound, SelectedNode.Name, SoundSection)
            {
                Text = GenericFunctions.TruncateLongString(SelectedNode.Text, 25) + " - Properties",
                Tag = Tag,
                Owner = this,
                ShowInTaskbar = false,
            };
            FormSoundProps.ShowDialog();
            FormSoundProps.Dispose();
            ProjectInfo.FileHasBeenModified = true;
        }

        private void RemoveAudioAndWarningDependencies(TreeNode SelectedNode)
        {
            IEnumerable<string> Dependencies = EXSoundbanksFunctions.GetAudioDependencies(SelectedNode.Name, SelectedNode.Text, SoundsList, TreeView_File, false);
            if (Dependencies.Any())
            {
                GenericFunctions.ShowErrorsAndWarningsList(Dependencies, "Deleting Audio", this);
            }
            else
            {
                RemoveAudioSelectedNode(SelectedNode);
            }
        }

        private void RemoveAudioSelectedNode(TreeNode SelectedNode)
        {
            //Show warning
            if (GlobalPreferences.ShowWarningMessagesBox)
            {
                EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox(string.Join(" ", new string[] { "Delete Audio:", SelectedNode.Text.ToString() }), "Warning", true);
                if (WarningDialog.ShowDialog() == DialogResult.OK)
                {
                    GlobalPreferences.ShowWarningMessagesBox = WarningDialog.ShowWarningAgain;
                    RemoveAudio();
                }
                WarningDialog.Dispose();
            }
            else
            {
                RemoveAudio();
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

        private void RemoveSampleSelectedNode(TreeNode SelectedNode)
        {
            //Show warning
            if (GlobalPreferences.ShowWarningMessagesBox)
            {
                EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox(string.Join(" ", new string[] { "Delete Sample:", SelectedNode.Text }), "Warning", true);
                if (WarningDialog.ShowDialog() == DialogResult.OK)
                {
                    GlobalPreferences.ShowWarningMessagesBox = WarningDialog.ShowWarningAgain;
                    RemoveSample();
                }
                WarningDialog.Dispose();
            }
            else
            {
                RemoveSample();
            }
        }

        private void RemoveSoundSelectedNode(TreeNode SelectedNode)
        {
            //Show warning
            if (GlobalPreferences.ShowWarningMessagesBox)
            {
                EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox(string.Join(" ", new string[] { "Delete Sound:", SelectedNode.Text }), "Warning", true);
                if (WarningDialog.ShowDialog() == DialogResult.OK)
                {
                    GlobalPreferences.ShowWarningMessagesBox = WarningDialog.ShowWarningAgain;
                    RemoveSound();
                }
                WarningDialog.Dispose();
            }
            else
            {
                RemoveSound();
            }
        }

        private void RemoveAudio()
        {
            TreeNode SelectedNode = TreeView_File.SelectedNode;

            //Save to Undo List
            KeyValuePair<string, EXAudio> SoundtoSave = new KeyValuePair<string, EXAudio>(SelectedNode.Name, AudioDataDict[SelectedNode.Name]);
            SaveSnapshot(SoundtoSave, SelectedNode);

            //EXObjectsFunctions.RemoveSound(TreeView_File.SelectedNode.Name, SoundsList);
            EXSoundbanksFunctions.DeleteAudio(AudioDataDict, SelectedNode.Name);
            TreeNodeFunctions.TreeNodeDeleteNode(TreeView_File, SelectedNode, SelectedNode.Tag.ToString());
        }

        private void RemoveRecursivelyFolder()
        {
            //Remove child nodes sounds and samples
            IList<TreeNode> ChildNodesCollection = new List<TreeNode>();
            foreach (TreeNode ChildNode in TreeNodeFunctions.GetNodesInsideFolder(TreeView_File, TreeView_File.SelectedNode, ChildNodesCollection))
            {
                EXSoundbanksFunctions.DeleteSound(ChildNode.Name, SoundsList);
            }
            TreeNodeFunctions.TreeNodeDeleteNode(TreeView_File, TreeView_File.SelectedNode, TreeView_File.SelectedNode.Tag.ToString());
        }

        private void RemoveSample()
        {
            EXSound ParentSound = EXSoundbanksFunctions.ReturnSoundFromDictionary(uint.Parse(TreeView_File.SelectedNode.Parent.Name), SoundsList);
            if (ParentSound != null)
            {
                ParentSound.Samples.Remove(uint.Parse(TreeView_File.SelectedNode.Name));
            }
            TreeView_File.SelectedNode.Remove();
        }

        private void RemoveSound()
        {
            TreeNode SelectedNode = TreeView_File.SelectedNode;
            //Save to Undo List
            KeyValuePair<uint, EXSound> SoundtoSave = new KeyValuePair<uint, EXSound>(uint.Parse(SelectedNode.Name), SoundsList[uint.Parse(SelectedNode.Name)]);
            SaveSnapshot(SoundtoSave, SelectedNode);

            EXSoundbanksFunctions.DeleteSound(SelectedNode.Name, SoundsList);
            TreeNodeFunctions.TreeNodeDeleteNode(TreeView_File, SelectedNode, TreeView_File.SelectedNode.Tag.ToString());
        }

        private void UpdateHashcodesValidList()
        {

            UpdateList = new Thread(() =>
            {
                //Clear List
                ListView_Hashcodes.BeginInvoke((MethodInvoker)delegate
                {
                    ListView_Hashcodes.Items.Clear();
                    ListView_Hashcodes.Enabled = false;
                });

                //Level Hashcode
                ListViewItem LevelHashcode = new ListViewItem(new[] { "", string.Join("", new string[] { "0x", ProjectInfo.Hashcode.ToString("X8") }), "<Label Not Found>", "File Properties" });
                if (Hashcodes.SB_Defines.ContainsKey(ProjectInfo.Hashcode))
                {
                    LevelHashcode.SubItems[0].Text = "OK";
                    LevelHashcode.ImageIndex = 2;
                    LevelHashcode.SubItems[2].Text = Hashcodes.SB_Defines[ProjectInfo.Hashcode];
                }
                else
                {
                    LevelHashcode.SubItems[0].Text = "Missing";
                    LevelHashcode.ImageIndex = 0;
                }
                LevelHashcode.UseItemStyleForSubItems = false;
                LevelHashcode.Tag = "Project";
                GenericFunctions.AddItemToListView(LevelHashcode, ListView_Hashcodes);

                //Sounds Hashcodes
                try
                {
                    TreeNode DisplayName;
                    //string DisplayName;
                    foreach (KeyValuePair<uint, EXSound> Sound in SoundsList)
                    {
                        DisplayName = TreeView_File.Nodes.Find(Sound.Key.ToString(), true)[0];
                        ListViewItem Hashcode = new ListViewItem(new[] { "", string.Join("", new string[] { "0x", Sound.Value.Hashcode.ToString("X8") }), "<Label Not Found>", DisplayName.Text });
                        if (Hashcodes.SFX_Defines.ContainsKey(Convert.ToUInt32(Sound.Value.Hashcode)))
                        {
                            Hashcode.SubItems[0].Text = "OK";
                            Hashcode.ImageIndex = 2;
                            Hashcode.SubItems[2].Text = Hashcodes.SFX_Defines[Convert.ToUInt32(Sound.Value.Hashcode)];
                        }
                        else
                        {
                            Hashcode.SubItems[0].Text = "Missing";
                            Hashcode.ImageIndex = 0;
                            Hashcode.SubItems[2].Text = "<Hashcode Not Found>";
                        }
                        Hashcode.UseItemStyleForSubItems = false;
                        Hashcode.Tag = DisplayName.Name.ToString();
                        GenericFunctions.AddItemToListView(Hashcode, ListView_Hashcodes);

                        GenericFunctions.ParentFormStatusBar.ShowProgramStatus(string.Join(" ", new string[] { "Checking hashcode:", Hashcode.SubItems[2].Text }));

                        //Show Items Count
                        Textbox_HashcodesCount.BeginInvoke((MethodInvoker)delegate
                        {
                            Textbox_HashcodesCount.Text = ListView_Hashcodes.Items.Count.ToString();
                        });
                        Thread.Sleep(30);
                    }
                    //Clear List
                    ListView_Hashcodes.BeginInvoke((MethodInvoker)delegate
                    {
                        ListView_Hashcodes.Enabled = true;
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
            UpdateList.Start();
        }

        private void UpdateStreamedDataList()
        {
            UpdateStreamDataList = new Thread(() =>
            {
                try
                {
                    string SoundDisplayName;
                    TreeNode NodeToCheck;

                    //Clear List
                    ListView_StreamData.BeginInvoke((MethodInvoker)delegate
                    {
                        ListView_StreamData.Items.Clear();
                        ListView_StreamData.Enabled = false;
                    });

                    foreach (KeyValuePair<uint, EXSound> Sound in SoundsList)
                    {
                        SoundDisplayName = TreeView_File.Nodes.Find(Sound.Key.ToString(), true)[0].Text;
                        foreach (KeyValuePair<uint, EXSample> Sample in Sound.Value.Samples)
                        {
                            if (Sample.Value.FileRef < 0)
                            {
                                NodeToCheck = TreeView_File.Nodes.Find(Sample.Key.ToString(), true)[0];
                                ListViewItem ItemStreamed = new ListViewItem(new[]
                                {
                                    NodeToCheck.Text,
                                    Sample.Value.FileRef.ToString(),
                                    SoundDisplayName,
                                    "HC00FFFF"
                                })
                                {
                                    UseItemStyleForSubItems = false
                                };
                                ItemStreamed.Tag = NodeToCheck.Name;
                                GenericFunctions.AddItemToListView(ItemStreamed, ListView_StreamData);

                                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(string.Join(" ", new string[] { "Checking Sample:", NodeToCheck.Text }));

                                //Show Items Count
                                Textbox_StreamFilesCount.BeginInvoke((MethodInvoker)delegate
                                {
                                    Textbox_StreamFilesCount.Text = ListView_StreamData.Items.Count.ToString();
                                });

                                Thread.Sleep(35);
                            }
                        }
                    }

                    //Enable List
                    ListView_StreamData.BeginInvoke((MethodInvoker)delegate
                    {
                        ListView_StreamData.Enabled = true;
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
            UpdateStreamDataList.Start();
        }

        private void UpdateWavDataList()
        {
            UpdateWavList = new Thread(() =>
            {
                try
                {
                    TreeNode NodeToCheck;

                    //Clear List
                    ListView_WavHeaderData.BeginInvoke((MethodInvoker)delegate
                    {
                        ListView_WavHeaderData.Items.Clear();
                        ListView_WavHeaderData.Enabled = false;
                    });

                    //Add data to list
                    foreach (KeyValuePair<string, EXAudio> item in AudioDataDict)
                    {
                        NodeToCheck = TreeView_File.Nodes.Find(item.Key, true)[0];
                        ListViewItem Hashcode = new ListViewItem(new[]
                        {
                            NodeToCheck.Text.ToString(),
                            item.Value.LoopOffset.ToString(),
                            item.Value.Frequency.ToString(),
                            item.Value.Channels.ToString(),
                            item.Value.Bits.ToString(),
                            item.Value.PCMdata.Length.ToString(),
                            item.Value.Encoding.ToString(),
                            item.Value.Duration.ToString(),
                        })
                        {
                            UseItemStyleForSubItems = false
                        };
                        Hashcode.Tag = NodeToCheck.Name;
                        GenericFunctions.AddItemToListView(Hashcode, ListView_WavHeaderData);

                        GenericFunctions.ParentFormStatusBar.ShowProgramStatus(string.Join(" ", new string[] { "Checking Audio:", item.Value.LoadedFileName.ToString() }));

                        //Show Items Count
                        Textbox_DataCount.Invoke((MethodInvoker)delegate
                        {
                            Textbox_DataCount.Text = ListView_WavHeaderData.Items.Count.ToString();
                        });

                        Thread.Sleep(25);
                    }

                    //Enable List
                    ListView_WavHeaderData.BeginInvoke((MethodInvoker)delegate
                    {
                        ListView_WavHeaderData.Enabled = true;
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
        // Save a snapshot in the undo list.
        private void SaveSnapshot(object SoundToSave, TreeNode NodeToSave)
        {
            //Save the snapshot.
            UndoListSounds.Push(SoundToSave);
            UndoListNodes.Push(new KeyValuePair<string, TreeNode>(NodeToSave.Parent.Name, NodeToSave));

            //Enable or disable the Undo and Redo menu items.
            EnableUndo();
        }

        //Enable or disable the Undo and Redo menu items.
        private void EnableUndo()
        {
            MenuItem_Edit_Undo.Enabled = (UndoListSounds.Count > 0);
        }
    }
}