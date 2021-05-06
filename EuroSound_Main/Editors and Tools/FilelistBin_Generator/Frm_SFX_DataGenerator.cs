using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.ApplicationRegistryFunctions;
using EuroSound_Application.GenerateDataBinaryFile;
using EuroSound_Application.HashCodesFunctions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application.SFXData
{
    public partial class Frm_SFX_DataGenerator : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private GenerateSFXDataFiles SFXDataBin_Generator = new GenerateSFXDataFiles();
        private Thread LoadSfxDataTable;

        public Frm_SFX_DataGenerator()
        {
            InitializeComponent();

            //Buttons
            Button_Search.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("Gen_SearchHashcode")); };
            Button_Search.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            Button_Reload.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("Gen_ReloadHashTable")); };
            Button_Reload.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };

            button_generateFile.MouseDown += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(true); GenericFunctions.ParentFormStatusBar.ShowToolTipText(GenericFunctions.ResourcesManager.GetString("Gen_GenerateSFXDataFile")); };
            button_generateFile.MouseUp += (se, ev) => { GenericFunctions.ParentFormStatusBar.ToolTipModeStatus(false); };
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_SFX_DataGenerator_Load(object sender, EventArgs e)
        {
            // Fixes bug where loading form maximised in MDI window shows incorrect icon. 
            Icon = Icon.Clone() as Icon;

            //Add Hashcodes to combobox
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsMD5, GlobalPreferences.HT_SoundsPath))
            {
                Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
            }

            //Load Last State
            using (RegistryKey WindowStateConfig = WindowsRegistryFunctions.ReturnRegistryKey("WindowState"))
            {
                if (Convert.ToBoolean(WindowStateConfig.GetValue("SFXData_IsIconic", 0)))
                {
                    WindowState = FormWindowState.Minimized;
                }
                else if (Convert.ToBoolean(WindowStateConfig.GetValue("SFXData_IsMaximized", 0)))
                {
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
        }

        private void Frm_SFX_DataGenerator_Shown(object sender, EventArgs e)
        {
            //Update from title
            if (WindowState != FormWindowState.Maximized)
            {
                MdiParent.Text = "EuroSound - SFX Data Table";
            }

            Hashcodes.AddDataToCombobox(Combobox_LabelHashcodes, Hashcodes.SFX_Defines);

            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));

            //Start Thread
            LoadSfxDataTable = new Thread(LoadDataFromHashtable)
            {
                IsBackground = true
            };
            LoadSfxDataTable.Start();

            //Update File name label
            UpdateStatusBarLabels();
        }

        private void Frm_SFX_DataGenerator_Enter(object sender, EventArgs e)
        {
            //Update File name label
            UpdateStatusBarLabels();

            if (WindowState == FormWindowState.Maximized)
            {
                MdiParent.Text = "EuroSound";
            }
            else
            {
                MdiParent.Text = "EuroSound - SFX Data Table";
            }
        }


        private void Frm_SFX_DataGenerator_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                MdiParent.Text = "EuroSound";
            }
            else
            {
                MdiParent.Text = "EuroSound - SFX Data Table";
            }
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

            WindowsRegistryFunctions.SaveWindowState("SFXData", Location.X, Location.Y, Width, Height, WindowState == FormWindowState.Minimized, WindowState == FormWindowState.Maximized, 0);

            //Reset title
            MdiParent.Text = "EuroSound";

            //Update File name label
            UpdateStatusBarLabels();

            //Update Status Bar
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }

        private void UpdateStatusBarLabels()
        {
            //Update File name label
            GenericFunctions.SetCurrentFileLabel(string.Empty, "SBPanel_File");

            //Update Hashcode name label
            GenericFunctions.SetCurrentFileLabel(string.Empty, "SBPanel_Hashcode");
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void LoadDataFromHashtable()
        {
            ListView_HashTableData.Invoke((MethodInvoker)delegate
            {
                ListView_HashTableData.Enabled = false;
                ListView_HashTableData.Items.Clear();
            });

            Button_Reload.Invoke((MethodInvoker)delegate
            {
                Button_Reload.Enabled = false;
            });

            button_generateFile.Invoke((MethodInvoker)delegate
            {
                button_generateFile.Enabled = false;
            });

            Combobox_LabelHashcodes.Invoke((MethodInvoker)delegate
            {
                Combobox_LabelHashcodes.Enabled = false;
            });

            Button_Search.Invoke((MethodInvoker)delegate
            {
                Button_Search.Enabled = false;
            });

            foreach (float[] ItemValue in Hashcodes.SFX_Data)
            {
                ListViewItem ItemToAdd = new ListViewItem(new[] { ItemValue[0].ToString(), ItemValue[1].ToString("n1"), ItemValue[2].ToString("n1"), ItemValue[3].ToString("n1"), ItemValue[4].ToString("n6"), ItemValue[5].ToString(), ItemValue[6].ToString(), ItemValue[7].ToString() })
                {
                    Tag = GlobalPreferences.SfxPrefix | (uint)ItemValue[0]
                };

                //Save check in case the object is disposed. 
                try
                {
                    ListView_HashTableData.Invoke((MethodInvoker)delegate
                    {
                        ListView_HashTableData.Items.Add(ItemToAdd);
                    });

                    Textbox_TotalItems.Invoke((MethodInvoker)delegate
                    {
                        Textbox_TotalItems.Text = ListView_HashTableData.Items.Count.ToString();
                    });
                }
                catch (ObjectDisposedException)
                {

                }
                GenericFunctions.ParentFormStatusBar.ShowProgramStatus("Checking Hashcode: " + ItemValue[0]);
            }


            ListView_HashTableData.Invoke((MethodInvoker)delegate
            {
                ListView_HashTableData.Enabled = true;
            });


            Button_Reload.Invoke((MethodInvoker)delegate
            {
                Button_Reload.Enabled = true;
            });

            button_generateFile.Invoke((MethodInvoker)delegate
            {
                button_generateFile.Enabled = true;
            });

            Combobox_LabelHashcodes.Invoke((MethodInvoker)delegate
            {
                Combobox_LabelHashcodes.Enabled = true;
            });

            Button_Search.Invoke((MethodInvoker)delegate
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
            //Deselect all items
            ListView_HashTableData.SelectedIndices.Clear();

            //Get hashcode form combobox
            uint Hashcode = Convert.ToUInt32(Combobox_LabelHashcodes.SelectedValue);

            //Focus control
            ListView_HashTableData.Focus();

            //Select Item
            foreach (ListViewItem Item in ListView_HashTableData.Items)
            {
                uint ItemTag = Convert.ToUInt32(Item.Tag);
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
            IEnumerable<string> ListToPrint = SFXDataBin_Generator.GenerateSFXDataBinaryFile();
            if (ListToPrint.Any())
            {
                GenericFunctions.ShowErrorsAndWarningsList(ListToPrint, "Output Errors", this);
            }
        }

        private void Button_Reload_Click(object sender, EventArgs e)
        {
            //--Load Sound Data Hashcodes--
            GenericFunctions.ParentFormStatusBar.ShowProgramStatus("Loading Hashcodes Sounds Data");

            Hashcodes.LoadSoundDataFile(GlobalPreferences.HT_SoundsDataPath);

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
            if (ListView_HashTableData.SelectedItems.Count > 0)
            {
                ListViewItem SelectedItem = ListView_HashTableData.SelectedItems[0];
                if (SelectedItem.SubItems.Count > 0)
                {
                    uint SelectedHash = GlobalPreferences.SfxPrefix | uint.Parse(SelectedItem.SubItems[0].Text);
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