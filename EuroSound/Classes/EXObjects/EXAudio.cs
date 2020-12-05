namespace EuroSound_SB_Editor
{
    public class EXAudio
    {
        public string Name { get; set; }
        public string Encoding { get; set; } = "<Null>";

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
        public byte[] PCMdata { get; set; } = new byte[] { 1, 0, 8 };

        /*--Functions--*/
        public bool IsEmpty()
        {
            bool Empty = false;
            if (PCMdata.Length == 3)
            {
                if (PCMdata[0] == 1 && PCMdata[1] == 0 && PCMdata[2] == 8)
                {
                    Empty = true;
                }
            }

            return Empty;
        }
    }
}
