using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EuroSound_Application
{
    class GenerateSFXDataFiles
    {
        internal static void GenerateSFXDataBinaryFile(string OutputPath, ListView ControlToPrint)
        {
            BinaryWriter BWriter = new BinaryWriter(File.Open(OutputPath + "\\SFX_Data.bin", FileMode.Create, FileAccess.Write), Encoding.ASCII);

            foreach (KeyValuePair<string, float[]> Item in Hashcodes.SFX_Data)
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


        private static string FloatToHex(float Number)
        {
            string HexNumber;

            uint NumberData = BitConverter.ToUInt32(BitConverter.GetBytes(Number), 0);
            HexNumber = NumberData.ToString("X8");

            return HexNumber;
        }
    }
}
