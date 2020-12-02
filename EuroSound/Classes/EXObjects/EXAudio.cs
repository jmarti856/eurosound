namespace EuroSound
{
    public class EXAudio
    {
        public string Name { get; set; }
        public byte[] AllData { get; set; } = new byte[] { 0, 0, 0 };
        public string Encoding { get; set; }

        /*---ENGINE X Required---*/
        public int Flags { get; set; } = 0;
        public int DataSize { get; set; }
        public int Frequency { get; set; }
        public int RealSize { get; set; }
        public int Channels { get; set; }
        public int Bits { get; set; }
        public int PSIsample { get; set; } = 0;
        public int LoopOffset { get; set; } = 0;
        public int Duration { get; set; }
        public byte[] PCMdata { get; set; } = new byte[] { 0, 0, 0 };

        /*--Functions--*/
        public bool IsEmpty()
        {
            bool Empty = false;
            if (AllData.Length == 3 || PCMdata.Length == 3)
            {
                if (AllData[0] == 0 && PCMdata[2] == 0)
                {
                    Empty = true;
                }
            }

            return Empty;
        }
    }
}
