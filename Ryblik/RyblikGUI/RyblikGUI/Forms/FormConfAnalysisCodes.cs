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
    public partial class FormConfAnalysisCodes : Form
    {
        public bool ok = false;
        public int start = 0;
        public int end = 0x7FFFFFFF;
        public List<int> codes = new List<int>();

        public FormConfAnalysisCodes()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            start = Conversions.convert2int(txtStart.Text);
            end = Conversions.convert2int(txtEnd.Text);
            List<String> lines = txtCodes.Text.Split('\n').ToList();
            foreach (String lineIt in lines)
            {
                String line = lineIt.Trim();
                if (line.Length < 2 || line.StartsWith("#"))
                    continue;
                codes.Add(Conversions.convert2int(line));
            }
            ok = true;
            this.Close();
        }
    }
}
