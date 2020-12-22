using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_SFX_DataGenerator : Form
    {
        private GenerateSFXDataFiles SFXDataBin_Generator;

        public Frm_SFX_DataGenerator()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_FilelistBinGenerator_Load(object sender, EventArgs e)
        {
            SFXDataBin_Generator = new GenerateSFXDataFiles();

            if (!BackgroundWorker_LoadData.IsBusy)
            {
                ListView_HashTableData.Enabled = false;
                Button_Reload.Enabled = false;
                button_generateFile.Enabled = false;
                BackgroundWorker_LoadData.RunWorkerAsync();
            }
        }

        private void Frm_SFX_DataGenerator_FormClosing(object sender, FormClosingEventArgs e)
        {
            BackgroundWorker_LoadData.CancelAsync();
        }

        //*===============================================================================================
        //* BACKGROUND WORKER EVENTS
        //*===============================================================================================
        private void BackgroundWorker_LoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            ListView_HashTableData.Invoke((MethodInvoker)delegate
            {
                ListView_HashTableData.Items.Clear();
            });

            foreach (KeyValuePair<Int32, float[]> Item in Hashcodes.SFX_Data)
            {
                if (BackgroundWorker_LoadData.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
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
                    Thread.Sleep(5);

                    GenericFunctions.SetStatusToStatusBar("Checking Hashcode: " + Item.Key);
                }
            }
        }

        private void BackgroundWorker_LoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                ListView_HashTableData.Dispose();
                BackgroundWorker_LoadData.Dispose();
            }
            else
            {
                ListView_HashTableData.Enabled = true;
                Button_Reload.Enabled = true;
                button_generateFile.Enabled = true;
            }

            /*Set Program status*/
            GenericFunctions.SetStatusToStatusBar(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        //*===============================================================================================
        //* FORM CONTROL EVENTS
        //*===============================================================================================
        private void Button_generateFile_Click(object sender, EventArgs e)
        {
            if (FolderSavePath.ShowDialog() == DialogResult.OK)
            {
                SFXDataBin_Generator.GenerateSFXDataBinaryFile(FolderSavePath.SelectedPath);
            }
        }

        private void Button_Reload_Click(object sender, EventArgs e)
        {
            /*--Load Sound Data Hashcodes--*/
            GenericFunctions.SetStatusToStatusBar("Loading Hashcodes Sounds Data");

            Hashcodes.LoadSoundDataFile();

            /*Update Status Bar*/
            GenericFunctions.SetStatusToStatusBar(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));

            if (!BackgroundWorker_LoadData.IsBusy)
            {
                ListView_HashTableData.Enabled = false;
                Button_Reload.Enabled = false;
                button_generateFile.Enabled = false;
                BackgroundWorker_LoadData.RunWorkerAsync();
            }
        }

        //*===============================================================================================
        //* LIST VIEW DATA EVENTS
        //*===============================================================================================
        private void ListView_HashTableData_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedHash;
            Int32 Hashcode;

            if (ListView_HashTableData.SelectedItems.Count > 0)
            {
                ListViewItem SelectedItem = ListView_HashTableData.SelectedItems[0];
                if (SelectedItem.SubItems.Count > 0)
                {
                    SelectedHash = int.Parse(SelectedItem.SubItems[0].Text);
                    Hashcode = Convert.ToInt32("0x1A" + SelectedHash.ToString("X8").Substring(2),16);
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