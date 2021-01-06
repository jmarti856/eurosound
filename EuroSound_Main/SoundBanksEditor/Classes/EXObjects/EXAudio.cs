namespace EuroSound_Application.SoundBanksEditor
{
    public class EXAudio
    {
        public uint Bits { get; set; }
        public uint Channels { get; set; }
        public uint DataSize { get; set; }
        public string Dependencies { get; set; } = string.Empty;
        public uint Duration { get; set; }
        public string Encoding { get; set; } = "<Null>";
        public ushort Flags { get; set; }
        public uint Frequency { get; set; }
        public uint LoopOffset { get; set; }
        public string LoadedFileName { get; set; } = string.Empty;
        /*---ENGINE X Required---*/
        public byte[] PCMdata { get; set; } = new byte[] { 1, 0, 8 };
        public uint PSIsample { get; set; }
        public uint RealSize { get; set; }
    }
}