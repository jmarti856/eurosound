using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application
{
    internal class ESF_SaveStreamedSounds
    {
        internal void SaveStreamedSounds(BinaryWriter BWriter, TreeView TreeViewControl, Dictionary<uint, EXSoundStream> StreamSoundsList, ProjectFile FileProperties)
        {
            /*File Hashcode*/
            BWriter.Write(Convert.ToUInt32(FileProperties.Hashcode));
            /*Sounds List Offset*/
            BWriter.Write(Convert.ToUInt32(00000000));
            /*TreeViewData Offset*/
            BWriter.Write(Convert.ToUInt32(00000000));
            /*Dictionary Data Offset*/
            BWriter.Write(Convert.ToUInt32(00000000));
            /*FileSize*/
            BWriter.Write(Convert.ToUInt32(00000000));
            /*File Name*/
            BWriter.Write(FileProperties.FileName);

            //*===============================================================================================
            //* TreeView
            //*===============================================================================================
            BWriter.Seek(1024, SeekOrigin.Current);
            long TreeViewDataOffset = BWriter.BaseStream.Position;
            SaveTreeViewData(TreeViewControl, BWriter);
        }

        private void SaveTreeViewData(TreeView TreeViewControl, BinaryWriter BWriter)
        {
            BWriter.Write((TreeViewControl.GetNodeCount(true) - 1));
            SaveTreeNodes(TreeViewControl, TreeViewControl.Nodes[0], BWriter);
        }

        private void SaveTreeNodes(TreeView TreeViewControl, TreeNode Selected, BinaryWriter BWriter)
        {
            if (!Selected.Tag.Equals("Root"))
            {
                if (Selected.Parent == null)
                {
                    BWriter.Write("Root");
                }
                else
                {
                    BWriter.Write(Selected.Parent.Name);
                }
                BWriter.Write(Selected.Name);
                BWriter.Write(Selected.Text);
                BWriter.Write(Selected.Index);
                BWriter.Write(Selected.ImageKey);
                BWriter.Write(Selected.SelectedImageIndex);
                BWriter.Write(Selected.ImageIndex);
                BWriter.Write(Selected.Tag.ToString());
                BWriter.Write(Selected.ForeColor.ToArgb());
                BWriter.Write(Selected.IsVisible);
            }
            foreach (TreeNode Node in Selected.Nodes)
            {
                SaveTreeNodes(TreeViewControl, Node, BWriter);
            }
        }
    }
}
