using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RyblikGUI.RBLK;

namespace RyblikGUI
{
    class TreeNodeLogDriver : TreeNodeLog
    {
        public String driver;
        public Dictionary<String, TreeNodeLogDevice> devices = new Dictionary<string, TreeNodeLogDevice>();

        public TreeNodeLogDriver(String driverIn) : base(driverIn)
        {
            driver = driverIn;
        }

        public void add(RblkMessageStruct log)
        {
            if(!devices.ContainsKey(log.deviceName))
                devices.Add(log.deviceName, new TreeNodeLogDevice(log.driverName, log.deviceName));
            devices[log.deviceName].add(log);
        }

        public void remove(RblkMessageStruct log)
        {
            if (devices.ContainsKey(log.deviceName))
            {
                devices[log.deviceName].remove(log);
                if (devices[log.deviceName].getRequests().Count() == 0)
                    Nodes.Remove(devices[log.deviceName]);
            }
        }
        public void removeNullInputs()
        {
            foreach(TreeNodeLogDevice dev in devices.Values)
            {
                dev.removeNullInputs();
                if(dev.getRequests().Count() == 0)
                    Nodes.Remove(dev);
            }
        }

        public void build()
        {
            foreach (TreeNodeLogDevice node in devices.Values)
            {
                node.build();
                Nodes.Add(node);
            }
        }
        public override List<RblkMessageStruct> getRequests()
        {
            List<RblkMessageStruct> ret = new List<RblkMessageStruct>();
            foreach (TreeNodeLogDevice node in devices.Values)
                ret.AddRange(node.getRequests());
            return ret;
        }
    }
}
