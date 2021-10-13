using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RyblikGUI.RBLK;

namespace RyblikGUI
{
    public partial class FormLogData : Form
    {        
        private Form1 parent = null;
        //private Dictionary<String, TreeNodeLogDriver> drivers = new Dictionary<string, TreeNodeLogDriver>();
        private List<RblkMessageStruct> currentList = new List<RblkMessageStruct>();
        private MainMenu mainMenu;
        private ContextMenu treeMenu = new ContextMenu();
        private TreeNodeLog selectedNode;
        private SaveFileDialog saveFileDialog = new SaveFileDialog();



        public FormLogData(Form1 parentIn, List<RblkMessageStruct> callsIn)
        {
            InitializeComponent();
            parent = parentIn;
            createMenu();
            fillTree(callsIn);
        }

        private void createMenu()
        {
            mainMenu = new MainMenu();

            MenuItem file = mainMenu.MenuItems.Add("&File");
            file.MenuItems.Add(new MenuItem("&Save", MenuItemSave));
            file.MenuItems.Add(new MenuItem("&Export python code (all)", MenuItemExportPythonAll));
            file.MenuItems.Add(new MenuItem("&Export python code (selected)", MenuItemExportPythonSelected));
            file.MenuItems.Add(new MenuItem("&Exit"));

            MenuItem File = mainMenu.MenuItems.Add("&Edit");
            File.MenuItems.Add(new MenuItem("&Remove 0 sized inputs", MenuItemRemoveNullInput));
            File.MenuItems.Add(new MenuItem("&Remove Ryblik requests", MenuItemRemoveRyblik));
            File.MenuItems.Add(new MenuItem("&Exit"));
            this.Menu = mainMenu;

            treeMenu.MenuItems.Add(new MenuItem("Delete", treeMenuDelete));
        }

        public void fillTree(List<RblkMessageStruct> calls)
        {
            Dictionary<String, TreeNodeLogDriver> drivers = new Dictionary<string, TreeNodeLogDriver>();
            foreach (RblkMessageStruct call in calls)
            {
                if (!drivers.ContainsKey(call.driverName))
                    drivers[call.driverName] = new TreeNodeLogDriver(call.driverName);
                drivers[call.driverName].add(call);
            }

            foreach (TreeNodeLogDriver node in drivers.Values)
            {
                node.build();
                tree.Nodes.Add(node);
            }
            tree.Sort();
        }

        private void refreshList()
        {
            int idx = 0;
            table.Rows.Clear();
            foreach (RblkMessageStruct log in currentList)
            {
                object[] row = new object[] { idx++, log.driverName, log.deviceName, "0x" + log.code.ToString("X"), "0x" + log.input.Length.ToString("X"), "0x" + log.outputSize.ToString("X"), "open", false };
                table.Rows.Add(row);
            }
        }

        private void tree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node is TreeNodeLog node)
            {
                table.Rows.Clear();
                currentList = node.getRequests();
                refreshList();
            }
        }
        
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            currentList = new List<RblkMessageStruct>();
            foreach (TreeNodeLogDriver node in tree.Nodes)
                currentList.AddRange(node.getRequests());
            refreshList();
        }

        private void table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            int action = e.ColumnIndex;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<RblkMessageStruct> toBeDeleted = new List<RblkMessageStruct>();
            for (int x = 0; x < table.Rows.Count; x++)
            {
                int idx = (int)table.Rows[x].Cells[0].Value;
                bool sel = table.Rows[x].Selected;
                if (sel)
                    toBeDeleted.Add(currentList[idx]);
            }
            foreach (RblkMessageStruct call in toBeDeleted)
            {
                currentList.Remove(call);
                foreach (TreeNodeLogDriver node in tree.Nodes)
                {
                    if (node.Text == call.driverName)
                    {
                        node.remove(call);
                        if (node.Nodes.Count == 0)
                            node.Remove();
                        break;
                    }
                }
            }
            refreshList();
        }

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int idx = e.RowIndex;
            int action = e.ColumnIndex;
            if (idx < 0)
                return;

            if (action == 6)
            {
                idx = (int)table.Rows[idx].Cells[0].Value;
                RblkMessageStruct selection = currentList[idx];
                parent.addRequestTab(selection.deviceName, selection.code, selection.input, selection.outputSize);
            }
        }

        private void table_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        }

        private void MenuItemSave(Object sender, System.EventArgs e)
        {
            saveFileDialog.Filter = "Ryblik file|*.rblk";
            saveFileDialog.Title = "Save an Ryblik File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                List<RblkMessageStruct> all = new List<RblkMessageStruct>();
                foreach (TreeNodeLogDriver node in tree.Nodes)
                    all.AddRange(node.getRequests());
                RBLK.save(saveFileDialog.FileName, all);
            }
        }

        private void MenuItemExportPythonAll(Object sender, System.EventArgs e)
        {
            saveFileDialog.Filter = "Python file|*.py";
            saveFileDialog.Title = "Save an Python File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                List<RblkMessageStruct> all = new List<RblkMessageStruct>();
                foreach (TreeNodeLogDriver node in tree.Nodes)
                    all.AddRange(node.getRequests());
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
                ExportPython.exportMultiple(all, fs);
                fs.Close();

            }
        }

        private void MenuItemExportPythonSelected(Object sender, System.EventArgs e)
        {
            saveFileDialog.Filter = "Python file|*.py";
            saveFileDialog.Title = "Save an Python File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
                ExportPython.exportMultiple(currentList, fs);
                fs.Close();

            }
        }

        private void MenuItemRemoveNullInput(Object sender, System.EventArgs e)
        {
            for(int x=0; x<tree.Nodes.Count; x++)
            {
                TreeNodeLogDriver node = (TreeNodeLogDriver)tree.Nodes[x];
                node.removeNullInputs();
                if (node.getRequests().Count == 0)
                {
                    tree.Nodes.Remove(node);
                    x--;
                }
            }
        }
        private void MenuItemRemoveRyblik(Object sender, System.EventArgs e)
        {
            foreach (TreeNodeLogDriver node in tree.Nodes)
            {
                if (node.Text == "\\Driver\\RyblikDriver")
                {
                    tree.Nodes.Remove(node);
                    break;
                }
            }
        }
        private void treeMenuDelete(Object sender, System.EventArgs e)
        {
            if(selectedNode != null)
                selectedNode.Remove();
        }

        private void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                selectedNode = (TreeNodeLog)e.Node;
                treeMenu.Show(tree, e.Location);
            }
        }
    }
}
