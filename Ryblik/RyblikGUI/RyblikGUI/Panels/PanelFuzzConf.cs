using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RyblikGUI.Fuzz;

namespace RyblikGUI
{
    public partial class PanelFuzzConf : UserControl
    {
        public PanelFuzzConf()
        {
            InitializeComponent();
            Random rand = new Random();
            txtSeed.Text = rand.Next(0, 0x7FFFFFFF).ToString();
        }

        public FUZZ_CONF[] getConf()
        {
            List<FUZZ_CONF> conf = new List<FUZZ_CONF>();
            List<String> lines = txtConf.Text.Split('\n').ToList();
            foreach(String lineIt in lines)
            {
                String line = lineIt.Trim();
                if (line.Length < 2 || line.StartsWith("#"))
                    continue;
                String func = line.Substring(0, line.IndexOf('(')).Trim();
                line = line.Substring(line.IndexOf('(')+1);
                line = line.Substring(0, line.Length - 1).Trim();
                List<String> parametersStrs = line.Split(',').ToList();
                List<int> parameters = new List<int>();
                foreach (String parametersStr in parametersStrs)
                    parameters.Add(Conversions.convert2int(parametersStr));
                FUZZ_CONF confLine = new FUZZ_CONF();
                confLine.scriptLine = lineIt.Trim();
                confLine.mutator = func;
                confLine.parameters = parameters.ToArray();
                confLine.seed = Conversions.convert2int(txtSeed.Text);
                conf.Add(confLine);
            }
            return conf.ToArray();
        }

        public String getLogFilename()
        {
            if (txtLogFile.Text.Trim().Length < 2)
                return null;
            return txtLogFile.Text;
        }

        private void btnLogFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Fuzzing log file|*.rblk_fl";
            saveFileDialog1.Title = "Set an Ryblik fuzzing log file";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName == "")
                return;
            txtLogFile.Text = saveFileDialog1.FileName;
        }
    }
}
