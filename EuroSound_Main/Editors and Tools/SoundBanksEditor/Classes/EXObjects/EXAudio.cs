namespace EuroSound_Application.SoundBanksEditor
{
    public class EXAudio
    {
        public uint Bits { get; set; }
        public uint Channels { get; set; }
        public string Dependencies { get; set; } = string.Empty;
        public uint Duration { get; set; }
        public string Encoding { get; set; } = "<Null>";
        public ushort Flags { get; set; }
        public uint Frequency { get; set; }
        public uint LoopOffset { get; set; }
        public string LoadedFileName { get; set; } = string.Empty;
        public byte[] PCMdata { get; set; } = new byte[] { 1, 0, 8 };
        public uint PSIsample { get; set; }

        //---PS2----
        public uint FrequencyPS2 { get; set; } = 11025;
        public uint LoopOffsetPS2 { get; set; } = 0;
        public bool LoopOffsetPS2Locked { get; set; } = false;
    }
}