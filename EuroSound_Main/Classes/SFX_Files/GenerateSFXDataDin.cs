using EuroSound_Application.ApplicationPreferences;
using EuroSound_Application.HashCodesFunctions;
using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EuroSound_Application.GenerateDataBinaryFile
{

    internal class GenerateSFXDataFiles
    {
        internal IEnumerable<string> GenerateSFXDataBinaryFile()
        {
            if (Directory.Exists(GlobalPreferences.DebugFilesOutputDirectory))
            {
                using (BinaryStream BWriter = new BinaryStream(File.Open(GlobalPreferences.DebugFilesOutputDirectory + "\\SFX_Data.bin", FileMode.Create, FileAccess.Write), null, Encoding.ASCII))
                {
                    foreach (float[] Values in Hashcodes.SFX_Data)
                    {
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
            }
            else
            {
                yield return string.Join("", new string[] { "0Unable to open: ", GlobalPreferences.DebugFilesOutputDirectory, "\\SFX_Data.bin" });
            }
        }

        private string FloatToHex(float Number)
        {
            uint NumberData = BitConverter.ToUInt32(BitConverter.GetBytes(Number), 0);
            string HexNumber = NumberData.ToString("X8");

            return HexNumber;
        }
    }
}