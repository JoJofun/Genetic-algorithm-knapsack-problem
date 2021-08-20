using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackGenetic
{
    class item
    {
        private int weight;
        private int value;
        public item(int Weight, int Value)
        {
            weight = Weight;
            value = Value;
        }
        
        public int get_v()
        {
            return value;
        }

        public int get_w()
        {
            return weight;
        }

    }
}
