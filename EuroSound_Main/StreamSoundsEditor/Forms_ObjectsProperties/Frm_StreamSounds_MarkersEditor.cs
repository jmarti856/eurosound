﻿using System;
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
        private Dictionary<uint, string> MarkerTypes;
        private uint v_MarkerPos, v_MarkerCount;

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
            v_MarkerCount = (uint)TemporalSelectedSound.StartMarkers.Count;
            v_MarkerPos = TemporalSelectedSound.MarkerDataCounterID;

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
            MarkerType = (uint)ComboBox_MarkerType.SelectedValue;

            //---------------------------------ADD Start Marker-------------------------------*/
            if (MarkerType != 9)
            {
                EXStreamStartMarker NewStartMarker = new EXStreamStartMarker
                {
                    Position = (uint)Numeric_MarkerPosition.Value,
                    MusicMakerType = 10,
                    Flags = uint.Parse(Textbox_Flags.Text),
                    Extra = uint.Parse(Textbox_Extra.Text),
                    MarkerCount = v_MarkerCount,
                    MarkerPos = v_MarkerPos
                };

                TemporalSelectedSound.StartMarkers.Add(NewStartMarker);
                AddMarkerStartToListView(NewStartMarker);
            }

            //---------------------------------Add Marker-------------------------------*/
            if (MarkerType == 6)
            {
                v_MarkerPos += 2;
                EXStreamMarker NewStartLoopMarker = new EXStreamMarker
                {
                    Name = (int)v_MarkerCount,
                    Position = 0,
                    MusicMakerType = (uint)ComboBox_MarkerType.SelectedValue,
                    Flags = uint.Parse(Textbox_Flags.Text),
                    Extra = uint.Parse(Textbox_Extra.Text),
                    LoopStart = 0,
                    MarkerCount = v_MarkerCount,
                    LoopMarkerCount = 0
                };

                EXStreamMarker NewLoopMarker = new EXStreamMarker
                {
                    Name = (int)v_MarkerCount,
                    Position = (uint)Numeric_MarkerPosition.Value,
                    MusicMakerType = (uint)ComboBox_MarkerType.SelectedValue,
                    Flags = uint.Parse(Textbox_Flags.Text),
                    Extra = uint.Parse(Textbox_Extra.Text),
                    LoopStart = 0,
                    MarkerCount = (v_MarkerCount + 1),
                    LoopMarkerCount = 1
                };

                TemporalSelectedSound.Markers.Add(NewStartLoopMarker);
                TemporalSelectedSound.Markers.Add(NewLoopMarker);

                AddMarkerDataToListView(NewStartLoopMarker);
                AddMarkerDataToListView(NewLoopMarker);
            }
            else if (MarkerType == 9)
            {
                v_MarkerPos += 1;
                EXStreamMarker NewMarker = new EXStreamMarker
                {
                    Name = (int)-1,
                    Position = (uint)Numeric_MarkerPosition.Value,
                    MusicMakerType = (uint)ComboBox_MarkerType.SelectedValue,
                    Flags = uint.Parse(Textbox_Flags.Text),
                    Extra = uint.Parse(Textbox_Extra.Text),
                    LoopStart = 0,
                    MarkerCount = v_MarkerCount,
                    LoopMarkerCount = 0
                };

                TemporalSelectedSound.Markers.Add(NewMarker);
                AddMarkerDataToListView(NewMarker);
            }
            else
            {
                v_MarkerPos += 1;
                EXStreamMarker NewMarker = new EXStreamMarker
                {
                    Name = (int)v_MarkerCount,
                    Position = (uint)Numeric_MarkerPosition.Value,
                    MusicMakerType = (uint)ComboBox_MarkerType.SelectedValue,
                    Flags = uint.Parse(Textbox_Flags.Text),
                    Extra = uint.Parse(Textbox_Extra.Text),
                    LoopStart = 0,
                    MarkerCount = v_MarkerCount,
                    LoopMarkerCount = 0
                };

                TemporalSelectedSound.Markers.Add(NewMarker);
                AddMarkerDataToListView(NewMarker);
            }

            v_MarkerCount += 1;
            TemporalSelectedSound.MarkerDataCounterID = v_MarkerPos;
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            TemporalSelectedSound.Markers.Clear();
            ListView_Markers.Items.Clear();
            ListView_MarkerData.Items.Clear();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            SelectedSound.StartMarkers = new List<EXStreamStartMarker>(TemporalSelectedSound.StartMarkers);
            SelectedSound.Markers = new List<EXStreamMarker>(TemporalSelectedSound.Markers);

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
                MarkerItem.StateA.ToString(),
                MarkerItem.StateB.ToString(),
                MarkerItem.MarkerCount.ToString()
            });
            ListView_Markers.Items.Add(Marker);
        }
    }
}
