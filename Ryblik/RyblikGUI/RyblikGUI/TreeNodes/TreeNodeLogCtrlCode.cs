using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RyblikGUI.RBLK;

namespace RyblikGUI
{
    class TreeNodeLogCtrlCode : TreeNodeLog
    {
        public String driver;
        public String device;
        public uint code;
        public Dictionary<String, TreeNodeLogInputOutput> requestNodes = new Dictionary<String, TreeNodeLogInputOutput>();

        public TreeNodeLogCtrlCode(String driverIn, String deviceIn, uint codeIn) : base("0x" + codeIn.ToString("X"))
        {
            driver = driverIn;
            device = deviceIn;
            code = codeIn;
        }

        public void add(RblkMessageStruct log)
        {
            if (!requestNodes.ContainsKey(log.input.Length.ToString("X") + ":" + log.outputSize.ToString("X")))
                requestNodes.Add(log.input.Length.ToString("X") + ":" + log.outputSize.ToString("X"), new TreeNodeLogInputOutput(log.driverName, log.deviceName, log.code, (uint)log.input.Length, log.outputSize));
            requestNodes[log.input.Length.ToString("X") + ":" + log.outputSize.ToString("X")].add(log);
        }

        public void remove(RblkMessageStruct log)
        {
            if (requestNodes.ContainsKey(log.input.Length.ToString("X") + ":" + log.outputSize.ToString("X")))
            {
                requestNodes[log.input.Length.ToString("X") + ":" + log.outputSize.ToString("X")].remove(log);
                if (requestNodes[log.input.Length.ToString("X") + ":" + log.outputSize.ToString("X")].getRequests().Count() == 0)
                    Nodes.Remove(requestNodes[log.input.Length.ToString("X") + ":" + log.outputSize.ToString("X")]);
            }
        }

        public void removeNullInputs()
        {
            foreach (TreeNodeLogInputOutput io in requestNodes.Values)
            {
                io.removeNullInputs();
                if (io.getRequests().Count() == 0)
                    Nodes.Remove(io);
            }
        }

        public void build()
        {
            foreach (TreeNodeLogInputOutput node in requestNodes.Values)
            {
                Nodes.Add(node);
            }
        }

        public override List<RblkMessageStruct> getRequests()
        {
            List<RblkMessageStruct> ret = new List<RblkMessageStruct>();
            foreach (TreeNodeLogInputOutput node in requestNodes.Values)
                ret.AddRange(node.getRequests());
            return ret;
        }
    }
}
