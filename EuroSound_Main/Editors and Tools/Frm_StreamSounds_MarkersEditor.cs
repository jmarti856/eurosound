using EuroSound_Application.Clases;
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
        private int v_MarkerName;

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

            ComboBox_MarkerType.Items.Add(new { Text = "Stream Start Marker", Value = (int)GenericFunctions.ESoundMarkers.Start });
            ComboBox_MarkerType.Items.Add(new { Text = "Stream End Marker", Value = (int)GenericFunctions.ESoundMarkers.End });
            ComboBox_MarkerType.Items.Add(new { Text = "Stream Goto Marker", Value = (int)GenericFunctions.ESoundMarkers.Goto });
            ComboBox_MarkerType.Items.Add(new { Text = "Stream Start Loop", Value = (int)GenericFunctions.ESoundMarkers.Loop });
            ComboBox_MarkerType.Items.Add(new { Text = "Stream Pause Marker", Value = (int)GenericFunctions.ESoundMarkers.Pause });
            ComboBox_MarkerType.Items.Add(new { Text = "Stream Jump Marker", Value = (int)GenericFunctions.ESoundMarkers.Jump });

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
            string filePath = BrowsersAndDialogs.FileBrowserDialog("EuroSound Markers Files|*.mrk", 0, true);

            if (!string.IsNullOrEmpty(filePath))
            {
                if (MusicMode)
                {
                    MarkerFilesMusic_Loader markerFilesFunctions = new MarkerFilesMusic_Loader();

                    //Load Markers
                    IList<string> ImportResults = markerFilesFunctions.LoadMusicMarkersFile(filePath, TemporalSelectedMusic);
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
                    IList<string> ImportResults = MarkerFilesFunctions.LoadSTRMarkersFile(filePath, TemporalSelectedSound);
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
                MarkerFilesMusic_Exporter markersExporter = new MarkerFilesMusic_Exporter();
                string SoundName = Path.GetFileNameWithoutExtension(TemporalSelectedMusic.WAVFileMD5_LeftChannel);
                string FilePath = BrowsersAndDialogs.SaveFileBrowser("EuroSound Markers Files|*.mrk", 0, true, SoundName);

                if (!string.IsNullOrEmpty(FilePath))
                {
                    markersExporter.ExportMarkersFromMusic(FilePath, SoundName, TemporalSelectedMusic);
                }
            }
            else
            {
                MarkerFiles_Exporter markersExporter = new MarkerFiles_Exporter();
                string SoundName = Path.GetFileNameWithoutExtension(TemporalSelectedSound.WAVFileName);
                string FilePath = BrowsersAndDialogs.SaveFileBrowser("EuroSound Markers Files|*.mrk", 0, true, SoundName);

                if (!string.IsNullOrEmpty(FilePath))
                {
                    markersExporter.ExportMarkersFromSound(FilePath, SoundName, TemporalSelectedSound);
                }
            }
        }

        private void Button_AddMarker_Click(object sender, EventArgs e)
        {
            uint position, loopStart;
            uint markerType = (uint)(ComboBox_MarkerType.SelectedItem as dynamic).Value;
            uint[] IMA_States;

            if (MusicMode)
            {
                loopStart = (uint)Numeric_MarkerLoopStart.Value * 4;
                position = (uint)Numeric_Position.Value * 4;
                v_MarkerName = TemporalSelectedMusic.StartMarkers.Count;

                if (markerType == (int)GenericFunctions.ESoundMarkers.End)
                {
                    //--------------------------------------------------[Markers]---------------------------------------------------
                    //----Add End Marker----
                    EXStreamMarker MarkerStart = MarkerFunctions.CreateMarker(TemporalSelectedMusic.Markers, (TemporalSelectedMusic.StartMarkers.Count - 1), position, markerType, 0, (uint)TemporalSelectedMusic.StartMarkers.Count, 0, 0);
                    AddMarkerDataToListView(MarkerStart);
                }
                else if (markerType == (int)GenericFunctions.ESoundMarkers.Loop)
                {
                    //Calculate States --Loop Start Control--
                    IMA_States = MarkerFunctions.GetEngineXMarkerStates_Stereo(TemporalSelectedMusic.IMA_ADPCM_DATA_LeftChannel, TemporalSelectedMusic.IMA_ADPCM_DATA_RightChannel, (int)loopStart);

                    //--------------------------------------------------[Start Markers]--------------------------------------------------
                    EXStreamStartMarker MarkerStartData = MarkerFunctions.CreateStartMarker(TemporalSelectedMusic.StartMarkers, position, 10, 0, loopStart, 0, (uint)TemporalSelectedMusic.Markers.Count, IMA_States[0], IMA_States[1]);
                    AddMarkerStartToListView(MarkerStartData);

                    //-----------------------------------------------------[Markers Start Loop]------------------------------------------
                    //----Add Start Marker----
                    EXStreamMarker MarkerLoopStart = MarkerFunctions.CreateMarker(TemporalSelectedMusic.Markers, v_MarkerName, loopStart, 10, 0, (uint)TemporalSelectedMusic.Markers.Count, 0, 0);
                    AddMarkerDataToListView(MarkerLoopStart);

                    //----Add Loop Marker----
                    EXStreamMarker MarkerLoop = MarkerFunctions.CreateMarker(TemporalSelectedMusic.Markers, v_MarkerName, position, markerType, 0, (uint)TemporalSelectedMusic.Markers.Count, loopStart, 1);
                    AddMarkerDataToListView(MarkerLoop);

                    //-----------------------------------------------------[Markers End Loop]--------------------------------------------
                    //Calculate States
                    IMA_States = MarkerFunctions.GetEngineXMarkerStates_Stereo(TemporalSelectedMusic.IMA_ADPCM_DATA_LeftChannel, TemporalSelectedMusic.IMA_ADPCM_DATA_RightChannel, (int)position);

                    v_MarkerName = TemporalSelectedMusic.StartMarkers.Count;
                    //--------------------------------------------------[Start Markers]--------------------------------------------------
                    EXStreamStartMarker MarkerStart = MarkerFunctions.CreateStartMarker(TemporalSelectedMusic.StartMarkers, position, markerType, 0, loopStart, 0, (uint)TemporalSelectedMusic.Markers.Count, IMA_States[0], IMA_States[1]);
                    AddMarkerStartToListView(MarkerStart);

                    //-----------------------------------------------------[Markers]-----------------------------------------------------
                    //----Add Start Marker----
                    EXStreamMarker Marker = MarkerFunctions.CreateMarker(TemporalSelectedMusic.Markers, v_MarkerName, position, 10, 0, (uint)v_MarkerName, 0, 0);
                    AddMarkerDataToListView(Marker);

                    //Reset Values
                    Numeric_Position.Value = 0;
                    Numeric_MarkerLoopStart.Value = 0;
                }
                else if (markerType == (int)GenericFunctions.ESoundMarkers.Goto)
                {
                    //Calculate States --Position Control--
                    IMA_States = MarkerFunctions.GetEngineXMarkerStates_Stereo(TemporalSelectedMusic.IMA_ADPCM_DATA_LeftChannel, TemporalSelectedMusic.IMA_ADPCM_DATA_RightChannel, (int)position);

                    //--------------------------------------------------[Start Markers]--------------------------------------------------
                    EXStreamStartMarker MarkerStart = MarkerFunctions.CreateStartMarker(TemporalSelectedMusic.StartMarkers, position, 7, 0, loopStart, 1, (uint)TemporalSelectedMusic.Markers.Count, IMA_States[0], IMA_States[1]);
                    AddMarkerStartToListView(MarkerStart);

                    //-----------------------------------------------------[Markers]-----------------------------------------------------
                    EXStreamMarker Marker = MarkerFunctions.CreateMarker(TemporalSelectedMusic.Markers, v_MarkerName, position, 7, 0, (uint)TemporalSelectedMusic.Markers.Count, loopStart, 1);
                    AddMarkerDataToListView(Marker);
                }
                else
                {
                    //Calculate States --Position Control--
                    IMA_States = MarkerFunctions.GetEngineXMarkerStates_Stereo(TemporalSelectedMusic.IMA_ADPCM_DATA_LeftChannel, TemporalSelectedMusic.IMA_ADPCM_DATA_RightChannel, (int)position);

                    //--------------------------------------------------[Start Markers]--------------------------------------------------
                    EXStreamStartMarker MarkerStart = MarkerFunctions.CreateStartMarker(TemporalSelectedMusic.StartMarkers, position, markerType, 0, loopStart, 0, (uint)TemporalSelectedMusic.Markers.Count, IMA_States[0], IMA_States[1]);
                    AddMarkerStartToListView(MarkerStart);

                    //-----------------------------------------------------[Markers]-----------------------------------------------------
                    EXStreamMarker Marker = MarkerFunctions.CreateMarker(TemporalSelectedMusic.Markers, v_MarkerName, position, markerType, 0, (uint)TemporalSelectedMusic.Markers.Count, 0, 0);
                    AddMarkerDataToListView(Marker);
                }
            }
            else
            {
                loopStart = (uint)Numeric_MarkerLoopStart.Value * 2;
                position = (uint)Numeric_Position.Value * 2;

                v_MarkerName = TemporalSelectedSound.StartMarkers.Count;

                if (markerType == (int)GenericFunctions.ESoundMarkers.End)
                {
                    //--------------------------------------------------[Markers]--------------------------------------------------------
                    //----Add End Marker----
                    EXStreamMarker MarkerStart = MarkerFunctions.CreateMarker(TemporalSelectedSound.Markers, -1, position, markerType, 2, (uint)TemporalSelectedSound.StartMarkers.Count, 0, 0);
                    AddMarkerDataToListView(MarkerStart);

                }
                else if (markerType == (int)GenericFunctions.ESoundMarkers.Loop)
                {
                    //Calculate States --Loop Start Control--
                    IMA_States = MarkerFunctions.GetEngineXMarkerStates_Mono(TemporalSelectedSound.IMA_ADPCM_DATA, (int)loopStart);

                    //--------------------------------------------------[Start Markers]--------------------------------------------------
                    EXStreamStartMarker MarkerStartData = MarkerFunctions.CreateStartMarker(TemporalSelectedSound.StartMarkers, loopStart, 10, 2, 0, 0, (uint)TemporalSelectedSound.Markers.Count, IMA_States[0], IMA_States[1]);
                    AddMarkerStartToListView(MarkerStartData);

                    //-----------------------------------------------------[Markers Start Loop]------------------------------------------
                    //----Add Start Marker----
                    EXStreamMarker MarkerLoopStart = MarkerFunctions.CreateMarker(TemporalSelectedSound.Markers, v_MarkerName, loopStart, 10, 2, (uint)TemporalSelectedSound.Markers.Count, 0, 0);
                    AddMarkerDataToListView(MarkerLoopStart);

                    //----Add Loop Marker----
                    EXStreamMarker MarkerLoop = MarkerFunctions.CreateMarker(TemporalSelectedSound.Markers, v_MarkerName, position, markerType, 2, (uint)TemporalSelectedSound.Markers.Count, loopStart, 1);
                    AddMarkerDataToListView(MarkerLoop);

                    //-----------------------------------------------------[Markers End Loop]--------------------------------------------
                    //Calculate States
                    IMA_States = MarkerFunctions.GetEngineXMarkerStates_Mono(TemporalSelectedSound.IMA_ADPCM_DATA, (int)position);

                    v_MarkerName = TemporalSelectedSound.StartMarkers.Count;
                    //--------------------------------------------------[Start Markers]--------------------------------------------------
                    EXStreamStartMarker MarkerStart = MarkerFunctions.CreateStartMarker(TemporalSelectedSound.StartMarkers, position, 10, 2, 0, 0, (uint)TemporalSelectedSound.Markers.Count, IMA_States[0], IMA_States[1]);
                    AddMarkerStartToListView(MarkerStart);

                    //-----------------------------------------------------[Markers]-----------------------------------------------------
                    //----Add Start Marker----
                    EXStreamMarker Marker = MarkerFunctions.CreateMarker(TemporalSelectedSound.Markers, v_MarkerName, position, 10, 2, (uint)v_MarkerName, 0, 0);
                    AddMarkerDataToListView(Marker);

                    //Reset Values
                    Numeric_Position.Value = 0;
                    Numeric_MarkerLoopStart.Value = 0;
                }
                else if (markerType == (int)GenericFunctions.ESoundMarkers.Goto)
                {
                    //Calculate States --Position Control--
                    IMA_States = MarkerFunctions.GetEngineXMarkerStates_Mono(TemporalSelectedSound.IMA_ADPCM_DATA, (int)loopStart);

                    //--------------------------------------------------[Start Markers]--------------------------------------------------
                    EXStreamStartMarker MarkerStart = MarkerFunctions.CreateStartMarker(TemporalSelectedSound.StartMarkers, position, 7, 2, 0, 0, (uint)TemporalSelectedSound.Markers.Count, IMA_States[0], IMA_States[1]);
                    AddMarkerStartToListView(MarkerStart);

                    //-----------------------------------------------------[Markers]-----------------------------------------------------
                    EXStreamMarker Marker = MarkerFunctions.CreateMarker(TemporalSelectedSound.Markers, v_MarkerName, position, 7, 2, (uint)TemporalSelectedSound.Markers.Count, loopStart, 1);
                    AddMarkerDataToListView(Marker);
                }
                else if (markerType == (int)GenericFunctions.ESoundMarkers.Pause)
                {
                    //--------------------------------------------------[Markers]--------------------------------------------------------
                    //----Add End Marker----
                    EXStreamMarker MarkerStart = MarkerFunctions.CreateMarker(TemporalSelectedSound.Markers, v_MarkerName, position, markerType, 2, (uint)TemporalSelectedSound.StartMarkers.Count, 0, 0);
                    AddMarkerDataToListView(MarkerStart);
                }
                else
                {
                    //Calculate States --Position Control--
                    IMA_States = MarkerFunctions.GetEngineXMarkerStates_Mono(TemporalSelectedSound.IMA_ADPCM_DATA, (int)position);

                    //--------------------------------------------------[Start Markers]--------------------------------------------------
                    EXStreamStartMarker MarkerStart = MarkerFunctions.CreateStartMarker(TemporalSelectedSound.StartMarkers, position, markerType, 2, 0, 0, (uint)TemporalSelectedSound.Markers.Count, IMA_States[0], IMA_States[1]);
                    AddMarkerStartToListView(MarkerStart);

                    //-----------------------------------------------------[Markers]-----------------------------------------------------
                    EXStreamMarker Marker = MarkerFunctions.CreateMarker(TemporalSelectedSound.Markers, v_MarkerName, position, markerType, 2, (uint)TemporalSelectedSound.Markers.Count, 0, 0);
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
            ListView_MarkerData.Items.Add(new ListViewItem(new[]
            {
                MarkerItem.Name.ToString(),
                MarkerItem.Position.ToString(),
                EXStreamSoundsFunctions.GetMarkerType(MarkerItem.MusicMakerType),
                MarkerItem.Flags.ToString(),
                MarkerItem.Extra.ToString(),
                MarkerItem.LoopStart.ToString(),
                MarkerItem.MarkerCount.ToString(),
                MarkerItem.LoopMarkerCount.ToString(),
            }));
        }

        private void AddMarkerStartToListView(EXStreamStartMarker MarkerItem)
        {
            ListView_Markers.Items.Add(new ListViewItem(new[]
            {
                MarkerItem.MarkerPos.ToString(),
                MarkerItem.IsInstant.ToString(),
                MarkerItem.StateA.ToString("X8"),
                MarkerItem.StateB.ToString("X8"),
                MarkerItem.MarkerCount.ToString()
            }));
        }

        private void PrintStartMarkers(List<EXStreamStartMarker> StartMarkersList)
        {
            foreach (EXStreamStartMarker markerToPrint in StartMarkersList)
            {
                AddMarkerStartToListView(markerToPrint);
            }
        }

        private void PrintMarkers(List<EXStreamMarker> MarkersList)
        {
            foreach (EXStreamMarker markerToPrint in MarkersList)
            {
                AddMarkerDataToListView(markerToPrint);
            }
        }
    }
}
