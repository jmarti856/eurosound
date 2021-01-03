using EuroSound_Application.ApplicationPreferences;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
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
            string HeaderSoundPath = GenericFunctions.OpenFileBrowser("Header Files (*.h)|*.h", 0);

            if (!string.IsNullOrEmpty(HeaderSoundPath))
            {
                Textbox_HT_Sound.Text = HeaderSoundPath;
            }
        }

        private void Button_HT_SoundData_Click(object sender, System.EventArgs e)
        {
            string HeaderSoundData = GenericFunctions.OpenFileBrowser("Header Files (*.h)|*.h", 0);

            if (!string.IsNullOrEmpty(HeaderSoundData))
            {
                Textbox_HT_Sound_Data.Text = HeaderSoundData;
            }
        }

        private void Button_HT_Music_Click(object sender, System.EventArgs e)
        {
            string HeaderMusicFile = GenericFunctions.OpenFileBrowser("Header Files (*.h)|*.h", 0);

            if (!string.IsNullOrEmpty(HeaderMusicFile))
            {
                Textbox_HT_Music.Text = HeaderMusicFile;
            }
        }

        private void Frm_HashTablesConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form OpenForm = GenericFunctions.GetFormByName("Frm_MainPreferences", Tag.ToString());
            ((Frm_MainPreferences)OpenForm).HT_SoundsPathTEMPORAL = Textbox_HT_Sound.Text;
            ((Frm_MainPreferences)OpenForm).HT_SoundsDataPathTEMPORAL = Textbox_HT_Sound_Data.Text;
            ((Frm_MainPreferences)OpenForm).HT_MusicPathTEMPORAL = Textbox_HT_Music.Text;
        }
    }
}
