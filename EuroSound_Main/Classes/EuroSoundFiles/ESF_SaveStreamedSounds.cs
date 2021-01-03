using EuroSound_Application.StreamSounds;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundFilesFunctions
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
            uint NumberOfStartMarkers, NumberOfMarkers;

            BWriter.Write(StreamSoundsList.Count);
            foreach (KeyValuePair<uint, EXSoundStream> Sound in StreamSoundsList)
            {
                BWriter.Write(Sound.Key);
                BWriter.Write(Sound.Value.DisplayName);
                BWriter.Write(Sound.Value.BaseVolume);
                BWriter.Write(Sound.Value.IMA_ADPCM_DATA.Length);
                BWriter.Write(Sound.Value.IMA_ADPCM_DATA);

                /*Start Markers List*/
                NumberOfStartMarkers = (uint)Sound.Value.StartMarkers.Count;
                BWriter.Write(NumberOfStartMarkers);
                for (int i = 0; i < NumberOfStartMarkers; i++)
                {
                    BWriter.Write(Sound.Value.StartMarkers[i].Name);
                    BWriter.Write(Sound.Value.StartMarkers[i].Position);
                    BWriter.Write(Sound.Value.StartMarkers[i].MusicMakerType);
                    BWriter.Write(Sound.Value.StartMarkers[i].Flags);
                    BWriter.Write(Sound.Value.StartMarkers[i].Extra);
                    BWriter.Write(Sound.Value.StartMarkers[i].LoopStart);
                    BWriter.Write(Sound.Value.StartMarkers[i].MarkerCount);
                    BWriter.Write(Sound.Value.StartMarkers[i].LoopMarkerCount);
                    BWriter.Write(Sound.Value.StartMarkers[i].MarkerPos);
                    BWriter.Write(Sound.Value.StartMarkers[i].IsInstant);
                    BWriter.Write(Sound.Value.StartMarkers[i].InstantBuffer);
                    BWriter.Write(Sound.Value.StartMarkers[i].StateA);
                    BWriter.Write(Sound.Value.StartMarkers[i].StateB);
                }

                /*Markers List*/
                NumberOfMarkers = (uint)Sound.Value.Markers.Count;
                BWriter.Write(NumberOfMarkers);
                for (int j = 0; j < NumberOfMarkers; j++)
                {
                    BWriter.Write(Sound.Value.Markers[j].Name);
                    BWriter.Write(Sound.Value.Markers[j].Position);
                    BWriter.Write(Sound.Value.Markers[j].MusicMakerType);
                    BWriter.Write(Sound.Value.Markers[j].Flags);
                    BWriter.Write(Sound.Value.Markers[j].Extra);
                    BWriter.Write(Sound.Value.Markers[j].LoopStart);
                    BWriter.Write(Sound.Value.Markers[j].MarkerCount);
                    BWriter.Write(Sound.Value.Markers[j].LoopMarkerCount);
                }

                //Extra info
                BWriter.Write(Sound.Value.IMA_Data_MD5);
                BWriter.Write(Sound.Value.IMA_Data_Name);
                BWriter.Write(Sound.Value.OutputThisSound);

                //IDS
                BWriter.Write(Sound.Value.MarkerDataCounterID);
                BWriter.Write(Sound.Value.MarkerID);
            }
        }
    }
}
