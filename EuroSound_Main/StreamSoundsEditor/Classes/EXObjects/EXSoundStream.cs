using System.Collections.Generic;

namespace EuroSound_Application.StreamSounds
{
    public class EXSoundStream
    {
        public string DisplayName = string.Empty;
        public uint BaseVolume = 100;
        public byte[] IMA_ADPCM_DATA = new byte[2];
        public List<EXStreamStartMarker> StartMarkers = new List<EXStreamStartMarker>();
        public List<EXStreamMarker> Markers = new List<EXStreamMarker>();

        //Extra info (Not required for the output)
        public string IMA_Data_MD5 = string.Empty;
        public string IMA_Data_Name = string.Empty;
        public bool OutputThisSound = true;

        //IDs
        public uint MarkerDataCounterID { get; set; } = 0;
        public uint MarkerID { get; set; } = 0;
    }
}
