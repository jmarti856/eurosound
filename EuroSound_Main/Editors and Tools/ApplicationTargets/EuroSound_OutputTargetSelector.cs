using EuroSound_Application.ApplicationTargets;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EuroSound_Application.CustomControls
{
    public partial class EuroSound_OutputTargetSelector : Form
    {
        private Dictionary<uint, EXAppTarget> availableTargets;
        public EXAppTarget SelectedTarget;

        public EuroSound_OutputTargetSelector(Dictionary<uint, EXAppTarget> projectTargets)
        {
            InitializeComponent();
            availableTargets = projectTargets;
        }

        private void EuroSound_OutputTargetSelector_Load(object sender, System.EventArgs e)
        {
            foreach (KeyValuePair<uint, EXAppTarget> itemTarget in availableTargets)
            {
                ListView_TargetsList.Items.Add(new ListViewItem(itemTarget.Value.Name) { Tag = itemTarget.Key });
            }
        }

        private void ListView_TargetsList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ListView_TargetsList.SelectedItems.Count > 0)
            {
                Button_OK.Enabled = true;
            }
        }

        private void Button_OK_Click(object sender, System.EventArgs e)
        {
            SelectedTarget = availableTargets[uint.Parse(ListView_TargetsList.SelectedItems[0].Tag.ToString())];
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
