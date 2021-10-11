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
    public partial class FormConfAnalysisLoops : Form
    {
        public bool ok = false;
        public int start = 0;
        public int end = 0;
        public int interval = 100;

        public FormConfAnalysisLoops(int endIn)
        {
            InitializeComponent();
            end = endIn;
            txtEnd.Text = "0x" + end.ToString("X");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            start = Conversions.convert2int(txtStart.Text);
            end = Conversions.convert2int(txtEnd.Text);
            interval = Conversions.convert2int(txtLog.Text);
            ok = true;
            this.Close();
        }
    }
}
