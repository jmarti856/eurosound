using System.Collections.Generic;

namespace EuroSound_Application.StreamSounds
{
    public class EXSoundStream
    {
        public string DisplayName = string.Empty;
        public uint BaseVolume = 100;
        public List<EXStreamStartMarker> StartMarkers = new List<EXStreamStartMarker>();
        public List<EXStreamMarker> Markers = new List<EXStreamMarker>();

        //Extra info (Not required for the output)
        public bool OutputThisSound = true;

        //WAV File
        public byte[] PCM_Data { get; set; } = new byte[2];
        public uint Frequency { get; set; }
        public byte Channels { get; set; }
        public uint Bits { get; set; }
        public uint Duration { get; set; }
        public uint RealSize { get; set; }
        public string Encoding { get; set; } = string.Empty;
        public string WAVFileMD5 { get; set; } = string.Empty;
        public string WAVFileName { get; set; } = string.Empty;

        //IMA ADPCM
        public byte[] IMA_ADPCM_DATA { get; set; } = new byte[2];

        //IDs
        public uint MarkerDataCounterID { get; set; } = 0;
        public uint MarkerID { get; set; } = 0;
    }
}
