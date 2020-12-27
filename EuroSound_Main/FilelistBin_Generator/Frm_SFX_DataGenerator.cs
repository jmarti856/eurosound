using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_SFX_DataGenerator : Form
    {
        private GenerateSFXDataFiles SFXDataBin_Generator = new GenerateSFXDataFiles();

        public Frm_SFX_DataGenerator()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_SFX_DataGenerator_Shown(object sender, EventArgs e)
        {
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
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
            ListViewItem ItemToAdd;
            float[] ItemValue;

            ListView_HashTableData.Invoke((MethodInvoker)delegate
            {
                ListView_HashTableData.Items.Clear();
            });

            foreach (KeyValuePair<uint, float[]> Item in Hashcodes.SFX_Data)
            {
                if (BackgroundWorker_LoadData.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    if (Item.Key != 436207616)
                    {
                        ItemValue = Item.Value;
                        ItemToAdd = new ListViewItem(new[] { ItemValue[0].ToString(), ItemValue[1].ToString("n1"), ItemValue[2].ToString("n1"), ItemValue[3].ToString("n1"), ItemValue[4].ToString("n6"), ItemValue[5].ToString(), ItemValue[6].ToString(), ItemValue[7].ToString() });

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
                        GenericFunctions.ParentFormStatusBar.ShowProgramStatus("Checking Hashcode: " + Item.Key);
                    }
                }
                Thread.Sleep(14);
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
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        //*===============================================================================================
        //* FORM CONTROL EVENTS
        //*===============================================================================================
        private void Button_generateFile_Click(object sender, EventArgs e)
        {
            List<string> ListToPrint = SFXDataBin_Generator.GenerateSFXDataBinaryFile();
            if (ListToPrint.Count > 0)
            {
                //Show Import results
                EuroSound_ErrorsAndWarningsList ImportResults = new EuroSound_ErrorsAndWarningsList(ListToPrint)
                {
                    Text = "Output Errors",
                    ShowInTaskbar = false,
                    TopMost = true
                };
                ImportResults.ShowDialog();
                ImportResults.Dispose();
            }
        }

        private void Button_Reload_Click(object sender, EventArgs e)
        {
            /*--Load Sound Data Hashcodes--*/
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus("Loading Hashcodes Sounds Data");

            Hashcodes.LoadSoundDataFile();

            /*Update Status Bar*/
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));

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
            uint SelectedHash;

            if (ListView_HashTableData.SelectedItems.Count > 0)
            {
                ListViewItem SelectedItem = ListView_HashTableData.SelectedItems[0];
                if (SelectedItem.SubItems.Count > 0)
                {
                    SelectedHash = 436207616 + uint.Parse(SelectedItem.SubItems[0].Text);
                    if (Hashcodes.SFX_Defines.ContainsKey(SelectedHash))
                    {
                        Textbox_SelectedHashcode.Text = Hashcodes.GetHashcodeLabel(Hashcodes.SFX_Defines, SelectedHash);
                    }
                    else
                    {
                        MessageBox.Show("The hashcode: 0x" + SelectedHash.ToString("X8") + " has not found, please check the \"SFX_Defines\" hashtable", "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
    }
}