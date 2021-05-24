namespace EuroSound_Application
{
    internal static class Enumerations
    {
        internal enum EXMarkerType : byte
        {
            Start = 10,
            End = 9,
            Goto = 7,
            Loop = 6,
            Pause = 5,
            Jump = 0
        }

        internal enum ESoundFileType : byte
        {
            SoundBanks = 0,
            StreamSounds = 1,
            MusicBanks = 2
        }

        internal enum EXObjectType : byte
        {
            EXAudio = 1,
            EXSound = 2,
            EXSample = 3,
            EXMusic = 4,
            EXSoundFolder = 5,
            EXMusicFolder = 6
        }

        internal enum TreeNodeType : byte
        {
            Folder = 1,
            Sound = 2,
            Sample = 3,
            Music = 4
        }
    }
}
