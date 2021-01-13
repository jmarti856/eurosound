using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application.CustomControls.WarningsList
{
    public partial class EuroSound_ErrorsAndWarningsList : Form
    {
        private List<string> ErrorsAndWarningsListToPrint;

        public EuroSound_ErrorsAndWarningsList(List<string> ErrorsAndWarningsList)
        {
            InitializeComponent();

            ErrorsAndWarningsListToPrint = ErrorsAndWarningsList;
        }

        private void Button_Copy_Click(object sender, EventArgs e)
        {
            string Text = string.Empty;

            Thread CopyDataToClipboard = new Thread(() =>
            {
                Clipboard.Clear();

                ListView_Reports.BeginInvoke((MethodInvoker)delegate
                {
                    foreach (ListViewItem lvItem in ListView_Reports.Items)
                    {
                        if (lvItem.SubItems[0].BackColor == Color.Red)
                        {
                            Text += "Error:    ";
                        }
                        else if (lvItem.SubItems[0].BackColor == Color.Yellow)
                        {
                            Text += "Warning:    ";
                        }
                        Text += string.Format("{0}\n", lvItem.SubItems[1].Text);
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

        private void Button_OK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EuroSound_ImportResultsList_Load(object sender, EventArgs e)
        {
            foreach (string item in ErrorsAndWarningsListToPrint)
            {
                char MessageType = item[0];
                ListViewItem Item = new ListViewItem(new[] { "", item.Substring(1) });

                if (MessageType == '0')
                {
                    Item.SubItems[0].Text = "Error";
                    Item.ImageIndex = 0;
                }
                else if (MessageType == '1')
                {
                    Item.SubItems[0].Text = "Warning";
                    Item.ImageIndex = 1;
                }
                else
                {
                    Item.SubItems[0].Text = "Info";
                    Item.ImageIndex = 2;
                }
                ListView_Reports.Items.Add(Item);
            }
            ErrorsAndWarningsListToPrint = null;
        }
    }
}