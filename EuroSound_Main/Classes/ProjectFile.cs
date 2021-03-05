using EuroSound_Application.SoundBanksEditor;
using EuroSound_Application.StreamSounds;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EuroSound_Application.CurrentProjectFunctions
{
    public class ProjectFile
    {
        /*====================[Available Types Of Data]====================
         0 = Soundbanks
         1 = Stream Sounds (does not allow adding new stream sounds to the game, there's a bug that will not probably be fixed)
         2 = Musics (not sure if will be ever implemented)
        */
        public string FileName = "Unnamed";
        public sbyte TypeOfData = 0;
        public uint Hashcode = 0x00000000;
        public uint SoundID = 0;
        public uint SampleID = 0;
        public uint AudioID = 0;
        public bool FileHasBeenModified = false;

        public void ClearSoundBankStoredData(Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudioDataDict, TreeView TreeViewControl)
        {
            SoundsList.Clear();
            AudioDataDict.Clear();
            TreeViewControl.Nodes[0].Nodes.Clear();
            TreeViewControl.Nodes[1].Nodes.Clear();
            TreeViewControl.Nodes[2].Nodes.Clear();
        }

        public void ClearStreamSoundStoredData(Dictionary<uint, EXSoundStream> SoundsDict, TreeView TreeViewControl)
        {
            SoundsDict.Clear();
            TreeViewControl.Nodes[0].Nodes.Clear();
        }
    }
}