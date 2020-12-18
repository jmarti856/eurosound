using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class EuroSound_ItemUsage : Form
    {
        private List<string> UsageItemsList;

        public EuroSound_ItemUsage(List<string> ListToPrint)
        {
            InitializeComponent();
            UsageItemsList = ListToPrint;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EuroSound_ItemUsage_Shown(object sender, EventArgs e)
        {
            string[] LineSplit;
            foreach (string Item in UsageItemsList)
            {
                LineSplit = Item.Split(',');
                ListViewItem ItemToAdd = new ListViewItem(new[] { LineSplit[0], LineSplit[1] })
                {
                    ImageIndex = 0
                };
                ListView_ItemUsage.Items.Add(ItemToAdd);
            }
            UsageItemsList = null;
        }
    }
}