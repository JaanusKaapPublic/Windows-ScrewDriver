using RyblikGUI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyblikGUI.FuzzMutations
{
    class FuzzMutationFlipBits : FuzzMutation
    {
        protected int posStart, posEnd, changes, count, iter;

        public int getCount()
        {
            return count-iter;
        }

        public int getTotalCount()
        {
            return count;
        }

        public byte[] mutate(Random rand, byte[] input, out string description)
        {
            byte[] result = (byte[])input.Clone();
            description = "";
            if (posStart < 0 || posStart >= input.Length)
                throw new ExceptionFuzzing("Start location is not valid");
            for (uint x = 0; x< changes; x++)
            {
                int pos = rand.Next(posStart, Math.Min(posEnd, input.Length-1));
                byte bit = (byte)(1 << rand.Next(0, 7));
                byte orig = result[pos];
                result[pos] = (byte)(result[pos] ^ bit);
                description += "BYTE @ 0x" + pos.ToString("X") + " : 0x" + orig.ToString("X2") + " -> 0x" + result[pos].ToString("X2") + "\n";
            }
            
            iter++;
            return result;
        }

        public void reset()
        {
            iter = 0;
        }

        public void setConf(int[] parameters)
        {
            if (parameters.Length != 4)
                throw new ExceptionFuzzing("Wrong number on parameters");
            posStart = parameters[0];
            posEnd = parameters[1];
            changes = parameters[2];
            count = parameters[3];
        }
    }
}
