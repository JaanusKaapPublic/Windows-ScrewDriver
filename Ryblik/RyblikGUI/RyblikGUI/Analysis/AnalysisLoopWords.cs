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
    class AnalysisLoopWords
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

            Dictionary<uint, Dictionary<uint, List<int>>> errors = new Dictionary<uint, Dictionary<uint, List<int>>>();
            byte[] bufOut;
            int confEnd = Math.Min(this.confEnd, data.Length);
            uint count = 0, total = (uint)(confEnd - confStart - 1) * 0x10000;
            uint baseErr = Driver.devCtrlReq(device, access, code, data, out bufOut, outSize);
            listForm.setProgress(count, 0x10000 * ((uint)data.Length-1), null);
            for (uint pos = 0; pos < data.Length-1; pos++)
            {
                byte[] original = new byte[2];
                original[0] = data[pos];
                original[1] = data[pos+1];
                for (int val = 0x00; val < 0x10000; val++)
                {
                    if ((byte)(val & 0xFF) == original[0] && (byte)((val >> 8) & 0xFF) == original[1])
                        continue;
                    data[pos] = (byte)(val & 0xFF);
                    data[pos+1] = (byte)((val >> 8) & 0xFF);
                    count++;
                    uint err = Driver.devCtrlReq(device, access, code, data, out bufOut, outSize);
                    if (count % confInteval == 0)
                        listForm.setProgress(count, total, "@ 0x" + pos.ToString("X") + ": 0x" + val.ToString("X4"));
                    if (listForm == null || (listForm != null && !listForm.Visible))
                    {
                        MessageBox.Show("Analysis stopped");
                        return;
                    }
                    if (baseErr == err)
                        continue;
                    if (!errors.ContainsKey(err))
                        errors.Add(err, new Dictionary<uint, List<int>>());
                    if (!errors[err].ContainsKey(pos))
                        errors[err].Add(pos, new List<int>());
                    errors[err][pos].Add(val);
                }
                listForm.setProgress(count, total, null);
                data[pos] = original[0];
                data[pos + 1] = original[1];
            }

            listForm.setProgress(0x10000 * ((uint)data.Length-1), 0x10000 * ((uint)data.Length-1), "DONE");
            foreach (KeyValuePair<uint, Dictionary<uint, List<int>>> err in errors)
            {
                listForm.addValueStr("0x" + err.Key.ToString("X") + ": " + err.Value.Count.ToString());
                foreach (KeyValuePair<uint, List<int>> positions in err.Value)
                {
                    listForm.addValueStr("    @ 0x" + positions.Key.ToString("X") + ": " + positions.Value.Count.ToString());
                    for (int x = 0; x < Math.Min(positions.Value.Count, 8); x++)
                        listForm.addValueStr("        0x" + positions.Value[x].ToString("X2"));
                    if (positions.Value.Count > 8)
                        listForm.addValueStr("        [" + (positions.Value.Count - 8).ToString() + " more]");
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
