using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationTargets;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.HashCodesFunctions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EuroSound_Application.CustomControls.ProjectSettings
{
    public partial class Frm_FileProperties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private ProjectFile CurrentFileProperties;
        private Dictionary<uint, EXAppTarget> currentOutputTargets;

        public Frm_FileProperties(ProjectFile FileProperties, Dictionary<uint, EXAppTarget> outputTargets)
        {
            InitializeComponent();
            CurrentFileProperties = FileProperties;
            currentOutputTargets = outputTargets;
        }

        //*===============================================================================================
        //* Form Events
        //*===============================================================================================
        private void Frm_FileProperties_Load(object sender, EventArgs e)
        {
            Textbox_FileName.Text = CurrentFileProperties.FileName;
            Combobox_TypeOfData.SelectedIndex = CurrentFileProperties.TypeOfData;
            Textbox_Sounds_Path.Text = GlobalPreferences.HT_SoundsPath;
            Textbox_SFXData_Path.Text = GlobalPreferences.HT_SoundsDataPath;
            Textbox_Musics_Path.Text = GlobalPreferences.HT_MusicPath;
        }

        private void Frm_FileProperties_Shown(object sender, EventArgs e)
        {
            //Datasource Combobox
            if (CurrentFileProperties.TypeOfData != (int)Enumerations.ESoundFileType.MusicBanks)
            {
                Hashcodes.AddDataToCombobox(Combobox_FileHashcode, Hashcodes.SB_Defines);
            }
            else
            {
                Hashcodes.AddDataToCombobox(Combobox_FileHashcode, Hashcodes.MFX_Defines);
            }

            //Put the selected hashcode in case is not null
            if (CurrentFileProperties.Hashcode != 0x00000000)
            {
                Combobox_FileHashcode.SelectedValue = CurrentFileProperties.Hashcode;
            }

            if (CurrentFileProperties.Hashcode == GlobalPreferences.StreamFileHashCode)
            {
                Combobox_FileHashcode.Enabled = false;
            }
        }

        //*===============================================================================================
        //* Form Controls Events
        //*===============================================================================================
        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            //Close current form
            Close();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            //Update properties
            CurrentFileProperties.FileName = Textbox_FileName.Text.Trim();

            //Update Current File label
            GenericFunctions.SetCurrentFileLabel(CurrentFileProperties.FileName, "SBPanel_File");

            //Check we have selected a value
            if (Combobox_FileHashcode.SelectedValue != null)
            {
                uint SelectedHashcode = Convert.ToUInt32(Combobox_FileHashcode.SelectedValue.ToString());

                //Soundbanks and Music project types can't have the hashcode 0x0000FFFF
                if (CurrentFileProperties.TypeOfData != (int)Enumerations.ESoundFileType.StreamSounds)
                {
                    if (SelectedHashcode == GlobalPreferences.StreamFileHashCode)
                    {
                        MessageBox.Show(GenericFunctions.resourcesManager.GetString("ProjectSettings_ErrorHashcode"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        //Assign Hashcode
                        CurrentFileProperties.Hashcode = SelectedHashcode;

                        //Update Hashcode File Label
                        if (CurrentFileProperties.TypeOfData == (int)Enumerations.ESoundFileType.MusicBanks)
                        {
                            string SelectedHashcodeLabel = Hashcodes.GetHashcodeLabel(Hashcodes.MFX_Defines, CurrentFileProperties.Hashcode);
                            if (SelectedHashcodeLabel.StartsWith("JMP"))
                            {
                                MessageBox.Show(GenericFunctions.resourcesManager.GetString("ProjectSettingsErrorJumpCodes"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                GenericFunctions.SetCurrentFileLabel(SelectedHashcodeLabel, "SBPanel_Hashcode");
                            }
                        }
                        else
                        {
                            GenericFunctions.SetCurrentFileLabel(Hashcodes.GetHashcodeLabel(Hashcodes.SB_Defines, CurrentFileProperties.Hashcode), "SBPanel_Hashcode");
                        }
                    }
                }
            }

            //Update binary name 
            string binaryName = EXAppTarget_Functions.GetBinaryName(CurrentFileProperties, GlobalPreferences.SelectedProfileName);
            EXAppTarget_Functions.UpdateAppTargetName(binaryName, currentOutputTargets);

            //Update project status variable
            CurrentFileProperties.FileHasBeenModified = true;

            //Close current form
            Close();
        }

        private void Combobox_FileHashcode_Click(object sender, EventArgs e)
        {
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsMD5, GlobalPreferences.HT_SoundsPath))
            {
                Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
                Hashcodes.AddDataToCombobox(Combobox_FileHashcode, Hashcodes.SB_Defines);
            }
        }
    }
}