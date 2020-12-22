using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EuroSound_Application
{
    internal class GenerateSFXDataFiles
    {
        internal void GenerateSFXDataBinaryFile(string OutputPath)
        {
            BinaryWriter BWriter = new BinaryWriter(File.Open(OutputPath + "\\SFX_Data.bin", FileMode.Create, FileAccess.Write), Encoding.ASCII);

            foreach (KeyValuePair<Int32, float[]> Item in Hashcodes.SFX_Data)
            {
                float[] Values = Item.Value;
                BWriter.Write(Convert.ToUInt32(((int)Values[0]).ToString("X8"), 16));
                BWriter.Write(Convert.ToUInt32(FloatToHex(Values[1]), 16));
                BWriter.Write(Convert.ToUInt32(FloatToHex(Values[2]), 16));
                BWriter.Write(Convert.ToUInt32(FloatToHex(Values[3]), 16));
                BWriter.Write(Convert.ToUInt32(FloatToHex(Values[4]), 16));
                BWriter.Write(Convert.ToSByte((int)Values[5]));
                BWriter.Write(Convert.ToSByte((int)Values[6]));
                BWriter.Write(Convert.ToSByte((int)Values[7]));
                BWriter.Write(Convert.ToSByte(0));
            }

            BWriter.Close();
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