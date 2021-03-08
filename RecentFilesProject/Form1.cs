using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace RecentFilesProject
{
    public partial class Form1 : Form
    {
        protected MostRecentFilesMenu mruMenu;
        static string mruRegKey = "SOFTWARE\\Eurocomm\\EuroSound\\RecentFilesT";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mruMenu = new MruStripMenuInline(MainMenu_File, MenuItemFile_RecentFiles, new MostRecentFilesMenu.ClickedHandler(OnMruFile), mruRegKey, 16);
            mruMenu.LoadFromRegistry();
        }

		private void OnMruFile(int number, String filename)
		{
			DialogResult result = MessageBox.Show(
							"MRU open (" + number.ToString() + ") " + filename
							+ "\n\nClick \"Yes\" to mimic the behavior of the file opening successfully\n"
							+ "Click \"No\" to mimic the behavior of the file open failing"
							, "MruToolStripMenu Demo"
							, MessageBoxButtons.YesNo);

			// You may want to use exception handlers in your code

			if (result == DialogResult.Yes)
			{
				mruMenu.SetFirstFile(number);
			}
			else
			{
				MessageBox.Show("The file '" + filename + "'cannot be opened and will be removed from the Recent list(s)"
					, "MruToolStripMenu Demo"
					, MessageBoxButtons.OK
					, MessageBoxIcon.Error);
				mruMenu.RemoveFile(number);
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
			mruMenu.SaveToRegistry();
        }

        private void MenuItemFile_OpenESF_Click(object sender, EventArgs e)
        {
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
			openFileDialog.FilterIndex = 2;
			openFileDialog.RestoreDirectory = true;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				mruMenu.AddFile(openFileDialog.FileName);
			}
		}
    }
}
