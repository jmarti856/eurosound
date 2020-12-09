using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SoundBanks_Editor
{
    public partial class Frm_Soundbanks_Main
    {
        //string ProgramStatus;
        internal void OpenSelectedNodeSampleProperties(ProjectFile ProjectProperties)
        {
            if (TreeView_File.SelectedNode != null)
            {
                if (TreeView_File.SelectedNode.Tag.Equals("Sound"))
                {
                    OpenSoundProperties(ProjectProperties);
                }
                else if (TreeView_File.SelectedNode.Tag.Equals("Sample"))
                {
                    OpenSampleProperties();
                }
            }
        }

        internal void OpenSoundProperties(ProjectFile ProjectProperties)
        {
            EXSound SelectedSound = EXObjectsFunctions.GetSoundByName(int.Parse(TreeView_File.SelectedNode.Name), SoundsList);
            Frm_EffectProperties FormSoundProps = new Frm_EffectProperties(SelectedSound, TreeView_File.SelectedNode.Name, ProjectProperties, HashcodesSFX, HashcodesSFXData, SFX_Defines, SB_Defines, SFX_Data, this.Tag.ToString(), ResourcesManager)
            {
                Text = TreeView_File.SelectedNode.Text + " Properties",
                Tag = this.Tag,
                Owner = this,
                ShowInTaskbar = false,
            };
            FormSoundProps.ShowDialog();
        }

        internal void OpenAudioProperties()
        {
            EXAudio SelectedSound = TreeNodeFunctions.GetSelectedAudio(TreeView_File.SelectedNode.Name, AudioDataDict);
            if (SelectedSound != null)
            {
                Frm_AudioProperties FormAudioProps = new Frm_AudioProperties(SelectedSound, TreeView_File.SelectedNode.Name, AudioDataDict, ProjectInfo)
                {
                    Text = TreeView_File.SelectedNode.Text + " Properties",
                    Tag = this.Tag,
                    Owner = this,
                    ShowInTaskbar = false
                };
                FormAudioProps.Show();
            }
        }

        internal void OpenSampleProperties()
        {
            EXSound ParentSound = EXObjectsFunctions.GetSoundByName(int.Parse(TreeView_File.SelectedNode.Parent.Name), SoundsList);
            EXSample SelectedSample = TreeNodeFunctions.GetSelectedSample(ParentSound, TreeView_File.SelectedNode.Name);

            Frm_SampleProperties FormSampleProps = new Frm_SampleProperties(SelectedSample, AudioDataDict, TreeView_File, EXObjectsFunctions.SubSFXFlagChecked(ParentSound.Flags), ProjectInfo, HashcodesSFX, SFX_Defines, SB_Defines, ResourcesManager)
            {
                Text = TreeView_File.SelectedNode.Text + " Properties",
                Tag = this.Tag,
                Owner = this,
                ShowInTaskbar = false
            };
            FormSampleProps.ShowDialog();
            FormSampleProps.Dispose();
        }

        internal void RemoveFolderSelectedNode()
        {
            /*Check we are not trying to delete a root folder*/
            if (!(TreeView_File.SelectedNode == null || TreeView_File.SelectedNode.Tag.Equals("Root")))
            {
                /*Show warning*/
                EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox("Delete Folder: " + TreeView_File.SelectedNode.Text, "Warning", false);
                if (WarningDialog.ShowDialog() == DialogResult.OK)
                {
                    /*Remove child nodes sounds and samples*/
                    IList<TreeNode> ChildNodesCollection = new List<TreeNode>();
                    foreach (TreeNode ChildNode in TreeNodeFunctions.GetNodesInsideFolder(TreeView_File, TreeView_File.SelectedNode, ChildNodesCollection))
                    {
                        EXObjectsFunctions.RemoveSound(ChildNode.Name, SoundsList);
                    }
                    TreeNodeFunctions.TreeNodeDeleteNode(TreeView_File, TreeView_File.SelectedNode, TreeView_File.SelectedNode.Tag.ToString(), ProjectInfo);
                }
            }
        }
        internal void RemoveSampleSelectedNode()
        {
            /*Show warning*/
            EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox("Delete Sample: " + TreeView_File.SelectedNode.Text, "Warning", false);
            if (WarningDialog.ShowDialog() == DialogResult.OK)
            {
                EXSound ParentSound = EXObjectsFunctions.GetSoundByName(int.Parse(TreeView_File.SelectedNode.Parent.Name), SoundsList);
                EXObjectsFunctions.RemoveSampleFromSound(ParentSound, TreeView_File.SelectedNode.Name);
                TreeView_File.SelectedNode.Remove();
            }
        }

        internal void RemoveSoundSelectedNode()
        {
            /*Show warning*/
            EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox("Delete Sound: " + TreeView_File.SelectedNode.Text, "Warning", false);
            if (WarningDialog.ShowDialog() == DialogResult.OK)
            {
                EXObjectsFunctions.RemoveSound(TreeView_File.SelectedNode.Name, SoundsList);
                TreeNodeFunctions.TreeNodeDeleteNode(TreeView_File, TreeView_File.SelectedNode, TreeView_File.SelectedNode.Tag.ToString(), ProjectInfo);
            }
        }

        internal void RemoveAudioSelectedNode()
        {
            /*Show warning*/
            EuroSound_WarningBox WarningDialog = new EuroSound_WarningBox("Delete Audio: " + TreeView_File.SelectedNode.Text, "Warning", false);
            if (WarningDialog.ShowDialog() == DialogResult.OK)
            {
                EXObjectsFunctions.RemoveSound(TreeView_File.SelectedNode.Name, SoundsList);
                TreeNodeFunctions.TreeNodeDeleteNode(TreeView_File, TreeView_File.SelectedNode, TreeView_File.SelectedNode.Tag.ToString(), ProjectInfo);
            }
        }

        private void UpdateHashcodesValidList()
        {
            Thread UpdateList = new Thread(() =>
            {
                /*Clear List*/
                ListView_Hashcodes.Invoke((MethodInvoker)delegate
                {
                    ListView_Hashcodes.Items.Clear();
                });

                /*Level Hashcode*/
                ListViewItem LevelHashcode = new ListViewItem(new[] { "", ProjectInfo.Hashcode, "<Label Not Found>", "File Properties" });
                if (SB_Defines.ContainsKey(ProjectInfo.Hashcode))
                {
                    LevelHashcode.SubItems[0].BackColor = Color.Green;
                    LevelHashcode.SubItems[2].Text = SB_Defines[ProjectInfo.Hashcode];
                }
                else
                {
                    LevelHashcode.SubItems[0].BackColor = Color.Red;
                }
                LevelHashcode.UseItemStyleForSubItems = false;
                AddItemToListView(LevelHashcode, ListView_Hashcodes);

                /*Sounds Hashcodes*/
                foreach (KeyValuePair<int, EXSound> Sound in SoundsList)
                {
                    ListViewItem Hashcode = new ListViewItem(new[] { "", Sound.Value.Hashcode, "<Label Not Found>", Sound.Value.DisplayName });
                    if (SFX_Defines.ContainsKey(Sound.Value.Hashcode))
                    {
                        Hashcode.SubItems[0].BackColor = Color.Green;
                        Hashcode.SubItems[2].Text = SFX_Defines[Sound.Value.Hashcode];
                    }
                    else
                    {
                        Hashcode.SubItems[0].BackColor = Color.Red;
                        Hashcode.SubItems[2].Text = "<Hashcode Not Found>";
                    }
                    Hashcode.UseItemStyleForSubItems = false;
                    AddItemToListView(Hashcode, ListView_Hashcodes);

                    Thread.Sleep(2);
                }
            })
            {
                IsBackground = true
            };
            UpdateList.Start();

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
    }

}