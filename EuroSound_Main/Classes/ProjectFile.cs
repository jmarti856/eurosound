using System.Collections.Generic;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public class ProjectFile
    {
        public string FileName { get; set; } = "Unnamed";
        public int TypeOfData { get; set; } = 0;
        public string Hashcode { get; set; } = "0x00000001";
        public int SoundID { get; set; } = 0;
        public int StreamedSoundID { get; set; } = 0;
        public bool FileHasBeenModified { get; set; } = false;

        public void ClearSoundBankStoredData(Dictionary<int, EXSound> SoundsList, Dictionary<string, EXAudio> AudioDataDict, TreeView TreeViewControl)
        {
            SoundsList.Clear();
            AudioDataDict.Clear();
            TreeViewControl.Nodes[0].Nodes.Clear();
            TreeViewControl.Nodes[1].Nodes.Clear();
            TreeViewControl.Nodes[2].Nodes.Clear();
        }
    }
}