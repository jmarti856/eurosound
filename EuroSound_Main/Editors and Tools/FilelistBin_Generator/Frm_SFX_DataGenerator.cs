using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.CustomControls.WarningsList;
using EuroSound_Application.GenerateDataBinaryFile;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application.SFXData
{
    public partial class Frm_SFX_DataGenerator : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private WindowsRegistryFunctions WRegFunctions = new WindowsRegistryFunctions();
        private GenerateSFXDataFiles SFXDataBin_Generator = new GenerateSFXDataFiles();
        private Thread LoadSfxDataTable;

        public Frm_SFX_DataGenerator()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_SFX_DataGenerator_Load(object sender, EventArgs e)
        {
            //Load Preferences
            using (RegistryKey WindowStateConfig = WRegFunctions.ReturnRegistryKey("WindowState"))
            {
                bool IsIconic = Convert.ToBoolean(WindowStateConfig.GetValue("SFXData_IsIconic", 0));
                bool IsMaximized = Convert.ToBoolean(WindowStateConfig.GetValue("SFXData_IsMaximized", 0));

                if (IsIconic)
                {
                    WindowState = FormWindowState.Minimized;
                }
                else if (IsMaximized)
                {
                    // Fixes bug where loading form maximised in MDI window shows incorrect icon. 
                    Icon = Icon.Clone() as Icon;
                    WindowState = FormWindowState.Maximized;
                }
                else
                {
                    Location = new Point(Convert.ToInt32(WindowStateConfig.GetValue("SFXData_PositionX", 0)), Convert.ToInt32(WindowStateConfig.GetValue("SFXData_PositionY", 0)));
                }
                Width = Convert.ToInt32(WindowStateConfig.GetValue("SFXData_Width", 997));
                Height = Convert.ToInt32(WindowStateConfig.GetValue("SFXData_Height", 779));

                WindowStateConfig.Close();
            }

            //Add Hashcodes to combobox
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsMD5, GlobalPreferences.HT_SoundsPath))
            {
                Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
            }
        }

        private void Frm_SFX_DataGenerator_Shown(object sender, EventArgs e)
        {
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
            Hashcodes.AddDataToCombobox(Combobox_LabelHashcodes, Hashcodes.SFX_Defines);

            LoadSfxDataTable = new Thread(LoadDataFromHashtable)
            {
                IsBackground = true
            };
            LoadSfxDataTable.Start();
        }

        private void Frm_SFX_DataGenerator_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Stop thread
            LoadSfxDataTable.Abort();

            //Dispose buttons
            button_generateFile.Dispose();
            Button_Reload.Dispose();
            ListView_HashTableData.Dispose();
            Combobox_LabelHashcodes.Dispose();

            //Clear array
            SFXDataBin_Generator = null;


            WRegFunctions.SaveWindowState("SFXData", Location.X, Location.Y, Width, Height, WindowState == FormWindowState.Minimized, WindowState == FormWindowState.Maximized);

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private void LoadDataFromHashtable()
        {
            ListViewItem ItemToAdd;
            float[] ItemValue;

            ListView_HashTableData.BeginInvoke((MethodInvoker)delegate
            {
                ListView_HashTableData.Enabled = false;
                ListView_HashTableData.Items.Clear();
            });

            Button_Reload.BeginInvoke((MethodInvoker)delegate
            {
                Button_Reload.Enabled = false;
            });

            button_generateFile.BeginInvoke((MethodInvoker)delegate
            {
                button_generateFile.Enabled = false;
            });

            Combobox_LabelHashcodes.BeginInvoke((MethodInvoker)delegate
            {
                Combobox_LabelHashcodes.Enabled = false;
            });

            Button_Search.BeginInvoke((MethodInvoker)delegate
            {
                Button_Search.Enabled = false;
            });

            foreach (KeyValuePair<uint, float[]> Item in Hashcodes.SFX_Data)
            {
                if (Item.Key != 436207616)
                {
                    ItemValue = Item.Value;
                    ItemToAdd = new ListViewItem(new[] { ItemValue[0].ToString(), ItemValue[1].ToString("n1"), ItemValue[2].ToString("n1"), ItemValue[3].ToString("n1"), ItemValue[4].ToString("n6"), ItemValue[5].ToString(), ItemValue[6].ToString(), ItemValue[7].ToString() })
                    {
                        Tag = Item.Key
                    };

                    //Save check in case the object is disposed. 
                    try
                    {
                        ListView_HashTableData.Invoke((MethodInvoker)delegate
                        {
                            ListView_HashTableData.Items.Add(ItemToAdd);
                        });
                    }
                    catch (ObjectDisposedException)
                    {

                    }
                    GenericFunctions.ParentFormStatusBar.ShowProgramStatus("Checking Hashcode: " + Item.Key);
                }
                Thread.Sleep(14);
            }

            ListView_HashTableData.BeginInvoke((MethodInvoker)delegate
            {
                ListView_HashTableData.Enabled = true;
            });


            Button_Reload.BeginInvoke((MethodInvoker)delegate
            {
                Button_Reload.Enabled = true;
            });

            button_generateFile.BeginInvoke((MethodInvoker)delegate
            {
                button_generateFile.Enabled = true;
            });

            Combobox_LabelHashcodes.BeginInvoke((MethodInvoker)delegate
            {
                Combobox_LabelHashcodes.Enabled = true;
            });

            Button_Search.BeginInvoke((MethodInvoker)delegate
            {
                Button_Search.Enabled = true;
            });

            /*Set Program status*/
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        //*===============================================================================================
        //* FORM CONTROL EVENTS
        //*===============================================================================================
        private void Combobox_LabelHashcodes_Click(object sender, EventArgs e)
        {
            //Add Hashcodes to combobox
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsMD5, GlobalPreferences.HT_SoundsPath))
            {
                Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
                Hashcodes.AddDataToCombobox(Combobox_LabelHashcodes, Hashcodes.SFX_Defines);
            }
        }

        private void Button_Search_Click(object sender, EventArgs e)
        {
            uint Hashcode, ItemTag;

            //Deselect all items
            ListView_HashTableData.SelectedIndices.Clear();

            //Get hashcode form combobox
            Hashcode = Convert.ToUInt32(Combobox_LabelHashcodes.SelectedValue);

            //Focus control
            ListView_HashTableData.Focus();

            //Select Item
            foreach (ListViewItem Item in ListView_HashTableData.Items)
            {
                ItemTag = Convert.ToUInt32(Item.Tag);
                if (ItemTag == Hashcode)
                {
                    Item.Selected = true;
                    Item.Focused = true;
                    Item.EnsureVisible();
                    break;
                }
            }

            //Inform User the hashcode does not exists
            if (ListView_HashTableData.SelectedItems.Count == 0)
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("SFXDataHashcodeNotFound"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Button_generateFile_Click(object sender, EventArgs e)
        {
            List<string> ListToPrint = SFXDataBin_Generator.GenerateSFXDataBinaryFile();
            if (ListToPrint.Count > 0)
            {
                //Show Import results
                EuroSound_ErrorsAndWarningsList ImportResults = new EuroSound_ErrorsAndWarningsList(ListToPrint)
                {
                    Text = "Output Errors"
                };
                ImportResults.ShowDialog();
                ImportResults.Dispose();
            }
        }

        private void Button_Reload_Click(object sender, EventArgs e)
        {
            //--Load Sound Data Hashcodes--
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus("Loading Hashcodes Sounds Data");

            Hashcodes.LoadSoundDataFile();

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));

            LoadSfxDataTable = new Thread(LoadDataFromHashtable)
            {
                IsBackground = true
            };
            LoadSfxDataTable.Start();
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