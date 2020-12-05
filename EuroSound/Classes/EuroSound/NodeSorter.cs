using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EuroSound_SB_Editor
{
    // Create a node sorter that implements the IComparer interface.
    public class NodeSorter : IComparer
    {
        // Compare the length of the strings, or the strings
        // themselves, if they are the same length.
        public int Compare(object x, object y)
        {
            int index = 2;

            TreeNode tx = x as TreeNode;
            TreeNode ty = y as TreeNode;

            if (tx.Tag.ToString().Equals("Folder") || ty.Tag.ToString().Equals("Folder"))
            {
                index = string.Compare(tx.Tag.ToString(), ty.Tag.ToString(), true);
            }

            return index;
        }
    }
}
