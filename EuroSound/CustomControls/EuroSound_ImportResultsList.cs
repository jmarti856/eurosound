using System;
using System.Drawing;
using System.Windows.Forms;

namespace EuroSound_SB_Editor
{
    public partial class EuroSound_ImportResultsList : Form
    {
        ListView Reports;
        public EuroSound_ImportResultsList(ListView ControlToPrint)
        {
            InitializeComponent();

            Reports = ControlToPrint;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EuroSound_ImportResultsList_Load(object sender, EventArgs e)
        {
            foreach (ListViewItem item in Reports.Items)
            {
                ListView_Reports.Items.Add((ListViewItem)item.Clone());
            }
            Reports.Items.Clear();
        }

        private void Button_Copy_Click(object sender, EventArgs e)
        {
            string Text = string.Empty;

            Clipboard.Clear();

            foreach (ListViewItem lvItem in ListView_Reports.Items)
            {
                if (lvItem.SubItems[0].BackColor == Color.Red)
                {
                    Text += "Error - ";
                }
                else if (lvItem.SubItems[0].BackColor == Color.Yellow)
                {
                    Text += "Warning - ";
                }
                Text += lvItem.SubItems[1].Text + Environment.NewLine;
            }

            Clipboard.SetText(Text);
        }
    }
}
