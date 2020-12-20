using System.Collections.Generic;

namespace EuroSound_Application
{
    public class EXSoundStream
    {
        public string DisplayName { get; set; }
        public string Marker { get; set; }
        public int MarkerPosition { get; set; }
        public int BaseVolume { get; set; }
        public List<EXStreamSoundMarkerData> MarkersData = new List<EXStreamSoundMarkerData>();
        public bool IsInstant { get; set; }
        public bool InstantBuffer { get; set; }
        public byte[] State { get; set; } = new byte[2];
        public byte[] IMA_ADPCM_DATA { get; set; }
        public byte[] PCM_DATA { get; set; }

        //Extra info (Not required for the output)
        public string Hashcode { get; set; }
        public int DataSize { get; set; }
        public int Channels { get; set; }
        public string Encoding { get; set; } = "<Null>";
        public int Frequency { get; set; }
        public int Bits { get; set; }
        public int RealSize { get; set; }
        public int Duration { get; set; }
    }
}
