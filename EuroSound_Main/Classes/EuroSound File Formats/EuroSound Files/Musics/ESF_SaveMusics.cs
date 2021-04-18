using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.Musics;
using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundMusicFilesFunctions
{
    public class ESF_SaveMusics
    {
        public void SaveMusics(BinaryStream BWriter, TreeView TreeViewControl, Dictionary<uint, EXMusic> MusicsList, ProjectFile FileProperties)
        {
            long AlignOffset;

            //File Hashcode
            BWriter.Write(FileProperties.Hashcode);
            //Stream Sound ID
            BWriter.Write(FileProperties.SoundID);
            //Sounds List Offset
            BWriter.Write(Convert.ToUInt32(00000000));
            //TreeViewData Offset
            BWriter.Write(Convert.ToUInt32(00000000));
            //Dictionary Data Offset
            BWriter.Write(Convert.ToUInt32(00000000));
            //FileSize
            BWriter.Write(Convert.ToUInt32(00000000));
            //File Name
            BWriter.Write(FileProperties.FileName);
            //Associated Profile
            BWriter.Write(GlobalPreferences.SelectedProfile);
            //Associated Profile Name
            BWriter.Write(GlobalPreferences.SelectedProfileName);

            BWriter.Seek(2048, SeekOrigin.Current);

            //*===============================================================================================
            //* NamesList
            //*===============================================================================================
            //Align Bytes
            AlignOffset = (BWriter.BaseStream.Position + 2048) & (2048 - 1);
            BWriter.Seek(AlignOffset, SeekOrigin.Current);
            BWriter.Align(16);

            //Write Data
            long SoundsNamesListOffset = BWriter.BaseStream.Position;
            SaveSoundsLisNames(TreeViewControl, BWriter);

            //*===============================================================================================
            //* TreeView
            //*===============================================================================================
            //Align Bytes
            AlignOffset = (BWriter.BaseStream.Position + 2048) & (2048 - 1);
            BWriter.Seek(AlignOffset, SeekOrigin.Current);
            BWriter.Align(16);

            //Write Data
            long TreeViewDataOffset = BWriter.BaseStream.Position;
            SaveTreeViewData(TreeViewControl, BWriter);

            //*===============================================================================================
            //* Dictionary Info
            //*===============================================================================================
            //Align Bytes
            AlignOffset = (BWriter.BaseStream.Position + 2048) & (2048 - 1);
            BWriter.Seek(AlignOffset, SeekOrigin.Current);
            BWriter.Align(16);

            //Write Data
            long DictionaryDataOffset = BWriter.BaseStream.Position;
            SaveDictionaryData(MusicsList, BWriter);
            long FileSize = BWriter.BaseStream.Position;

            //*===============================================================================================
            //* FINAL OFFSETS
            //*===============================================================================================
            //Go to section offsets position
            BWriter.Seek(0x10, SeekOrigin.Begin);

            //Write section offsets
            BWriter.Write(Convert.ToUInt32(SoundsNamesListOffset));
            BWriter.Write(Convert.ToUInt32(TreeViewDataOffset));
            BWriter.Write(Convert.ToUInt32(DictionaryDataOffset));

            //Write File Size
            BWriter.Write(Convert.ToUInt32(FileSize));
        }

        private void SaveSoundsLisNames(TreeView TreeViewControl, BinaryStream BWriter)
        {
            BWriter.Write((TreeViewControl.GetNodeCount(true) - 1));
            SaveOnlyNodeName(TreeViewControl.Nodes[0], BWriter);
        }

        private void SaveOnlyNodeName(TreeNode SelectedNode, BinaryStream BWriter)
        {
            if (!SelectedNode.Tag.Equals("Root"))
            {
                BWriter.Write(SelectedNode.Text);
            }
            foreach (TreeNode Node in SelectedNode.Nodes)
            {
                SaveOnlyNodeName(Node, BWriter);
            }
        }

        private void SaveTreeViewData(TreeView TreeViewControl, BinaryStream BWriter)
        {
            BWriter.Write((TreeViewControl.GetNodeCount(true) - 1));
            SaveTreeNodes(TreeViewControl.Nodes[0], BWriter);
        }

        private void SaveTreeNodes(TreeNode Selected, BinaryStream BWriter)
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

                //Added in the version 1.0.0.8
                BWriter.Write(Selected.Parent.IsExpanded);
                BWriter.Write(Selected.IsExpanded);
                BWriter.Write(Selected.IsSelected);
            }
            foreach (TreeNode Node in Selected.Nodes)
            {
                SaveTreeNodes(Node, BWriter);
            }
        }

        private void SaveDictionaryData(Dictionary<uint, EXMusic> MusicsList, BinaryStream BWriter)
        {
            uint NumberOfStartMarkers, NumberOfMarkers;

            BWriter.Write(MusicsList.Count);
            foreach (KeyValuePair<uint, EXMusic> Music in MusicsList)
            {
                BWriter.Write(Music.Key);
                BWriter.Write(Music.Value.BaseVolume);

                //Save Data Left Channel
                BWriter.Write(Music.Value.Frequency_LeftChannel);
                BWriter.Write(Music.Value.Channels_LeftChannel);
                BWriter.Write(Music.Value.Bits_LeftChannel);
                BWriter.Write(Music.Value.Duration_LeftChannel);
                BWriter.Write(Music.Value.RealSize_LeftChannel);
                BWriter.Write(Music.Value.Encoding_LeftChannel);
                BWriter.Write(Music.Value.WAVFileMD5_LeftChannel);
                BWriter.Write(Music.Value.WAVFileName_LeftChannel);
                BWriter.Write(Music.Value.PCM_Data_LeftChannel.Length);
                BWriter.Write(Music.Value.PCM_Data_LeftChannel);
                BWriter.Write(Music.Value.IMA_ADPCM_DATA_LeftChannel.Length);
                BWriter.Write(Music.Value.IMA_ADPCM_DATA_LeftChannel);

                //Save Data Right Channel
                BWriter.Write(Music.Value.Frequency_RightChannel);
                BWriter.Write(Music.Value.Channels_RightChannel);
                BWriter.Write(Music.Value.Bits_RightChannel);
                BWriter.Write(Music.Value.Duration_RightChannel);
                BWriter.Write(Music.Value.RealSize_RightChannel);
                BWriter.Write(Music.Value.Encoding_RightChannel);
                BWriter.Write(Music.Value.WAVFileMD5_RightChannel);
                BWriter.Write(Music.Value.WAVFileName_RightChannel);
                BWriter.Write(Music.Value.PCM_Data_RightChannel.Length);
                BWriter.Write(Music.Value.PCM_Data_RightChannel);
                BWriter.Write(Music.Value.IMA_ADPCM_DATA_RightChannel.Length);
                BWriter.Write(Music.Value.IMA_ADPCM_DATA_RightChannel);

                //Start Markers List
                NumberOfStartMarkers = (uint)Music.Value.StartMarkers.Count;
                BWriter.Write(NumberOfStartMarkers);
                for (int i = 0; i < NumberOfStartMarkers; i++)
                {
                    BWriter.Write(Music.Value.StartMarkers[i].Name);
                    BWriter.Write(Music.Value.StartMarkers[i].Position);
                    BWriter.Write(Music.Value.StartMarkers[i].MusicMakerType);
                    BWriter.Write(Music.Value.StartMarkers[i].Flags);
                    BWriter.Write(Music.Value.StartMarkers[i].Extra);
                    BWriter.Write(Music.Value.StartMarkers[i].LoopStart);
                    BWriter.Write(Music.Value.StartMarkers[i].MarkerCount);
                    BWriter.Write(Music.Value.StartMarkers[i].LoopMarkerCount);
                    BWriter.Write(Music.Value.StartMarkers[i].MarkerPos);
                    BWriter.Write(Music.Value.StartMarkers[i].IsInstant);
                    BWriter.Write(Music.Value.StartMarkers[i].InstantBuffer);
                    BWriter.Write(Music.Value.StartMarkers[i].StateA);
                    BWriter.Write(Music.Value.StartMarkers[i].StateB);
                }

                //Markers List
                NumberOfMarkers = (uint)Music.Value.Markers.Count;
                BWriter.Write(NumberOfMarkers);
                for (int j = 0; j < NumberOfMarkers; j++)
                {
                    BWriter.Write(Music.Value.Markers[j].Name);
                    BWriter.Write(Music.Value.Markers[j].Position);
                    BWriter.Write(Music.Value.Markers[j].MusicMakerType);
                    BWriter.Write(Music.Value.Markers[j].Flags);
                    BWriter.Write(Music.Value.Markers[j].Extra);
                    BWriter.Write(Music.Value.Markers[j].LoopStart);
                    BWriter.Write(Music.Value.Markers[j].MarkerCount);
                    BWriter.Write(Music.Value.Markers[j].LoopMarkerCount);
                }

                //Extra info
                BWriter.Write(Music.Value.OutputThisSound);
            }
        }
    }
}
