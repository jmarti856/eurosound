using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_SB_Editor
{
    static class SaveData
    {
        internal static void SaveDataToEuroSoundFile(TreeView TreeViewControl, List<EXSound> SoundsList, string FilePath)
        {
            BinaryWriter BWriter = new BinaryWriter(File.Open(FilePath, FileMode.Create, FileAccess.Write), Encoding.ASCII);

            BWriter.Write(Encoding.ASCII.GetBytes("ESF"));
            BWriter.Write(EXFile.TypeOfData);
            BWriter.Write(EXFile.FileName);
            BWriter.Write(EXFile.Hashcode);
            BWriter.Write(EXFile.HT_SoundsDataPath);
            BWriter.Write(EXFile.HT_SoundsDataMD5);
            BWriter.Write(EXFile.HT_SoundsPath);
            BWriter.Write(EXFile.HT_SoundsMD5);

            /*Tree view Data*/
            BWriter.Write((Convert.ToInt32(BWriter.BaseStream.Position) + 200));
            BWriter.Seek(200, SeekOrigin.Current);
            SaveTreeViewData(TreeViewControl, BWriter);

            /*SoundsList Data*/
            BWriter.Write((Convert.ToInt32(BWriter.BaseStream.Position) + 100));
            BWriter.Seek(100, SeekOrigin.Current);
            SaveSoundsListData(SoundsList, BWriter);

            BWriter.Close();
            BWriter.Dispose();
        }

        private static void SaveSoundsListData(List<EXSound> SoundsList, BinaryWriter BWriter)
        {
            BWriter.Write(SoundsList.Count);

            foreach (EXSound SoundItem in SoundsList)
            {
                /*Display Info*/
                BWriter.Write(SoundItem.Hashcode);
                BWriter.Write(SoundItem.Name);
                BWriter.Write(SoundItem.DisplayName);

                /*---Required for EngineX---*/
                BWriter.Write(SoundItem.DuckerLenght);
                BWriter.Write(SoundItem.MinDelay);
                BWriter.Write(SoundItem.MaxDelay);
                BWriter.Write(SoundItem.InnerRadiusReal);
                BWriter.Write(SoundItem.OuterRadiusReal);
                BWriter.Write(SoundItem.ReverbSend);
                BWriter.Write(SoundItem.TrackingType);
                BWriter.Write(SoundItem.MaxVoices);
                BWriter.Write(SoundItem.Priority);
                BWriter.Write(SoundItem.Ducker);
                BWriter.Write(SoundItem.MasterVolume);
                BWriter.Write(SoundItem.Flags);

                /*Write Samples*/
                BWriter.Write(SoundItem.Samples.Count);
                foreach (EXSample ItemSample in SoundItem.Samples)
                {
                    /*Display Info*/
                    BWriter.Write(ItemSample.Name);
                    BWriter.Write(ItemSample.DisplayName);
                    BWriter.Write(ItemSample.IsStreamed);
                    BWriter.Write(ItemSample.FileRef);

                    /*---Required for EngineX---*/
                    BWriter.Write(ItemSample.PitchOffset);
                    BWriter.Write(ItemSample.RandomPitchOffset);
                    BWriter.Write(ItemSample.BaseVolume);
                    BWriter.Write(ItemSample.RandomVolumeOffset);
                    BWriter.Write(ItemSample.Pan);
                    BWriter.Write(ItemSample.RandomPan);

                    /*Check we have added an audio*/
                    BWriter.Write(ItemSample.Audio.IsEmpty());

                    if (ItemSample.Audio.IsEmpty() == false)
                    {
                        /*Write Audio*/
                        BWriter.Write(ItemSample.Audio.Name);
                        BWriter.Write(ItemSample.Audio.AllData.Length);
                        BWriter.Write(ItemSample.Audio.AllData);
                        BWriter.Write(ItemSample.Audio.Encoding);

                        /*---Required for EngineX---*/
                        BWriter.Write(ItemSample.Audio.Flags);

                        BWriter.Write(ItemSample.Audio.DataSize);
                        BWriter.Write(ItemSample.Audio.Frequency);
                        BWriter.Write(ItemSample.Audio.RealSize);
                        BWriter.Write(ItemSample.Audio.Channels);
                        BWriter.Write(ItemSample.Audio.Bits);
                        BWriter.Write(ItemSample.Audio.PSIsample);
                        BWriter.Write(ItemSample.Audio.LoopOffset);
                        BWriter.Write(ItemSample.Audio.Duration);
                        BWriter.Write(ItemSample.Audio.PCMdata.Length);
                        BWriter.Write(ItemSample.Audio.PCMdata);
                    }
                }
            }
        }

        private static void SaveTreeViewData(TreeView TreeViewControl, BinaryWriter BWriter)
        {
            BWriter.Write((TreeViewControl.GetNodeCount(true) - 2));
            SaveTreeNodes(TreeViewControl, TreeViewControl.Nodes[0], BWriter);
            SaveTreeNodes(TreeViewControl, TreeViewControl.Nodes[1], BWriter);
        }

        private static void SaveTreeNodes(TreeView TreeViewControl, TreeNode Selected, BinaryWriter BWriter)
        {
            if (!Selected.Tag.Equals("Root"))
            {
                if (Selected.Parent == null)
                {
                    BWriter.Write("Root");
                }
                else
                {
                    BWriter.Write(Selected.Parent.Name);
                }
                BWriter.Write(Selected.Text);
                BWriter.Write(Selected.SelectedImageIndex);
                BWriter.Write(Selected.ImageIndex);
                BWriter.Write(Selected.Tag.ToString());
                BWriter.Write(Selected.ForeColor.ToArgb());

            }
            foreach (TreeNode Node in Selected.Nodes)
            {
                SaveTreeNodes(TreeViewControl, Node, BWriter);
            }
        }

        internal static void LoadDataFromEuroSoundFile(TreeView TreeViewControl, List<EXSound> SoundsList, ToolStripLabel ProjectName, string FilePath)
        {
            //Init reader
            BinaryReader BReader = new BinaryReader(File.Open(FilePath, FileMode.Open, FileAccess.Read), Encoding.ASCII);
            if (FileIsCorrect(BReader))
            {
                EXFile.TypeOfData = BReader.ReadInt32();
                EXFile.FileName = BReader.ReadString();
                EXFile.Hashcode = BReader.ReadString();
                EXFile.HT_SoundsDataPath = BReader.ReadString();
                EXFile.HT_SoundsDataMD5 = BReader.ReadString();
                EXFile.HT_SoundsPath = BReader.ReadString();
                EXFile.HT_SoundsMD5 = BReader.ReadString();

                /*Tree view Data*/
                BReader.BaseStream.Position = (BReader.ReadInt32() + 4);
                ReadTreeViewData(BReader, TreeViewControl);

                /*Sounds list data*/
                BReader.BaseStream.Position = (BReader.ReadInt32() + 4);
                ReadSoundsListData(BReader, SoundsList);

                ProjectName.Text = EXFile.FileName;
            }
            BReader.Close();
            BReader.Dispose();

            TreeViewControl.Nodes[0].Collapse();
            TreeViewControl.Nodes[0].Expand();
            TreeViewControl.Nodes[1].Collapse();
            TreeViewControl.Nodes[1].Expand();
        }
        private static void ReadSoundsListData(BinaryReader BReader, List<EXSound> SoundsList)
        {
            int NumberOfSounds, NumberOfSamples, AllAuidoDataLength, AudioPCMdataLength;
            bool SampleAudioIsEmpty;

            NumberOfSounds = BReader.ReadInt32();

            for (int i = 0; i < NumberOfSounds; i++)
            {
                EXSound NewSound = new EXSound
                {
                    Hashcode = BReader.ReadString(),
                    Name = BReader.ReadString(),
                    DisplayName = BReader.ReadString(),

                    /*---Required for EngineX---*/
                    DuckerLenght = BReader.ReadInt32(),
                    MinDelay = BReader.ReadInt32(),
                    MaxDelay = BReader.ReadInt32(),
                    InnerRadiusReal = BReader.ReadInt32(),
                    OuterRadiusReal = BReader.ReadInt32(),
                    ReverbSend = BReader.ReadInt32(),
                    TrackingType = BReader.ReadInt32(),
                    MaxVoices = BReader.ReadInt32(),
                    Priority = BReader.ReadInt32(),
                    Ducker = BReader.ReadInt32(),
                    MasterVolume = BReader.ReadInt32(),
                    Flags = BReader.ReadInt32()
                };

                NumberOfSamples = BReader.ReadInt32();
                for (int j = 0; j < NumberOfSamples; j++)
                {
                    EXSample NewSample = new EXSample
                    {
                        Name = BReader.ReadString(),
                        DisplayName = BReader.ReadString(),
                        IsStreamed = BReader.ReadBoolean(),
                        FileRef = BReader.ReadInt32(),

                        /*---Required For EngineX---*/
                        PitchOffset = BReader.ReadInt32(),
                        RandomPitchOffset = BReader.ReadInt32(),
                        BaseVolume = BReader.ReadInt32(),
                        RandomVolumeOffset = BReader.ReadInt32(),
                        Pan = BReader.ReadInt32(),
                        RandomPan = BReader.ReadInt32()
                    };

                    SampleAudioIsEmpty = BReader.ReadBoolean();
                    if (SampleAudioIsEmpty == false)
                    {
                        NewSample.Audio.Name = BReader.ReadString();
                        AllAuidoDataLength = BReader.ReadInt32();
                        NewSample.Audio.AllData = BReader.ReadBytes(AllAuidoDataLength);
                        NewSample.Audio.Encoding = BReader.ReadString();

                        /*---Required for EngineX---*/
                        NewSample.Audio.Flags = BReader.ReadInt32();
                        NewSample.Audio.DataSize = BReader.ReadInt32();
                        NewSample.Audio.Frequency = BReader.ReadInt32();
                        NewSample.Audio.RealSize = BReader.ReadInt32();
                        NewSample.Audio.Channels = BReader.ReadInt32();
                        NewSample.Audio.Bits = BReader.ReadInt32();
                        NewSample.Audio.PSIsample = BReader.ReadInt32();
                        NewSample.Audio.LoopOffset = BReader.ReadInt32();
                        NewSample.Audio.Duration = BReader.ReadInt32();
                        AudioPCMdataLength = BReader.ReadInt32();
                        NewSample.Audio.PCMdata = BReader.ReadBytes(AudioPCMdataLength);
                    }

                    NewSound.Samples.Add(NewSample);
                }

                SoundsList.Add(NewSound);
            }
        }

        private static void ReadTreeViewData(BinaryReader BReader, TreeView TreeViewControl)
        {
            int NumberOfNodes;

            NumberOfNodes = BReader.ReadInt32();

            for (int i = 0; i < NumberOfNodes; i++)
            {
                TreeNodeFunctions.TreeNodeAddNewNode(BReader.ReadString(), BReader.ReadString(), BReader.ReadInt32(), BReader.ReadInt32(), BReader.ReadString(), Color.FromArgb(BReader.ReadInt32()), TreeViewControl);
            }
        }

        private static bool FileIsCorrect(BinaryReader BReader)
        {
            string Magic;
            bool FileCorrect;

            //Read MAGIC
            Magic = Encoding.ASCII.GetString(BReader.ReadBytes(3));

            if (Magic.Equals("ESF"))
            {
                FileCorrect = true;
            }
            else
            {
                FileCorrect = false;
            }

            return FileCorrect;
        }
    }
}
