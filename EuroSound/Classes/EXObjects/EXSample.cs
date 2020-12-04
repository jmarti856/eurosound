namespace EuroSound_SB_Editor
{
    public class EXSample
    {
        public EXAudio Audio { get; set; } = new EXAudio();
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsStreamed { get; set; } = false;
        public int FileRef { get; set; } = 0;

        /*---ENGINE X Required---*/
        public int PitchOffset { get; set; } = 0;
        public int RandomPitchOffset { get; set; } = 0;
        public int BaseVolume { get; set; } = 0;
        public int RandomVolumeOffset { get; set; } = 0;
        public int Pan { get; set; } = 0;
        public int RandomPan { get; set; } = 0;

        /*--Functions--*/
        public void RemoveAudio()
        {
            this.Audio = new EXAudio();
        }
    }
}
