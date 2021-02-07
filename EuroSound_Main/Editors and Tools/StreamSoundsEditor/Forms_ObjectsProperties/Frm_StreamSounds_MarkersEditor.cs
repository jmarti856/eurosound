using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EuroSound_Application.StreamSounds
{
    public partial class Frm_StreamSounds_MarkersEditor : Form
    { 
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private EXSoundStream SelectedSound, TemporalSelectedSound;
        private uint v_MarkerPos, v_MarkerCount;
        private EngineXImaAdpcm.ImaADPCM_Decoder ImaADPCM = new EngineXImaAdpcm.ImaADPCM_Decoder();
        private uint StateA = 0, StateB = 0;

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
            ComboBox_MarkerType.DisplayMember = "Text";
            ComboBox_MarkerType.ValueMember = "Value";

            ComboBox_MarkerType.Items.Add(new { Text = "Start", Value = 10 });
            ComboBox_MarkerType.Items.Add(new { Text = "End", Value = 9 });
            ComboBox_MarkerType.Items.Add(new { Text = "Goto", Value = 7 });
            ComboBox_MarkerType.Items.Add(new { Text = "Loop", Value = 6 });
            ComboBox_MarkerType.Items.Add(new { Text = "Pause", Value = 5 });
            ComboBox_MarkerType.Items.Add(new { Text = "Jump", Value = 0 });

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
        private void Button_AddMarker_Click(object sender, EventArgs e)
        {
            int SamplesToDecode, IMAPosition;
            int[] IMAStates;
            uint MarkerType;
            bool PendingToAddStates = false;

            //Get type of the selected combobox value
            MarkerType = (uint)(ComboBox_MarkerType.SelectedItem as dynamic).Value;

            //---------------------------------[Start Marker]-------------------------------
            EXStreamStartMarker NewStartMarker = new EXStreamStartMarker
            {
                Position = (uint)Numeric_Position.Value,
                MusicMakerType = 10,
                Flags = 2,
                Extra = 0,
                MarkerCount = v_MarkerCount,
                MarkerPos = v_MarkerPos
            };

            //---------------------------------[Add Marker]---------------------------------
            if (MarkerType == 6) //Loop
            {
                v_MarkerPos += 2;
                EXStreamMarker NewStartLoopMarker = new EXStreamMarker
                {
                    Name = (int)v_MarkerCount,
                    MusicMakerType = 10,
                    MarkerCount = v_MarkerCount
                };

                //Get States
                if ((uint)Numeric_MarkerLoopStart.Value > 0)
                {
                    NewStartLoopMarker.Position = (uint)Numeric_MarkerLoopStart.Value;

                    //Calculate States
                    SamplesToDecode = TemporalSelectedSound.IMA_ADPCM_DATA.Length * 2;
                    IMAStates = new int[SamplesToDecode];
                    ImaADPCM.DecodeIMA_ADPCM(TemporalSelectedSound.IMA_ADPCM_DATA, SamplesToDecode, IMAStates);

                    //Get States
                    IMAPosition = ((int)Numeric_MarkerLoopStart.Value / 2) - 1;
                    StateA = (uint)IMAStates[IMAPosition];
                    StateB = (uint)IMAStates[IMAPosition];

                    //Add States
                    PendingToAddStates = true;
                }
                else
                {
                    StateA = 0;
                    StateB = 0;
                }

                EXStreamMarker NewLoopMarker = new EXStreamMarker
                {
                    Name = (int)v_MarkerCount,
                    Position = (uint)Numeric_Position.Value,
                    MusicMakerType = MarkerType,
                    MarkerCount = (v_MarkerCount + 1),
                    LoopStart = (uint)Numeric_MarkerLoopStart.Value,
                    LoopMarkerCount = 1
                };

                //Add Markers
                TemporalSelectedSound.Markers.Add(NewStartLoopMarker);
                TemporalSelectedSound.Markers.Add(NewLoopMarker);

                AddMarkerDataToListView(NewStartLoopMarker);
                AddMarkerDataToListView(NewLoopMarker);
            }
            else if (MarkerType == 9) //End
            {
                v_MarkerPos += 1;
                EXStreamMarker NewMarker = new EXStreamMarker
                {
                    Name = (int)-1,
                    Position = (uint)Numeric_Position.Value,
                    MusicMakerType = MarkerType,
                    MarkerCount = v_MarkerCount,
                };

                TemporalSelectedSound.Markers.Add(NewMarker);
                AddMarkerDataToListView(NewMarker);
            }
            else if (MarkerType == 10) //Start
            {
                v_MarkerPos += 1;
                EXStreamMarker NewMarker = new EXStreamMarker
                {
                    Name = (int)v_MarkerCount,
                    Position = (uint)Numeric_Position.Value,
                    MusicMakerType = 10,
                    MarkerCount = v_MarkerCount,
                };

                TemporalSelectedSound.Markers.Add(NewMarker);
                AddMarkerDataToListView(NewMarker);

                //Get States
                if ((uint)Numeric_Position.Value > 0)
                {
                    //Calculate States
                    SamplesToDecode = TemporalSelectedSound.IMA_ADPCM_DATA.Length * 2;
                    IMAStates = new int[SamplesToDecode];
                    ImaADPCM.DecodeIMA_ADPCM(TemporalSelectedSound.IMA_ADPCM_DATA, SamplesToDecode, IMAStates);

                    //Get States
                    IMAPosition = ((int)Numeric_Position.Value / 2) - 1;
                    StateA = (uint)IMAStates[IMAPosition];
                    StateB = (uint)IMAStates[IMAPosition];

                    PendingToAddStates = true;
                }
                else
                {
                    PendingToAddStates = false;
                    StateA = 0;
                    StateB = 0;
                }
            }
            else
            {
                v_MarkerPos += 1;
                EXStreamMarker NewMarker = new EXStreamMarker
                {
                    Name = (int)v_MarkerCount,
                    Position = (uint)Numeric_Position.Value,
                    MusicMakerType = MarkerType,
                    MarkerCount = v_MarkerCount,
                };

                TemporalSelectedSound.Markers.Add(NewMarker);
                AddMarkerDataToListView(NewMarker);
            }

            //---------------------------------[Add Start Marker]-------------------------------
            if (MarkerType != 9)
            {
                if (PendingToAddStates)
                {
                    if (StateA != 0 && StateB != 0)
                    {
                        NewStartMarker.StateA = StateA;
                        NewStartMarker.StateB = StateB;
                    }
                }

                TemporalSelectedSound.StartMarkers.Add(NewStartMarker);
                AddMarkerStartToListView(NewStartMarker);
            }

            v_MarkerCount += 1;
            TemporalSelectedSound.MarkerDataCounterID = v_MarkerPos;
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

            if (MarkerValue == 10)
            {
                MType = "Start";
            }
            else if (MarkerValue == 9)
            {
                MType = "End";
            }
            else if (MarkerValue == 7)
            {
                MType = "Goto";
            }
            else if (MarkerValue == 6)
            {
                MType = "Loop";
            }
            else if (MarkerValue == 5)
            {
                MType = "Pause";
            }
            else
            {
                MType = "Jump";
            }

            return MType;
        }
    }
}
