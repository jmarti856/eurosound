using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_HashTablesConfig : Form
    {
        public Frm_HashTablesConfig()
        {
            InitializeComponent();
        }

        private void Frm_HashTablesConfig_Load(object sender, System.EventArgs e)
        {
            Textbox_HT_Sound.Text = GlobalPreferences.HT_SoundsPath;
            Textbox_HT_Sound_Data.Text = GlobalPreferences.HT_SoundsDataPath;
            Textbox_HT_Music.Text = GlobalPreferences.HT_MusicPath;
        }

        private void Button_HT_Sound_Click(object sender, System.EventArgs e)
        {
            Textbox_HT_Sound.Text = GenericFunctions.OpenFileBrowser("Header Files|*.h", 0);
            GlobalPreferences.HT_SoundsPathTEMPORAL = Textbox_HT_Sound.Text;
        }

        private void Button_HT_SoundData_Click(object sender, System.EventArgs e)
        {
            Textbox_HT_Sound_Data.Text = GenericFunctions.OpenFileBrowser("Header Files|*.h", 0);
            GlobalPreferences.HT_SoundsDataPathTEMPORAL = Textbox_HT_Sound_Data.Text;
        }

        private void Button_HT_Music_Click(object sender, System.EventArgs e)
        {
            Textbox_HT_Music.Text = GenericFunctions.OpenFileBrowser("Header Files|*.h", 0);
            GlobalPreferences.HT_MusicPathTEMPORAL = Textbox_HT_Music.Text;
        }
    }
}
