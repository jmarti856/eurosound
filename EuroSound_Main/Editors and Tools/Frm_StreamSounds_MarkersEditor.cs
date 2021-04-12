using EuroSound_Application.MarkerFiles;
using EuroSound_Application.MarkerFiles.StreamSoundsEditor.Classes;
using EuroSound_Application.Musics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.StreamSounds
{
    public partial class Frm_StreamSounds_MarkersEditor : Form
    {
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private EXSoundStream SelectedSound, TemporalSelectedSound;
        private EXMusic SelectedMusic, TemporalSelectedMusic;
        private MarkersFunctions MarkerFunctions = new MarkersFunctions();

        private bool MusicMode;
        int v_MarkerName;

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        public Frm_StreamSounds_MarkersEditor(EXSoundStream Sound, EXMusic Music, bool UseMusic)
        {
            InitializeComponent();
            SelectedSound = Sound;
            SelectedMusic = Music;
            MusicMode = UseMusic;
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

            if (MusicMode)
            {
                //Temporal Music
                TemporalSelectedMusic = new EXMusic
                {
                    StartMarkers = new List<EXStreamStartMarker>(SelectedMusic.StartMarkers),
                    Markers = new List<EXStreamMarker>(SelectedMusic.Markers)
                };
                Reflection.CopyProperties(SelectedMusic, TemporalSelectedMusic);

                //Marker Pos
                v_MarkerName = TemporalSelectedMusic.Markers.Count;

                //Print Markers
                PrintStartMarkers(TemporalSelectedMusic.StartMarkers);
                PrintMarkers(TemporalSelectedMusic.Markers);
            }
            else
            {
                //Temporal Sound
                TemporalSelectedSound = new EXSoundStream
                {
                    StartMarkers = new List<EXStreamStartMarker>(SelectedSound.StartMarkers),
                    Markers = new List<EXStreamMarker>(SelectedSound.Markers)
                };
                Reflection.CopyProperties(SelectedSound, TemporalSelectedSound);

                //Marker Pos
                v_MarkerName = TemporalSelectedSound.Markers.Count;

                //Print Markers
                PrintStartMarkers(TemporalSelectedSound.StartMarkers);
                PrintMarkers(TemporalSelectedSound.Markers);
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
                if (MusicMode)
                {
                    MarkerFilesMusic_Loader MarkerFilesFunctions = new MarkerFilesMusic_Loader();

                    //Load Markers
                    List<string> ImportResults = MarkerFilesFunctions.LoadMusicMarkersFile(FilePath, TemporalSelectedMusic);
                    if (ImportResults.Count > 0)
                    {
                        GenericFunctions.ShowErrorsAndWarningsList(ImportResults, "Import Results", this);
                    }

                    //Clear Lists
                    ListView_Markers.Items.Clear();
                    ListView_MarkerData.Items.Clear();

                    //Print Markers
                    PrintStartMarkers(TemporalSelectedMusic.StartMarkers);
                    PrintMarkers(TemporalSelectedMusic.Markers);
                }
                else
                {
                    MarkerFiles_Loader MarkerFilesFunctions = new MarkerFiles_Loader();

                    //Load Markers
                    List<string> ImportResults = MarkerFilesFunctions.LoadSTRMarkersFile(FilePath, TemporalSelectedSound);
                    if (ImportResults.Count > 0)
                    {
                        GenericFunctions.ShowErrorsAndWarningsList(ImportResults, "Import Results", this);
                    }

                    //Clear Lists
                    ListView_Markers.Items.Clear();
                    ListView_MarkerData.Items.Clear();

                    //Print Markers
                    PrintStartMarkers(TemporalSelectedSound.StartMarkers);
                    PrintMarkers(TemporalSelectedSound.Markers);
                }
            }
        }

        private void Button_SaveMarkers_Click(object sender, EventArgs e)
        {
            if (MusicMode)
            {
                MarkerFilesMusic_Exporter MKExporter = new MarkerFilesMusic_Exporter();
                string SoundName = Path.GetFileNameWithoutExtension(TemporalSelectedMusic.WAVFileMD5_LeftChannel);
                string FilePath = GenericFunctions.SaveFileBrowser("EuroSound Markers Files|*.mrk", 0, true, SoundName);

                if (!string.IsNullOrEmpty(FilePath))
                {
                    MKExporter.ExportMarkersFromMusic(FilePath, SoundName, TemporalSelectedMusic);
                }
            }
            else
            {
                MarkerFiles_Exporter MKExporter = new MarkerFiles_Exporter();
                string SoundName = Path.GetFileNameWithoutExtension(TemporalSelectedSound.WAVFileName);
                string FilePath = GenericFunctions.SaveFileBrowser("EuroSound Markers Files|*.mrk", 0, true, SoundName);

                if (!string.IsNullOrEmpty(FilePath))
                {
                    MKExporter.ExportMarkersFromSound(FilePath, SoundName, TemporalSelectedSound);
                }
            }
        }

        private void Button_AddMarker_Click(object sender, EventArgs e)
        {
            uint Position, LoopStart;
            uint MarkerType = (uint)(ComboBox_MarkerType.SelectedItem as dynamic).Value;
            uint[] IMA_States;

            if (MusicMode)
            {
                LoopStart = (uint)Numeric_MarkerLoopStart.Value * 4;
                Position = (uint)Numeric_Position.Value * 4;
                v_MarkerName = TemporalSelectedMusic.StartMarkers.Count;

                //End
                if (MarkerType == 9)
                {
                    //--------------------------------------------------[Markers]---------------------------------------------------
                    //----Add End Marker----
                    EXStreamMarker MarkerStart = MarkerFunctions.CreateMarker(TemporalSelectedMusic.Markers, (TemporalSelectedMusic.StartMarkers.Count - 1), Position, MarkerType, 0, (uint)TemporalSelectedMusic.StartMarkers.Count, 0, 0);
                    AddMarkerDataToListView(MarkerStart);
                }
                //Loop
                else if (MarkerType == 6)
                {
                    //Calculate States --Loop Start Control--
                    IMA_States = MarkerFunctions.GetEngineXMarkerStates_Stereo(TemporalSelectedMusic.IMA_ADPCM_DATA_LeftChannel, TemporalSelectedMusic.IMA_ADPCM_DATA_RightChannel, (int)LoopStart);

                    //--------------------------------------------------[Start Markers]--------------------------------------------------
                    EXStreamStartMarker MarkerStartData = MarkerFunctions.CreateStartMarker(TemporalSelectedMusic.StartMarkers, Position, 10, 0, LoopStart, 0, (uint)TemporalSelectedMusic.Markers.Count, IMA_States[0], IMA_States[1]);
                    AddMarkerStartToListView(MarkerStartData);

                    //-----------------------------------------------------[Markers Start Loop]------------------------------------------
                    //----Add Start Marker----
                    EXStreamMarker MarkerLoopStart = MarkerFunctions.CreateMarker(TemporalSelectedMusic.Markers, v_MarkerName, LoopStart, 10, 0, (uint)TemporalSelectedMusic.Markers.Count, 0, 0);
                    AddMarkerDataToListView(MarkerLoopStart);

                    //----Add Loop Marker----
                    EXStreamMarker MarkerLoop = MarkerFunctions.CreateMarker(TemporalSelectedMusic.Markers, v_MarkerName, Position, MarkerType, 0, (uint)TemporalSelectedMusic.Markers.Count, LoopStart, 1);
                    AddMarkerDataToListView(MarkerLoop);

                    //-----------------------------------------------------[Markers End Loop]--------------------------------------------
                    //Calculate States
                    IMA_States = MarkerFunctions.GetEngineXMarkerStates_Stereo(TemporalSelectedMusic.IMA_ADPCM_DATA_LeftChannel, TemporalSelectedMusic.IMA_ADPCM_DATA_RightChannel, (int)Position);

                    v_MarkerName = TemporalSelectedMusic.StartMarkers.Count;
                    //--------------------------------------------------[Start Markers]--------------------------------------------------
                    EXStreamStartMarker MarkerStart = MarkerFunctions.CreateStartMarker(TemporalSelectedMusic.StartMarkers, Position, MarkerType, 0, LoopStart, 0, (uint)TemporalSelectedMusic.Markers.Count, IMA_States[0], IMA_States[1]);
                    AddMarkerStartToListView(MarkerStart);

                    //-----------------------------------------------------[Markers]-----------------------------------------------------
                    //----Add Start Marker----
                    EXStreamMarker Marker = MarkerFunctions.CreateMarker(TemporalSelectedMusic.Markers, v_MarkerName, Position, 10, 0, (uint)v_MarkerName, 0, 0);
                    AddMarkerDataToListView(Marker);

                    //Reset Values
                    Numeric_Position.Value = 0;
                    Numeric_MarkerLoopStart.Value = 0;
                }
                //Goto
                else if (MarkerType == 7)
                {
                    //Calculate States --Position Control--
                    IMA_States = MarkerFunctions.GetEngineXMarkerStates_Stereo(TemporalSelectedMusic.IMA_ADPCM_DATA_LeftChannel, TemporalSelectedMusic.IMA_ADPCM_DATA_RightChannel, (int)Position);

                    //--------------------------------------------------[Start Markers]--------------------------------------------------
                    EXStreamStartMarker MarkerStart = MarkerFunctions.CreateStartMarker(TemporalSelectedMusic.StartMarkers, Position, 7, 0, LoopStart, 1, (uint)TemporalSelectedMusic.Markers.Count, IMA_States[0], IMA_States[1]);
                    AddMarkerStartToListView(MarkerStart);

                    //-----------------------------------------------------[Markers]-----------------------------------------------------
                    EXStreamMarker Marker = MarkerFunctions.CreateMarker(TemporalSelectedMusic.Markers, v_MarkerName, Position, 7, 0, (uint)TemporalSelectedMusic.Markers.Count, LoopStart, 1);
                    AddMarkerDataToListView(Marker);
                }
                else
                {
                    //Calculate States --Position Control--
                    IMA_States = MarkerFunctions.GetEngineXMarkerStates_Stereo(TemporalSelectedMusic.IMA_ADPCM_DATA_LeftChannel, TemporalSelectedMusic.IMA_ADPCM_DATA_RightChannel, (int)Position);

                    //--------------------------------------------------[Start Markers]--------------------------------------------------
                    EXStreamStartMarker MarkerStart = MarkerFunctions.CreateStartMarker(TemporalSelectedMusic.StartMarkers, Position, MarkerType, 0, LoopStart, 0, (uint)TemporalSelectedMusic.Markers.Count, IMA_States[0], IMA_States[1]);
                    AddMarkerStartToListView(MarkerStart);

                    //-----------------------------------------------------[Markers]-----------------------------------------------------
                    EXStreamMarker Marker = MarkerFunctions.CreateMarker(TemporalSelectedMusic.Markers, v_MarkerName, Position, MarkerType, 0, (uint)TemporalSelectedMusic.Markers.Count, 0, 0);
                    AddMarkerDataToListView(Marker);
                }
            }
            else
            {
                LoopStart = (uint)Numeric_MarkerLoopStart.Value * 2;
                Position = (uint)Numeric_Position.Value * 2;

                v_MarkerName = TemporalSelectedSound.StartMarkers.Count;

                //End
                if (MarkerType == 9)
                {
                    //--------------------------------------------------[Markers]--------------------------------------------------------
                    //----Add End Marker----
                    EXStreamMarker MarkerStart = MarkerFunctions.CreateMarker(TemporalSelectedSound.Markers, -1, Position, MarkerType, 2, (uint)TemporalSelectedSound.StartMarkers.Count, 0, 0);
                    AddMarkerDataToListView(MarkerStart);

                }
                //Loop
                else if (MarkerType == 6)
                {
                    //Calculate States --Loop Start Control--
                    IMA_States = MarkerFunctions.GetEngineXMarkerStates_Mono(TemporalSelectedSound.IMA_ADPCM_DATA, (int)LoopStart);

                    //--------------------------------------------------[Start Markers]--------------------------------------------------
                    EXStreamStartMarker MarkerStartData = MarkerFunctions.CreateStartMarker(TemporalSelectedSound.StartMarkers, Position, 10, 2, 0, 0, (uint)TemporalSelectedSound.Markers.Count, IMA_States[0], IMA_States[1]);
                    AddMarkerStartToListView(MarkerStartData);

                    //-----------------------------------------------------[Markers Start Loop]------------------------------------------
                    //----Add Start Marker----
                    EXStreamMarker MarkerLoopStart = MarkerFunctions.CreateMarker(TemporalSelectedSound.Markers, v_MarkerName, LoopStart, 10, 2, (uint)TemporalSelectedSound.Markers.Count, 0, 0);
                    AddMarkerDataToListView(MarkerLoopStart);

                    //----Add Loop Marker----
                    EXStreamMarker MarkerLoop = MarkerFunctions.CreateMarker(TemporalSelectedSound.Markers, v_MarkerName, Position, MarkerType, 2, (uint)TemporalSelectedSound.Markers.Count, LoopStart, 1);
                    AddMarkerDataToListView(MarkerLoop);

                    //-----------------------------------------------------[Markers End Loop]--------------------------------------------
                    //Calculate States
                    IMA_States = MarkerFunctions.GetEngineXMarkerStates_Mono(TemporalSelectedSound.IMA_ADPCM_DATA, (int)Position);

                    v_MarkerName = TemporalSelectedSound.StartMarkers.Count;
                    //--------------------------------------------------[Start Markers]--------------------------------------------------
                    EXStreamStartMarker MarkerStart = MarkerFunctions.CreateStartMarker(TemporalSelectedSound.StartMarkers, Position, MarkerType, 2, 0, 0, (uint)TemporalSelectedSound.Markers.Count, IMA_States[0], IMA_States[1]);
                    AddMarkerStartToListView(MarkerStart);

                    //-----------------------------------------------------[Markers]-----------------------------------------------------
                    //----Add Start Marker----
                    EXStreamMarker Marker = MarkerFunctions.CreateMarker(TemporalSelectedSound.Markers, v_MarkerName, Position, 10, 2, (uint)v_MarkerName, 0, 0);
                    AddMarkerDataToListView(Marker);

                    //Reset Values
                    Numeric_Position.Value = 0;
                    Numeric_MarkerLoopStart.Value = 0;
                }
                //Goto
                else if (MarkerType == 7)
                {
                    //Calculate States --Position Control--
                    IMA_States = MarkerFunctions.GetEngineXMarkerStates_Mono(TemporalSelectedSound.IMA_ADPCM_DATA, (int)LoopStart);

                    //--------------------------------------------------[Start Markers]--------------------------------------------------
                    EXStreamStartMarker MarkerStart = MarkerFunctions.CreateStartMarker(TemporalSelectedSound.StartMarkers, Position, 7, 2, 0, 0, (uint)TemporalSelectedSound.Markers.Count, IMA_States[0], IMA_States[1]);
                    AddMarkerStartToListView(MarkerStart);

                    //-----------------------------------------------------[Markers]-----------------------------------------------------
                    EXStreamMarker Marker = MarkerFunctions.CreateMarker(TemporalSelectedSound.Markers, v_MarkerName, Position, 7, 2, (uint)TemporalSelectedSound.Markers.Count, LoopStart, 1);
                    AddMarkerDataToListView(Marker);
                }
                else
                {
                    //Calculate States --Position Control--
                    IMA_States = MarkerFunctions.GetEngineXMarkerStates_Mono(TemporalSelectedSound.IMA_ADPCM_DATA, (int)Position);

                    //--------------------------------------------------[Start Markers]--------------------------------------------------
                    EXStreamStartMarker MarkerStart = MarkerFunctions.CreateStartMarker(TemporalSelectedSound.StartMarkers, Position, MarkerType, 2, 0, 0, (uint)TemporalSelectedSound.Markers.Count, IMA_States[0], IMA_States[1]);
                    AddMarkerStartToListView(MarkerStart);

                    //-----------------------------------------------------[Markers]-----------------------------------------------------
                    EXStreamMarker Marker = MarkerFunctions.CreateMarker(TemporalSelectedSound.Markers, v_MarkerName, Position, MarkerType, 2, (uint)TemporalSelectedSound.Markers.Count, 0, 0);
                    AddMarkerDataToListView(Marker);
                }
            }
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            //Clear Lists
            if (MusicMode)
            {
                TemporalSelectedMusic.StartMarkers.Clear();
                TemporalSelectedMusic.Markers.Clear();
            }
            else
            {
                TemporalSelectedSound.StartMarkers.Clear();
                TemporalSelectedSound.Markers.Clear();
            }
            v_MarkerName = 0;

            //Clear List Views 
            ListView_Markers.Items.Clear();
            ListView_MarkerData.Items.Clear();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            if (MusicMode)
            {
                //Trim lists
                TemporalSelectedMusic.StartMarkers.TrimExcess();
                TemporalSelectedMusic.Markers.TrimExcess();

                //Get lists
                SelectedMusic.StartMarkers = new List<EXStreamStartMarker>(TemporalSelectedMusic.StartMarkers);
                SelectedMusic.Markers = new List<EXStreamMarker>(TemporalSelectedMusic.Markers);

                //Get Data
                SelectedMusic.Markers = TemporalSelectedMusic.Markers;
            }
            else
            {
                //Trim lists
                TemporalSelectedSound.StartMarkers.TrimExcess();
                TemporalSelectedSound.Markers.TrimExcess();

                //Get lists
                SelectedSound.StartMarkers = new List<EXStreamStartMarker>(TemporalSelectedSound.StartMarkers);
                SelectedSound.Markers = new List<EXStreamMarker>(TemporalSelectedSound.Markers);

                //Get Data
                SelectedSound.Markers = TemporalSelectedSound.Markers;
            }
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            if (MusicMode)
            {
                TemporalSelectedMusic = null;
            }
            else
            {
                TemporalSelectedSound = null;
            }
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
                EXStreamSoundsFunctions.GetMarkerType(MarkerItem.MusicMakerType),
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
                MarkerItem.StateA.ToString("X8"),
                MarkerItem.StateB.ToString("X8"),
                MarkerItem.MarkerCount.ToString()
            });
            ListView_Markers.Items.Add(Marker);
        }

        private void PrintStartMarkers(List<EXStreamStartMarker> StartMarkersList)
        {
            foreach (EXStreamStartMarker Marker in StartMarkersList)
            {
                AddMarkerStartToListView(Marker);
            }
        }

        private void PrintMarkers(List<EXStreamMarker> MarkersList)
        {
            foreach (EXStreamMarker MarkerData in MarkersList)
            {
                AddMarkerDataToListView(MarkerData);
            }
        }
    }
}
