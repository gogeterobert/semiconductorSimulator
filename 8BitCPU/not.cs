using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8BitCPU
{
    class not
    {
        public int x, y;
        public wire input, output;
        public bool isOn = false;

        public not(int x, int y)
        {
            this.x = x;
            this.y = y;
            input = new wire(x + 4, y + 15, x + 4, y + 4, null);
            output = new wire(x + 4, y - 11, x + 4, y + 4, null);
        }
    }
}
