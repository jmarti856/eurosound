using NAudio.Wave;
using System;
using System.Windows.Forms;

namespace EuroSound_Application.ApplicationPreferencesForms
{
    public partial class Frm_OutputDevices : Form
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES
        //*===============================================================================================
        private Form OpenForm;

        public Frm_OutputDevices()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_OutputDevicecs_Load(object sender, EventArgs e)
        {
            int AudioDeviceNumber;
            OpenForm = GenericFunctions.GetFormByName("Frm_MainPreferences", Tag.ToString());

            //Driver, always MME
            Combobox_Driver.SelectedIndex = 0;

            //Add available devices
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                WaveOutCapabilities WOC = WaveOut.GetCapabilities(i);
                Combobox_AvailableDevices.Items.Add(WOC.ProductName);

            }

            //Select Default Device
            AudioDeviceNumber = ((Frm_MainPreferences)OpenForm).DefaultAudioDeviceTEMPORAL;
            if (Combobox_AvailableDevices.Items.Count > AudioDeviceNumber)
            {
                Combobox_AvailableDevices.SelectedIndex = AudioDeviceNumber;
            }
            else
            {
                Combobox_AvailableDevices.SelectedIndex = 0;
            }
        }

        private void Frm_OutputDevicecs_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Frm_MainPreferences)OpenForm).DefaultAudioDeviceTEMPORAL = Combobox_AvailableDevices.SelectedIndex;
        }
    }
}
