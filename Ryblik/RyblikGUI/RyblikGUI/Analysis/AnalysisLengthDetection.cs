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
    class AnalysisLengthDetection
    {
        public void start(String device, uint code, uint access, uint outSize)
        {
            FormResultList listForm = null;
            new Thread(() => { listForm = new FormResultList(); listForm.ShowDialog(); }).Start();
            while (listForm == null)
                Thread.Sleep(500);

            Dictionary<uint, List<uint>> errors = new Dictionary<uint, List<uint>>();
            byte[] bufOut;
            for (uint len = 0; len < 0x1000; len++)
            {
                byte[] buf = new byte[len];
                uint err = Driver.devCtrlReq(device, access, code, buf, out bufOut, outSize);
                if (!errors.ContainsKey(err))
                    errors.Add(err, new List<uint>());
                errors[err].Add(len);
                if(len % 0x10 == 0)
                    listForm.setProgress(len, 0x1000, null);
                if (listForm == null || (listForm != null && !listForm.Visible))
                {
                    MessageBox.Show("Analysis stopped");
                    return;
                }
            }
            listForm.setProgress(0x1000, 0x1000, "DONE");
            foreach (KeyValuePair<uint, List<uint>> err in errors)
            {
                //listForm.addValueStr("  0x" + err.Key.ToString("X") + " [" + Conversions.errorNames(err.Key) + "]: " + err.Value.Count.ToString());

                uint begin = err.Value[0];
                for(int x = 1; x<err.Value.Count; x++)
                {
                    if(err.Value[x] != err.Value[x-1]+1)
                    {
                        if(begin == err.Value[x - 1])
                            //listForm.addValue("    0x" + begin.ToString("X"));
                            listForm.addValue(new string[] {
                                "0x" + err.Key.ToString("X") + " [" + Conversions.errorNames(err.Key) + "]: " + err.Value.Count.ToString(),
                                "0x" + begin.ToString("X") 
                            });
                        else
                            //listForm.addValue("    0x" + begin.ToString("X") + " - 0x" + err.Value[x - 1].ToString("X"));
                            listForm.addValue(new string[] {
                                "0x" + err.Key.ToString("X") + " [" + Conversions.errorNames(err.Key) + "]: " + err.Value.Count.ToString(),
                                "0x" + begin.ToString("X") + " - 0x" + err.Value[x - 1].ToString("X")
                            });
                        begin = err.Value[x];
                    }
                }
                if (begin == err.Value[err.Value.Count - 1])
                    //listForm.addValue("    0x" + begin.ToString("X"));
                    listForm.addValue(new string[] {
                                "0x" + err.Key.ToString("X") + " [" + Conversions.errorNames(err.Key) + "]: " + err.Value.Count.ToString(),
                                "0x" + begin.ToString("X")
                            });
                else
                    //listForm.addValue("    0x" + begin.ToString("X") + " - 0x" + err.Value[err.Value.Count - 1].ToString("X"));
                    listForm.addValue(new string[] {
                                "0x" + err.Key.ToString("X") + " [" + Conversions.errorNames(err.Key) + "]: " + err.Value.Count.ToString(),
                                "0x" + begin.ToString("X") + " - 0x" + err.Value[err.Value.Count - 1].ToString("X")
                            });
            }
        }

        public bool getconf()
        {
            return true;
        }
    }
}
