﻿using System.Collections.Generic;

namespace EuroSound_Application
{
    public class EXSound
    {
        public string DisplayName = string.Empty;
        public sbyte Ducker;
        public short DuckerLenght;
        public ushort Flags;
        public uint Hashcode;
        public short InnerRadiusReal;
        public sbyte MasterVolume;
        public short MaxDelay;
        public sbyte MaxVoices;
        public short MinDelay;
        public short OuterRadiusReal;
        public bool OutputThisSound = true;
        public sbyte Priority;
        public sbyte ReverbSend;
        public List<EXSample> Samples { get; set; } = new List<EXSample>();

        /*---Required for Engine X--*/
        public sbyte TrackingType;
    }
}