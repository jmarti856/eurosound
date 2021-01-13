﻿using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.CurrentProjectFunctions;
using System;
using System.Windows.Forms;

namespace EuroSound_Application.CurrentProjectForm
{
    public partial class Frm_FileProperties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private ProjectFile CurrentFileProperties;

        public Frm_FileProperties(ProjectFile FileProperties)
        {
            InitializeComponent();
            CurrentFileProperties = FileProperties;
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
            Hashcodes.AddDataToCombobox(Combobox_FileHashcode, Hashcodes.SB_Defines);

            //Put the selected hashcode in case is not null
            if (CurrentFileProperties.Hashcode != 0x00000000)
            {
                Combobox_FileHashcode.SelectedValue = CurrentFileProperties.Hashcode;
            }

            if (CurrentFileProperties.Hashcode == 65535)
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

            if (Combobox_FileHashcode.SelectedValue != null)
            {
                CurrentFileProperties.Hashcode = Convert.ToUInt32(Combobox_FileHashcode.SelectedValue.ToString());
            }

            //Update Current File label
            GenericFunctions.SetCurrentFileLabel(CurrentFileProperties.FileName);

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