using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_StreamSounds_Properties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private EXSoundStream SelectedSound;
        private byte[] IMA_ADPCM_Data;
        private string IMA_ADPCM_FileName, IMA_ADPCM_MD5, SelectedSoundKey;

        public Frm_StreamSounds_Properties(EXSoundStream SoundToCheck, string SoundKey)
        {
            InitializeComponent();
            SelectedSound = SoundToCheck;
            SelectedSoundKey = SoundKey;
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_StreamSounds_Properties_Load(object sender, System.EventArgs e)
        {
            /*Sound Defines*/
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsMD5, GlobalPreferences.HT_SoundsPath))
            {
                Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
            }
            Hashcodes.AddHashcodesToCombobox(Combobox_Hashcode, Hashcodes.SFX_Defines);

            /*Editable Data*/
            Combobox_Hashcode.SelectedValue = SelectedSound.Hashcode;
            Numeric_BaseVolume.Value = SelectedSound.BaseVolume;

            /*ADPCM Data*/
            Textbox_IMA_ADPCM.Text = SelectedSound.IMA_Data_Name;
            Textbox_MD5_Hash.Text = SelectedSound.IMA_Data_MD5;

            CheckBox_OutputThisSound.Checked = SelectedSound.OutputThisSound;
        }

        private void Button_MarkersEditor_Click(object sender, System.EventArgs e)
        {
            Frm_StreamSounds_MarkersEditor MarkersEditr = new Frm_StreamSounds_MarkersEditor(SelectedSound)
            {
                Text = "Streamed Sound Markers Editor",
                Tag = Tag,
                Owner = this,
                ShowInTaskbar = false
            };
            MarkersEditr.ShowDialog();
            MarkersEditr.Dispose();
        }

        private void Button_SearchIMA_Click(object sender, System.EventArgs e)
        {
            string AudioPath = GenericFunctions.OpenFileBrowser("IMA ADPCM Files (*.ima)|*.ima", 0);
            if (!string.IsNullOrEmpty(AudioPath))
            {
                IMA_ADPCM_MD5 = GenericFunctions.CalculateMD5(AudioPath);
                IMA_ADPCM_Data = File.ReadAllBytes(AudioPath);
                IMA_ADPCM_FileName = Path.GetFileName(AudioPath);

                if (IMA_ADPCM_Data != null)
                {
                    Textbox_MD5_Hash.Text = IMA_ADPCM_MD5;
                    Textbox_IMA_ADPCM.Text = IMA_ADPCM_FileName;
                }
                else
                {
                    MessageBox.Show("Error reading this file, seems that is being used by another process", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Button_OK_Click(object sender, System.EventArgs e)
        {
            if (Combobox_Hashcode.SelectedValue != null)
            {
                SelectedSound.Hashcode = (uint)Combobox_Hashcode.SelectedValue;
            }
            SelectedSound.BaseVolume = (uint)Numeric_BaseVolume.Value;
            SelectedSound.OutputThisSound = CheckBox_OutputThisSound.Checked;

            if (IMA_ADPCM_Data != null)
            {
                SelectedSound.IMA_Data_MD5 = IMA_ADPCM_MD5;
                SelectedSound.IMA_Data_Name = IMA_ADPCM_FileName;
                SelectedSound.IMA_ADPCM_DATA = IMA_ADPCM_Data;
            }

            /*--Change icon in the parent form--*/
            Form OpenForm = GenericFunctions.GetFormByName("Frm_StreamSoundsEditorMain", Tag.ToString());
            TreeNode[] Results = ((Frm_StreamSoundsEditorMain)OpenForm).TreeView_StreamData.Nodes.Find(SelectedSoundKey, true);
            if (Results.Length > 0)
            {
                if (SelectedSound.OutputThisSound)
                {
                    TreeNodeFunctions.TreeNodeSetNodeImage(Results[0], 2, 2);
                }
                else
                {
                    TreeNodeFunctions.TreeNodeSetNodeImage(Results[0], 5, 5);
                }
            }

            /*--Close this form--*/
            Close();
        }

        private void Button_Cancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
