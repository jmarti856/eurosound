using System.Collections.Generic;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public class ProjectFile
    {
        public string FileName = "Unnamed";
        public byte TypeOfData = 0;
        public uint Hashcode = 0x00000000;
        public uint SoundID = 0;
        public uint StreamedSoundID = 0;
        public bool FileHasBeenModified = false;

        public void ClearSoundBankStoredData(Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudioDataDict, TreeView TreeViewControl)
        {
            SoundsList.Clear();
            AudioDataDict.Clear();
            TreeViewControl.Nodes[0].Nodes.Clear();
            TreeViewControl.Nodes[1].Nodes.Clear();
            TreeViewControl.Nodes[2].Nodes.Clear();
        }
    }
}