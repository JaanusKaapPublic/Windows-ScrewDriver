using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RyblikGUI.RBLK;

namespace RyblikGUI
{
    class TreeNodeLog : TreeNode
    {
        public TreeNodeLog(String label):base(label)
        {

        }
               
        public virtual List<RblkMessageStruct> getRequests()
        {
            return new List<RblkMessageStruct>();
        }
    }
}
