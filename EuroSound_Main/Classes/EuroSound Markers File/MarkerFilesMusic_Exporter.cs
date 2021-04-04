using EuroSound_Application.Musics;
using EuroSound_Application.StreamSounds;
using System;
using System.IO;

namespace EuroSound_Application.MarkerFiles
{
    class MarkerFilesMusic_Exporter
    {
        internal void ExportMarkersFromMusic(string FilePath, string SoundName, EXMusic MusicToExport)
        {
            using (StreamWriter TextFileWriter = File.CreateText(FilePath))
            {
                TextFileWriter.WriteLine("*EUROSOUND_MARKERS_FILE V1.0");
                TextFileWriter.WriteLine("*COMMENT EuroSound Version: {0}", GenericFunctions.GetEuroSoundVersion());
                TextFileWriter.WriteLine("*COMMENT " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
                TextFileWriter.WriteLine("*COMMENT Markers Exported from: \"{0}\"", SoundName);
                TextFileWriter.WriteLine("");
                TextFileWriter.WriteLine("*STRSTARTMARKERS " + MusicToExport.StartMarkers.Count + " {");
                foreach (EXStreamStartMarker StartMarker in MusicToExport.StartMarkers)
                {
                    TextFileWriter.WriteLine("\t*MARKER {");
                    TextFileWriter.WriteLine("\t\t*COMMENT Stream {0} Marker", EXStreamSoundsFunctions.GetMarkerType(StartMarker.MusicMakerType));
                    TextFileWriter.WriteLine("\t\t*POSITION={0}", StartMarker.Position);
                    TextFileWriter.WriteLine("\t\t*TYPE={0}", StartMarker.MusicMakerType);
                    TextFileWriter.WriteLine("\t\t*MARKERCOUNT={0}", StartMarker.MarkerCount);
                    TextFileWriter.WriteLine("\t\t*MARKERPOS={0}", StartMarker.MarkerPos);
                    TextFileWriter.WriteLine("\t\t*STATEA={0}", StartMarker.StateA);
                    TextFileWriter.WriteLine("\t\t*STATEB={0}", StartMarker.StateB);
                    TextFileWriter.WriteLine("\t}");
                }
                TextFileWriter.WriteLine("}");
                TextFileWriter.WriteLine("");
                TextFileWriter.WriteLine("*STRMARKERS " + MusicToExport.Markers.Count + " {");
                foreach (EXStreamMarker Marker in MusicToExport.Markers)
                {
                    TextFileWriter.WriteLine("\t*MARKER {");
                    TextFileWriter.WriteLine("\t\t*COMMENT Stream {0} Marker", EXStreamSoundsFunctions.GetMarkerType(Marker.MusicMakerType));
                    TextFileWriter.WriteLine("\t\t*NAME={0}", Marker.Name);
                    TextFileWriter.WriteLine("\t\t*POSITION={0}", Marker.Position);
                    TextFileWriter.WriteLine("\t\t*TYPE={0}", Marker.MusicMakerType);
                    TextFileWriter.WriteLine("\t\t*MARKERCOUNT={0}", Marker.MarkerCount);
                    TextFileWriter.WriteLine("\t\t*LOOPSTART={0}", Marker.LoopStart);
                    TextFileWriter.WriteLine("\t\t*LOOPMARKERCOUNT={0}", Marker.LoopMarkerCount);
                    TextFileWriter.WriteLine("\t}");
                }
                TextFileWriter.WriteLine("}");
            }
        }
    }
}
