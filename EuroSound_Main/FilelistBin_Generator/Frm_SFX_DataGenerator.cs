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

        private void Frm_FilelistBinGenerator_Load(object sender, EventArgs e)
        {
            Hashcodes.AddHashcodesToCombobox(combobox_hashcode, Hashcodes.SFX_Defines);
            ProgressBar_Loading.Maximum = Hashcodes.SFX_Data.Keys.Count;

            if (!BackgroundWorker_LoadData.IsBusy)
            {
                BackgroundWorker_LoadData.RunWorkerAsync();
            }
        }

        private void Button_add_Click(object sender, EventArgs e)
        {

        }

        private void Button_deleteselection_Click(object sender, EventArgs e)
        {

        }

        private void Button_generateFile_Click(object sender, EventArgs e)
        {
            if (FolderSavePath.ShowDialog() == DialogResult.OK)
            {
                GenerateSFXDataFiles.GenerateSFXDataBinaryFile(FolderSavePath.SelectedPath, listView_ColumnSortingClick1);
            }
        }

        private void BackgroundWorker_LoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (KeyValuePair<string, float[]> Item in Hashcodes.SFX_Data)
            {
                float[] ItemValue = Item.Value;
                ListViewItem ItemToAdd = new ListViewItem(new[] { ItemValue[0].ToString(), ItemValue[1].ToString(), ItemValue[2].ToString(), ItemValue[3].ToString(), ItemValue[4].ToString(), ItemValue[5].ToString(), ItemValue[6].ToString(), ItemValue[7].ToString() });

                listView_ColumnSortingClick1.Invoke((MethodInvoker)delegate
                {
                    listView_ColumnSortingClick1.Items.Add(ItemToAdd);
                });

                ProgressBar_Loading.Invoke((MethodInvoker)delegate
                {
                    ProgressBar_Loading.Value += 1;
                });

                GenericFunctions.SetProgramStateShowToStatusBar("Checking Hashcode: " + Item.Key);
            }
        }

        private void BackgroundWorker_LoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressBar_Loading.Maximum = 100;
            ProgressBar_Loading.Value = 0;

            /*Set Program status*/
            GenericFunctions.SetProgramStateShowToStatusBar(GenericFunctions.ResourcesManager.GetString("StatusBar_Status_Ready"));
        }
    }
}
