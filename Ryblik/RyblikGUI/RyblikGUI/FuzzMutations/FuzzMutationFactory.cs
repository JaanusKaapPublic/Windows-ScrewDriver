using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyblikGUI.FuzzMutations
{
    class FuzzMutationFactory
    {
        static public FuzzMutation create(String mutName)
        {
            switch(mutName)
            {
                case "BITFLIP":
                    return new FuzzMutationFlipBits();
            }
            return null;
        }
    }
}
