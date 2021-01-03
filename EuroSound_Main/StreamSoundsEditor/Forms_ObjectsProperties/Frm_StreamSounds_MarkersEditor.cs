using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EuroSound_Application.StreamSounds
{
    public partial class Frm_StreamSounds_MarkersEditor : Form
    {
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private EXSoundStream SelectedSound, TemporalSelectedSound;
        Dictionary<uint, string> MarkerTypes;
        private uint v_MarkerIndex, v_MarkerCounter;

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        public Frm_StreamSounds_MarkersEditor(EXSoundStream Sound)
        {
            InitializeComponent();
            SelectedSound = Sound;
        }

        private void Frm_StreamSounds_MarkersEditor_Load(object sender, EventArgs e)
        {
            //Combobox Markers
            MarkerTypes = new Dictionary<uint, string>
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
                StartMarkers = new List<EXStreamStartMarker>(SelectedSound.StartMarkers),
                Markers = new List<EXStreamMarker>(SelectedSound.Markers)

            };
            Reflection.CopyProperties(SelectedSound, TemporalSelectedSound);

            //IDs
            v_MarkerCounter = TemporalSelectedSound.MarkerDataCounterID;
            v_MarkerIndex = TemporalSelectedSound.MarkerID;

            //Print Start Markers
            foreach (EXStreamStartMarker Marker in TemporalSelectedSound.StartMarkers)
            {
                AddMarkerStartToListView(Marker);
            }
            //Print Markers
            foreach (EXStreamMarker MarkerData in TemporalSelectedSound.Markers)
            {
                AddMarkerDataToListView(MarkerData);
            }
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_AddMarker_Click(object sender, EventArgs e)
        {
            uint MarkerType;

            //Get type of the selected combobox value
            if (ComboBox_MarkerType.SelectedValue != null)
            {
                MarkerType = (uint)ComboBox_MarkerType.SelectedValue;

                //Create a new marker
                EXStreamStartMarker NewMarker = new EXStreamStartMarker
                {
                    MarkerPos = v_MarkerCounter,
                    IsInstant = 0,
                    InstantBuffer = 0
                };

                NewMarker.StateA = 0;
                NewMarker.StateB = 0;

                //Create a new marker data
                EXStreamMarker NewMarkerData = new EXStreamMarker
                {
                    Name = (int)v_MarkerIndex,
                    Position = (uint)Numeric_MarkerPosition.Value,
                    MusicMakerType = MarkerType,
                    Flags = Convert.ToUInt32(Textbox_Flags.Text),
                    Extra = Convert.ToUInt32(Textbox_Extra.Text),
                    LoopStart = Convert.ToUInt32(Numeric_MarkerLoopStart.Value),
                    MarkerCount = v_MarkerIndex,
                    LoopMarkerCount = 0
                };

                //ADD ITEMS TO LIST
                //NewMarker.MarkersData.Add(NewMarkerData);
                //TemporalSelectedSound.Markers.Add(NewMarker);

                AddMarkerStartToListView(NewMarker);
                AddMarkerDataToListView(NewMarkerData);

                //Add new value to IDs
                v_MarkerIndex += 1;
                if (MarkerType == 6)
                {
                    v_MarkerCounter += 2;
                }
                else
                {
                    v_MarkerCounter += 1;
                }

                //Update IDs in the parent sound
                TemporalSelectedSound.MarkerID = v_MarkerIndex;
                TemporalSelectedSound.MarkerDataCounterID = v_MarkerCounter;
            }
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            TemporalSelectedSound.Markers.Clear();
            ListView_Markers.Items.Clear();
            ListView_MarkerData.Items.Clear();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            SelectedSound.MarkerDataCounterID = TemporalSelectedSound.MarkerDataCounterID;
            SelectedSound.MarkerID = TemporalSelectedSound.MarkerID;
            SelectedSound.Markers = TemporalSelectedSound.Markers;

            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            TemporalSelectedSound = null;
            Close();
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void AddMarkerDataToListView(EXStreamMarker MarkerItem)
        {
            ListViewItem Marker = new ListViewItem(new[]
            { 
                //Name
                MarkerItem.Name.ToString(),
                //Position
                MarkerItem.Position.ToString(), 
                //Type
                MarkerTypes[MarkerItem.MusicMakerType],
                //Flags
                MarkerItem.Flags.ToString(),
                //Extra
                MarkerItem.Extra.ToString(), 
                //Loop Start
                MarkerItem.LoopStart.ToString(), 
                //Marker Count
                MarkerItem.MarkerCount.ToString(),
                //Loop Marker COunt
                MarkerItem.LoopMarkerCount.ToString(),
            });
            ListView_MarkerData.Items.Add(Marker);
        }

        private void AddMarkerStartToListView(EXStreamStartMarker MarkerItem)
        {
            ListViewItem Marker = new ListViewItem(new[]
            {
                MarkerItem.MarkerPos.ToString(),
                MarkerItem.IsInstant.ToString(),
                MarkerItem.InstantBuffer.ToString(),
                MarkerItem.StateA.ToString(),
                MarkerItem.StateB.ToString()
            });
            ListView_Markers.Items.Add(Marker);
        }
    }
}
