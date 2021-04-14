using System.Drawing;

namespace EuroSound_Application.SoundBanksEditor
{
    public class EXSample
    {
        public sbyte BaseVolume;
        public string ComboboxSelectedAudio = string.Empty;
        public short FileRef;
        public uint HashcodeSubSFX;
        public bool IsStreamed = false;
        //---ENGINE X Required---
        public sbyte Pan;
        public short PitchOffset;
        public sbyte RandomPan;
        public short RandomPitchOffset;
        public sbyte RandomVolumeOffset;
        //---ESIF Required
        public Color NodeColor; //Only used for the importer
        public string Name; //Only used for the importer

    }
}