namespace EuroSound_Application
{
    public class EXSample
    {
        public int BaseVolume { get; set; } = 0;
        public string ComboboxSelectedAudio { get; set; } = string.Empty;
        public string DisplayName { get; set; }
        public int FileRef { get; set; } = 0;
        public string HashcodeSubSFX { get; set; } = string.Empty;
        public bool IsStreamed { get; set; } = false;
        public string Name { get; set; }
        /*---ENGINE X Required---*/
        public int Pan { get; set; } = 0;
        public int PitchOffset { get; set; } = 0;
        public int RandomPan { get; set; } = 0;
        public int RandomPitchOffset { get; set; } = 0;
        public int RandomVolumeOffset { get; set; } = 0;
    }
}