using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace RyblikGUI
{
    public partial class PanelDevice : UserControl
    {
        private PanelMainCtrl parent;
        private long driver, device;

        public PanelDevice(PanelMainCtrl parentIn, long driverIn, long deviceIn, String title)
        {
            InitializeComponent();
            parent = parentIn;
            driver = driverIn;
            device = deviceIn;
            handleEnabled();
            lblTitle.Text = title;
        }
        private void handleEnabled()
        {
            if (Driver.isHookedDriver(driver, device))
            {
                btnHook.Enabled = false;
                btnUnhook.Enabled = true;
                txtPID.Enabled = true;
                txtCode.Enabled = true;
                btnBreakPID.Enabled = true;
                btnBreakCode.Enabled = true;

                if (Driver.getHookConfValue(driver, device, 1) > 0)
                {
                    btnEnableDbgBreak.Enabled = false;
                    btnDisableDbgBreak.Enabled = true;
                }
                else
                {
                    btnEnableDbgBreak.Enabled = true;
                    btnDisableDbgBreak.Enabled = false;
                }

                if (Driver.getHookConfValue(driver, device, 2) > 0)
                {
                    btnEnableDbgLog.Enabled = false;
                    btnDisableDbgLog.Enabled = true;
                }
                else
                {
                    btnEnableDbgLog.Enabled = true;
                    btnDisableDbgLog.Enabled = false;
                }

                if (Driver.getHookConfValue(driver, device, 3) > 0)
                {
                    btnEnableFileLog.Enabled = false;
                    btnDisableFileLog.Enabled = true;
                }
                else
                {
                    btnEnableFileLog.Enabled = true;
                    btnDisableFileLog.Enabled = false;
                }

                if (Driver.getHookConfValue(driver, device, 5) > 0)
                {
                    txtPID.Text = Driver.getHookConfValue(driver, device, 5).ToString();
                }
                else
                {
                    txtPID.Text = "";
                }

                if (Driver.getHookConfValue(driver, device, 6) > 0)
                {
                    txtCode.Text = "0x" + Driver.getHookConfValue(driver, device, 6).ToString("X");
                }
                else
                {
                    txtCode.Text = "";
                }
            }
            else
            {
                btnHook.Enabled = true;
                btnUnhook.Enabled = false;
                btnDisableDbgBreak.Enabled = false;
                btnDisableDbgLog.Enabled = false;
                btnDisableFileLog.Enabled = false;
                btnEnableDbgBreak.Enabled = false;
                btnEnableDbgLog.Enabled = false;
                btnEnableFileLog.Enabled = false;
                txtPID.Enabled = false;
                txtCode.Enabled = false;
                btnBreakPID.Enabled = false;
                btnBreakCode.Enabled = false;
            }
        }

        private void btnHook_Click(object sender, EventArgs e)
        {
            if (Driver.hookDriver(driver, device))
            {
                parent.refreshTree();
                handleEnabled();
            }
        }

        private void btnEnableDbgBreak_Click(object sender, EventArgs e)
        {
            if (Driver.setHookConfValue(driver, device, 1, 1))
            {
                parent.refreshTree();
                handleEnabled();
            }
        }

        private void btnDisableDbgBreak_Click(object sender, EventArgs e)
        {
            if (Driver.setHookConfValue(driver, device, 1, 0))
            {
                parent.refreshTree();
                handleEnabled();
            }
        }

        private void btnEnableDbgLog_Click(object sender, EventArgs e)
        {
            if (Driver.setHookConfValue(driver, device, 2, 1))
            {
                parent.refreshTree();
                handleEnabled();
            }
        }

        private void btnDisableDbgLog_Click(object sender, EventArgs e)
        {
            if (Driver.setHookConfValue(driver, device, 2, 0))
            {
                parent.refreshTree();
                handleEnabled();
            }
        }

        private void btnEnableFileLog_Click(object sender, EventArgs e)
        {
            String times = Dialogs.InputBox("How many logs", "How many request will be loged per device (0=no limit)", "0");
            uint timesInt = UInt32.Parse(times);
            if (timesInt == 0)
                timesInt = 0xFFFFFFFF;

            if (Driver.setHookConfValue(driver, device, 3, timesInt))
            {
                parent.refreshTree();
                handleEnabled();
            }
        }

        private void btnDisableFileLog_Click(object sender, EventArgs e)
        {
            if (Driver.setHookConfValue(driver, device, 3, 0))
            {
                parent.refreshTree();
                handleEnabled();
            }
        }

        private void btnEnableFuzzing_Click(object sender, EventArgs e)
        {
            if (Driver.setHookConfValue(driver, device, 4, 1))
            {
                parent.refreshTree();
                handleEnabled();
            }
        }

        private void btnDisableFuzzing_Click(object sender, EventArgs e)
        {
            if (Driver.setHookConfValue(driver, device, 4, 0))
            {
                parent.refreshTree();
                handleEnabled();
            }
        }

        private void btnBreakPID_Click(object sender, EventArgs e)
        {
            try
            {
                uint nr = Conversions.convert2uint(txtPID.Text);
                Driver.setHookConfValue(driver, device, 5, nr);
            }
            catch (FormatException) 
            { 
                Driver.setHookConfValue(driver, device, 5, 0); 
            }
        }

        private void btnBreakCode_Click(object sender, EventArgs e)
        {
            try
            {
                uint nr = Conversions.convert2uint(txtCode.Text);
                Driver.setHookConfValue(driver, device, 6, nr);
            }
            catch (FormatException) 
            {
                Driver.setHookConfValue(driver, device, 6, 0);
            }
        }

        private void btnBreakThisPID_Click(object sender, EventArgs e)
        {
            Process proc = Process.GetCurrentProcess(); 
            uint nr = Convert.ToUInt32(proc.Id);
            txtPID.Text = nr.ToString();
            Driver.setHookConfValue(driver, device, 5, nr);
        }

        private void btnUnhook_Click(object sender, EventArgs e)
        {
            if (Driver.unhookDriver(driver, device))
            {
                parent.refreshTree();
                handleEnabled();
            }
        }
    }
}
