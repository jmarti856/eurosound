using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.TreeViewLibraryFunctions;
using Syroot.BinaryData;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundFilesFunctions.NewVersion
{
    internal class EuroSoundFiles_CommonFunctions
    {
        //*===============================================================================================
        //* Read Functions
        //*===============================================================================================
        internal void ReadTreeViewData(BinaryReader BReader, TreeView TreeViewControl)
        {
            uint NumberOfNodes = BReader.ReadUInt32();

            for (int i = 0; i < NumberOfNodes; i++)
            {
                string parentNode = BReader.ReadString();
                string nodeName = BReader.ReadString();
                string displayName = BReader.ReadString();
                string nodeTag = BReader.ReadString();
                int selectedImageIndex = BReader.ReadInt32();
                int imageIndex = BReader.ReadInt32();
                Color nodeColor = Color.FromArgb(BReader.ReadInt32());
                bool parentIsExpanded = BReader.ReadBoolean();
                bool isExpanded = BReader.ReadBoolean();
                bool nodeIsSelected = BReader.ReadBoolean();

                //Ignore state
                if (GlobalPreferences.TV_IgnoreStlyesFromESF)
                {
                    parentIsExpanded = false;
                    isExpanded = false;
                    nodeIsSelected = false;
                }

                //Whenever possible use system colors
                if (nodeColor.GetBrightness() < 0.1 || GlobalPreferences.TV_IgnoreStlyesFromESF)
                {
                    nodeColor = SystemColors.WindowText;
                }

                TreeNodeFunctions.TreeNodeAddNewNode(parentNode, nodeName, displayName, selectedImageIndex, imageIndex, nodeTag, parentIsExpanded, isExpanded, nodeIsSelected, nodeColor, TreeViewControl);
            }
        }

        internal void ReadAppTargetData(BinaryReader BReader, Dictionary<uint, EXAppTarget> outputTargets)
        {
            uint ouputTargetsCount = BReader.ReadUInt32();
            for (int i = 0; i < ouputTargetsCount; i++)
            {
                uint newTargetKey = BReader.ReadUInt32();
                EXAppTarget newTarget = new EXAppTarget()
                {
                    Project = BReader.ReadUInt32(),
                    Name = BReader.ReadString(),
                    OutputDirectory = BReader.ReadString(),
                    BinaryName = BReader.ReadString(),
                    UpdateFileList = BReader.ReadBoolean()
                };
                outputTargets.Add(newTargetKey, newTarget);
            }
        }

        //*===============================================================================================
        //* Write Functions
        //*===============================================================================================
        internal void SaveTreeViewData(TreeView treeViewControl, BinaryStream binaryWriter, uint rootNodesCount)
        {
            binaryWriter.WriteUInt32((uint)treeViewControl.GetNodeCount(true) - rootNodesCount);
            for (int i = 0; i < rootNodesCount; i++)
            {
                SaveTreeNodes(treeViewControl.Nodes[i], binaryWriter);
            }
        }

        private void SaveTreeNodes(TreeNode treeNodeToExport, BinaryStream BWriter)
        {
            if (!treeNodeToExport.Tag.Equals("Root"))
            {
                BWriter.WriteString(treeNodeToExport.Parent.Name);
                BWriter.WriteString(treeNodeToExport.Name);
                BWriter.WriteString(treeNodeToExport.Text);
                BWriter.WriteString(treeNodeToExport.Tag.ToString());
                BWriter.WriteInt32(treeNodeToExport.SelectedImageIndex);
                BWriter.WriteInt32(treeNodeToExport.ImageIndex);
                BWriter.WriteInt32(treeNodeToExport.ForeColor.ToArgb());
                BWriter.WriteBoolean(treeNodeToExport.Parent.IsExpanded);
                BWriter.WriteBoolean(treeNodeToExport.IsExpanded);
                BWriter.WriteBoolean(treeNodeToExport.IsSelected);
            }
            foreach (TreeNode Node in treeNodeToExport.Nodes)
            {
                SaveTreeNodes(Node, BWriter);
            }
        }

        internal void SaveAppTargetData(Dictionary<uint, EXAppTarget> OutputTargets, BinaryStream binaryWriter)
        {
            binaryWriter.WriteUInt32((uint)OutputTargets.Count);
            foreach (KeyValuePair<uint, EXAppTarget> target in OutputTargets)
            {
                //Key
                binaryWriter.WriteUInt32(target.Key);
                binaryWriter.WriteUInt32(target.Value.Project);
                binaryWriter.WriteString(target.Value.Name);
                binaryWriter.WriteString(target.Value.OutputDirectory);
                binaryWriter.WriteString(target.Value.BinaryName);
                binaryWriter.WriteBoolean(target.Value.UpdateFileList);
            }
        }
    }
}
