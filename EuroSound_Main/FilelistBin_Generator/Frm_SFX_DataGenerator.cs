using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_SFX_DataGenerator : Form
    {
        public Frm_SFX_DataGenerator()
        {
            InitializeComponent();
        }

        private void BackgroundWorker_LoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            ListView_HashTableData.Invoke((MethodInvoker)delegate
            {
                ListView_HashTableData.Items.Clear();
            });

            foreach (KeyValuePair<string, float[]> Item in Hashcodes.SFX_Data)
            {
                float[] ItemValue = Item.Value;
                ListViewItem ItemToAdd = new ListViewItem(new[] { ItemValue[0].ToString(), ItemValue[1].ToString("n1"), ItemValue[2].ToString("n1"), ItemValue[3].ToString("n1"), ItemValue[4].ToString("n6"), ItemValue[5].ToString(), ItemValue[6].ToString(), ItemValue[7].ToString() });

                //Save check in case the object is disposed. 
                try
                {
                    ListView_HashTableData.Invoke((MethodInvoker)delegate
                    {
                        ListView_HashTableData.Items.Add(ItemToAdd);
                    });
                }
                catch
                {

                }

                GenericFunctions.SetProgramStateShowToStatusBar("Checking Hashcode: " + Item.Key);
            }
        }

        private void BackgroundWorker_LoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ListView_HashTableData.Enabled = true;
            Button_Reload.Enabled = true;
            button_generateFile.Enabled = true;

            /*Set Program status*/
            GenericFunctions.SetProgramStateShowToStatusBar(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private void Button_generateFile_Click(object sender, EventArgs e)
        {
            if (FolderSavePath.ShowDialog() == DialogResult.OK)
            {
                GenerateSFXDataFiles.GenerateSFXDataBinaryFile(FolderSavePath.SelectedPath);
            }
        }

        private void Button_Reload_Click(object sender, EventArgs e)
        {
            /*--Load Sound Data Hashcodes--*/
            GenericFunctions.SetProgramStateShowToStatusBar("Loading Hashcodes Sounds Data");

            Hashcodes.LoadSoundDataFile();

            /*Update Status Bar*/
            GenericFunctions.SetProgramStateShowToStatusBar(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));

            if (!BackgroundWorker_LoadData.IsBusy)
            {
                ListView_HashTableData.Enabled = false;
                Button_Reload.Enabled = false;
                button_generateFile.Enabled = false;
                BackgroundWorker_LoadData.RunWorkerAsync();
            }
        }

        private void Frm_FilelistBinGenerator_Load(object sender, EventArgs e)
        {
            if (!BackgroundWorker_LoadData.IsBusy)
            {
                ListView_HashTableData.Enabled = false;
                Button_Reload.Enabled = false;
                button_generateFile.Enabled = false;
                BackgroundWorker_LoadData.RunWorkerAsync();
            }
        }
        private void ListView_HashTableData_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedHash;
            string Hashcode;

            if (ListView_HashTableData.SelectedItems.Count > 0)
            {
                ListViewItem SelectedItem = ListView_HashTableData.SelectedItems[0];
                if (SelectedItem.SubItems.Count > 0)
                {
                    SelectedHash = int.Parse(SelectedItem.SubItems[0].Text);
                    Hashcode = "0x1A" + SelectedHash.ToString("X8").Substring(2);
                    if (Hashcodes.SFX_Defines.ContainsKey(Hashcode))
                    {
                        Textbox_SelectedHashcode.Text = Hashcodes.GetHashcodeLabel(Hashcodes.SFX_Defines, Hashcode);
                    }
                    else
                    {
                        if (!Hashcode.Equals("0x1A000000"))
                        {
                            MessageBox.Show("The hashcode: " + Hashcode + " has not found, please check the \"SFX_Defines\" hashtable", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }
    }
}