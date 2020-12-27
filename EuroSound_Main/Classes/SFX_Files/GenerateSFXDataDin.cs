using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EuroSound_Application
{

    internal class GenerateSFXDataFiles
    {
        internal List<string> GenerateSFXDataBinaryFile()
        {
            List<string> ErrorsList = new List<string>();

            if (Directory.Exists(GlobalPreferences.MusicOutputPath))
            {
                BinaryWriter BWriter = new BinaryWriter(File.Open(GlobalPreferences.MusicOutputPath + "\\SFX_Data.bin", FileMode.Create, FileAccess.Write), Encoding.ASCII);

                foreach (KeyValuePair<uint, float[]> Item in Hashcodes.SFX_Data)
                {
                    float[] Values = Item.Value;

                    //Hashcode
                    BWriter.Write(Convert.ToUInt32(((int)Values[0])));
                    //Inner Radius
                    BWriter.Write(Convert.ToUInt32(FloatToHex(Values[1]), 16));
                    //Outer Radius
                    BWriter.Write(Convert.ToUInt32(FloatToHex(Values[2]), 16));
                    //Alertness
                    BWriter.Write(Convert.ToUInt32(FloatToHex(Values[3]), 16));
                    //Duration
                    BWriter.Write(Convert.ToUInt32(FloatToHex(Values[4]), 16));
                    //Looping
                    BWriter.Write(Convert.ToSByte((int)Values[5]));
                    //Tracking 3D
                    BWriter.Write(Convert.ToSByte((int)Values[6]));
                    //SampleStreamed
                    BWriter.Write(Convert.ToSByte((int)Values[7]));
                    //Padding
                    BWriter.Write(Convert.ToSByte(0));
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