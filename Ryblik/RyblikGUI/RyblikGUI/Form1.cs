using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RyblikGUI.RBLK;

namespace RyblikGUI
{
    public partial class Form1 : Form
    {
        private TabPage ctrlTab;
        private PanelMainCtrl ctrlPanel;
        private MainMenu mainMenu;
        private MenuItem menuSave;
        private MenuItem menuClose;
        private MenuItem menuHooking;
        private SaveFileDialog saveFileDialog = new SaveFileDialog();
        private ContextMenu tabMenu = new ContextMenu();
        private TabPage tabMenuSelected;

        public Form1()
        {
            InitializeComponent();
            createMenu();
            createTabs();
        }

        private void createTabs()
        {
            ctrlTab = new TabPage("Main");
            ctrlPanel = new PanelMainCtrl(this);
            ctrlTab.Controls.Add(ctrlPanel);
            tabs.TabPages.Add(ctrlTab);

            tabMenu.MenuItems.Add(new MenuItem("Delete", tabMenuDelete));
        }

        private void createMenu()
        {
            mainMenu = new MainMenu();
            MenuItem File = mainMenu.MenuItems.Add("&File");
            {
                File.MenuItems.Add(new MenuItem("&New request", MenuItemNew));
                menuSave = new MenuItem("&Save", MenuItemSave);
                menuSave.Enabled = false;
                File.MenuItems.Add(menuSave);
                File.MenuItems.Add(new MenuItem("&Open", MenuItemOpen));
                menuClose = new MenuItem("&Close", MenuItemClose);
                menuClose.Enabled = false;
                File.MenuItems.Add(menuClose);
                File.MenuItems.Add(new MenuItem("&Exit"));
            }
            menuHooking = mainMenu.MenuItems.Add("&Hooking");
            {
                menuHooking.MenuItems.Add(new MenuItem("&Log all accessible", MenuItemLogAllAccessible));
            }
            MenuItem About = mainMenu.MenuItems.Add("&About");
            {
                About.MenuItems.Add(new MenuItem("&About"));
            }
            this.Menu = mainMenu;
        }

        public void addRequestTab(String device, byte[] input)
        {
            TabPage reqTab = new TabPage((device == null || device.Length == 0 ? "Request" : device));
            reqTab.Controls.Add(new PanelRequest(this, device, input));
            tabs.TabPages.Add(reqTab);
        }

        public void addRequestTab(String device, uint code, byte[] input, uint outSize)
        {
            TabPage reqTab = new TabPage((device == null || device.Length == 0 ? "Request" : device));
            reqTab.Controls.Add(new PanelRequest(this, device, code, input, outSize));
            tabs.TabPages.Add(reqTab);
        }

        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            menuSave.Enabled = (tabs.SelectedTab != ctrlTab);
            menuClose.Enabled = (tabs.SelectedTab != ctrlTab);
        }
        private void MenuItemClose(Object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Sure you want to close?", "Closing tab", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                tabs.TabPages.Remove(tabs.SelectedTab);
            }
        }

        private void MenuItemOpen(Object sender, System.EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Ryblik log (*.rblk)|*.rblk|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<RblkMessageStruct> calls = RBLK.open(openFileDialog.FileName);
                if (calls == null)
                {
                    MessageBox.Show("Opening file failed");
                    return;
                }

                if (calls.Count > 1)
                {
                    FormLogData selectionWindow = new FormLogData(this, calls);
                    selectionWindow.Show();
                }
                else
                {
                    if (calls.Count == 1)
                        addRequestTab(calls[0].deviceName, calls[0].code, calls[0].input, calls[0].outputSize);
                    else
                        MessageBox.Show("This file contained no data");
                }
            }
        }

        private void MenuItemNew(Object sender, System.EventArgs e)
        {
            addRequestTab(null, new byte[0]);
        }

        private void MenuItemSave(Object sender, System.EventArgs e)
        {
            try
            {
                PanelRequest panel = (PanelRequest)tabs.SelectedTab.Controls[0];
                panel.save();
            }
            catch
            {
                MessageBox.Show("FAILED: " + tabs.SelectedTab.Controls[0].GetType().Name);
            }
        }
        
        private void MenuItemLogAllAccessible(Object sender, System.EventArgs e)
        {
            ctrlPanel.logAllAccessiblesDevices();
        }
        private void tabMenuDelete(Object sender, System.EventArgs e)
        {
            tabs.TabPages.Remove(tabMenuSelected);
        }

        private void tabs_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int x = 1; x < tabs.TabCount; ++x)
                {
                    if (tabs.GetTabRect(x).Contains(e.Location))
                    {
                        tabMenuSelected = tabs.TabPages[x];
                        tabMenu.Show(tabs, e.Location);
                    }
                }
            }
        }
    }
}
