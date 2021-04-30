using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application.CustomControls.WarningsList
{
    public partial class EuroSound_ErrorsAndWarningsList : Form
    {
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private IEnumerable<string> ErrorsAndWarningsListToPrint;

        public EuroSound_ErrorsAndWarningsList(IEnumerable<string> ErrorsAndWarningsList)
        {
            InitializeComponent();

            ErrorsAndWarningsListToPrint = ErrorsAndWarningsList;
        }

        //*===============================================================================================
        //* FORM Events
        //*===============================================================================================
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

        //*===============================================================================================
        //* FORM Controls Events
        //*===============================================================================================
        private void Button_Copy_Click(object sender, EventArgs e)
        {
            Thread CopyDataToClipboard = new Thread(() =>
            {
                string FinalFile = string.Empty;
                Clipboard.Clear();

                ListView_Reports.Invoke((MethodInvoker)delegate
                {
                    foreach (ListViewItem lvItem in ListView_Reports.Items)
                    {
                        string MessageStatus;

                        if (lvItem.ImageIndex == 0)
                        {
                            MessageStatus = "Error:";
                        }
                        else if (lvItem.ImageIndex == 1)
                        {
                            MessageStatus = "Warning:";
                        }
                        else
                        {
                            MessageStatus = "Info:";
                        }
                        FinalFile += string.Format("{0} {1}\n", MessageStatus, lvItem.SubItems[1].Text);
                    }
                });
                Clipboard.SetText(FinalFile);
            })
            {
                IsBackground = true
            };
            CopyDataToClipboard.SetApartmentState(ApartmentState.STA);
            CopyDataToClipboard.Start();
        }

        private void Button_Print_Click(object sender, EventArgs e)
        {
            // If the result is OK then print the document.
            PrintDialog_Document = new PrintDialog
            {
                Document = PrintDocument_DocumentToPrint
            };

            if (PrintDialog_Document.ShowDialog() == DialogResult.OK)
            {
                PrintDocument_DocumentToPrint.Print();
            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            Close();
        }

        //*===============================================================================================
        //* PRINT EVENTS
        //*===============================================================================================
        private void PrintDocument_DocumentToPrint_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Get document to string
            foreach (ListViewItem lvItem in ListView_Reports.Items)
            {
                string MessageStatus;

                if (lvItem.ImageIndex == 0)
                {
                    MessageStatus = "Error:";
                }
                else if (lvItem.ImageIndex == 1)
                {
                    MessageStatus = "Warning:";
                }
                else
                {
                    MessageStatus = "Info:";
                }
                e.Graphics.DrawString(string.Format("{0} {1}\n", MessageStatus, lvItem.SubItems[1].Text), DefaultFont, new SolidBrush(Color.Black), 0, 0);
            }
        }
    }
}