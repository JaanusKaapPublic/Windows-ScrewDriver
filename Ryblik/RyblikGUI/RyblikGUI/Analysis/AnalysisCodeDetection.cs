using RyblikGUI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RyblikGUI.Analysis
{
    class AnalysisCodeDetection
    {
        private uint lastCodeChecked;
        private uint confStart;
        private uint confEnd;
        private List<int> confCodes;


        public void start(String device)
        {
            FormResultList listForm = null;
            new Thread(() => { listForm = new FormResultList(); listForm.ShowDialog();  }).Start();
            while(listForm == null)
                Thread.Sleep(500);
            byte[] inputBuffer = new byte[0];
            byte[] outputBuffer;

            List<uint> validCodes = new List<uint>();
            for (lastCodeChecked = 0; lastCodeChecked < confEnd; lastCodeChecked++)
            {
                uint access = 0;
                if ((lastCodeChecked & 0x4000) > 0)
                    access |= 1;
                if ((lastCodeChecked & 0x8000) > 0)
                    access |= 2;
                uint err = Driver.devCtrlReq(device, access, lastCodeChecked, inputBuffer, out outputBuffer, 0x1000);
                if (!confCodes.Contains((int)err))
                {
                    validCodes.Add(lastCodeChecked);
                    if (listForm != null)
                    {
                        listForm.addValueStr("0x" + lastCodeChecked.ToString("X") + "  0x" + err.ToString("X") + " " + Conversions.errorNames(err) + "");
                        listForm.addValue(new string[]{
                            "0x" + err.ToString("X") + " [" + Conversions.errorNames(err) + "]",
                            "0x" + lastCodeChecked.ToString("X")
                        });
                    }
                }
                if (lastCodeChecked % 0x1000 == 0)
                {
                    if (listForm != null)
                        listForm.setProgress(lastCodeChecked, confEnd, null);
                }
                if (listForm == null || (listForm != null && !listForm.Visible))
                {
                    MessageBox.Show("Analysis stopped");
                    return;
                }
                if (validCodes.Count > 1000)
                {
                    listForm.addValueStr("Too many codes detected - probably something fishy");
                    listForm.setProgress(lastCodeChecked, confEnd, "STOPPED");
                    return;
                }
            }
            listForm.setProgress(confEnd, confEnd, "DONE");
        }

        public bool getconf()
        {
            FormConfAnalysisCodes conf = new FormConfAnalysisCodes(); 
            conf.ShowDialog();
            if(conf.ok)
            {
                confStart = (uint)conf.start;
                confEnd = (uint)conf.end;
                confCodes = conf.codes;
                return true;
            }
            return false;
        }
    }
}
