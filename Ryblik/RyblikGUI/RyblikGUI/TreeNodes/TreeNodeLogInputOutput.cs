using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RyblikGUI.RBLK;

namespace RyblikGUI
{
    class TreeNodeLogInputOutput : TreeNodeLog
    {
        public String driver;
        public String device;
        public uint code;
        public uint inputSize;
        public uint outputSize;
        public List<RblkMessageStruct> requests = new List<RblkMessageStruct>();

        public TreeNodeLogInputOutput(String driverIn, String deviceIn, uint codeIn, uint inputSizeIn, uint outputSizeIn) :
            base("INPUT: 0x" + inputSizeIn.ToString("X")+"   OUTPUT: 0x" + outputSizeIn.ToString("X"))
        {
            driver = driverIn;
            device = deviceIn;
            code = codeIn;
            inputSize = inputSizeIn;
            outputSize= outputSizeIn;
        }

        public void add(RblkMessageStruct log)
        {
            requests.Add(log);
        }
        
        public void remove(RblkMessageStruct log)
        {
            if (requests.Contains(log))
                requests.Remove(log);
        }

        public void removeNullInputs()
        {
            requests.RemoveAll(req => req.input == null);
            requests.RemoveAll(req => req.input.Length == 0);
        }

        public override List<RblkMessageStruct> getRequests()
        {
            return requests;
        }
    }
}
