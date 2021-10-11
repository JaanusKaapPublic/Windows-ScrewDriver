using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RyblikGUI.Forms
{
    public partial class FormResultList : Form
    {
        public FormResultList()
        {
            InitializeComponent();
        }

        private void FormResultList_Load(object sender, EventArgs e)
        {

        }

        public void setProgress(uint current, uint total, String comment)
        {
            String percentage = ((double)current / (double)total * 100.0).ToString("0.##");
            lblProgress.Text = "0x" + current.ToString("X") + " / 0x" + total.ToString("X") + "  " + percentage + "%";
            if (comment == null)
                lblComment.Text = "";
            else
                lblComment.Text = comment;
        }

        public void addValueStr(String value)
        {
            txtResult.Text += value + "\r\n";
        }

        public void addValue(String[] value)
        {
            IAsyncResult res = treeResults.BeginInvoke((MethodInvoker)delegate
            {
                treeResults.BeginUpdate();
                TreeNode parent, tmp;
                TreeNode[] parents = treeResults.Nodes
                                    .Cast<TreeNode>()
                                    .Where(r => r.Text == value[0])
                                    .ToArray();
                if (parents.Length == 0)
                {
                    parent = new TreeNode(value[0]);
                    treeResults.Nodes.Add(parent);
                }
                else
                {
                    parent = parents[0];
                }
                for (int x = 1; x < value.Length; x++)
                {
                    parents = parent.Nodes
                                    .Cast<TreeNode>()
                                    .Where(r => r.Text == value[x])
                                    .ToArray();
                    if (parents.Length == 0)
                    {
                        if (x == value.Length - 1 && parent.Nodes.Count > 100)
                        {
                            if (parent.Nodes
                                    .Cast<TreeNode>()
                                    .Where(r => r.Text == "...")
                                    .ToArray().Length == 0)
                                parent.Nodes.Add(new TreeNode("..."));
                        }
                        tmp = new TreeNode(value[x]);
                        parent.Nodes.Add(tmp);
                        parent = tmp;
                    }
                    else
                    {
                        parent = parents[0];
                    }
                }
                treeResults.EndUpdate();
            });

            while(!res.IsCompleted)
                Thread.Sleep(500);
        }
    }
}
