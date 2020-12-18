using System.Collections;
using System.Windows.Forms;

namespace EuroSound_Application
{
    // Create a node sorter that implements the IComparer interface.
    public class NodeSorter : IComparer
    {
        // Compare the length of the strings, or the strings
        // themselves, if they are the same length.
        public int Compare(object x, object y)
        {
            //int index = 2;

            TreeNode tx = x as TreeNode;
            TreeNode ty = y as TreeNode;

            //if (tx.Tag.ToString().Equals("Folder") || ty.Tag.ToString().Equals("Folder"))
            //{
            //    index = string.Compare(tx.Tag.ToString(), ty.Tag.ToString(), true);
            //}
            return string.Compare(tx.Text, ty.Text);
            //return index;
        }
    }
}