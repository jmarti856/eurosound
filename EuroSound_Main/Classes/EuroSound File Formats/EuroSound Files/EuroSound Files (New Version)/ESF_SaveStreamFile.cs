using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.StreamSounds;
using Syroot.BinaryData;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundFilesFunctions.NewVersion.StreamFile
{
    internal class ESF_SaveStreamFile
    {
        public void SaveStreamedSounds(BinaryStream BWriter, TreeView TreeViewControl, Dictionary<uint, EXSoundStream> StreamSoundsList, Dictionary<uint, EXAppTarget> OutputTargets, ProjectFile FileProperties)
        {
            int SpaceBetweenBlocks = 512;
            EuroSoundFiles_CommonFunctions ESF_CommonFunctions = new EuroSoundFiles_CommonFunctions();

            //File Size
            BWriter.WriteUInt32(0);
            //Dictionary Data Offset
            BWriter.WriteUInt32(0);
            //TreeViewData Offset
            BWriter.WriteUInt32(0);
            //Target App 
            BWriter.WriteUInt32(0);
            //Sounds List Offset
            BWriter.WriteUInt32(0);
            //File Section 5
            BWriter.WriteUInt32(0);
            //File Section 6
            BWriter.WriteUInt32(0);
            //Project Name
            BWriter.WriteString(FileProperties.FileName);
            //Project Description
            BWriter.WriteString(FileProperties.ProjectDescription);
            //Profile Name
            BWriter.WriteString(GlobalPreferences.SelectedProfileName);
            //Profile Path
            BWriter.WriteString(GlobalPreferences.SelectedProfile);

            //Space between sections
            GenericFunctions.CustomSeek(2048, BWriter, (byte)'«');

            //*===============================================================================================
            //* Dictionary Data
            //*===============================================================================================
            //Align Bytes
            BWriter.Align(16);

            //Write Data
            long dictionaryDataOffset = BWriter.BaseStream.Position;
            SaveDictionaryData(StreamSoundsList, BWriter);

            //Align Bytes
            BWriter.Align(16, true);

            //Space between sections
            GenericFunctions.CustomSeek(SpaceBetweenBlocks, BWriter, (byte)'«');

            //*===============================================================================================
            //* TreeView
            //*===============================================================================================
            //Align Bytes
            BWriter.Align(16, true);

            //Write Data
            long TreeViewDataOffset = BWriter.BaseStream.Position;
            ESF_CommonFunctions.SaveTreeViewData(TreeViewControl, BWriter, 2);

            //Align Bytes
            BWriter.Align(16, true);

            //Space between sections
            GenericFunctions.CustomSeek(SpaceBetweenBlocks, BWriter, (byte)'«');

            //*===============================================================================================
            //* APP Target
            //*===============================================================================================
            //Align Bytes
            BWriter.Align(16, true);

            //Write Data
            long AppTargetDataOffset = BWriter.BaseStream.Position;
            ESF_CommonFunctions.SaveAppTargetData(OutputTargets, BWriter);

            //Align Bytes
            BWriter.Align(16, true);

            //Space between sections
            GenericFunctions.CustomSeek(SpaceBetweenBlocks, BWriter, (byte)'«');

            //*===============================================================================================
            //* NamesList
            //*===============================================================================================
            //Align Bytes
            BWriter.Align(16, true);

            //Write Data
            long StreamSoundsListOffset = BWriter.BaseStream.Position;
            SaveSoundsLisNames(TreeViewControl, BWriter);
            BWriter.Align(16, true);

            long FileSize = BWriter.BaseStream.Position;

            //*===============================================================================================
            //* FINAL OFFSETS
            //*===============================================================================================
            //Go to section offsets position
            BWriter.Seek(0x10, SeekOrigin.Begin);

            BWriter.WriteUInt32((uint)FileSize);
            BWriter.WriteUInt32((uint)dictionaryDataOffset);
            BWriter.WriteUInt32((uint)TreeViewDataOffset);
            BWriter.WriteUInt32((uint)AppTargetDataOffset);
            BWriter.WriteUInt32((uint)StreamSoundsListOffset);
        }

        private void SaveDictionaryData(Dictionary<uint, EXSoundStream> StreamSoundsList, BinaryStream BWriter)
        {
            BWriter.WriteUInt32((uint)StreamSoundsList.Count);
            foreach (KeyValuePair<uint, EXSoundStream> Sound in StreamSoundsList)
            {
                BWriter.WriteUInt32(Sound.Key);
                BWriter.WriteUInt32(Sound.Value.BaseVolume);

                //Save WAV
                BWriter.WriteUInt32((uint)Sound.Value.PCM_Data.Length);
                BWriter.WriteBytes(Sound.Value.PCM_Data);
                BWriter.WriteUInt32((uint)Sound.Value.IMA_ADPCM_DATA.Length);
                BWriter.WriteBytes(Sound.Value.IMA_ADPCM_DATA);
                BWriter.WriteUInt32(Sound.Value.Frequency);
                BWriter.WriteUInt32(Sound.Value.Bits);
                BWriter.WriteUInt32(Sound.Value.Duration);
                BWriter.WriteByte(Sound.Value.Channels);
                BWriter.WriteString(Sound.Value.Encoding);
                BWriter.WriteString(Sound.Value.WAVFileMD5);
                BWriter.WriteString(Sound.Value.WAVFileName);

                //Start Markers List
                uint NumberOfStartMarkers = (uint)Sound.Value.StartMarkers.Count;
                BWriter.WriteUInt32(NumberOfStartMarkers);
                for (int i = 0; i < NumberOfStartMarkers; i++)
                {
                    BWriter.WriteUInt32(Sound.Value.StartMarkers[i].Name);
                    BWriter.WriteUInt32(Sound.Value.StartMarkers[i].Position);
                    BWriter.WriteUInt32(Sound.Value.StartMarkers[i].MusicMakerType);
                    BWriter.WriteUInt32(Sound.Value.StartMarkers[i].Flags);
                    BWriter.WriteUInt32(Sound.Value.StartMarkers[i].Extra);
                    BWriter.WriteUInt32(Sound.Value.StartMarkers[i].LoopStart);
                    BWriter.WriteUInt32(Sound.Value.StartMarkers[i].MarkerCount);
                    BWriter.WriteUInt32(Sound.Value.StartMarkers[i].LoopMarkerCount);
                    BWriter.WriteUInt32(Sound.Value.StartMarkers[i].MarkerPos);
                    BWriter.WriteUInt32(Sound.Value.StartMarkers[i].IsInstant);
                    BWriter.WriteUInt32(Sound.Value.StartMarkers[i].InstantBuffer);
                    BWriter.WriteUInt32(Sound.Value.StartMarkers[i].StateA);
                    BWriter.WriteUInt32(Sound.Value.StartMarkers[i].StateB);
                }

                //Markers List
                uint NumberOfMarkers = (uint)Sound.Value.Markers.Count;
                BWriter.WriteUInt32(NumberOfMarkers);
                for (int j = 0; j < NumberOfMarkers; j++)
                {
                    BWriter.WriteInt32(Sound.Value.Markers[j].Name);
                    BWriter.WriteUInt32(Sound.Value.Markers[j].Position);
                    BWriter.WriteUInt32(Sound.Value.Markers[j].MusicMakerType);
                    BWriter.WriteUInt32(Sound.Value.Markers[j].Flags);
                    BWriter.WriteUInt32(Sound.Value.Markers[j].Extra);
                    BWriter.WriteUInt32(Sound.Value.Markers[j].LoopStart);
                    BWriter.WriteUInt32(Sound.Value.Markers[j].MarkerCount);
                    BWriter.WriteUInt32(Sound.Value.Markers[j].LoopMarkerCount);
                }

                //Extra info
                BWriter.WriteBoolean(Sound.Value.OutputThisSound);
            }
        }

        private void SaveSoundsLisNames(TreeView TreeViewControl, BinaryStream BWriter)
        {
            BWriter.WriteUInt32((uint)TreeViewControl.Nodes[0].Nodes.Count);
            SaveOnlyNodeName(TreeViewControl.Nodes[0], BWriter);
        }

        private void SaveOnlyNodeName(TreeNode SelectedNode, BinaryStream BWriter)
        {
            if (SelectedNode.Level > 0)
            {
                BWriter.WriteString(SelectedNode.Text);
            }
            foreach (TreeNode Node in SelectedNode.Nodes)
            {
                SaveOnlyNodeName(Node, BWriter);
            }
        }
    }
}
