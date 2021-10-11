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
    class AnalysisLoopBytes
    {
        private int confStart;
        private int confEnd;
        private int confInteval;

        public void start(String device, byte[] data, uint code, uint access, uint outSize)
        {
            FormResultList listForm = null;
            new Thread(() => { listForm = new FormResultList(); listForm.ShowDialog(); }).Start();
            while (listForm == null)
                Thread.Sleep(500);

            Dictionary<uint, Dictionary<uint, List<byte>>> errors = new Dictionary<uint, Dictionary<uint, List<byte>>>();
            byte[] bufOut;
            int confEnd = Math.Min(this.confEnd, data.Length);
            uint count = 0, total = (uint)(confEnd-confStart) * 0xFF;
            uint baseErr = Driver.devCtrlReq(device, access, code, data, out bufOut, outSize);
            for (uint pos = (uint)confStart; pos < confEnd; pos++)
            {
                for (int val = 0x00; val < 0xFF; val++)
                {
                    count++;
                    data[pos] += 1;
                    uint err = Driver.devCtrlReq(device, access, code, data, out bufOut, outSize);
                    if(count % confInteval == 0)
                        listForm.setProgress(count, total, "@ 0x" + pos.ToString("X") + ": 0x" + data[pos].ToString("X2"));
                    if (listForm == null || (listForm != null && !listForm.Visible))
                    {
                        MessageBox.Show("Analysis stopped");
                        return;
                    }
                    if (baseErr == err)
                        continue;
                    if (!errors.ContainsKey(err))
                        errors.Add(err, new Dictionary<uint, List<byte>>());
                    if (!errors[err].ContainsKey(pos))
                        errors[err].Add(pos, new List<byte>());
                    errors[err][pos].Add(data[pos]);
                }
                data[pos] += 1;
            }

            listForm.setProgress(count, total, "DONE");
            foreach (KeyValuePair<uint, Dictionary<uint, List<byte>>> err in errors)
            {
                listForm.addValueStr("0x" + err.Key.ToString("X") + ": " + err.Value.Count.ToString());
                foreach (KeyValuePair<uint, List<byte>> positions in err.Value)
                {
                    listForm.addValueStr("    @ 0x" + positions.Key.ToString("X") + ": " + positions.Value.Count.ToString());
                    for (int x = 0; x < Math.Min(positions.Value.Count, 8); x++)
                        listForm.addValueStr("        0x" + positions.Value[x].ToString("X2"));
                    if(positions.Value.Count > 8)
                        listForm.addValueStr("        [" + (positions.Value.Count-8).ToString() + " more]");
                }
            }
        }

        public bool getconf(int len)
        {
            FormConfAnalysisLoops conf = new FormConfAnalysisLoops(len);
            conf.ShowDialog();
            if (conf.ok)
            {
                confStart = conf.start;
                confEnd = conf.end;
                confInteval = conf.interval;
                return true;
            }
            return false;
        }
    }
}
