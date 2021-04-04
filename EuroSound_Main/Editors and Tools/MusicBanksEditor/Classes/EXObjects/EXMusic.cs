using EuroSound_Application.StreamSounds;
using System.Collections.Generic;

namespace EuroSound_Application.Musics
{
    public class EXMusic
    {
        public uint BaseVolume = 100;
        public List<EXStreamStartMarker> StartMarkers = new List<EXStreamStartMarker>();
        public List<EXStreamMarker> Markers = new List<EXStreamMarker>();

        //Extra info (Not required for the output)
        public bool OutputThisSound = true;

        public uint Frequency { get; set; }
        public byte Channels { get; set; }
        public uint Bits { get; set; }
        public uint Duration { get; set; }
        public uint RealSize { get; set; }
        public string Encoding { get; set; } = string.Empty;
        public string WAVFileMD5 { get; set; } = string.Empty;
        public string WAVFileName { get; set; } = string.Empty;

        //*===============================================================================================
        //* LEFT CHANNEL
        //*===============================================================================================
        public byte[] PCM_Data_LeftChannel { get; set; } = new byte[2];
        public byte[] IMA_ADPCM_DATA_LeftChannel { get; set; } = new byte[2];

        //*===============================================================================================
        //* RIGHT CHANNEL
        //*===============================================================================================
        public byte[] PCM_Data_RightChannel { get; set; } = new byte[2];
        public byte[] IMA_ADPCM_DATA_RightChannel { get; set; } = new byte[2];

        //*===============================================================================================
        //* STEREO TRACK
        //*===============================================================================================
        public byte[] PCM_Data { get; set; } = new byte[2];
    }
}
