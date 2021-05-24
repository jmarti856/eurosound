using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.Musics;
using Syroot.BinaryData;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundFilesFunctions.NewVersion.Musicbanks
{
    internal class ESF_SaveMusicBanks
    {
        public void SaveMusics(BinaryStream BWriter, TreeView TreeViewControl, Dictionary<uint, EXMusic> MusicsList, Dictionary<uint, EXAppTarget> OutputTargets, ProjectFile FileProperties)
        {
            EuroSoundFiles_CommonFunctions ESF_CommonFunctions = new EuroSoundFiles_CommonFunctions();

            //File Size
            BWriter.WriteUInt32(0);
            //Dictionary Data Offset
            BWriter.WriteUInt32(0);
            //TreeViewData Offset
            BWriter.WriteUInt32(0);
            //Target App
            BWriter.WriteUInt32(0);
            //File Section 4
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
            BWriter.Write(GlobalPreferences.SelectedProfileName);

            BWriter.Seek(2048, SeekOrigin.Current);

            //*===============================================================================================
            //* Dictionary Info
            //*===============================================================================================
            //Align Bytes
            BWriter.Align(16);

            //Write Data
            long DictionaryDataOffset = BWriter.BaseStream.Position;
            SaveDictionaryData(MusicsList, BWriter);

            //*===============================================================================================
            //* TreeView
            //*===============================================================================================
            //Align Bytes
            BWriter.Align(16, true);

            //Write Data
            long TreeViewDataOffset = BWriter.BaseStream.Position;
            ESF_CommonFunctions.SaveTreeViewData(TreeViewControl, BWriter, 2);

            //*===============================================================================================
            //* APP Target
            //*===============================================================================================
            //Align Bytes
            BWriter.Align(16, true);

            //Write Data
            long AppTargetDataOffset = BWriter.BaseStream.Position;
            ESF_CommonFunctions.SaveAppTargetData(OutputTargets, BWriter);
            BWriter.Align(16, true);

            long FileSize = BWriter.BaseStream.Position;

            //*===============================================================================================
            //* FINAL OFFSETS
            //*===============================================================================================
            //Go to section offsets position
            BWriter.Seek(0x10, SeekOrigin.Begin);

            BWriter.WriteUInt32((uint)FileSize);
            BWriter.WriteUInt32((uint)DictionaryDataOffset);
            BWriter.WriteUInt32((uint)TreeViewDataOffset);
            BWriter.WriteUInt32((uint)AppTargetDataOffset);
        }


        private void SaveDictionaryData(Dictionary<uint, EXMusic> MusicsList, BinaryStream BWriter)
        {
            BWriter.WriteUInt32((uint)MusicsList.Count);
            foreach (KeyValuePair<uint, EXMusic> Music in MusicsList)
            {
                BWriter.WriteUInt32(Music.Key);
                BWriter.WriteUInt32(Music.Value.BaseVolume);

                //Save Data Left Channel
                BWriter.WriteByte(Music.Value.Channels_LeftChannel);
                BWriter.WriteUInt32(Music.Value.Frequency_LeftChannel);
                BWriter.WriteUInt32(Music.Value.Bits_LeftChannel);
                BWriter.WriteUInt32(Music.Value.Duration_LeftChannel);
                BWriter.WriteUInt32(Music.Value.RealSize_LeftChannel);
                BWriter.WriteString(Music.Value.Encoding_LeftChannel);
                BWriter.WriteString(Music.Value.WAVFileMD5_LeftChannel);
                BWriter.WriteString(Music.Value.WAVFileName_LeftChannel);
                BWriter.WriteUInt32((uint)Music.Value.PCM_Data_LeftChannel.Length);
                BWriter.WriteBytes(Music.Value.PCM_Data_LeftChannel);
                BWriter.WriteUInt32((uint)Music.Value.IMA_ADPCM_DATA_LeftChannel.Length);
                BWriter.WriteBytes(Music.Value.IMA_ADPCM_DATA_LeftChannel);

                //Save Data Right Channel
                BWriter.WriteByte(Music.Value.Channels_RightChannel);
                BWriter.WriteUInt32(Music.Value.Frequency_RightChannel);
                BWriter.WriteUInt32(Music.Value.Bits_RightChannel);
                BWriter.WriteUInt32(Music.Value.Duration_RightChannel);
                BWriter.WriteUInt32(Music.Value.RealSize_RightChannel);
                BWriter.WriteString(Music.Value.Encoding_RightChannel);
                BWriter.WriteString(Music.Value.WAVFileMD5_RightChannel);
                BWriter.WriteString(Music.Value.WAVFileName_RightChannel);
                BWriter.WriteUInt32((uint)Music.Value.PCM_Data_RightChannel.Length);
                BWriter.WriteBytes(Music.Value.PCM_Data_RightChannel);
                BWriter.WriteUInt32((uint)Music.Value.IMA_ADPCM_DATA_RightChannel.Length);
                BWriter.WriteBytes(Music.Value.IMA_ADPCM_DATA_RightChannel);

                //Start Markers List
                uint NumberOfStartMarkers = (uint)Music.Value.StartMarkers.Count;
                BWriter.WriteUInt32(NumberOfStartMarkers);
                for (int i = 0; i < NumberOfStartMarkers; i++)
                {
                    BWriter.WriteUInt32(Music.Value.StartMarkers[i].Name);
                    BWriter.WriteUInt32(Music.Value.StartMarkers[i].Position);
                    BWriter.WriteUInt32(Music.Value.StartMarkers[i].MusicMakerType);
                    BWriter.WriteUInt32(Music.Value.StartMarkers[i].Flags);
                    BWriter.WriteUInt32(Music.Value.StartMarkers[i].Extra);
                    BWriter.WriteUInt32(Music.Value.StartMarkers[i].LoopStart);
                    BWriter.WriteUInt32(Music.Value.StartMarkers[i].MarkerCount);
                    BWriter.WriteUInt32(Music.Value.StartMarkers[i].LoopMarkerCount);
                    BWriter.WriteUInt32(Music.Value.StartMarkers[i].MarkerPos);
                    BWriter.WriteUInt32(Music.Value.StartMarkers[i].IsInstant);
                    BWriter.WriteUInt32(Music.Value.StartMarkers[i].InstantBuffer);
                    BWriter.WriteUInt32(Music.Value.StartMarkers[i].StateA);
                    BWriter.WriteUInt32(Music.Value.StartMarkers[i].StateB);
                }

                //Markers List
                uint NumberOfMarkers = (uint)Music.Value.Markers.Count;
                BWriter.Write(NumberOfMarkers);
                for (int j = 0; j < NumberOfMarkers; j++)
                {
                    BWriter.WriteInt32(Music.Value.Markers[j].Name);
                    BWriter.WriteUInt32(Music.Value.Markers[j].Position);
                    BWriter.WriteUInt32(Music.Value.Markers[j].MusicMakerType);
                    BWriter.WriteUInt32(Music.Value.Markers[j].Flags);
                    BWriter.WriteUInt32(Music.Value.Markers[j].Extra);
                    BWriter.WriteUInt32(Music.Value.Markers[j].LoopStart);
                    BWriter.WriteUInt32(Music.Value.Markers[j].MarkerCount);
                    BWriter.WriteUInt32(Music.Value.Markers[j].LoopMarkerCount);
                }

                //Extra info
                BWriter.WriteBoolean(Music.Value.OutputThisSound);
            }
        }
    }
}
