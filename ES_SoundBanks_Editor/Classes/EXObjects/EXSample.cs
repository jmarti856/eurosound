namespace SoundBanks_Editor
{
    public class EXSample
    {
        public string ComboboxSelectedAudio { get; set; } = string.Empty;
        public string HashcodeSubSFX { get; set; } = string.Empty;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsStreamed { get; set; } = false;

        /*---ENGINE X Required---*/
        public int FileRef { get; set; } = 0;
        public int PitchOffset { get; set; } = 0;
        public int RandomPitchOffset { get; set; } = 0;
        public int BaseVolume { get; set; } = 0;
        public int RandomVolumeOffset { get; set; } = 0;
        public int Pan { get; set; } = 0;
        public int RandomPan { get; set; } = 0;
    }
}
