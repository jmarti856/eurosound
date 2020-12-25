using System;

namespace EuroSound_Application
{
    public class EXSample
    {
        public sbyte BaseVolume;
        public string ComboboxSelectedAudio = string.Empty;
        public string DisplayName = string.Empty;
        public Int16 FileRef;
        public uint HashcodeSubSFX;
        public bool IsStreamed;
        public string Name;
        /*---ENGINE X Required---*/
        public sbyte Pan;
        public Int16 PitchOffset;
        public sbyte RandomPan;
        public Int16 RandomPitchOffset;
        public sbyte RandomVolumeOffset;
    }
}