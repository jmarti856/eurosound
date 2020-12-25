using System;
using System.Collections.Generic;

namespace EuroSound_Application
{
    public class EXSound
    {
        public string DisplayName = string.Empty;
        public sbyte Ducker;
        public Int16 DuckerLenght;
        public UInt16 Flags;
        public uint Hashcode;
        public Int16 InnerRadiusReal;
        public sbyte MasterVolume;
        public Int16 MaxDelay;
        public sbyte MaxVoices;
        public Int16 MinDelay;
        public Int16 OuterRadiusReal;
        public bool OutputThisSound = true;
        public sbyte Priority;
        public sbyte ReverbSend;
        public List<EXSample> Samples { get; set; } = new List<EXSample>();

        /*---Required for Engine X--*/
        public sbyte TrackingType;
    }
}