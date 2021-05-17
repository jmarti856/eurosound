using EngineXImaAdpcm;
using EuroSound_Application.StreamSounds;
using System.Collections.Generic;

namespace EuroSound_Application.MarkerFiles.StreamSoundsEditor.Classes
{
    internal class MarkersFunctions
    {
        internal EXStreamStartMarker CreateStartMarker(List<EXStreamStartMarker> MarkersList, uint v_pos, uint v_type, uint v_flags, uint v_loopstart, uint v_loopmarkercount, uint v_mkrpos, uint v_stateA, uint v_stateB)
        {
            //Create Start Marker
            EXStreamStartMarker NewStartMarker = new EXStreamStartMarker
            {
                Name = (uint)MarkersList.Count,
                Position = v_pos,
                MusicMakerType = v_type,
                Flags = v_flags,
                Extra = 0,
                LoopStart = v_loopstart,
                MarkerCount = (uint)MarkersList.Count,
                LoopMarkerCount = v_loopmarkercount,
                MarkerPos = v_mkrpos,
                StateA = v_stateA,
                StateB = v_stateB
            };

            //Add Start Marker To List
            MarkersList.Add(NewStartMarker);

            return NewStartMarker;
        }

        internal EXStreamMarker CreateMarker(List<EXStreamMarker> MarkersDataList, int v_name, uint v_position, uint v_type, uint v_flags, uint v_markercount, uint v_loopstart, uint v_loopmarkercount)
        {
            //Create Marker
            EXStreamMarker NewMarker = new EXStreamMarker
            {
                Name = v_name,
                Position = v_position,
                MusicMakerType = v_type,
                Flags = v_flags,
                MarkerCount = v_markercount,
                LoopStart = v_loopstart,
                LoopMarkerCount = v_loopmarkercount
            };

            //Add Start Marker To List
            MarkersDataList.Add(NewMarker);

            return NewMarker;
        }

        internal uint[] GetEngineXMarkerStates_Mono(byte[] ImaADPCM_File, int Position)
        {
            ImaADPCM_Functions imaADPCMFunctions = new ImaADPCM_Functions();
            uint[] enginexStates = new uint[2];

            int roundedIndex = ((Position & -256) / 2) - 1;

            //Calculate States
            int samplesToDecode = ImaADPCM_File.Length * 2;
            uint[] decodedStates = new uint[samplesToDecode];
            imaADPCMFunctions.DecodeIMA_ADPCM(ImaADPCM_File, samplesToDecode, decodedStates);

            //Get states
            if (roundedIndex > 0 && roundedIndex <= decodedStates.Length)
            {
                enginexStates[0] = decodedStates[roundedIndex];
                enginexStates[1] = decodedStates[roundedIndex];
            }

            return enginexStates;
        }

        internal uint[] GetEngineXMarkerStates_Stereo(byte[] ImaADPCM_FileLeftChannel, byte[] ImaADPCM_FileRightChannel, int Position)
        {
            ImaADPCM_Functions imaADPCMFunctions = new ImaADPCM_Functions();
            uint[] enginexStates = new uint[2];

            //Calculate States Left Channel
            int samplesToDecode_leftChannel = ImaADPCM_FileLeftChannel.Length * 2;
            uint[] decodedStates_leftChannel = new uint[samplesToDecode_leftChannel];
            imaADPCMFunctions.DecodeIMA_ADPCM(ImaADPCM_FileLeftChannel, samplesToDecode_leftChannel, decodedStates_leftChannel);

            //Calculate States Right Channel
            int samplesToDecode_rightChannel = ImaADPCM_FileRightChannel.Length * 2;
            uint[] decodedStates_rightChannel = new uint[samplesToDecode_rightChannel];
            imaADPCMFunctions.DecodeIMA_ADPCM(ImaADPCM_FileRightChannel, samplesToDecode_rightChannel, decodedStates_rightChannel);

            int roundedIndex = ((((Position / 256) * 256) / 4));

            //Get states
            if (roundedIndex > 0 && roundedIndex <= decodedStates_leftChannel.Length)
            {
                enginexStates[0] = decodedStates_leftChannel[roundedIndex - 1];
                enginexStates[1] = decodedStates_rightChannel[roundedIndex - 1];
            }

            return enginexStates;
        }
    }
}
