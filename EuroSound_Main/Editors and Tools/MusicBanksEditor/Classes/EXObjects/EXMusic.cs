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

        //*===============================================================================================
        //* LEFT CHANNEL
        //*===============================================================================================
        public uint Frequency_LeftChannel { get; set; }
        public byte Channels_LeftChannel { get; set; }
        public uint Bits_LeftChannel { get; set; }
        public uint Duration_LeftChannel { get; set; }
        public uint RealSize_LeftChannel { get; set; }
        public string Encoding_LeftChannel { get; set; } = string.Empty;
        public string WAVFileMD5_LeftChannel { get; set; } = string.Empty;
        public string WAVFileName_LeftChannel { get; set; } = string.Empty;
        public byte[] PCM_Data_LeftChannel { get; set; } = new byte[2];
        public byte[] IMA_ADPCM_DATA_LeftChannel { get; set; } = new byte[2];

        //*===============================================================================================
        //* RIGHT CHANNEL
        //*===============================================================================================
        public uint Frequency_RightChannel { get; set; }
        public byte Channels_RightChannel { get; set; }
        public uint Bits_RightChannel { get; set; }
        public uint Duration_RightChannel { get; set; }
        public uint RealSize_RightChannel { get; set; }
        public string Encoding_RightChannel { get; set; } = string.Empty;
        public string WAVFileMD5_RightChannel { get; set; } = string.Empty;
        public string WAVFileName_RightChannel { get; set; } = string.Empty;
        public byte[] PCM_Data_RightChannel { get; set; } = new byte[2];
        public byte[] IMA_ADPCM_DATA_RightChannel { get; set; } = new byte[2];
    }
}
