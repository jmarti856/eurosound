using System.Collections.Generic;

namespace EuroSound_Application
{
    public class EXSoundStream
    {
        public string DisplayName = string.Empty;
        public uint BaseVolume;
        public uint Hashcode;
        public byte[] IMA_ADPCM_DATA = new byte[2];
        public List<EXStreamSoundMarker> Markers = new List<EXStreamSoundMarker>();

        //Extra info (Not required for the output)
        public string IMA_Data_MD5 = string.Empty;
        public string IMA_Data_Name = string.Empty;
        public bool OutputThisSound = true;

        //IDs
        public uint IDMarkerName = 0;
        public uint IDMarkerPos = 0;
    }
}
