using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.AudioConverter
{
    public partial class Frm_AudioConverter
    {
        private void OpenFile(string PathToFile)
        {
            if (File.Exists(PathToFile))
            {
                Process.Start(PathToFile);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Gen_FileNotExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenFolder(string PathToFolder)
        {
            if (Directory.Exists(PathToFolder))
            {
                Process.Start(PathToFolder);
            }
            else
            {
                MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Gen_DirectoryNotExists"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddFileToListView(string FileName, string DirectoryName, string FileExtension, string FileSize)
        {
            ListViewItem ItemToAdd = new ListViewItem(new[] { FileName, DirectoryName, FileExtension, FileSize });
            //Add item to list
            if (ListView_ItemsToConvert.InvokeRequired)
            {
                ListView_ItemsToConvert.Invoke((MethodInvoker)delegate
                {
                    ListView_ItemsToConvert.Items.Add(ItemToAdd);
                });
            }
            else
            {
                ListView_ItemsToConvert.Items.Add(ItemToAdd);
            }

            UpdateListViewCounter();
        }

        private void UpdateListViewCounter()
        {
            //Update counter
            if (Textbox_ItemsCount.InvokeRequired)
            {
                Textbox_ItemsCount.Invoke((MethodInvoker)delegate
                {
                    Textbox_ItemsCount.Text = ListView_ItemsToConvert.Items.Count.ToString();
                });
            }
            else
            {
                Textbox_ItemsCount.Text = ListView_ItemsToConvert.Items.Count.ToString();
            }
        }
    }
}
