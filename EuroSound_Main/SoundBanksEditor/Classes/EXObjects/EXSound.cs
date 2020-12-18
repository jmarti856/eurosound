using System.Collections.Generic;

namespace EuroSound_Application
{
    public class EXSound
    {
        public string DisplayName { get; set; }
        public int Ducker { get; set; } = 0;
        public int DuckerLenght { get; set; } = 0;
        public int Flags { get; set; } = 0;
        public string Hashcode { get; set; }
        public int InnerRadiusReal { get; set; } = 0;
        public int MasterVolume { get; set; } = 0;
        public int MaxDelay { get; set; } = 0;
        public int MaxVoices { get; set; } = 0;
        public int MinDelay { get; set; } = 0;
        public int OuterRadiusReal { get; set; } = 0;
        public bool OutputThisSound { get; set; }
        public int Priority { get; set; } = 0;
        public int ReverbSend { get; set; } = 0;
        public List<EXSample> Samples { get; set; } = new List<EXSample>();

        /*---Required for Engine X--*/
        public int TrackingType { get; set; } = 0;
    }
}