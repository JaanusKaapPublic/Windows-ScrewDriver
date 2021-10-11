using RyblikGUI.Exceptions;
using RyblikGUI.Forms;
using RyblikGUI.FuzzMutations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RyblikGUI
{
    public class Fuzz
    {
        [DllImport("kernel32", SetLastError = true)]
        private static extern bool FlushFileBuffers(IntPtr handle);

        String currentFuzzLine;
        String logFile = null;
        int currentFuzzLineDone, currentFuzzLineTotal;

        public enum FuzzType
        {
            RANDOM_FLIPPING = 1
        }

        public struct FUZZ_CONF
        {
            public String scriptLine;
            public String mutator;
            public int[] parameters;
            public int seed;
        }

        public void setLogFile(String filename)
        {
            logFile = filename;
        }

        public void fuzz(String device, uint access, uint code, byte[] input, uint outSize, FUZZ_CONF[] confs)
        {
            FormResultText resForm = null;
            new Thread(() => { resForm = new FormResultText(); resForm.ShowDialog(); }).Start();
            while (resForm == null)
                Thread.Sleep(500);

            byte[] outData;
            resForm.addValueStr("STARTING");
            for (uint x = 0; x < confs.Length; x++)
            {
                FUZZ_CONF conf = confs[x];
                resForm.setProgress(x, (uint)confs.Length, conf.scriptLine);
                resForm.addValueStr(conf.scriptLine);
                try
                {
                    Random rnd = new Random(conf.seed);
                    FuzzMutation mutator = FuzzMutationFactory.create(conf.mutator);
                    if (mutator == null)
                        throw new ExceptionFuzzing("Invalid fuzzer type", conf.scriptLine);
                    mutator.setConf(conf.parameters);
                    while (mutator.getCount() != 0)
                    {
                        String description;
                        byte[] tmp = mutator.mutate(rnd, input, out description);
                        if (tmp == null)
                            throw new ExceptionFuzzing("Mutator did not return bytes");
                        if (logFile != null)
                        {
                            FileStream f = File.Create(logFile, 4, FileOptions.WriteThrough);
                            byte[] bytes = Encoding.ASCII.GetBytes(description);
                            f.Write(bytes, 0, bytes.Length);
                            f.Flush();
                            f.Close();
                        }
                        Driver.devCtrlReq(device, access, code, tmp, out outData, outSize);
                    }
                }catch(ExceptionFuzzing ex)
                {
                    ex.setErrorLine(conf.scriptLine);
                    throw ex;
                }
            }
            resForm.setProgress((uint)confs.Length, (uint)confs.Length, null);
            resForm.addValueStr("DONE");
        }
    }
}
