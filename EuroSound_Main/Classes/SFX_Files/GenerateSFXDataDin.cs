using EuroSound_Application.ApplicationPreferences;
using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EuroSound_Application.GenerateDataBinaryFile
{

    internal class GenerateSFXDataFiles
    {
        internal List<string> GenerateSFXDataBinaryFile()
        {
            List<string> ErrorsList = new List<string>();

            if (Directory.Exists(GlobalPreferences.MusicOutputPath))
            {
                BinaryStream BWriter = new BinaryStream(File.Open(GlobalPreferences.MusicOutputPath + "\\SFX_Data.bin", FileMode.Create, FileAccess.Write), null, Encoding.ASCII);

                foreach (KeyValuePair<uint, float[]> Item in Hashcodes.SFX_Data)
                {
                    float[] Values = Item.Value;

                    //Hashcode
                    BWriter.WriteUInt32(Convert.ToUInt32(((int)Values[0])));
                    //Inner Radius
                    BWriter.WriteUInt32(Convert.ToUInt32(FloatToHex(Values[1]), 16));
                    //Outer Radius
                    BWriter.WriteUInt32(Convert.ToUInt32(FloatToHex(Values[2]), 16));
                    //Alertness
                    BWriter.WriteUInt32(Convert.ToUInt32(FloatToHex(Values[3]), 16));
                    //Duration
                    BWriter.WriteUInt32(Convert.ToUInt32(FloatToHex(Values[4]), 16));
                    //Looping
                    BWriter.WriteSByte(Convert.ToSByte((int)Values[5]));
                    //Tracking 3D
                    BWriter.WriteSByte(Convert.ToSByte((int)Values[6]));
                    //SampleStreamed
                    BWriter.WriteSByte(Convert.ToSByte((int)Values[7]));
                    //Padding
                    BWriter.WriteSByte(Convert.ToSByte(0));
                }

                BWriter.Close();
            }
            else
            {
                ErrorsList.Add("0Unable to open: " + GlobalPreferences.MusicOutputPath + "\\SFX_Data.bin");
            }

            return ErrorsList;
        }

        private string FloatToHex(float Number)
        {
            string HexNumber;

            uint NumberData = BitConverter.ToUInt32(BitConverter.GetBytes(Number), 0);
            HexNumber = NumberData.ToString("X8");

            return HexNumber;
        }
    }
}