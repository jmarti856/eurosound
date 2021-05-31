using EuroSound_Application.CurrentProjectFunctions;
using EuroSound_Application.HashCodesFunctions;
using EuroSound_Application.TreeViewLibraryFunctions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EuroSound_Application.EuroSoundInterchangeFile.Functions
{
    internal class ESIF_LoaderFunctions
    {
        internal Regex RemoveCharactersFromPathString = new Regex(@"[\p{Cc}\p{Cf}\p{Mn}\p{Me}\p{Zl}\p{Zp}]");

        //*===============================================================================================
        //* Functions
        //*===============================================================================================
        internal bool AddItemInCustomFolder(string ParentFolderName, string NewSoundKey, string NodeName, string RootFolderName, byte TypeOfObject, TreeView TreeViewControl, Color DefaultNodeColor, List<string> ImportResults)
        {
            bool NodeAddedInFolder = false;

            if (!string.IsNullOrEmpty(ParentFolderName))
            {
                //Check the folder exists
                TreeNode Folders = TreeNodeFunctions.SearchNodeRecursiveByText(TreeViewControl.Nodes, ParentFolderName, TreeViewControl, false);
                if (Folders != null)
                {
                    //Check that the folder is in the correct section (Sounds, Audios, StreamSounds)
                    if (TreeNodeFunctions.FindRootNode(Folders).Name.Equals(RootFolderName))
                    {
                        TreeNodeFunctions.TreeNodeAddNewNode(Folders.Name, NewSoundKey.ToString(), GenericFunctions.GetNextAvailableName(NodeName, TreeViewControl), 2, 2, TypeOfObject, false, false, false, DefaultNodeColor, TreeViewControl);
                        NodeAddedInFolder = true;
                    }
                }
                else
                {
                    ImportResults.Add(string.Join("", "1", "The folder ", ParentFolderName, " could not been found"));
                }
            }
            return NodeAddedInFolder;
        }

        internal void ReadProjectSettingsBlock(string[] FileLines, int CurrentIndex, ProjectFile FileProperties, List<string> ImportResults)
        {
            string CurrentKeyWord;
            string[] KeyWordValues;

            while (!FileLines[CurrentIndex].Trim().Equals("}") && CurrentIndex < FileLines.Length)
            {
                CurrentKeyWord = GetKeyWord(FileLines[CurrentIndex]);
                KeyWordValues = GetKeyValues(FileLines[CurrentIndex]);
                if (KeyWordValues.Length > 0)
                {
                    if (CurrentKeyWord.Equals("FILENAME"))
                    {
                        FileProperties.FileName = KeyWordValues[0];
                        GenericFunctions.SetCurrentFileLabel(FileProperties.FileName, "SBPanel_File");
                    }
                    if (CurrentKeyWord.Equals("HASHCODE"))
                    {
                        FileProperties.Hashcode = Convert.ToUInt32(KeyWordValues[0], 16);
                        GenericFunctions.SetCurrentFileLabel(Hashcodes.GetHashcodeLabel(Hashcodes.SB_Defines, FileProperties.Hashcode), "SBPanel_Hashcode");
                    }
                }
                else
                {
                    if (!CurrentKeyWord.Equals("COMMENT"))
                    {
                        ImportResults.Add(string.Join(" ", "0Error in line:", (CurrentIndex + 1), CurrentKeyWord, "does not contains any value"));
                        break;
                    }
                }
                CurrentIndex++;
            }
        }

        internal string GetKeyWord(string FileLine)
        {
            string KeyWord = string.Empty;

            MatchCollection Matches = Regex.Matches(FileLine, @"(?<=[*])\w[A-Z]+");
            if (Matches.Count > 0)
            {
                KeyWord = Matches[0].ToString();
            }

            return KeyWord;
        }

        internal string[] GetKeyValues(string FileLine)
        {
            string[] Values = Regex.Matches(FileLine, @"(?<=[*])\w+[\s-[\r\n]]*""?(.*?)""?\r?$").Cast<Match>().Select(x => x.Groups[1].Value).ToArray();
            return Values;
        }
    }
}
