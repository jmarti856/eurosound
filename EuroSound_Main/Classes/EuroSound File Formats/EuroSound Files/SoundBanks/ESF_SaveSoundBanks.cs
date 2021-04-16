using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.SoundBanksEditor;
using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundSoundBanksFilesFunctions
{
    internal class ESF_SaveSoundBanks
    {
        internal void SaveSoundBanks(BinaryStream BWriter, TreeView TreeViewControl, Dictionary<uint, EXSound> SoundsList, Dictionary<string, EXAudio> AudiosList, ProjectFile FileProperties)
        {
            long AlignOffset;

            //File Hashcode
            BWriter.Write(FileProperties.Hashcode);
            //Latest SoundID value
            BWriter.Write(FileProperties.SoundID);
            //TreeViewData Offset
            BWriter.Write(Convert.ToUInt32(00000000));
            //SoundsListData Offset
            BWriter.Write(Convert.ToUInt32(00000000));
            //AudioData Offset
            BWriter.Write(Convert.ToUInt32(00000000));
            //FileSize
            BWriter.Write(Convert.ToUInt32(00000000));
            //File Name
            BWriter.Write(FileProperties.FileName);
            //Associated Profile
            BWriter.Write(GlobalPreferences.SelectedProfile);
            //Associated Profile Name
            BWriter.Write(GlobalPreferences.SelectedProfileName);

            //Go to section
            BWriter.Seek(2048, SeekOrigin.Current);

            //*===============================================================================================
            //* TreeView
            //*===============================================================================================
            //Align Bytes
            AlignOffset = (BWriter.BaseStream.Position + 2048) & (2048 - 1);
            BWriter.Seek(AlignOffset, SeekOrigin.Current);
            BWriter.Align(16);

            //Write Data
            long TreeViewDataOffset = BWriter.BaseStream.Position;
            SaveTreeViewData(TreeViewControl, BWriter);

            //*===============================================================================================
            //* Sounds List Data
            //*===============================================================================================
            //Align Bytes
            AlignOffset = (BWriter.BaseStream.Position + 2048) & (2048 - 1);
            BWriter.Seek(AlignOffset, SeekOrigin.Current);
            BWriter.Align(16);

            //Write Data
            long SoundsListDataOffset = BWriter.BaseStream.Position;
            SaveSoundsListData(SoundsList, BWriter);

            //*===============================================================================================
            //* Audio Data
            //*===============================================================================================
            //Align Bytes
            AlignOffset = (BWriter.BaseStream.Position + 2048) & (2048 - 1);
            BWriter.Seek(AlignOffset, SeekOrigin.Current);
            BWriter.Align(16);

            //Write Data
            long AudioDataOffset = BWriter.BaseStream.Position;
            SaveAudiosData(AudiosList, BWriter);
            long FileSize = BWriter.BaseStream.Position;

            //*===============================================================================================
            //* FINAL OFFSETS
            //*===============================================================================================
            //Go to section offsets position
            BWriter.Seek(0x10, SeekOrigin.Begin);

            //Write section offsets
            BWriter.Write(Convert.ToUInt32(TreeViewDataOffset));
            BWriter.Write(Convert.ToUInt32(SoundsListDataOffset));
            BWriter.Write(Convert.ToUInt32(AudioDataOffset));

            //Write File Size
            BWriter.Write(Convert.ToUInt32(FileSize));
        }

        private void SaveAudiosData(Dictionary<string, EXAudio> AudiosList, BinaryStream BWriter)
        {
            BWriter.Write(AudiosList.Count);

            foreach (KeyValuePair<string, EXAudio> Entry in AudiosList)
            {
                BWriter.Write(Entry.Key);
                BWriter.Write(Entry.Value.Dependencies);
                BWriter.Write(Entry.Value.LoadedFileName);
                BWriter.Write(Entry.Value.Encoding);
                BWriter.Write(Entry.Value.Flags);
                BWriter.Write(Entry.Value.DataSize);
                BWriter.Write(Entry.Value.Frequency);
                BWriter.Write(Entry.Value.RealSize);
                BWriter.Write(Entry.Value.Channels);
                BWriter.Write(Entry.Value.Bits);
                BWriter.Write(Entry.Value.PSIsample);
                BWriter.Write(Entry.Value.LoopOffset);
                BWriter.Write(Entry.Value.Duration);
                BWriter.Write(Entry.Value.PCMdata.Length);
                BWriter.Write(Entry.Value.PCMdata);
            }
        }

        private void SaveSoundsListData(Dictionary<uint, EXSound> SoundsList, BinaryStream BWriter)
        {
            BWriter.Write(SoundsList.Count);

            foreach (KeyValuePair<uint, EXSound> SoundItem in SoundsList)
            {
                //Display Info
                BWriter.Write(SoundItem.Key);
                BWriter.Write(SoundItem.Value.Hashcode);
                BWriter.Write(SoundItem.Value.OutputThisSound);

                //---Required for EngineX---
                BWriter.Write(SoundItem.Value.DuckerLenght);
                BWriter.Write(SoundItem.Value.MinDelay);
                BWriter.Write(SoundItem.Value.MaxDelay);
                BWriter.Write(SoundItem.Value.InnerRadiusReal);
                BWriter.Write(SoundItem.Value.OuterRadiusReal);
                BWriter.Write(SoundItem.Value.ReverbSend);
                BWriter.Write(SoundItem.Value.TrackingType);
                BWriter.Write(SoundItem.Value.MaxVoices);
                BWriter.Write(SoundItem.Value.Priority);
                BWriter.Write(SoundItem.Value.Ducker);
                BWriter.Write(SoundItem.Value.MasterVolume);
                BWriter.Write(SoundItem.Value.Flags);

                //Write Samples
                BWriter.Write(SoundItem.Value.Samples.Count);
                foreach (KeyValuePair<uint, EXSample> ItemSample in SoundItem.Value.Samples)
                {
                    //Key
                    BWriter.Write(ItemSample.Key);

                    //Display Info
                    BWriter.Write(ItemSample.Value.IsStreamed);
                    BWriter.Write(ItemSample.Value.FileRef);
                    BWriter.Write(ItemSample.Value.ComboboxSelectedAudio);
                    BWriter.Write(Convert.ToUInt32(ItemSample.Value.HashcodeSubSFX));

                    //---Required for EngineX---
                    BWriter.Write(ItemSample.Value.PitchOffset);
                    BWriter.Write(ItemSample.Value.RandomPitchOffset);
                    BWriter.Write(ItemSample.Value.BaseVolume);
                    BWriter.Write(ItemSample.Value.RandomVolumeOffset);
                    BWriter.Write(ItemSample.Value.Pan);
                    BWriter.Write(ItemSample.Value.RandomPan);
                }
            }
        }

        private void SaveTreeViewData(TreeView TreeViewControl, BinaryStream BWriter)
        {
            BWriter.Write((TreeViewControl.GetNodeCount(true) - 3));
            SaveTreeNodes(TreeViewControl.Nodes[0], BWriter);
            SaveTreeNodes(TreeViewControl.Nodes[1], BWriter);
            SaveTreeNodes(TreeViewControl.Nodes[2], BWriter);
        }

        private void SaveTreeNodes(TreeNode Selected, BinaryStream BWriter)
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
                BWriter.Write(Selected.Name);
                BWriter.Write(Selected.Text);
                BWriter.Write(Selected.SelectedImageIndex);
                BWriter.Write(Selected.ImageIndex);
                BWriter.Write(Selected.Tag.ToString());
                BWriter.Write(Selected.ForeColor.ToArgb());
                BWriter.Write(Selected.IsVisible);
            }
            foreach (TreeNode Node in Selected.Nodes)
            {
                SaveTreeNodes(Node, BWriter);
            }
        }
    }
}
