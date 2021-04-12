using EngineXImaAdpcm;
using EuroSound_Application.StreamSounds;
using System.Collections.Generic;

namespace EuroSound_Application.MarkerFiles.StreamSoundsEditor.Classes
{
    class MarkersFunctions
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
            ImaADPCM_Functions ImaADPCM = new ImaADPCM_Functions();
            int Pointer, SamplesToDecode;
            uint[] States, IMAStates;
            States = new uint[2];

            Pointer = ((Position & -256) / 2) - 1;

            //Calculate States
            SamplesToDecode = ImaADPCM_File.Length * 2;
            IMAStates = new uint[SamplesToDecode];
            ImaADPCM.DecodeIMA_ADPCM(ImaADPCM_File, SamplesToDecode, IMAStates);

            //Get states
            if (Pointer > 0 && Pointer <= IMAStates.Length)
            {
                States[0] = IMAStates[Pointer];
                States[1] = IMAStates[Pointer];
            }

            return States;
        }

        internal uint[] GetEngineXMarkerStates_Stereo(byte[] ImaADPCM_FileLeftChannel, byte[] ImaADPCM_FileRightChannel, int Position)
        {
            ImaADPCM_Functions ImaADPCM = new ImaADPCM_Functions();
            int Pointer, SamplesToDecode_LeftChannel, SamplesToDecode_RightChannel;
            uint[] States, IMAStates_RightChannel, IMAStates_LeftChannel;
            States = new uint[2];

            //Calculate States Left Channel
            SamplesToDecode_LeftChannel = ImaADPCM_FileLeftChannel.Length * 2;
            IMAStates_LeftChannel = new uint[SamplesToDecode_LeftChannel];
            ImaADPCM.DecodeIMA_ADPCM(ImaADPCM_FileLeftChannel, SamplesToDecode_LeftChannel, IMAStates_LeftChannel);

            //Calculate States Right Channel
            SamplesToDecode_RightChannel = ImaADPCM_FileRightChannel.Length * 2;
            IMAStates_RightChannel = new uint[SamplesToDecode_RightChannel];
            ImaADPCM.DecodeIMA_ADPCM(ImaADPCM_FileRightChannel, SamplesToDecode_RightChannel, IMAStates_RightChannel);

            Pointer = ((((Position / 256) * 256) / 4));

            //Get states
            if (Pointer > 0 && Pointer <= IMAStates_LeftChannel.Length)
            {
                States[0] = IMAStates_LeftChannel[Pointer - 1];
                States[1] = IMAStates_RightChannel[Pointer - 1];
            }

            return States;
        }
    }
}
