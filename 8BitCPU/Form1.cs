using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8BitCPU
{
    public partial class Form1 : Form
    {
        int x, y, x2, y2;
        bool clicked = false;

        string button;

        wire[] wires = new wire[1000];
        tranzistor[] tranzistors = new tranzistor[1000];
        input[] inputs = new input[100];
        not[] nots = new not[1000];

        int w = 0, t = 0, i = 0, n = 0;
        bool con = false;
        wire conn;
        input ip;

        string type;

        Timer timer = new Timer();
        

        public Form1()
        {
            InitializeComponent();

            timer.Interval = 100;
            timer.Enabled = true;

            Graphics e = this.CreateGraphics();

            timer.Tick += delegate
            {
                if (clicked)
                {
                    clicked = false;

                    if (button == "Right")
                    {
                        if (type == "T")
                        {
                            tranzistors[t++] = new tranzistor(x, y);
                        }
                        if (type == "I")
                        {
                            inputs[i++] = new input(x, y);
                        }
                        if (type == "N")
                        {
                            nots[n++] = new not(x, y);
                        }
                        if (type == "Q")
                        {
                            checkInputsClick();
                        }
                    }
                    else
                    {
                        //checkWiresClick();

                        checkTranzistorsClick();

                        checkInputsClick();

                        checkNotsClick();
                    }
                }
                update();

                updateTranzistors();

                updateNots();

                drawTranzistors(e);

                drawWires(e);

                drawInputs(e);

                drawNots(e);
            };
        }

        private void update()
        {
            for (int j = 0; j < w; j++)
            {
                if (wires[j].connection != null)
                {
                    bool on = false;
                    foreach (var con in wires[j].connection)
                    {
                        if (con != null)
                        {
                            if (con.isOn)
                            {
                                on = true;
                                break;
                            }
                        }
                        else
                            break;
                    }

                    if (on)
                    {
                        wires[j].isOn = true;
                    }
                    if (on == false)
                    {
                        wires[j].isOn = false;
                    }

                    if (wires[j].input != null && wires[j].input.isOn)
                    {
                        wires[j].isOn = true;
                    }
                    if (wires[j].input != null && !wires[j].input.isOn)
                    {
                        wires[j].isOn = false;
                    }
                }
                
            }
            for(int j = 0; j < t; j ++)
            {
                if (tranzistors[j].input.connection != null)
                {
                    bool on = false;
                    foreach (var con in tranzistors[j].input.connection)
                    {
                        if (con != null)
                        {
                            if (con.isOn)
                            {
                                on = true;
                                break;
                            }
                        }
                        else
                            break;
                    }

                    if (on)
                    {
                        tranzistors[j].input.isOn = true;
                    }
                    if (on == false)
                    {
                        tranzistors[j].input.isOn = false;
                    }
                }
                /*
                if (tranzistors[j].output.connection != null)
                {
                    bool on = false;
                    foreach (var con in tranzistors[j].output.connection)
                    {
                        if (con != null)
                        {
                            if (con.isOn)
                            {
                                on = true;
                                break;
                            }
                        }
                        else
                            break;
                    }

                    if (on)
                    {
                        tranzistors[j].output.isOn = true;
                    }
                    if (on == false)
                    {
                        tranzistors[j].output.isOn = false;
                    }
                }*/
                
                if (tranzistors[j].baze.connection != null)
                {
                    bool on = false;
                    foreach (var con in tranzistors[j].baze.connection)
                    {
                        if (con != null)
                        {
                            if (con.isOn)
                            {
                                on = true;
                                break;
                            }
                        }
                        else
                            break;
                    }

                    if (on)
                    {
                        tranzistors[j].baze.isOn = true;
                    }
                    if (on == false)
                    {
                        tranzistors[j].baze.isOn = false;
                    }
                }
            }
        }

        private void drawWire(wire wire, Graphics e)
        {
            Pen pen;
            if (wire.isOn)
                pen = new Pen(Color.Red);
            else
                pen = new Pen(Color.Gray);

            e.DrawLine(pen, wire.x1, wire.y1, wire.x2, wire.y2);

            Point[] points = new Point[4];
            points[0] = new Point(wire.x1 + 2, wire.y1);
            points[1] = new Point(wire.x1, wire.y1 + 2);
            points[2] = new Point(wire.x1 - 2, wire.y1);
            points[3] = new Point(wire.x1, wire.y1 - 2);

            e.DrawClosedCurve(pen, points);
        }

        private void drawWires(Graphics e)
        {
            foreach (var wr in wires)
            {
                if (wr != null)
                {
                    drawWire(wr, e);
                }
                else
                    break;
            }
        }

        private void drawTranzistor(tranzistor tranzistor, Graphics e)
        {
            Pen pen = new Pen(Color.Black);

            e.DrawRectangle(pen, new Rectangle(tranzistor.x1, tranzistor.y1, 18, 10));
        }

        private void drawTranzistors(Graphics e)
        {
            foreach (var tranz in tranzistors)
            {
                if (tranz != null)
                {
                    drawTranzistor(tranz, e);
                    drawWire(tranz.input, e);
                    drawWire(tranz.output, e);
                    drawWire(tranz.baze, e);
                }
                else
                    break;
            }
        }

        private void drawInput(input input, Graphics e)
        {
            Pen pen;
            if (input.isOn)
                pen = new Pen(Color.Red);
            else
                pen = new Pen(Color.Gray);

            Point[] points = new Point[4];
            points[0] = new Point(input.x + 5, input.y);
            points[1] = new Point(input.x, input.y + 5);
            points[2] = new Point(input.x - 5, input.y);
            points[3] = new Point(input.x, input.y - 5);

            e.DrawClosedCurve(pen, points);
        }

        private void drawInputs(Graphics e)
        {
            foreach(var inp in inputs)
            {
                if (inp != null)
                {
                    drawInput(inp, e);
                }
                else
                    break;
            }
        }

        private void drawNot(not not, Graphics e)
        {
            Pen pen = new Pen(Color.Black);

            e.DrawRectangle(pen, new Rectangle(not.x, not.y, 8, 8));
        }

        private void drawNots(Graphics e)
        {
            foreach (var n in nots)
            {
                if (n != null)
                {
                    drawNot(n, e);
                    drawWire(n.input, e);
                    drawWire(n.output, e);
                }
                else
                    break;
            }
        }

        private int checkWireClick(wire wire)
        {
            if (button == "Left" && wire != null)
            {
                if (wire.x1 - x < 2 && wire.x1 - x > -2 && wire.y1 - y < 2 && wire.y1 - y > -2)
                    return 1;
                if (wire.x2 - x < 2 && wire.x2 - x > -2 && wire.y2 - y < 2 && wire.y2 - y > -2)
                    return 2;
            }
            return 0;
        }

        private void checkWiresClick()
        {
            foreach (var wr in wires)
            {
                if (wr != null)
                {
                    if (checkWireClick(wr) == 1)
                    {
                        if (con == false)
                        {
                            con = true;
                            x2 = wr.x1;
                            y2 = wr.y1;
                            conn = wr;
                        }
                        else
                        {
                            wires[w++] = new wire(x, y, x2, y2, ip);
                            con = false;
                            ip = null;
                        }
                        
                        break;
                    }
                }
                else
                    break;
            }
        }

        private void checkTranzistorsClick()
        {
            foreach (var tr in tranzistors)
            {
                if (tr != null)
                {
                    if (checkWireClick(tr.input) == 1)
                    {
                        if (con == true)
                        {
                            wire a = new wire(x, y, x2, y2, ip);
                            a.addConnection(conn);
                            wires[w++] = a;
                            con = false;
                            tr.input.addConnection(wires[w - 1]);
                            ip = null;
                        }
                        else
                        {
                            con = true;
                            x2 = tr.input.x1;
                            y2 = tr.input.y1;
                            conn = tr.input;
                        }
                        break;
                    }
                    if (checkWireClick(tr.baze) == 1)
                    {
                        if (con == true)
                        {
                            wire a = new wire(x, y, x2, y2, ip);
                            a.addConnection(conn);
                            wires[w++] = a;
                            con = false;
                            tr.baze.addConnection(wires[w - 1]);
                            ip = null;
                        }
                        else
                        {
                            con = true;
                            x2 = tr.baze.x1;
                            y2 = tr.baze.y1;
                            conn = tr.baze;
                        }
                        break;
                    }
                    if (checkWireClick(tr.output) == 1)
                    {
                        if (con == true)
                        {
                            wire a = new wire(x, y, x2, y2, ip);
                            a.addConnection(conn);
                            wires[w++] = a;
                            tr.output.addConnection(wires[w - 1]);
                            con = false;
                            ip = null;
                        }
                        else
                        {
                            con = true;
                            x2 = tr.output.x1;
                            y2 = tr.output.y1;
                            conn = tr.output;
                        }
                        break;
                    }
                }
                else
                    break;
            }
        }

        private bool checkInputClick(input input)
        {
            if (input != null)
            {
                if (input.x - x < 5 && input.x - x > -5 && input.y - y < 5 && input.y - y > -5)
                    return true;
                return false;
            }
            else
                return false;
        }
        

        private void checkInputsClick()
        {
            foreach(var inp in inputs)
            {
                if (checkInputClick(inp))
                {
                    if (button == "Right")
                    {
                        inp.isOn = !inp.isOn;
                    }
                    if (button == "Left")
                    {
                        if (con == false)
                        {
                            con = true;
                            x2 = inp.x;
                            y2 = inp.y;
                            ip = inp;
                        }
                    }
                }
            }
        }

        private void checkNotsClick()
        {
            foreach (var n in nots)
            {
                if (n != null)
                {
                    if (checkWireClick(n.input) == 1)
                    {
                        if (con == true)
                        {
                            wire a = new wire(x, y, x2, y2, ip);
                            a.addConnection(conn);
                            wires[w++] = a;
                            con = false;
                            n.input.addConnection(wires[w - 1]);
                            ip = null;
                        }
                        else
                        {
                            con = true;
                            x2 = n.input.x1;
                            y2 = n.input.y1;
                            conn = n.input;
                        }
                        break;
                    }
                    if (checkWireClick(n.output) == 1)
                    {
                        if (con == true)
                        {
                            wire a = new wire(x, y, x2, y2, ip);
                            a.addConnection(conn);
                            wires[w++] = a;
                            n.output.addConnection(wires[w - 1]);
                            con = false;
                            ip = null;
                        }
                        else
                        {
                            con = true;
                            x2 = n.output.x1;
                            y2 = n.output.y1;
                            conn = n.output;
                        }
                        break;
                    }
                }
                else
                    break;
            }
        }

        private void updateTranzistors()
        {
            for (int j = 0; j < t; j++)
            {
                if (tranzistors[j].input.isOn && tranzistors[j].baze.isOn)
                {
                    tranzistors[j].output.isOn = true;
                }
                else
                    tranzistors[j].output.isOn = false;
            }
        }

        private void updateNots()
        {
            for (int j = 0; j < n; j++)
            {
                if (nots[j].input.connection != null)
                {
                    bool on = false;
                    foreach(var con in nots[j].input.connection)
                    {
                        if (con != null)
                        {
                            if (con.isOn)
                            {
                                on = true;
                                break;
                            }
                        }
                        else
                            break;
                    }
                    if (on)
                    {
                        nots[j].input.isOn = true;
                        nots[j].output.isOn = false;
                    }
                    else
                    {
                        nots[j].input.isOn = false;
                        nots[j].output.isOn = true;
                    }
                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            clicked = true;
            button = e.Button.ToString();
            x = e.X;
            y = e.Y;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            type = e.KeyData.ToString();
        }
    }
}
