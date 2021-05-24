using System;
using System.Windows.Forms;

namespace RicherTextBox
{
    public partial class FindForm : Form
    {
        private int lastFound = 0;
        private RichTextBox rtbInstance = null;
        public RichTextBox RtbInstance
        {
            set { rtbInstance = value; }
            get { return rtbInstance; }
        }

        public string InitialText
        {
            set { txtSearchText.Text = value; }
            get { return txtSearchText.Text; }
        }

        public FindForm()
        {
            InitializeComponent();
            TopMost = true;
        }

        private void RtbInstance_SelectionChanged(object sender, EventArgs e)
        {
            lastFound = rtbInstance.SelectionStart;
        }

        private void BtnDone_Click(object sender, EventArgs e)
        {
            rtbInstance.SelectionChanged -= RtbInstance_SelectionChanged;
            Close();
        }

        private void BtnFindNext_Click(object sender, EventArgs e)
        {
            RichTextBoxFinds options = RichTextBoxFinds.None;
            if (chkMatchCase.Checked) options |= RichTextBoxFinds.MatchCase;
            if (chkWholeWord.Checked) options |= RichTextBoxFinds.WholeWord;

            int index = rtbInstance.Find(txtSearchText.Text, lastFound, options);
            lastFound += txtSearchText.Text.Length;
            if (index >= 0)
            {
                rtbInstance.Parent.Focus();
            }
            else
            {
                MessageBox.Show("Search string not found", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lastFound = 0;
            }

        }

        private void FindForm_Load(object sender, EventArgs e)
        {
            if (rtbInstance != null)
                rtbInstance.SelectionChanged += new EventHandler(RtbInstance_SelectionChanged);
        }
    }
}
