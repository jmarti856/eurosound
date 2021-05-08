using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace EuroSound_Application.FunctionsListView
{
    internal class ListViewFunctions
    {
        internal void SelectAllItems(ListView ListViewControl)
        {
            Thread SelectAllItems = new Thread(() =>
            {
                ListViewControl.BeginInvoke((MethodInvoker)delegate
                {
                    foreach (ListViewItem item in ListViewControl.Items)
                    {
                        item.Selected = true;
                    }

                    ListViewControl.Focus();
                });
            })
            {
                IsBackground = true
            };
            SelectAllItems.Start();
        }

        internal void SelectNone(ListView ListViewControl)
        {
            Thread SelectAllItems = new Thread(() =>
            {
                ListViewControl.BeginInvoke((MethodInvoker)delegate
                {
                    foreach (ListViewItem item in ListViewControl.Items)
                    {
                        item.Selected = false;
                    }

                    ListViewControl.Focus();
                });
            })
            {
                IsBackground = true
            };
            SelectAllItems.Start();
        }

        internal void InvertSelection(ListView ListViewControl)
        {
            Thread InvertCurrentSelection = new Thread(() =>
            {
                ListViewControl.BeginInvoke((MethodInvoker)delegate
                {
                    int[] selectedArray = new int[ListViewControl.SelectedIndices.Count];

                    ListViewControl.SelectedIndices.CopyTo(selectedArray, 0);

                    HashSet<int> selected = new HashSet<int>();
                    selected.UnionWith(selectedArray);

                    ListViewControl.SelectedIndices.Clear();
                    for (int i = 0; i < ListViewControl.Items.Count; i++)
                    {
                        if (!selected.Contains(i))
                        {
                            ListViewControl.SelectedIndices.Add(i);
                        }
                    }
                    ListViewControl.Focus();

                    selected.Clear();
                });
            })
            {
                IsBackground = true
            };
            InvertCurrentSelection.Start();
        }
    }
}
