using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EuroSound_Application
{
    public partial class Frm_StreamSounds_MarkersEditor : Form
    {
        private readonly EXSoundStream SelectedSound;
        private EXSoundStream TemporalSelectedSound;
        private uint MarkerName, MarkerPos;
        public Frm_StreamSounds_MarkersEditor(EXSoundStream Sound)
        {
            InitializeComponent();
            SelectedSound = Sound;
        }

        private void Frm_StreamSounds_MarkersEditor_Load(object sender, EventArgs e)
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

            //Temporal Sounds
            TemporalSelectedSound = new EXSoundStream
            {
                IDMarkerName = SelectedSound.IDMarkerName,
                IDMarkerPos = SelectedSound.IDMarkerPos,
                Markers = SelectedSound.Markers
            };

            //IDs
            MarkerName = TemporalSelectedSound.IDMarkerName;
            MarkerPos = TemporalSelectedSound.IDMarkerPos;

            //Print Markers
            foreach (EXStreamSoundMarker Marker in TemporalSelectedSound.Markers)
            {
                foreach (EXStreamSoundMarkerData MarkerData in Marker.MarkersData)
                {
                    ListViewItem IT_Marker = new ListViewItem(new[] { MarkerData.Name.ToString(), MarkerData.Position.ToString(), MarkerData.MusicMakerType.ToString(), MarkerData.Flags.ToString(), MarkerData.Extra.ToString(), MarkerData.LoopStart.ToString(), MarkerData.MarkerCount.ToString(), MarkerData.LoopMarkerCount.ToString(), Marker.Position.ToString() });
                    ListView_Markers.Items.Add(IT_Marker);
                }
            }
        }

        private void Button_AddMarker_Click(object sender, EventArgs e)
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
                MusicMakerType = MarkerType,
                Flags = Convert.ToUInt32(Textbox_Flags.Text),
                Extra = Convert.ToUInt32(Textbox_Extra.Text),
                LoopStart = Convert.ToUInt32(Numeric_MarkerLoopStart.Value)
            };

            NewMarkerData.MarkerCount = MarkerName;
            NewMarkerData.LoopMarkerCount = 0;

            //ADD ITEMS TO LIST
            NewMarker.MarkersData.Add(NewMarkerData);
            TemporalSelectedSound.Markers.Add(NewMarker);

            MarkerName += 1;
            if (MarkerType == 6)
            {
                MarkerPos += 2;
            }
            else
            {
                MarkerPos += 1;
            }

            ListViewItem Marker = new ListViewItem(new[] { NewMarkerData.Name.ToString(), NewMarkerData.Position.ToString(), NewMarkerData.MusicMakerType.ToString(), NewMarkerData.Flags.ToString(), NewMarkerData.Extra.ToString(), NewMarkerData.LoopStart.ToString(), NewMarkerData.MarkerCount.ToString(), NewMarkerData.LoopMarkerCount.ToString(), NewMarker.Position.ToString() });
            ListView_Markers.Items.Add(Marker);

            //Update IDs in the parent sound
            TemporalSelectedSound.IDMarkerPos = MarkerPos;
            TemporalSelectedSound.IDMarkerName = MarkerName;
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            TemporalSelectedSound.Markers.Clear();
            ListView_Markers.Items.Clear();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            SelectedSound.IDMarkerName = TemporalSelectedSound.IDMarkerName;
            SelectedSound.IDMarkerPos = TemporalSelectedSound.IDMarkerPos;
            SelectedSound.Markers = TemporalSelectedSound.Markers;

            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
