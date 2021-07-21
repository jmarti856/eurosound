using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EuroSound_Application.CustomControls.statisticsForm
{
    public partial class EuroSound_Graphics : Form
    {
        private Dictionary<uint, uint> DataToShow = new Dictionary<uint, uint>();

        public EuroSound_Graphics(Dictionary<uint, uint> NumericData)
        {
            InitializeComponent();
            DataToShow = NumericData;
        }

        private void EuroSound_Graphics_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<uint, uint> ItemToAdd in DataToShow)
            {
                Chart_SoundsStatics.Series[0].Points.AddXY(ItemToAdd.Key.ToString() + "Hz", ItemToAdd.Value);
            }
        }
    }
}
