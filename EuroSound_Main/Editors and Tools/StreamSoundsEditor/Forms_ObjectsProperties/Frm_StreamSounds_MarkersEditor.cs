using EuroSound_Application.Editors_and_Tools.StreamSoundsEditor.Classes;
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
        private MarkerFiles MarkerFilesFunctions = new MarkerFiles();
        private MarkersFunctions MarkerFunctions = new MarkersFunctions();
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
            //Add Markers to the Combobox
            ComboBox_MarkerType.DisplayMember = "Text";
            ComboBox_MarkerType.ValueMember = "Value";

            ComboBox_MarkerType.Items.Add(new { Text = "Stream Start Marker", Value = 10 });
            ComboBox_MarkerType.Items.Add(new { Text = "Stream End Marker", Value = 9 });
            ComboBox_MarkerType.Items.Add(new { Text = "Stream Goto Marker", Value = 7 });
            ComboBox_MarkerType.Items.Add(new { Text = "Stream Start Loop", Value = 6 });
            ComboBox_MarkerType.Items.Add(new { Text = "Stream Pause Marker", Value = 5 });
            ComboBox_MarkerType.Items.Add(new { Text = "Stream Jump Marker", Value = 0 });

            ComboBox_MarkerType.SelectedIndex = 0;

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
        private void Button_LoadFile_Click(object sender, EventArgs e)
        {
            string FilePath = GenericFunctions.OpenFileBrowser("EuroSound Markers Files|*.mrk", 0, true);
            if (!string.IsNullOrEmpty(FilePath))
            {
                List<string> FileData = MarkerFilesFunctions.ReadMarkersFile(FilePath);
                if (FileData.Count > 0)
                {
                    MarkerFilesFunctions.ApplyMarkersReadedData(TemporalSelectedSound, FileData);
                }
                else
                {
                    MessageBox.Show(GenericFunctions.ResourcesManager.GetString("Gen_ErrorReading_FileIncorrect"), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Button_SaveMarkers_Click(object sender, EventArgs e)
        {

        }

        private void Button_AddMarker_Click(object sender, EventArgs e)
        {
            //Get type of the selected combobox value
            uint MarkerType = (uint)(ComboBox_MarkerType.SelectedItem as dynamic).Value;
            uint[] IMA_States;

            //End
            if (MarkerType == 9)
            {
                //--------------------------------------------------[Markers]---------------------------------------------------
                //----Add End Marker----
                EXStreamMarker MarkerStart = MarkerFunctions.CreateMarker(TemporalSelectedSound.Markers, -1, (uint)Numeric_Position.Value, MarkerType, v_MarkerCount, 0, 0);
                TemporalSelectedSound.Markers.Add(MarkerStart);
                AddMarkerDataToListView(MarkerStart);

            }
            //Loop
            else if (MarkerType == 6)
            {
                //Calculate States --Loop Start Control--
                IMA_States = MarkerFunctions.CalculateMarkerStates(TemporalSelectedSound.IMA_ADPCM_DATA, (int)Numeric_MarkerLoopStart.Value);

                //--------------------------------------------------[Start Markers]--------------------------------------------------
                EXStreamStartMarker MarkerStartData = MarkerFunctions.CreateStartMarker(TemporalSelectedSound.StartMarkers, (uint)Numeric_Position.Value, 10, v_MarkerCount, v_MarkerPos, IMA_States[0], IMA_States[1]);
                TemporalSelectedSound.StartMarkers.Add(MarkerStartData);
                AddMarkerStartToListView(MarkerStartData);

                //-----------------------------------------------------[Markers]-----------------------------------------------------
                //----Add Start Marker----
                EXStreamMarker MarkerStart = MarkerFunctions.CreateMarker(TemporalSelectedSound.Markers, (int)v_MarkerCount, (uint)Numeric_MarkerLoopStart.Value, 10, v_MarkerCount, 0, 0);
                TemporalSelectedSound.Markers.Add(MarkerStart);
                AddMarkerDataToListView(MarkerStart);
                v_MarkerPos += 1;

                //----Add Loop Marker----
                EXStreamMarker MarkerLoop = MarkerFunctions.CreateMarker(TemporalSelectedSound.Markers, (int)v_MarkerCount, (uint)Numeric_Position.Value, MarkerType, (v_MarkerCount + 1), (uint)Numeric_MarkerLoopStart.Value, 1);
                TemporalSelectedSound.Markers.Add(MarkerLoop);
                AddMarkerDataToListView(MarkerLoop);

            }
            else
            {
                //Calculate States --Position Control--
                IMA_States = MarkerFunctions.CalculateMarkerStates(TemporalSelectedSound.IMA_ADPCM_DATA, (int)Numeric_Position.Value);

                //--------------------------------------------------[Start Markers]--------------------------------------------------
                EXStreamStartMarker MarkerStartData = MarkerFunctions.CreateStartMarker(TemporalSelectedSound.StartMarkers, (uint)Numeric_Position.Value, MarkerType, v_MarkerCount, v_MarkerPos, IMA_States[0], IMA_States[1]);
                TemporalSelectedSound.StartMarkers.Add(MarkerStartData);
                AddMarkerStartToListView(MarkerStartData);

                //-----------------------------------------------------[Markers]-----------------------------------------------------
                //----Add Start Marker----
                EXStreamMarker MarkerStart = MarkerFunctions.CreateMarker(TemporalSelectedSound.Markers, (int)v_MarkerCount, (uint)Numeric_Position.Value, MarkerType, v_MarkerCount, 0, 0);
                TemporalSelectedSound.Markers.Add(MarkerStart);
                AddMarkerDataToListView(MarkerStart);
            }

            //--------------------------------------------------[Update Counters]--------------------------------------------------
            v_MarkerCount += 1;
            v_MarkerPos += 1;
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            TemporalSelectedSound.StartMarkers.Clear();
            TemporalSelectedSound.Markers.Clear();
            ListView_Markers.Items.Clear();
            ListView_MarkerData.Items.Clear();
            v_MarkerCount = 0;
            v_MarkerPos = 0;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            //Trim lists
            TemporalSelectedSound.StartMarkers.TrimExcess();
            TemporalSelectedSound.Markers.TrimExcess();

            //Get lists
            SelectedSound.StartMarkers = new List<EXStreamStartMarker>(TemporalSelectedSound.StartMarkers);
            SelectedSound.Markers = new List<EXStreamMarker>(TemporalSelectedSound.Markers);

            //Get Data
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
                MarkerItem.Name.ToString(),
                MarkerItem.Position.ToString(),
                //MarkerTypes[MarkerItem.MusicMakerType],
                GetMarkerType(MarkerItem.MusicMakerType),
                MarkerItem.Flags.ToString(),
                MarkerItem.Extra.ToString(),
                MarkerItem.LoopStart.ToString(),
                MarkerItem.MarkerCount.ToString(),
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
                MarkerItem.StateA.ToString(),
                MarkerItem.StateB.ToString(),
                MarkerItem.MarkerCount.ToString()
            });
            ListView_Markers.Items.Add(Marker);
        }

        private string GetMarkerType(uint MarkerValue)
        {
            string MType;

            switch (MarkerValue)
            {
                case 10:
                    MType = "Start";
                    break;
                case 9:
                    MType = "End";
                    break;
                case 7:
                    MType = "Goto";
                    break;
                case 6:
                    MType = "Loop";
                    break;
                case 5:
                    MType = "Pause";
                    break;
                default:
                    MType = "Jump";
                    break;
            }

            return MType;
        }
    }
}
