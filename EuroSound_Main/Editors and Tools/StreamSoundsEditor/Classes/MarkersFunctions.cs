﻿using EngineXImaAdpcm;
using EuroSound_Application.StreamSounds;
using System.Collections.Generic;

namespace EuroSound_Application.Editors_and_Tools.StreamSoundsEditor.Classes
{
    class MarkersFunctions
    {
        internal EXStreamStartMarker CreateStartMarker(List<EXStreamStartMarker> MarkersList, uint v_pos, uint v_type, uint v_mkrcount, uint v_mkrpos, uint v_stateA, uint v_stateB)
        {
            //Create Start Marker
            EXStreamStartMarker NewStartMarker = new EXStreamStartMarker
            {
                Position = v_pos,
                MusicMakerType = v_type,
                Flags = 2,
                Extra = 0,
                MarkerCount = v_mkrcount,
                MarkerPos = v_mkrpos,
                StateA = v_stateA,
                StateB = v_stateB
            };

            //Add Start Marker To List
            MarkersList.Add(NewStartMarker);

            return NewStartMarker;
        }

        internal EXStreamMarker CreateMarker(List<EXStreamMarker> MarkersDataList, int v_name, uint v_position, uint v_type, uint v_markercount, uint v_loopstart, uint v_loopmarkercount)
        {
            //Create Marker
            EXStreamMarker NewMarker = new EXStreamMarker
            {
                Name = v_name,
                Position = v_position,
                MusicMakerType = v_type,
                MarkerCount = v_markercount,
                LoopStart = v_loopstart,
                LoopMarkerCount = v_loopmarkercount
            };

            //Add Start Marker To List
            MarkersDataList.Add(NewMarker);

            return NewMarker;
        }

        internal uint[] CalculateMarkerStates(byte[] ImaADPCM_File, int Position)
        {
            ImaADPCM_Functions ImaADPCM = new ImaADPCM_Functions();
            uint[] States = new uint[2];
            int pointer = (Position / 2) - 1;

            //Calculate States
            int SamplesToDecode = ImaADPCM_File.Length * 2;
            int[] IMAStates = new int[SamplesToDecode];
            ImaADPCM.DecodeIMA_ADPCM(ImaADPCM_File, SamplesToDecode, IMAStates);

            //Get states
            if (pointer > 0 && pointer <= IMAStates.Length)
            {
                States[0] = (uint)IMAStates[pointer];
                States[1] = (uint)IMAStates[pointer];
            }

            return States;
        }
    }
}
