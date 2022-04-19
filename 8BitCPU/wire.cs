using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8BitCPU
{
    class wire
    {
        public bool isOn;
        public int x1, y1, x2, y2;
        public wire[] connection = new wire[10];
        public input input;
        public int c = 0;

        public wire(int x1, int y1, int x2, int y2, input input)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.input = input;
        }

        public void addConnection(wire connection)
        {
            this.connection[c++] = connection;
        }

    }
}
