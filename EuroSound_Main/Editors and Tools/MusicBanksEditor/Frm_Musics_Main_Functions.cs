using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.Clases;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.Editors_and_Tools.ApplicationTargets;
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

        internal void OpenMusicPropertiesForm(TreeNode SelectedNode)
        {
            string MusicKeyInDictionary = SelectedNode.Name;
            EXMusic SelectedMusic = EXMusicsFunctions.GetMusicByName(Convert.ToUInt32(MusicKeyInDictionary), MusicsList);
            if (SelectedMusic != null)
            {
                GenericFunctions.SetCurrentFileLabel(SelectedNode.Text, "SBPanel_LastFile");
                Frm_Musics_Properties FormAudioProps = new Frm_Musics_Properties(SelectedMusic, ProjectInfo, MusicKeyInDictionary, SelectedNode.Text)
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

                //Disable Update button
                Button_UpdateProperties.BeginInvoke((MethodInvoker)delegate
                {
                    Button_UpdateProperties.Enabled = false;
                });

                //Array of items
                ListViewItem[] itemsToAdd = new ListViewItem[MusicsList.Count * 2];

                //Add data to list
                int counter = 0;
                foreach (KeyValuePair<uint, EXMusic> item in MusicsList)
                {
                    TreeNode NodeToCheck = TreeView_MusicData.Nodes.Find(item.Key.ToString(), true)[0];

                    //Left Channel
                    ListViewItem LeftChannelInfo = new ListViewItem(new[]
                    {
                        NodeToCheck.Text + " L",
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
                        Tag = NodeToCheck.Name,
                        UseItemStyleForSubItems = false
                    };
                    itemsToAdd[counter] = LeftChannelInfo;
                    counter++;

                    //RightChannel
                    ListViewItem RightChannelInfo = new ListViewItem(new[]
                    {
                        NodeToCheck.Text + " R",
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
                        Tag = NodeToCheck.Name,
                        UseItemStyleForSubItems = false
                    };
                    itemsToAdd[counter] = RightChannelInfo;
                    counter++;
                }

                //Enable List
                ListView_WavHeaderData.BeginInvoke((MethodInvoker)delegate
                {
                    ListView_WavHeaderData.Items.AddRange(itemsToAdd);
                    ListView_WavHeaderData.EndUpdate();
                    ListView_WavHeaderData.Enabled = true;
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
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.resourcesManager.GetString("StatusBar_Status_Ready"));
            })
            {
                IsBackground = true
            };
            UpdateWavList.Start();
        }
    }
}
