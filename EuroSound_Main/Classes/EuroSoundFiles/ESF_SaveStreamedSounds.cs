using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public class ESF_SaveStreamedSounds
    {
        public void SaveStreamedSounds(BinaryWriter BWriter, TreeView TreeViewControl, Dictionary<uint, EXSoundStream> StreamSoundsList, ProjectFile FileProperties)
        {
            /*File Hashcode*/
            BWriter.Write(FileProperties.Hashcode);
            /*Stream Sound ID*/
            BWriter.Write(FileProperties.StreamedSoundID);
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
            //* NamesList
            //*===============================================================================================
            BWriter.Seek(1024, SeekOrigin.Current);
            long SoundsNamesListOffset = BWriter.BaseStream.Position;
            SaveSoundsLisNames(TreeViewControl, BWriter);

            //*===============================================================================================
            //* TreeView
            //*===============================================================================================
            long TreeViewDataOffset = BWriter.BaseStream.Position;
            SaveTreeViewData(TreeViewControl, BWriter);

            //*===============================================================================================
            //* Dictionary Info
            //*===============================================================================================
            long DictionaryDataOffset = BWriter.BaseStream.Position;
            SaveDictionaryData(StreamSoundsList, BWriter);
            long FileSize = BWriter.BaseStream.Position;

            //*===============================================================================================
            //* FINAL OFFSETS
            //*===============================================================================================
            /*Go to section offsets position*/
            BWriter.Seek(0x10, SeekOrigin.Begin);

            /*Write section offsets*/
            BWriter.Write(Convert.ToUInt32(SoundsNamesListOffset));
            BWriter.Write(Convert.ToUInt32(TreeViewDataOffset));
            BWriter.Write(Convert.ToUInt32(DictionaryDataOffset));

            /*Write File Size*/
            BWriter.Write(Convert.ToUInt32(FileSize));
        }

        private void SaveSoundsLisNames(TreeView TreeViewControl, BinaryWriter BWriter)
        {
            BWriter.Write((TreeViewControl.GetNodeCount(true) - 1));
            SaveOnlyNodeName(TreeViewControl.Nodes[0], BWriter);
        }

        private void SaveOnlyNodeName(TreeNode SelectedNode, BinaryWriter BWriter)
        {
            if (!SelectedNode.Tag.Equals("Root"))
            {
                if (SelectedNode.Parent == null)
                {
                    BWriter.Write("Root");
                }
                else
                {
                    BWriter.Write(SelectedNode.Parent.Name);
                }
                BWriter.Write(SelectedNode.Name);
            }
            foreach (TreeNode Node in SelectedNode.Nodes)
            {
                SaveOnlyNodeName(Node, BWriter);
            }
        }

        private void SaveTreeViewData(TreeView TreeViewControl, BinaryWriter BWriter)
        {
            BWriter.Write((TreeViewControl.GetNodeCount(true) - 1));
            SaveTreeNodes(TreeViewControl.Nodes[0], BWriter);
        }

        private void SaveTreeNodes(TreeNode Selected, BinaryWriter BWriter)
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
                SaveTreeNodes(Node, BWriter);
            }
        }

        private void SaveDictionaryData(Dictionary<uint, EXSoundStream> StreamSoundsList, BinaryWriter BWriter)
        {
            BWriter.Write(StreamSoundsList.Count);
            foreach (KeyValuePair<uint, EXSoundStream> Sound in StreamSoundsList)
            {
                BWriter.Write(Sound.Key);
                BWriter.Write(Sound.Value.DisplayName);
                BWriter.Write(Sound.Value.BaseVolume);
                BWriter.Write(Sound.Value.Hashcode);
                BWriter.Write(Sound.Value.IMA_ADPCM_DATA.Length);
                BWriter.Write(Sound.Value.IMA_ADPCM_DATA);

                /*Markers List*/
                BWriter.Write(Sound.Value.Markers.Count);
                foreach (var Marker in Sound.Value.Markers)
                {
                    BWriter.Write(Marker.Position);
                    BWriter.Write(Marker.IsInstant);
                    BWriter.Write(Marker.InstantBuffer);
                    BWriter.Write(Marker.State.Length);
                    BWriter.Write(Marker.State);

                    //Markers Data List
                    BWriter.Write(Marker.MarkersData.Count);
                    foreach (var MarkerData in Marker.MarkersData)
                    {
                        BWriter.Write(MarkerData.Name);
                        BWriter.Write(MarkerData.Position);
                        BWriter.Write(MarkerData.MusicMakerType);
                        BWriter.Write(MarkerData.Flags);
                        BWriter.Write(MarkerData.Extra);
                        BWriter.Write(MarkerData.LoopStart);
                        BWriter.Write(MarkerData.MarkerCount);
                        BWriter.Write(MarkerData.LoopMarkerCount);
                    }
                }

                //Extra info
                BWriter.Write(Sound.Value.IMA_Data_MD5);
                BWriter.Write(Sound.Value.IMA_Data_Name);
                BWriter.Write(Sound.Value.OutputThisSound);

                //IDS
                BWriter.Write(Sound.Value.IDMarkerName);
                BWriter.Write(Sound.Value.IDMarkerPos);
            }
        }
    }
}
