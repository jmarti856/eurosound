﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_Soundbanks_Main
    {
        public string SaveDocument(string LoadedFile, TreeView TreeView_File, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudioDataDict, ProjectFile ProjectProperties)
        {
            string NewFilePath;

            if (!string.IsNullOrEmpty(LoadedFile))
            {
                NewFilePath = SerializeInfo.SaveSoundBanksDocument(TreeView_File, SoundsList, AudioDataDict, LoadedFile, ProjectProperties);
            }
            else
            {
                NewFilePath = OpenSaveAsDialog(TreeView_File, SoundsList, AudioDataDict, ProjectProperties);
            }

            return NewFilePath;
        }

        internal string OpenSaveAsDialog(TreeView TreeView_File, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudioDataDict, ProjectFile FileProperties)
        {
            string SavePath = GenericFunctions.SaveFileBrowser("EuroSound Files (*.esf)|*.esf|All files (*.*)|*.*", 1, true, Hashcodes.GetHashcodeLabel(Hashcodes.SB_Defines, FileProperties.Hashcode));
            if (!string.IsNullOrEmpty(SavePath))
            {
                if (Directory.Exists(Path.GetDirectoryName(SavePath)))
                {
                    SerializeInfo.SaveSoundBanksDocument(TreeView_File, SoundsList, AudioDataDict, SavePath, FileProperties);
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
                    Text = SelectedNode.Text + " Properties",
                    Tag = this.Tag,
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
            EXSound ParentSound = EXSoundbanksFunctions.GetSoundByName(uint.Parse(SelectedNode.Parent.Name), SoundsList);
            EXSample SelectedSample = TreeNodeFunctions.GetSelectedSample(ParentSound, SelectedNode.Name);

            Frm_SampleProperties FormSampleProps = new Frm_SampleProperties(SelectedSample, EXSoundbanksFunctions.SubSFXFlagChecked(ParentSound.Flags))
            {
                Text = SelectedNode.Text + " Properties",
                Tag = this.Tag,
                Owner = this,
                ShowInTaskbar = false
            };
            FormSampleProps.ShowDialog();
            FormSampleProps.Dispose();
            ProjectInfo.FileHasBeenModified = true;
        }

        internal void OpenSelectedNodeSampleProperties(TreeNode SelectedNode)
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
            EXSound SelectedSound = EXSoundbanksFunctions.GetSoundByName(uint.Parse(SelectedNode.Name), SoundsList);
            Frm_EffectProperties FormSoundProps = new Frm_EffectProperties(SelectedSound, SelectedNode.Name)
            {
                Text = SelectedNode.Text + " Properties",
                Tag = this.Tag,
                Owner = this,
                ShowInTaskbar = false,
            };
            FormSoundProps.ShowDialog();
            FormSoundProps.Dispose();
            ProjectInfo.FileHasBeenModified = true;
        }

        internal void RemoveAudioAndWarningDependencies(TreeNode SelectedNode)
        {
            List<string> Dependencies = EXSoundbanksFunctions.GetAudioDependencies(SelectedNode.Name, SelectedNode.Text, SoundsList, false);
            if (Dependencies.Count > 0)
            {
                EuroSound_ErrorsAndWarningsList ShowDependencies = new EuroSound_ErrorsAndWarningsList(Dependencies)
                {
                    Text = "Deleting Audio",
                    ShowInTaskbar = false,
                    TopMost = true
                };
                ShowDependencies.ShowDialog();
                ShowDependencies.Dispose();
            }
            else
            {
                RemoveAudioSelectedNode(SelectedNode);
            }
        }

        internal void RemoveAudioSelectedNode(TreeNode SelectedNode)
        {
            /*Show warning*/
            if (GlobalPreferences.ShowWarningMessagesBox)
            {
                EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox("Delete Audio: " + SelectedNode.Text, "Warning", true);
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

        internal void RemoveFolderSelectedNode(TreeNode SelectedNode)
        {
            /*Check we are not trying to delete a root folder*/
            if (!(SelectedNode == null || SelectedNode.Tag.Equals("Root")))
            {
                /*Show warning*/
                if (GlobalPreferences.ShowWarningMessagesBox)
                {
                    EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox("Delete Folder: " + SelectedNode.Text, "Warning", true);
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

        internal void RemoveSampleSelectedNode(TreeNode SelectedNode)
        {
            /*Show warning*/
            if (GlobalPreferences.ShowWarningMessagesBox)
            {
                EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox("Delete Sample: " + SelectedNode.Text, "Warning", true);
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

        internal void RemoveSoundSelectedNode(TreeNode SelectedNode)
        {
            /*Show warning*/
            if (GlobalPreferences.ShowWarningMessagesBox)
            {
                EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox("Delete Sound: " + SelectedNode.Text, "Warning", true);
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

        private void AddItemToListView(ListViewItem ItemToAdd, ListView ListToAddItem)
        {
            try
            {
                ListToAddItem.Invoke((MethodInvoker)delegate
                {
                    if (!ListToAddItem.IsDisposed)
                    {
                        ListToAddItem.Items.Add(ItemToAdd);
                    }
                });
            }
            catch
            {
                // Ignore.  Control is disposed cannot update the UI.
            }
        }

        private void RemoveAudio()
        {
            //EXObjectsFunctions.RemoveSound(TreeView_File.SelectedNode.Name, SoundsList);
            EXSoundbanksFunctions.DeleteAudio(AudioDataDict, TreeView_File.SelectedNode.Name);
            TreeNodeFunctions.TreeNodeDeleteNode(TreeView_File, TreeView_File.SelectedNode, TreeView_File.SelectedNode.Tag.ToString());
        }

        private void RemoveRecursivelyFolder()
        {
            /*Remove child nodes sounds and samples*/
            IList<TreeNode> ChildNodesCollection = new List<TreeNode>();
            foreach (TreeNode ChildNode in TreeNodeFunctions.GetNodesInsideFolder(TreeView_File, TreeView_File.SelectedNode, ChildNodesCollection))
            {
                EXSoundbanksFunctions.RemoveSound(ChildNode.Name, SoundsList);
            }
            TreeNodeFunctions.TreeNodeDeleteNode(TreeView_File, TreeView_File.SelectedNode, TreeView_File.SelectedNode.Tag.ToString());
        }

        private void RemoveSample()
        {
            EXSound ParentSound = EXSoundbanksFunctions.GetSoundByName(uint.Parse(TreeView_File.SelectedNode.Parent.Name), SoundsList);
            EXSample SampleToRemove = TreeNodeFunctions.GetSelectedSample(ParentSound, TreeView_File.SelectedNode.Name);
            if (SampleToRemove != null)
            {
                ParentSound.Samples.Remove(SampleToRemove);
            }
            TreeView_File.SelectedNode.Remove();
        }

        private void RemoveSound()
        {
            EXSoundbanksFunctions.RemoveSound(TreeView_File.SelectedNode.Name, SoundsList);
            TreeNodeFunctions.TreeNodeDeleteNode(TreeView_File, TreeView_File.SelectedNode, TreeView_File.SelectedNode.Tag.ToString());
        }

        private void UpdateHashcodesValidList()
        {
            UpdateList = new Thread(() =>
            {
                /*Clear List*/
                ListView_Hashcodes.Invoke((MethodInvoker)delegate
                {
                    ListView_Hashcodes.Items.Clear();
                });

                /*Level Hashcode*/
                ListViewItem LevelHashcode = new ListViewItem(new[] { "", ProjectInfo.Hashcode.ToString(), "<Label Not Found>", "File Properties" });
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
                AddItemToListView(LevelHashcode, ListView_Hashcodes);

                /*Sounds Hashcodes*/
                try
                {
                    foreach (KeyValuePair<uint, EXSound> Sound in SoundsList)
                    {
                        ListViewItem Hashcode = new ListViewItem(new[] { "", Sound.Value.Hashcode.ToString(), "<Label Not Found>", Sound.Value.DisplayName });
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
                        AddItemToListView(Hashcode, ListView_Hashcodes);

                        GenericFunctions.SetStatusToStatusBar("Checking hashcode: " + Hashcode.SubItems[2].Text);

                        Thread.Sleep(5);
                    }
                }
                catch
                {
                    /*Clear List*/
                    ListView_Hashcodes.Invoke((MethodInvoker)delegate
                    {
                        ListView_Hashcodes.Items.Clear();
                    });
                }
                GenericFunctions.SetStatusToStatusBar(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
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
                    /*Clear List*/
                    ListView_StreamData.Invoke((MethodInvoker)delegate
                    {
                        ListView_StreamData.Items.Clear();
                    });

                    foreach (KeyValuePair<uint, EXSound> Sound in SoundsList)
                    {
                        foreach (EXSample Sample in Sound.Value.Samples)
                        {
                            if (Sample.FileRef < 0)
                            {
                                ListViewItem ItemStreamed = new ListViewItem(new[]
                                {
                                    Sample.DisplayName,
                                    Sample.FileRef.ToString(),
                                    Sound.Value.DisplayName,
                                    "HC00FFFF"
                                })
                                {
                                    UseItemStyleForSubItems = false
                                };
                                AddItemToListView(ItemStreamed, ListView_StreamData);

                                GenericFunctions.SetStatusToStatusBar("Checking Sample: " + Sample.DisplayName);

                                Thread.Sleep(5);
                            }
                        }
                    }
                }
                catch
                {
                    /*Clear List*/
                    ListView_StreamData.Invoke((MethodInvoker)delegate
                    {
                        ListView_StreamData.Items.Clear();
                    });
                }
                GenericFunctions.SetStatusToStatusBar(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
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
                    /*Clear List*/
                    ListView_WavHeaderData.Invoke((MethodInvoker)delegate
                    {
                        ListView_WavHeaderData.Items.Clear();
                    });

                    foreach (KeyValuePair<string, EXAudio> item in AudioDataDict)
                    {
                        string DisplayName = TreeView_File.Nodes.Find(item.Key, true)[0].Text;
                        ListViewItem Hashcode = new ListViewItem(new[]
                        {
                            DisplayName.ToString(),
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
                        AddItemToListView(Hashcode, ListView_WavHeaderData);

                        GenericFunctions.SetStatusToStatusBar("Checking audio: " + item.Value.Name.ToString());

                        Thread.Sleep(5);
                    }
                }
                catch
                {
                    //Cancel and clear list
                    ListView_WavHeaderData.Invoke((MethodInvoker)delegate
                    {
                        ListView_WavHeaderData.Items.Clear();
                    });
                }
                GenericFunctions.SetStatusToStatusBar(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
            })
            {
                IsBackground = true
            };
            UpdateWavList.Start();
        }
    }
}