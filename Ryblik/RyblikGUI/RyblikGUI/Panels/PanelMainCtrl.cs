using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RyblikGUI
{
    public partial class PanelMainCtrl : UserControl
    {
        private Form1 parent;

        public PanelMainCtrl(Form1 parentIn)
        {
            parent = parentIn;
            InitializeComponent();
            Driver.initialize();
            reloadTree();
        }

        public void reloadTree()
        {
            tree.Nodes.Clear();
            long[] drvs = Driver.getDriverAddresses();
            foreach (long drv in drvs)
            {
                String name = Driver.getDriverName(drv);
                TreeNodeDrvDev drvNode = new TreeNodeDrvDev(name, drv, 0);
                long[] devs = Driver.getDriverDeviceAddresses(drv);
                bool namedDevice = false;
                foreach (long dev in devs)
                {
                    String nameDev = Driver.getDriverDeviceName(drv, dev);
                    TreeNodeDrvDev devNode = new TreeNodeDrvDev("<NO NAME> 0x" + dev.ToString("X"), drv, dev); ;
                    if (nameDev != null && nameDev.Length != 0)
                    {
                        namedDevice = true;
                        devNode = new TreeNodeDrvDev(nameDev, drv, dev);
                        if (Driver.openable(nameDev, false, false))
                        {
                            drvNode.openWithNone = true;
                            devNode.openWithNone = true;
                        }
                        if (Driver.openable(nameDev, true, false))
                        {
                            drvNode.openWithRead = true;
                            devNode.openWithRead = true;
                        }
                        if (Driver.openable(nameDev, false, true))
                        {
                            drvNode.openWithWrite = true;
                            devNode.openWithWrite = true;
                        }
                    }

                    if (devNode.openWithRead || devNode.openWithWrite || devNode.openWithNone)
                        devNode.ForeColor = Color.DarkGreen;
                    if (Driver.isHookedDriver(drv, dev))
                        devNode.ForeColor = Color.DarkBlue;
                    drvNode.Nodes.Add(devNode);
                }
                if (drvNode.openWithRead || drvNode.openWithWrite || drvNode.openWithNone)
                    drvNode.ForeColor = Color.DarkGreen;
                if (Driver.isHookedDriver(drv, 0))
                    drvNode.ForeColor = Color.DarkBlue;
                if (chkHideNoAccess.Checked && !drvNode.openWithRead && !drvNode.openWithWrite && !drvNode.openWithNone)
                    continue;
                if (chkHideNoDevices.Checked && !namedDevice)
                    continue;
                if (chkRW.Checked && (!drvNode.openWithRead || !drvNode.openWithWrite || !drvNode.openWithNone))
                    continue;
                tree.Nodes.Add(drvNode);
            }
            tree.Sort();

            if (Driver.isFileLogActivated())
            {
                btnLogging.Text = "Stop logging";
            }
            else
            {
                btnLogging.Text = "Start logging";
            }
        }

        public void refreshTree()
        {
            TreeNode selected = tree.SelectedNode;
            foreach (TreeNodeDrvDev drvNode in tree.Nodes)
            {
                foreach (TreeNodeDrvDev devNode in drvNode.Nodes)
                {
                    if (Driver.isHookedDriver(drvNode.driver, devNode.device))
                    {
                        devNode.ForeColor = Color.DarkBlue;
                    }
                    else
                    {
                        if (devNode.openWithRead || devNode.openWithWrite || devNode.openWithNone)
                            devNode.ForeColor = Color.DarkGreen;
                        else
                            devNode.ForeColor = Color.Black;
                    }
                }

                if (Driver.isHookedDriver(drvNode.driver, 0))
                {
                    drvNode.ForeColor = Color.DarkBlue;
                }
                else
                {
                    if (drvNode.openWithRead || drvNode.openWithWrite || drvNode.openWithNone)
                        drvNode.ForeColor = Color.DarkGreen;
                    else
                        drvNode.ForeColor = Color.Black;
                }
            }
            tree.SelectedNode = selected;
        }

        private void chkHideNoAccess_CheckedChanged(object sender, EventArgs e)
        {
            reloadTree();
        }

        private void tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNodeDrvDev selected = (TreeNodeDrvDev)e.Node;
            if (selected.device == 0 && selected.driver != 0)
            {
                panelDetails.Controls.Clear();
                panelDetails.Controls.Add(new PanelDriver(this, selected.driver, selected.Text));
            }
            if (selected.device != 0 && selected.driver != 0)
            {
                panelDetails.Controls.Clear();
                panelDetails.Controls.Add(new PanelDevice(this, selected.driver, selected.device, selected.Text));
            }
        }

        private void tree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNodeDrvDev selected = (TreeNodeDrvDev)e.Node;
            if (selected.device != 0 && selected.driver != 0)
            {
                String device = Driver.getDriverDeviceName(selected.driver, selected.device);
                parent.addRequestTab(device, new byte[0]);
            }
        }

        private void btnLogging_Click(object sender, EventArgs e)
        {
            if(btnLogging.Text == "Stop logging")
            {
                Driver.stopFileLog();
            }
            else
            {
                if (txtLogFile.Text.Trim().Length == 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Ryblik file|*.rblk";
                    saveFileDialog1.Title = "Set an Ryblik data file";
                    saveFileDialog1.ShowDialog();
                    if (saveFileDialog1.FileName == "")
                        return;
                    txtLogFile.Text = saveFileDialog1.FileName;
                }
                Driver.startFileLog(txtLogFile.Text);
            }

            if (Driver.isFileLogActivated())
            {
                btnLogging.Text = "Stop logging";
            }
            else
            {
                btnLogging.Text = "Start logging";
            }
        }

        public void logAllAccessiblesDevices()
        {
            String times = Dialogs.InputBox("How many logs", "How many request will be loged per device (0=no limit)", "0");
            uint timesInt = UInt32.Parse(times);
            if (timesInt == 0)
                timesInt = 0xFFFFFFFF;

            long[] drvs = Driver.getDriverAddresses();
            foreach (long drv in drvs)
            {
                String name = Driver.getDriverName(drv);
                long[] devs = Driver.getDriverDeviceAddresses(drv);
                foreach (long dev in devs)
                {
                    bool accessible = false;
                    String nameDev = Driver.getDriverDeviceName(drv, dev);
                    //if (nameDev != null && nameDev.StartsWith("\\Device\\0000"))
                    //    break;
                    if (nameDev != null && nameDev.Length != 0)
                    {
                        if (Driver.openable(nameDev, false, false))
                            accessible = true;
                        if (Driver.openable(nameDev, true, false))
                            accessible = true;
                        if (Driver.openable(nameDev, false, true))
                            accessible = true;
                    }

                    if (accessible)
                    {
                        if (Driver.hookDriver(drv, dev))
                        {
                            Driver.setHookConfValue(drv, dev, 3, timesInt);
                        }
                    }
                }
            }
            refreshTree();
        }
    }
}
