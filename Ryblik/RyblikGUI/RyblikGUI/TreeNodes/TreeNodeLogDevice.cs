using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RyblikGUI.RBLK;

namespace RyblikGUI
{
    class TreeNodeLogDevice : TreeNodeLog
    {
        public String driver;
        public String device;
        public Dictionary<uint, TreeNodeLogCtrlCode> codes = new Dictionary<uint, TreeNodeLogCtrlCode>();

        public TreeNodeLogDevice(String driverIn, String deviceIn) : base(deviceIn)
        {
            driver = driverIn;
            device = deviceIn;
        }

        public void add(RblkMessageStruct log)
        {
            if (!codes.ContainsKey(log.code))
                codes.Add(log.code, new TreeNodeLogCtrlCode(log.driverName, log.deviceName, log.code));
            codes[log.code].add(log);
        }

        public void remove(RblkMessageStruct log)
        {
            if (codes.ContainsKey(log.code))
            {
                codes[log.code].remove(log);
                if (codes[log.code].getRequests().Count() == 0)
                    Nodes.Remove(codes[log.code]);
            }
        }
        public void removeNullInputs()
        {
            foreach (TreeNodeLogCtrlCode code in codes.Values)
            {
                code.removeNullInputs();
                if (code.getRequests().Count() == 0)
                    Nodes.Remove(code);
            }
        }

        public void build()
        {
            foreach(TreeNodeLogCtrlCode node in codes.Values)
            {
                node.build();
                Nodes.Add(node);
            }
        }

        public override List<RblkMessageStruct> getRequests()
        {
            List<RblkMessageStruct> ret = new List<RblkMessageStruct>();
            foreach (TreeNodeLogCtrlCode node in codes.Values)
                ret.AddRange(node.getRequests());
            return ret;
        }
    }
}
