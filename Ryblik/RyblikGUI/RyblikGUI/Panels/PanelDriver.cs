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
    public partial class PanelDriver : UserControl
    {
        private PanelMainCtrl parent;
        private long driver;

        public PanelDriver(PanelMainCtrl parentIn, long driverIn, String title)
        {
            InitializeComponent();
            parent = parentIn;
            driver = driverIn;
            showFunctions();
            showDevices();
            handleEnabled();
            lblTitle.Text = title;
        }

        public void showFunctions()
        {
            tableFuncs.Rows.Clear();
            long[] functions = Driver.getDriverMajorFunctions(driver);
            for (int x = 0; x < functions.Length; x++)
            {
                object[] row = new object[] { "0x" + x.ToString("X2") + " " + Conversions.majorFuncName(x), "0x" + functions[x].ToString("X") };
                tableFuncs.Rows.Add(row);
            }
        }

        public void showDevices()
        {
            tableDevices.Rows.Clear();
            long[] devices = Driver.getDriverDeviceAddresses(driver);
            for (int x = 0; x < devices.Length; x++)
            {
                String name = Driver.getDriverDeviceName(driver, devices[x]);
                int access = -1;
                if (Driver.openable(name, false, false))
                    access = 0;
                if (Driver.openable(name, true, false))
                    access |= 1;
                if (Driver.openable(name, false, true))
                    access |= 2;

                String accessStr = "NO ACCESS";
                switch (access)
                {
                    case 0:
                        accessStr = "ACCESS";
                        break;
                    case 1:
                        accessStr = "ONLY READ";
                        break;
                    case 2:
                        accessStr = "ONLY WRITE";
                        break;
                    case 3:
                        accessStr = "READ & WRITE";
                        break;

                }

                object[] row = new object[] { "0x"+ devices[x].ToString("X"), name, accessStr };
                tableDevices.Rows.Add(row);
            }
        }

        private void handleEnabled()
        {
            if(Driver.isHookedDriver(driver, 0))
            {
                btnHook.Enabled = false;
                btnUnhook.Enabled = true;
            }
            else
            {
                btnHook.Enabled = true;
                btnUnhook.Enabled = false;
            }
        }

        private void btnHook_Click(object sender, EventArgs e)
        {
            if (Driver.hookDriver(driver, 0))
            {
                parent.refreshTree();
                handleEnabled();
            }
        }

        private void btnUnhook_Click(object sender, EventArgs e)
        {
            if (Driver.unhookDriver(driver, 0))
            {
                parent.refreshTree();
                handleEnabled();
            }
        }
    }
}
