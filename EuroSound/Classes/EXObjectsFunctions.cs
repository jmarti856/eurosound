using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace EuroSound_SB_Editor
{
    public static class EXObjectsFunctions
    {
        internal static bool SoundExistsInList(string ItemToSearch, List<EXSound> SoundsList)
        {
            bool ItemExists = false;
            foreach (EXSound item in SoundsList)
            {
                if (item.Name.Equals(ItemToSearch))
                {
                    ItemExists = true;
                    break;
                }
            }

            return ItemExists;
        }

        public static bool SoundContainsSample(EXSound Sound, string SampleName)
        {
            bool ItemExists = false;
            foreach (EXSample item in Sound.Samples)
            {
                if (item.Name.Equals(SampleName))
                {
                    ItemExists = true;
                    break;
                }
            }

            return ItemExists;
        }

        public static bool SoundWillBeOutputed(List<EXSound> SoundsList, string SoundName)
        {
            bool Output = true;

            if (!(SoundName.Equals("Sounds") || SoundName.Equals("Streamed Sounds")))
            {
                foreach (EXSound Sound in SoundsList)
                {
                    if (Sound.Name.Equals(SoundName))
                    {
                        if (!Sound.OutputThisSound)
                        {
                            Output = false;
                            break;
                        }
                    }
                }
            }

            return Output;
        }

        internal static void RemoveSampleFromSound(EXSound SoundName, string SampleName)
        {
            EXSample SampleToRemove = TreeNodeFunctions.GetSelectedSample(SoundName, SampleName);
            if (SampleToRemove != null)
            {
                SoundName.Samples.Remove(SampleToRemove);
                Debug.WriteLine(string.Format("INFO -- Object {0} removed from the list.", SampleToRemove.Name));
            }
            else
            {
                Debug.WriteLine(string.Format("WARNING -- Object {0} not found in the list.", SampleName));
            }
        }

        internal static void RemoveSound(string Name, List<EXSound> SoundsList)
        {
            EXSound itemToRemove = TreeNodeFunctions.GetSelectedSound(Name, SoundsList);
            if (itemToRemove != null)
            {
                SoundsList.Remove(itemToRemove);
                Debug.WriteLine(string.Format("INFO -- Object {0} removed from the list.", itemToRemove.Name));
            }
            else
            {
                Debug.WriteLine(string.Format("WARNING -- Object {0} not found in the list.", Name));
            }
        }

        internal static void AddNewSound(string v_Name, string v_DisplayName, string v_Hashcode, int[] properties, List<EXSound> SoundsList)
        {
            /*string[] properties
            properties[0] = duckerLength || properties[1] = minDelay || properties[2] = maxDelay || properties[3] = innerRadiusReal
            properties[4] = outerRadiusReal || properties[5] = reverbSend || properties[6] = trackingType || properties[7] = maxVoices 
            properties[8] = priority || properties[9] = ducker || properties[10] = masterVolume || properties[11] = flags*/
            EXSound Sound = new EXSound
            {
                Name = RemoveWhiteSpaces(v_Name),
                DisplayName = v_DisplayName,
                Hashcode = v_Hashcode,

                /*EngineX required*/
                DuckerLenght = properties[0],
                MinDelay = properties[1],
                MaxDelay = properties[2],
                InnerRadiusReal = properties[3],
                OuterRadiusReal = properties[4],
                ReverbSend = properties[5],
                TrackingType = properties[6],
                MaxVoices = properties[7],
                Priority = properties[8],
                Ducker = properties[9],
                MasterVolume = properties[10],
                Flags = properties[11]
            };

            SoundsList.Add(Sound);
        }

        internal static bool AddSampleToSound(EXSound Sound, string SampleName, int[] SampleParams, bool StreamedSample)
        {
            bool AddedCorrectly = false;

            EXSample Sample = new EXSample
            {
                Name = RemoveWhiteSpaces(SampleName),
                DisplayName = SampleName,
                IsStreamed = StreamedSample,

                /*--Required for EngineX*/
                FileRef = SampleParams[0],
                PitchOffset = SampleParams[1],
                RandomPitchOffset = SampleParams[2],
                BaseVolume = SampleParams[3],
                RandomVolumeOffset = SampleParams[4],
                Pan = SampleParams[5],
                RandomPan = SampleParams[6]
            };


            Sound.Samples.Add(Sample);

            return AddedCorrectly;
        }

        internal static string RemoveWhiteSpaces(string TextToModify)
        {
            string NewString = string.Empty;

            /*Before remove whitespaces, first check that is not null*/
            if (!(string.IsNullOrEmpty(TextToModify)))
            {
                NewString = Regex.Replace(TextToModify, @"\s", "");
            }

            return NewString;
        }

        internal static EXSound GetSoundByName(string NameToSearch, List<EXSound> SoundsList)
        {
            EXSound config = SoundsList.Find(item => item.Name == NameToSearch);
            return config;
        }

        internal static byte[] GetRawPCMData(string AudioFilePath)
        {
            int dataSize;

            BinaryReader Reader = new BinaryReader(File.Open(AudioFilePath, FileMode.Open, FileAccess.Read));

            /*Go to RAW PCM data*/
            Reader.BaseStream.Seek(0x28, SeekOrigin.Begin);

            /*Read size*/
            dataSize = Reader.ReadInt32();

            /*Get data*/
            _ = new byte[dataSize];
            byte[] byteArray = Reader.ReadBytes(dataSize);

            Reader.Close();
            Reader.Dispose();

            return byteArray;
        }

        internal static List<EXSound> GetFinalListToExport(List<EXSound> SoundsList)
        {
            List<EXSound> FinalList = new List<EXSound>();

            foreach (EXSound Sound in SoundsList)
            {
                if (Sound.OutputThisSound)
                {
                    FinalList.Add(Sound);
                }
            }

            return FinalList;
        }
    }
}
