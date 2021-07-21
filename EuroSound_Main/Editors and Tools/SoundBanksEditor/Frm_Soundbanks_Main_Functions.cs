using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.Clases;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.CustomControls.statisticsForm;
using EuroSound_Application.Editors_and_Tools;
using EuroSound_Application.Editors_and_Tools.ApplicationTargets;
using EuroSound_Application.HashCodesFunctions;
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
        private string OpenSaveAsDialog(TreeView TreeView_File, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudioDataDict, ProjectFile FileProperties)
        {
            string savePath = BrowsersAndDialogs.SaveFileBrowser("EuroSound Files (*.esf)|*.esf|All files (*.*)|*.*", 1, true, Hashcodes.GetHashcodeLabel(Hashcodes.SB_Defines, FileProperties.Hashcode));
            if (!string.IsNullOrEmpty(savePath))
            {
                if (Directory.Exists(Path.GetDirectoryName(savePath)))
                {
                    EuroSoundFilesFunctions.SaveEuroSoundFile(TreeView_File, SoundsList, AudioDataDict, OutputTargets, savePath, FileProperties);

                    //Add file to recent list
                    RecentFilesMenu.AddFile(savePath);
                }
            }
            return savePath;
        }

        internal void OpenAudioProperties(TreeNode SelectedNode)
        {
            EXAudio selectedSound = TreeNodeFunctions.GetSelectedAudio(SelectedNode.Name, AudioDataDict);
            if (selectedSound != null)
            {
                GenericFunctions.SetCurrentFileLabel(SelectedNode.Text, "SBPanel_LastFile");
                Frm_AudioProperties FormAudioProps = new Frm_AudioProperties(selectedSound, ProjectInfo, SelectedNode.Name)
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

        internal void OpenSampleProperties(TreeNode SelectedNode)
        {
            string section = TreeNodeFunctions.FindRootNode(SelectedNode).Name;
            EXSound parentSound = EXSoundbanksFunctions.ReturnSoundFromDictionary(uint.Parse(SelectedNode.Parent.Name), SoundsList);
            EXSample selectedSample = EXSoundbanksFunctions.ReturnSampleFromSound(parentSound, uint.Parse(SelectedNode.Name));

            if (section.Equals("StreamedSounds"))
            {
                //Open form only if file exists
                if (File.Exists(GlobalPreferences.StreamFilePath))
                {
                    GenericFunctions.SetCurrentFileLabel(SelectedNode.Text, "SBPanel_LastFile");
                    using (Frm_NewStreamSound addStreamSound = new Frm_NewStreamSound(selectedSample, ProjectInfo))
                    {
                        addStreamSound.Text = GenericFunctions.TruncateLongString(SelectedNode.Text, 25) + " - Properties";
                        addStreamSound.Tag = Tag;
                        addStreamSound.Owner = this;
                        addStreamSound.ShowInTaskbar = false;
                        addStreamSound.ShowDialog();

                        if (addStreamSound.DialogResult == DialogResult.OK)
                        {
                            selectedSample.FileRef = (short)addStreamSound.SelectedSound;
                        }
                    };
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("The stream sounds file has not found, the file route is: \"" + GlobalPreferences.StreamFilePath + "\", do you want to specify another path ?", "EuroSound", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (dialogResult == DialogResult.Yes)
                    {
                        GlobalPreferences.StreamFilePath = BrowsersAndDialogs.FileBrowserDialog("EuroSound Files (*.esf)|*.esf", 0, true);
                        WindowsRegistryFunctions.SaveExternalFiles("StreamFile", "Path", GlobalPreferences.StreamFilePath);
                    }
                }
            }
            else
            {
                GenericFunctions.SetCurrentFileLabel(SelectedNode.Text, "SBPanel_LastFile");
                Frm_SampleProperties FormSampleProps = new Frm_SampleProperties(selectedSample, ProjectInfo, EXSoundbanksFunctions.SubSFXFlagChecked(parentSound.Flags))
                {
                    Text = GenericFunctions.TruncateLongString(SelectedNode.Text, 25) + " - Properties",
                    Tag = Tag,
                    Owner = this,
                    ShowInTaskbar = false
                };
                FormSampleProps.ShowDialog();
                FormSampleProps.Dispose();
            }
        }

        internal void OpenSoundProperties(TreeNode SelectedNode)
        {
            string soundSection = TreeNodeFunctions.FindRootNode(SelectedNode).Name;
            EXSound selectedSound = EXSoundbanksFunctions.ReturnSoundFromDictionary(uint.Parse(SelectedNode.Name), SoundsList);
            GenericFunctions.SetCurrentFileLabel(SelectedNode.Text, "SBPanel_LastFile");
            Frm_EffectProperties formSoundProps = new Frm_EffectProperties(selectedSound, ProjectInfo, SelectedNode.Name, soundSection)
            {
                Text = GenericFunctions.TruncateLongString(SelectedNode.Text, 25) + " - Properties",
                Tag = Tag,
                Owner = this,
                ShowInTaskbar = false,
            };
            formSoundProps.ShowDialog();
            formSoundProps.Dispose();
        }

        internal void OpenTargetProperties(TreeNode SelectedNode)
        {
            EXAppTarget outTarget = OutputTargets[Convert.ToUInt32(SelectedNode.Name)];
            using (Frm_ApplicationTarget newOutTarget = new Frm_ApplicationTarget(outTarget, SelectedNode, TreeView_File) { Owner = this })
            {
                newOutTarget.ShowDialog();

                if (newOutTarget.DialogResult == DialogResult.OK)
                {
                    //File has been modified
                    ProjectInfo.FileHasBeenModified = true;
                }
            }
        }

        private void RemoveAudioAndWarningDependencies(TreeNode SelectedNode)
        {
            IEnumerable<string> dependencies = EXSoundbanksFunctions.GetAudioDependencies(SelectedNode.Name, SelectedNode.Text, SoundsList, TreeView_File, false);
            if (dependencies.Any())
            {
                GenericFunctions.ShowErrorsAndWarningsList(dependencies, "Deleting Audio", this);
            }
            else
            {
                ToolsCommonFunctions.RemoveEngineXObject("Remove audio:", (int)Enumerations.EXObjectType.EXAudio, TreeView_File, SelectedNode, AudioDataDict, SoundsList, ProjectInfo, UndoListSounds, UndoListNodes, MenuItem_Edit_Undo, Tag.ToString());
            }
        }

        private void UpdateHashcodesValidList()
        {
            UpdateList = new Thread(() =>
            {
                //Update status bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus("Updating list..");

                //Clear List
                ListView_Hashcodes.BeginInvoke((MethodInvoker)delegate
                {
                    ListView_Hashcodes.Items.Clear();
                    ListView_Hashcodes.BeginUpdate();
                    ListView_Hashcodes.Enabled = false;
                });

                //Array of items
                ListViewItem[] itemsToAdd = new ListViewItem[SoundsList.Count + 1];

                //Level Hashcode
                ListViewItem levelHashcodeItem = new ListViewItem(new[] { "", string.Join("", new string[] { "0x", ProjectInfo.Hashcode.ToString("X8") }), "<Label Not Found>", "File Properties" });
                if (Hashcodes.SB_Defines.ContainsKey(ProjectInfo.Hashcode))
                {
                    levelHashcodeItem.SubItems[0].Text = "OK";
                    levelHashcodeItem.ImageIndex = 2;
                    levelHashcodeItem.SubItems[2].Text = Hashcodes.SB_Defines[ProjectInfo.Hashcode];
                }
                else
                {
                    levelHashcodeItem.SubItems[0].Text = "Missing";
                    levelHashcodeItem.ImageIndex = 0;
                }
                levelHashcodeItem.UseItemStyleForSubItems = false;
                levelHashcodeItem.Tag = "Project";

                itemsToAdd[0] = levelHashcodeItem;

                //Add data to list
                int counter = 1;
                foreach (KeyValuePair<uint, EXSound> soundItem in SoundsList)
                {
                    TreeNode DisplayName = TreeView_File.Nodes.Find(soundItem.Key.ToString(), true)[0];
                    Enumerations.OutputTarget target = (Enumerations.OutputTarget)soundItem.Value.OutputTarget;
                    ListViewItem Hashcode = new ListViewItem(new[] { "", string.Join("", new string[] { "0x", soundItem.Value.Hashcode.ToString("X8") }), "<Label Not Found>", DisplayName.Text, target.ToString() });
                    if (Hashcodes.SFX_Defines.ContainsKey(Convert.ToUInt32(soundItem.Value.Hashcode)))
                    {
                        Hashcode.SubItems[0].Text = "OK";
                        Hashcode.ImageIndex = 2;
                        Hashcode.SubItems[2].Text = Hashcodes.SFX_Defines[Convert.ToUInt32(soundItem.Value.Hashcode)];
                    }
                    else
                    {
                        Hashcode.SubItems[0].Text = "Missing";
                        Hashcode.ImageIndex = 0;
                        Hashcode.SubItems[2].Text = "<Hashcode Not Found>";
                    }
                    Hashcode.UseItemStyleForSubItems = false;
                    Hashcode.Tag = DisplayName.Name.ToString();

                    itemsToAdd[counter] = Hashcode;
                    counter++;
                }

                //Enable List
                ListView_Hashcodes.BeginInvoke((MethodInvoker)delegate
                {
                    ListView_Hashcodes.Items.AddRange(itemsToAdd);
                    ListView_Hashcodes.EndUpdate();
                    ListView_Hashcodes.Enabled = true;
                });

                //Show Items Count
                if (!(Textbox_HashcodesCount.IsDisposed || Textbox_HashcodesCount.Disposing))
                {
                    Textbox_HashcodesCount.BeginInvoke((MethodInvoker)delegate
                    {
                        Textbox_HashcodesCount.Text = ListView_Hashcodes.Items.Count.ToString();
                    });
                }

                //Enable button if there's content on the list
                if (counter > 0)
                {
                    if (!(Button_StopHashcodeUpdate.IsDisposed || Button_StopHashcodeUpdate.Disposing))
                    {
                        Button_StopHashcodeUpdate.BeginInvoke((MethodInvoker)delegate
                        {
                            Button_StopHashcodeUpdate.Enabled = true;
                        });
                    }
                }

                //Update status bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
            })
            {
                IsBackground = true
            };
            UpdateList.Start();
        }

        private void Button_StopHashcodeUpdate_Click(object sender, EventArgs e)
        {
            if (UpdateList != null)
            {
                UpdateList.Abort();
                ListView_Hashcodes.Items.Clear();
                ListView_Hashcodes.Enabled = true;
                Textbox_HashcodesCount.Text = "0";
                Button_StopHashcodeUpdate.Enabled = false;

                //Update status bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
            }
        }

        private void UpdateStreamedDataList()
        {
            UpdateStreamDataList = new Thread(() =>
            {
                //Update status bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus("Updating list..");

                //Clear List
                ListView_StreamData.BeginInvoke((MethodInvoker)delegate
                {
                    ListView_StreamData.Items.Clear();
                    ListView_StreamData.BeginUpdate();
                    ListView_StreamData.Enabled = false;
                });

                //Get stream sounds count
                int StreamedSounds = 0;
                foreach (KeyValuePair<uint, EXSound> soundItem in SoundsList)
                {
                    foreach (KeyValuePair<uint, EXSample> Sample in soundItem.Value.Samples)
                    {
                        if (Sample.Value.FileRef < 0)
                        {
                            StreamedSounds++;
                        }
                    }
                }

                //Add data to list
                int counter = 0;
                ListViewItem[] itemsToAdd = new ListViewItem[StreamedSounds];
                foreach (KeyValuePair<uint, EXSound> soundItem in SoundsList)
                {
                    string SoundDisplayName = TreeView_File.Nodes.Find(soundItem.Key.ToString(), true)[0].Text;
                    foreach (KeyValuePair<uint, EXSample> Sample in soundItem.Value.Samples)
                    {
                        if (Sample.Value.FileRef < 0)
                        {
                            TreeNode NodeToCheck = TreeView_File.Nodes.Find(Sample.Key.ToString(), true)[0];
                            ListViewItem ItemStreamed = new ListViewItem(new[]
                            {
                                NodeToCheck.Text,
                                Sample.Value.FileRef.ToString(),
                                SoundDisplayName,
                                "HC00FFFF"
                            })
                            {
                                UseItemStyleForSubItems = false,
                                Tag = NodeToCheck.Name
                            };

                            itemsToAdd[counter] = ItemStreamed;
                            counter++;
                        }
                    }
                }

                //Enable List
                if (!(ListView_StreamData.IsDisposed || ListView_StreamData.Disposing))
                {
                    ListView_StreamData.BeginInvoke((MethodInvoker)delegate
                    {
                        ListView_StreamData.Items.AddRange(itemsToAdd);
                        ListView_StreamData.EndUpdate();
                        ListView_StreamData.Enabled = true;
                    });
                }

                //Show Items Count
                if (!(Textbox_StreamFilesCount.IsDisposed || Textbox_StreamFilesCount.Disposing))
                {
                    Textbox_StreamFilesCount.BeginInvoke((MethodInvoker)delegate
                    {
                        Textbox_StreamFilesCount.Text = counter.ToString();
                    });
                }

                //Enable button if there's content on the list
                if (counter > 0)
                {
                    if (!(Button_StopStreamData.IsDisposed || Button_StopStreamData.Disposing))
                    {
                        Button_StopStreamData.BeginInvoke((MethodInvoker)delegate
                        {
                            Button_StopStreamData.Enabled = true;
                        });
                    }
                }

                //Update status bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
            })
            {
                IsBackground = true
            };
            UpdateStreamDataList.Start();
        }

        private void Button_StopStreamData_Click(object sender, EventArgs e)
        {
            if (UpdateStreamDataList != null)
            {
                UpdateStreamDataList.Abort();
                ListView_StreamData.Items.Clear();
                ListView_StreamData.Enabled = true;
                Textbox_StreamFilesCount.Text = "0";
                Button_StopStreamData.Enabled = false;

                //Update status bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
            }
        }

        private void UpdateWavDataList()
        {
            UpdateWavList = new Thread(() =>
            {
                //Update status bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus("Updating list..");

                //Clear List
                ListView_WavHeaderData.BeginInvoke((MethodInvoker)delegate
                {
                    ListView_WavHeaderData.Items.Clear();
                    ListView_WavHeaderData.BeginUpdate();
                    ListView_WavHeaderData.Enabled = false;
                });

                //Add data to list
                int counter = 0;
                ListViewItem[] itemsToAdd = new ListViewItem[AudioDataDict.Count];
                foreach (KeyValuePair<string, EXAudio> audioItem in AudioDataDict)
                {
                    TreeNode NodeToCheck = TreeView_File.Nodes.Find(audioItem.Key, true)[0];
                    ListViewItem listItem = new ListViewItem(new[]
                    {
                        NodeToCheck.Text,
                        audioItem.Value.LoopOffset.ToString(),
                        audioItem.Value.Flags.ToString(),
                        audioItem.Value.Frequency.ToString(),
                        audioItem.Value.FrequencyPS2.ToString(),
                        audioItem.Value.Channels.ToString(),
                        audioItem.Value.Bits.ToString(),
                        audioItem.Value.PCMdata.Length.ToString(),
                        audioItem.Value.Encoding.ToString(),
                        audioItem.Value.Duration.ToString(),
                    })
                    {
                        Tag = NodeToCheck.Name,
                        UseItemStyleForSubItems = false
                    };
                    itemsToAdd[counter] = listItem;
                    counter++;
                }

                //Enable List
                ListView_WavHeaderData.BeginInvoke((MethodInvoker)delegate
                {
                    ListView_WavHeaderData.Items.AddRange(itemsToAdd);
                    ListView_WavHeaderData.EndUpdate();
                    ListView_WavHeaderData.Enabled = true;
                });

                //Show Items Count
                if (!(Textbox_DataCount.IsDisposed || Textbox_DataCount.Disposing))
                {
                    Textbox_DataCount.BeginInvoke((MethodInvoker)delegate
                    {
                        Textbox_DataCount.Text = counter.ToString();
                    });
                }

                //Enable button if there's content on the list
                if (counter > 0)
                {
                    if (!(Button_Stop_WavUpdate.IsDisposed || Button_Stop_WavUpdate.Disposing))
                    {
                        Button_Stop_WavUpdate.BeginInvoke((MethodInvoker)delegate
                        {
                            Button_Stop_WavUpdate.Enabled = true;
                        });
                    }
                }

                //Update status bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
            })
            {
                IsBackground = true
            };
            UpdateWavList.Start();
        }

        private void Button_Stop_WavUpdate_Click(object sender, EventArgs e)
        {
            if (UpdateWavList != null)
            {
                UpdateWavList.Abort();
                ListView_WavHeaderData.Items.Clear();
                ListView_WavHeaderData.Enabled = true;
                Textbox_DataCount.Text = "0";
                Button_Stop_WavUpdate.Enabled = false;

                //Update status bar
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
            }
        }

        private void Button_Statistics_Click(object sender, EventArgs e)
        {
            if (AudioDataDict.Count > 0)
            {
                //Start with setting up the dictionary.
                Dictionary<uint, uint> dict = new Dictionary<uint, uint> { };

                //Iterate through the values, incrementing current count.
                foreach (EXAudio AudioToCheck in AudioDataDict.Values)
                {
                    if (dict.ContainsKey(AudioToCheck.FrequencyPS2))
                    {
                        dict[AudioToCheck.FrequencyPS2] += 1;
                    }
                    else
                    {
                        dict.Add(AudioToCheck.FrequencyPS2, 1);
                    }
                }

                //Show form
                EuroSound_Graphics statisticsForm = new EuroSound_Graphics(dict);
                statisticsForm.ShowDialog();
            }
            else
            {
                MessageBox.Show(GenericFunctions.resourcesManager.GetString("NoItemsToShow"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}