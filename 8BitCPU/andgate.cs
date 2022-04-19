using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8BitCPU
{
    class andgate
    {
        public int x, y;
        public tranzistor[] tranzistors = new tranzistor[3];
        public wire connection, wire;
        public input input;
        public andgate(int x, int y)
        {
            this.x = x;
            this.y = y;
            tranzistors[0] = new tranzistor(x, y);
            tranzistors[1] = new tranzistor(x + 20, y);
            connection = new wire(tranzistors[0].output.x1, tranzistors[0].output.y1, tranzistors[1].input.x1, tranzistors[1].input.y1, null);
            input = new input(x, y + 5);
            wire = new wire(input.x, input.y, tranzistors[0].input.x1, tranzistors[0].input.y1, input);
        }
    }
}
