using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_StreamSounds_MarkersEditor : Form
    {
        EXSoundStream SelectedSound;
        uint MarkerName, MarkerPos;
        public Frm_StreamSounds_MarkersEditor(EXSoundStream Sound)
        {
            InitializeComponent();
            SelectedSound = Sound;
        }

        private void Frm_StreamSounds_MarkersEditor_Load(object sender, System.EventArgs e)
        {
            Dictionary<uint, string> MarkerTypes = new Dictionary<uint, string>
            {
                { 10, "Start" },
                { 9, "End" },
                { 7, "Goto" },
                { 6, "Loop" },
                { 5, "Pause" },
                { 0, "Jump" }
            };
            ComboBox_MarkerType.DataSource = new BindingSource(MarkerTypes, null);
            ComboBox_MarkerType.DisplayMember = "Value";
            ComboBox_MarkerType.ValueMember = "Key";

            //IDs
            MarkerName = SelectedSound.IDMarkerName;
            MarkerPos = SelectedSound.IDMarkerPos;
        }

        private void Button_AddMarker_Click(object sender, System.EventArgs e)
        {
            uint MarkerType = 10;

            if (ComboBox_MarkerType.SelectedValue != null)
            {
                MarkerType = (uint)ComboBox_MarkerType.SelectedValue;
            }

            EXStreamSoundMarker NewMarker = new EXStreamSoundMarker
            {
                Position = MarkerPos,
                IsInstant = 0,
                InstantBuffer = 0
            };
            NewMarker.State[0] = 0;
            NewMarker.State[1] = 0;

            EXStreamSoundMarkerData NewMarkerData = new EXStreamSoundMarkerData
            {
                Name = MarkerName,
                Position = (uint)Numeric_MarkerPosition.Value,
                MusicMakerType = 10,
                Flags = Convert.ToUInt32(Textbox_Flags.Text),
                Extra = Convert.ToUInt32(Textbox_Extra.Text),
                LoopStart = 0
            };

            NewMarkerData.MakerCount = MarkerName;
            NewMarkerData.LoopMarkerCount = 0;

            //ADD ITEMS TO LIST
            NewMarker.MarkersData.Add(NewMarkerData);
            SelectedSound.Markers.Add(NewMarker);

            MarkerName += 1;
            if (MarkerType == 6)
            {
                MarkerPos += 2;
            }
            else
            {
                MarkerPos += 1;
            }

            ListViewItem Marker = new ListViewItem(new[] { NewMarkerData.Name.ToString(), NewMarkerData.Position.ToString(), NewMarkerData.MusicMakerType.ToString(), NewMarkerData.Flags.ToString(), NewMarkerData.Extra.ToString(), NewMarkerData.LoopStart.ToString(), NewMarkerData.MakerCount.ToString(), NewMarkerData.LoopMarkerCount.ToString(), NewMarker.Position.ToString() });
            ListView_Markers.Items.Add(Marker);

            //Update IDs in the parent sound
            SelectedSound.IDMarkerPos = MarkerPos;
            SelectedSound.IDMarkerName = MarkerName;
        }
    }
}
