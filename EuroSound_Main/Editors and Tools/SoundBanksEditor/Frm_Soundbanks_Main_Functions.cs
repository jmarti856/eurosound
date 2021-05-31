using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.Clases;
using EuroSound_Application.CurrentProjectFunctions;
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
                //Clear List
                ListView_Hashcodes.BeginInvoke((MethodInvoker)delegate
                {
                    ListView_Hashcodes.Items.Clear();
                    ListView_Hashcodes.Enabled = false;
                });

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

                GenericFunctions.AddItemToListView(levelHashcodeItem, ListView_Hashcodes);

                //Sounds Hashcodes
                foreach (KeyValuePair<uint, EXSound> soundItem in SoundsList)
                {
                    TreeNode DisplayName = TreeView_File.Nodes.Find(soundItem.Key.ToString(), true)[0];
                    ListViewItem Hashcode = new ListViewItem(new[] { "", string.Join("", new string[] { "0x", soundItem.Value.Hashcode.ToString("X8") }), "<Label Not Found>", DisplayName.Text });
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

                    GenericFunctions.AddItemToListView(Hashcode, ListView_Hashcodes);
                    GenericFunctions.ParentFormStatusBar.ShowProgramStatus(string.Join(" ", new string[] { "Checking hashcode:", Hashcode.SubItems[2].Text }));

                    //Show Items Count
                    Textbox_HashcodesCount.BeginInvoke((MethodInvoker)delegate
                    {
                        Textbox_HashcodesCount.Text = ListView_Hashcodes.Items.Count.ToString();
                    });
                    Thread.Sleep(30);
                }

                //Enable List
                ListView_Hashcodes.BeginInvoke((MethodInvoker)delegate
                {
                    ListView_Hashcodes.Enabled = true;
                });
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
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
            }
        }

        private void UpdateStreamedDataList()
        {
            UpdateStreamDataList = new Thread(() =>
            {
                //Clear List
                ListView_StreamData.BeginInvoke((MethodInvoker)delegate
                {
                    ListView_StreamData.Items.Clear();
                    ListView_StreamData.Enabled = false;
                });

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
                            { UseItemStyleForSubItems = false, Tag = NodeToCheck.Name };

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
                ListView_StreamData.Invoke((MethodInvoker)delegate
                {
                    ListView_StreamData.Enabled = true;
                });
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
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
            }
        }

        private void UpdateWavDataList()
        {
            UpdateWavList = new Thread(() =>
            {
                //Clear List
                ListView_WavHeaderData.BeginInvoke((MethodInvoker)delegate
                {
                    ListView_WavHeaderData.Items.Clear();
                    ListView_WavHeaderData.Enabled = false;
                });

                //Add data to list
                foreach (KeyValuePair<string, EXAudio> audioItem in AudioDataDict)
                {
                    TreeNode NodeToCheck = TreeView_File.Nodes.Find(audioItem.Key, true)[0];
                    ListViewItem Hashcode = new ListViewItem(new[]
                    {
                        NodeToCheck.Text,
                        audioItem.Value.LoopOffset.ToString(),
                        audioItem.Value.Flags.ToString(),
                        audioItem.Value.Frequency.ToString(),
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

                    GenericFunctions.AddItemToListView(Hashcode, ListView_WavHeaderData);
                    GenericFunctions.ParentFormStatusBar.ShowProgramStatus(string.Join(" ", new string[] { "Checking Audio:", audioItem.Value.LoadedFileName.ToString() }));

                    //Show Items Count
                    Textbox_DataCount.BeginInvoke((MethodInvoker)delegate
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
    }
}