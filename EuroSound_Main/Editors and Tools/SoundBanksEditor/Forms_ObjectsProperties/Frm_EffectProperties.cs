﻿using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.CustomControls.FlagsForm;
using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EuroSound_Application.SoundBanksEditor
{
    public partial class Frm_EffectProperties : Form
    {
        //*===============================================================================================
        //* Global Variables
        //*===============================================================================================
        private Form OpenForm;
        private EXSound SelectedSound;
        private string TreeNodeSoundName, SoundSection;
        private Thread DataToCombobox;

        public Frm_EffectProperties(EXSound SoundToCheck, string SoundName, string Section)
        {
            InitializeComponent();
            SelectedSound = SoundToCheck;
            TreeNodeSoundName = SoundName;
            SoundSection = Section;
        }

        //*===============================================================================================
        //* Form Load
        //*===============================================================================================
        private void Frm_EffectProperties_Load(object sender, EventArgs e)
        {
            //Sound Data defines
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsDataMD5, GlobalPreferences.HT_SoundsDataPath))
            {
                Hashcodes.LoadSoundDataFile(GlobalPreferences.HT_SoundsDataPath);
            }

            //Sound defines
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsMD5, GlobalPreferences.HT_SoundsPath))
            {
                Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
            }

            //Get Parent Form
            OpenForm = GenericFunctions.GetFormByName("Frm_Soundbanks_Main", Tag.ToString());

            //---Add data to combobox--
            AddDataToCombobox();
        }

        private void Frm_EffectProperties_Shown(object sender, EventArgs e)
        {
            //---Required for Engine X---
            numeric_duckerlength.Value = SelectedSound.DuckerLenght;
            numeric_mindelay.Value = SelectedSound.MinDelay;
            numeric_maxdelay.Value = SelectedSound.MaxDelay;
            Textbox_InnerRadius.Text = SelectedSound.InnerRadiusReal.ToString();
            Textbox_OuterRadius.Text = SelectedSound.OuterRadiusReal.ToString();
            numeric_reverbsend.Value = SelectedSound.ReverbSend;
            cbx_trackingtype.SelectedIndex = SelectedSound.TrackingType;
            numeric_maxvoices.Value = SelectedSound.MaxVoices;
            numeric_priority.Value = SelectedSound.Priority;
            numeric_ducker.Value = SelectedSound.Ducker;
            numeric_mastervolume.Value = SelectedSound.MasterVolume;
            textbox_flags.Text = SelectedSound.Flags.ToString();
            Checkbox_OutputThisSound.Checked = SelectedSound.OutputThisSound;

            //---Print Samples--
            Thread PrintSampleList = new Thread(() =>
            {
                if (SelectedSound.Samples != null)
                {
                    string SampleName;

                    foreach (KeyValuePair<uint, EXSample> sample in SelectedSound.Samples)
                    {
                        SampleName = ((Frm_Soundbanks_Main)OpenForm).TreeView_File.Nodes.Find(sample.Key.ToString(), true)[0].Text;
                        //Crate item
                        ListViewItem ItemToAdd = new ListViewItem
                        {
                            Text = SampleName,
                            Tag = sample.Key,
                            ImageIndex = 0,
                            StateImageIndex = 0
                        };

                        //Insert item
                        List_Samples.BeginInvoke((MethodInvoker)delegate
                        {
                            List_Samples.Items.Add(ItemToAdd);
                        });

                        Thread.Sleep(40);
                    }
                }
            })
            {
                IsBackground = true
            };
            PrintSampleList.Start();
        }

        //*===============================================================================================
        //* Form Events Controls
        //*===============================================================================================
        private void Button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {
            if (cbx_hashcode.SelectedValue != null)
            {
                SelectedSound.Hashcode = (uint)cbx_hashcode.SelectedValue;
            }
            SelectedSound.DuckerLenght = (short)numeric_duckerlength.Value;
            SelectedSound.MinDelay = (short)numeric_mindelay.Value;
            SelectedSound.MaxDelay = (short)numeric_maxdelay.Value;
            SelectedSound.InnerRadiusReal = Convert.ToInt16(Textbox_InnerRadius.Text);
            SelectedSound.OuterRadiusReal = Convert.ToInt16(Textbox_OuterRadius.Text);
            SelectedSound.ReverbSend = (sbyte)numeric_reverbsend.Value;
            SelectedSound.TrackingType = (sbyte)cbx_trackingtype.SelectedIndex;
            SelectedSound.MaxVoices = (sbyte)numeric_maxvoices.Value;
            SelectedSound.Priority = (sbyte)numeric_priority.Value;
            SelectedSound.Ducker = (sbyte)numeric_ducker.Value;
            SelectedSound.MasterVolume = (sbyte)numeric_mastervolume.Value;
            SelectedSound.Flags = Convert.ToUInt16(textbox_flags.Text);
            SelectedSound.OutputThisSound = Checkbox_OutputThisSound.Checked;

            //--Change icon in the parent form--
            TreeNode[] Results = ((Frm_Soundbanks_Main)OpenForm).TreeView_File.Nodes.Find(TreeNodeSoundName, true);
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

            Close();
        }

        private void Cbx_hashcode_Click(object sender, EventArgs e)
        {
            //Sound defines
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsMD5, GlobalPreferences.HT_SoundsPath))
            {
                Hashcodes.LoadSoundHashcodes(GlobalPreferences.HT_SoundsPath);
                if (DataToCombobox != null)
                {
                    DataToCombobox.Abort();
                }
                AddDataToCombobox();
            }

            //Sound Data defines
            if (GenericFunctions.FileIsModified(GlobalPreferences.HT_SoundsDataMD5, GlobalPreferences.HT_SoundsDataPath))
            {
                Hashcodes.LoadSoundDataFile(GlobalPreferences.HT_SoundsDataPath);
            }
        }

        private void Cbx_hashcode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            uint KeyToCheck = Convert.ToUInt32(cbx_hashcode.SelectedValue) - 0x1A000000;
            float[] SFXValues = Hashcodes.SFX_Data.FirstOrDefault(x => x.Value[0] == KeyToCheck).Value;

            if (SFXValues != null)
            {
                //[0] HashCode;    --USED--
                //[1] InnerRadius; --USED--
                //[2] OuterRadius; --USED--
                //[3] Altertness;
                //[4] Duration;
                //[5] Looping;
                //[6] Tracking3d; --USED--
                //[7] SampleStreamed;

                Textbox_InnerRadius.Text = SFXValues[1].ToString();
                Textbox_OuterRadius.Text = SFXValues[2].ToString();
                cbx_trackingtype.SelectedIndex = int.Parse(SFXValues[6].ToString());
            }
            else
            {
                Textbox_InnerRadius.Text = "0";
                Textbox_OuterRadius.Text = "0";
                cbx_trackingtype.SelectedIndex = 0;
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("HashcodeNotFoundInSFXDataHashTable"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Textbox_flags_Click(object sender, EventArgs e)
        {
            using (EuroSound_FlagsForm FormFlags = new EuroSound_FlagsForm(int.Parse(textbox_flags.Text), "SoundFlags", 16))
            {
                FormFlags.Text = "Sound Flags";
                FormFlags.Tag = Tag;
                FormFlags.Owner = this;

                if (FormFlags.ShowDialog() == DialogResult.OK)
                {
                    textbox_flags.Text = FormFlags.CheckedFlags.ToString();
                }
            }
        }

        private void List_Samples_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (List_Samples.SelectedItems.Count > 0)
            {
                string SampleName = List_Samples.SelectedItems[0].Text;
                uint SampleID = (uint)List_Samples.SelectedItems[0].Tag;
                EXSample SelectedSample = EXSoundbanksFunctions.ReturnSampleFromSound(SelectedSound, SampleID);

                if (SelectedSample != null)
                {
                    if (SoundSection.Equals("Sounds"))
                    {
                        Frm_SampleProperties FormSampleProps = new Frm_SampleProperties(SelectedSample, EXSoundbanksFunctions.SubSFXFlagChecked(SelectedSound.Flags))
                        {
                            Text = GenericFunctions.TruncateLongString(SampleName, 25) + " - Properties",
                            Tag = Tag,
                            Owner = this,
                            ShowInTaskbar = false
                        };
                        FormSampleProps.ShowDialog();
                        FormSampleProps.Dispose();
                    }
                    else
                    {
                        using (Frm_NewStreamSound AddStreamSound = new Frm_NewStreamSound(SelectedSample))
                        {
                            AddStreamSound.Text = GenericFunctions.TruncateLongString(SampleName, 25) + " - Properties";
                            AddStreamSound.Tag = Tag;
                            AddStreamSound.Owner = this;
                            AddStreamSound.ShowInTaskbar = false;
                            AddStreamSound.ShowDialog();

                            if (AddStreamSound.DialogResult == DialogResult.OK)
                            {
                                SelectedSample.FileRef = (short)AddStreamSound.SelectedSound;
                            }
                        };
                    }
                }
            }
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void AddDataToCombobox()
        {
            //---AddDataToCombobox
            DataToCombobox = new Thread(() =>
            {
                cbx_hashcode.BeginInvoke((MethodInvoker)delegate
                {
                    cbx_hashcode.Enabled = false;
                    cbx_hashcode.DataSource = null;
                    cbx_hashcode.Items.Clear();
                    cbx_hashcode.DataSource = Hashcodes.SFX_Defines.OrderBy(o => o.Value).ToList();
                });
                cbx_hashcode.BeginInvoke((MethodInvoker)async delegate
                {
                    await Task.Delay(50);
                    cbx_hashcode.ValueMember = "Key";
                    cbx_hashcode.DisplayMember = "Value";
                    cbx_hashcode.Update();
                    cbx_hashcode.SelectedValue = SelectedSound.Hashcode;
                    cbx_hashcode.Enabled = true;
                });
            })
            {
                IsBackground = true
            };
            DataToCombobox.Start();
        }
    }
}