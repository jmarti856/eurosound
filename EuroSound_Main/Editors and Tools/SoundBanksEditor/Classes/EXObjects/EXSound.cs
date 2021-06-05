using System.Collections.Generic;

namespace EuroSound_Application.SoundBanksEditor
{
    public class EXSound
    {
        public sbyte Ducker;
        public short DuckerLength;
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
        public byte OutputTarget;

        //public List<EXSample> Samples { get; set; } = new List<EXSample>();
        public Dictionary<uint, EXSample> Samples { get; set; } = new Dictionary<uint, EXSample>();

        //---Required for Engine X--
        public sbyte TrackingType;
    }
}