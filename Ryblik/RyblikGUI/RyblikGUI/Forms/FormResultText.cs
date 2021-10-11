using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RyblikGUI.Forms
{
    public partial class FormResultText : Form
    {
        public FormResultText()
        {
            InitializeComponent();
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
    }
}
