﻿using System.Collections;
using System.Windows.Forms;

namespace EuroSound_Application.TreeViewSorter
{
    // Create a node sorter that implements the IComparer interface.
    public class NodeSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            TreeNode nx = x as TreeNode;
            TreeNode ny = y as TreeNode;

            // Keep the order of the root nodes...
            if (nx.Level == 0 || ny.Level == 0) return 1;

            // If x is Folder...
            if (nx.Tag is string sx && sx == "Folder")
            {
                // And y is Folder...
                if (ny.Tag is string sy && sy == "Folder")
                {
                    // Then, sort them...
                    return string.Compare(nx.Text, ny.Text, true);
                }

                // Otherwise, x precedes y...
                return -1;
            }
            // If y is Folder...
            else if (ny.Tag is string sy && sy == "Folder")
            {
                // Then, x follows y...
                return 1;
            }

            // Sort the other nodes...
            return string.Compare(nx.Text, ny.Text, true);
        }
    }
}