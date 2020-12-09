using System.Collections.Generic;

namespace SoundBanks_Editor
{
    public class EXSound
    {
        public string Hashcode { get; set; } = "0x1A000001";
        public string DisplayName { get; set; }
        public bool OutputThisSound { get; set; } = true;
        public List<EXSample> Samples { get; set; } = new List<EXSample>();

        /*---Required for Engine X--*/
        public int DuckerLenght { get; set; } = 0;
        public int MinDelay { get; set; } = 0;
        public int MaxDelay { get; set; } = 0;
        public int InnerRadiusReal { get; set; } = 0;
        public int OuterRadiusReal { get; set; } = 0;
        public int ReverbSend { get; set; } = 0;
        public int TrackingType { get; set; } = 0;
        public int MaxVoices { get; set; } = 0;
        public int Priority { get; set; } = 0;
        public int Ducker { get; set; } = 0;
        public int MasterVolume { get; set; } = 0;
        public int Flags { get; set; } = 0;
    }
}
