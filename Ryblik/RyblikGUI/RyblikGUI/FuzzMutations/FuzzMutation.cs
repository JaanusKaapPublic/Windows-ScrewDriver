using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyblikGUI.FuzzMutations
{
    public interface FuzzMutation
    {
        void setConf(int[] parameters);
        int getTotalCount();
        int getCount();
        byte[] mutate(Random rand, byte[] input, out String description);
        void reset();
    }
}
