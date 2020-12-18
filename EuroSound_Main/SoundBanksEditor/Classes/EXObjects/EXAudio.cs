namespace EuroSound_Application
{
    public class EXAudio
    {
        public int Bits { get; set; }
        public int Channels { get; set; }
        public int DataSize { get; set; }
        public string Dependencies { get; set; } = string.Empty;
        public string DisplayName { get; set; }
        public int Duration { get; set; }
        public string Encoding { get; set; } = "<Null>";
        public int Flags { get; set; }
        public int Frequency { get; set; }
        public int LoopOffset { get; set; }
        public string Name { get; set; }
        /*---ENGINE X Required---*/
        public byte[] PCMdata { get; set; } = new byte[] { 1, 0, 8 };
        public int PSIsample { get; set; }
        public int RealSize { get; set; }
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