using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SoundBanks_Editor
{
    public partial class EuroSound_ErrorsAndWarningsList : Form
    {
        List<string> ErrorsAndWarningsListToPrint;
        public EuroSound_ErrorsAndWarningsList(List<string> ErrorsAndWarningsList)
        {
            InitializeComponent();

            ErrorsAndWarningsListToPrint = ErrorsAndWarningsList;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EuroSound_ImportResultsList_Load(object sender, EventArgs e)
        {
            foreach (string item in ErrorsAndWarningsListToPrint)
            {
                char MessageType = item[0];
                ListViewItem Item = new ListViewItem(new[] { "", item.Substring(1) });

                if (MessageType == '0')
                {
                    Item.SubItems[0].BackColor = Color.Red;
                }
                else if (MessageType == '1')
                {
                    Item.SubItems[0].BackColor = Color.Yellow;
                }
                else
                {
                    Item.SubItems[0].BackColor = Color.Green;
                }
                Item.UseItemStyleForSubItems = false;
                ListView_Reports.Items.Add(Item);
            }

        }

        private void Button_Copy_Click(object sender, EventArgs e)
        {
            string Text = string.Empty;

            Thread CopyDataToClipboard = new Thread(delegate ()
            {
                Clipboard.Clear();

                ListView_Reports.Invoke((MethodInvoker)delegate
                {
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
                });

                Clipboard.SetText(Text);
            })
            {
                IsBackground = true
            };
            CopyDataToClipboard.SetApartmentState(ApartmentState.STA);
            CopyDataToClipboard.Start();

        }
    }
}
