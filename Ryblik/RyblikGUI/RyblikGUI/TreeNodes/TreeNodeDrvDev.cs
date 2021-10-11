using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RyblikGUI
{
    class TreeNodeDrvDev : TreeNode
    {
        public long driver = 0;
        public long device = 0;
        public bool openWithNone = false;
        public bool openWithRead = false;
        public bool openWithWrite = false;

        public TreeNodeDrvDev(String label, long drv, long dev) : base(label)
        {
            driver = drv;
            device = dev;
        }
    }
}
