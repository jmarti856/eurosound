using System.Collections.Generic;

namespace EuroSound_Application
{
    public class EXSoundStream
    {
        public string DisplayName;
        public string Marker;
        public uint MarkerPosition;
        public uint BaseVolume;
        public uint Hashcode;
        public List<EXStreamSoundMarkerData> MarkersData = new List<EXStreamSoundMarkerData>();
        public EXAudio WAV_Audio = new EXAudio();
        public bool IsInstant;
        public bool InstantBuffer;
        public bool OutputThisSound;
        public byte[] State = new byte[2];
        public byte[] IMA_ADPCM_DATA;

        //Extra info (Not required for the output)
        public string PCM_Data_MD5;
        public string IMA_Data_MD5;
        public string IMA_Data_Name;
        public string PCM_Data_Name;
    }
}
