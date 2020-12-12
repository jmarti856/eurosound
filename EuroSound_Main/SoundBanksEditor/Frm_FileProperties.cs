using System;
using System.Collections.Generic;
using System.Resources;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_FileProperties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        ProjectFile CurrentFileProperties;
        public Dictionary<string, string> SB_Defines;
        public Dictionary<string, string> SFX_Defines;
        string HashcodesSFX, HashcodesData;

        ResourceManager ResourcesManager;

        public Frm_FileProperties(ProjectFile FileProperties, string v_HashcodesSFX, string v_HashcodesData, Dictionary<string, string> v_SB_Defines, Dictionary<string, string> v_SFX_Defines, ResourceManager v_ResourcesManager)
        {
            InitializeComponent();
            CurrentFileProperties = FileProperties;
            HashcodesSFX = v_HashcodesSFX;
            HashcodesData = v_HashcodesData;
            SB_Defines = v_SB_Defines;
            SFX_Defines = v_SFX_Defines;
            ResourcesManager = v_ResourcesManager;

        }

        //*===============================================================================================
        //* Form Load
        //*===============================================================================================
        private void Frm_FileProperties_Load(object sender, EventArgs e)
        {
            /*Datasource Combobox*/
            Hashcodes.AddHashcodesToCombobox(Combobox_FileHashcode, SB_Defines);

            Textbox_FileName.Text = CurrentFileProperties.FileName;
            Combobox_TypeOfData.SelectedIndex = CurrentFileProperties.TypeOfData;
            Textbox_Sounds_Path.Text = HashcodesSFX;
            Textbox_SFXData_Path.Text = HashcodesData;


            /*Put the selected hashcode in case is not null*/
            if (CurrentFileProperties.Hashcode != null)
            {
                Combobox_FileHashcode.SelectedValue = CurrentFileProperties.Hashcode;
            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            /*Update properties*/
            CurrentFileProperties.FileName = Textbox_FileName.Text;
            CurrentFileProperties.TypeOfData = Combobox_TypeOfData.SelectedIndex;

            CurrentFileProperties.Hashcode = Combobox_FileHashcode.SelectedValue.ToString();

            /*Update Current File label*/
            GenericFunctions.SetCurrentFileLabel(CurrentFileProperties.FileName);

            /*Close current form*/
            this.Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            /*Close current form*/
            this.Close();
        }

        private void Combobox_FileHashcode_Click(object sender, EventArgs e)
        {
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsMD5, HashcodesSFX))
            {
                Hashcodes.LoadSoundHashcodes(HashcodesSFX, SFX_Defines, SB_Defines, ResourcesManager);
                Hashcodes.AddHashcodesToCombobox(Combobox_FileHashcode, SB_Defines);
            }
        }
    }
}
