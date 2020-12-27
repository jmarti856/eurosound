using System.Collections.Generic;

namespace EuroSound_Application
{
    public class EXStreamSoundMarker
    {
        public uint Position;
        public uint IsInstant;
        public uint InstantBuffer;
        public byte[] State = new byte[8];
        public List<EXStreamSoundMarkerData> MarkersData = new List<EXStreamSoundMarkerData>();
    }
}
