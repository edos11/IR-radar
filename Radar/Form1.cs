using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
namespace Radar
{
    public partial class Form1 : Form
    {
        int centarX = 200;
        int centarY = 200;
        int x, y, r=200, ugao, prosla1=200, prosla2=150, brojac1=1, brojac2=0;
        int px, py;
        tacka[] niz = new tacka[38];
        Timer t = new Timer();
        Bitmap bmp;
        Pen p;
        Graphics g;
        SerialPort a = new SerialPort();

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(400,400);
            pictureBox1.BackColor = Color.Black;
            t.Interval = 500;
            ugao = 0;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
            try { 
                a.PortName = "COM1";
                a.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "ERROR");
            }

            tacka c = new tacka(200, 150);
            niz[0] = c;
        }

        private void t_Tick(object sender, EventArgs e)
        {
            p = new Pen(Color.Green, 1f);
            g = Graphics.FromImage(bmp);
            g.DrawEllipse(p, 0, 0, 400, 400);
            g.DrawEllipse(p, 20, 20, 400 - 40, 400 - 40);
            g.DrawEllipse(p, 40, 40, 400 - 80, 400 - 80);
            g.DrawEllipse(p, 60, 60, 400 - 120, 400 - 120);
            g.DrawEllipse(p, 80, 80, 400 - 160, 400 - 160);
            g.DrawEllipse(p, 100, 100, 400 - 200, 400 - 200);
            g.DrawEllipse(p, 120, 120, 400 - 240, 400 - 240);
            g.DrawEllipse(p, 140, 140, 400 - 280, 400 - 280);
            g.DrawEllipse(p, 160, 160, 400 - 320, 400 - 320);
            g.DrawEllipse(p, 180, 180, 400 - 360, 400 - 360);
            g.DrawLine(p, new Point(centarX, 0), new Point(centarX, 400));
            g.DrawLine(p, new Point(0, centarY), new Point(400, centarY));
            g.DrawLine(p, new Point(58, 58), new Point(342, 342));
            g.DrawLine(p, new Point(58, 342), new Point(342, 58));
            int pugao = (ugao - 10) % 360;
            if (ugao >= 0 && ugao <= 180)
            {
                x = centarX + (int)(r * Math.Sin(Math.PI * ugao / 180));
                y = centarY - (int)(r * Math.Cos(Math.PI * ugao / 180));
            }
            else
            {
                x = centarX - (int)(r * -Math.Sin(Math.PI * ugao / 180));
                y = centarY - (int)(r * Math.Cos(Math.PI * ugao / 180));
            }
            if (pugao >= 0 && pugao <= 180)
            {
                px = 200 + (int)(200 * Math.Sin(Math.PI * pugao / 180));
                py = 200 - (int)(200 * Math.Cos(Math.PI * pugao / 180));
            }
            else
            {
                px = 200 - (int)(200 * -Math.Sin(Math.PI * pugao / 180));
                py = 200 - (int)(200 * Math.Cos(Math.PI * pugao / 180));
            }
            g.DrawLine(new Pen(Color.Black, 1f), new Point(centarX, centarY), new Point(px, py));
            g.DrawLine(new Pen(Color.Green, 1f), new Point(centarX, centarY), new Point(x, y));
            int t1, t2, t3, t4;
            int b = Convert.ToInt32(a.ReadLine());
            {
                if (ugao >= 0 && ugao <= 180)
                {
                    t1 = centarX + (int)((b*2.5) * Math.Sin(Math.PI * ugao / 180));
                    t2 = centarY - (int)((b*2.5) * Math.Cos(Math.PI * ugao / 180));
                    t3 = centarX + (int)((b*2.5) * Math.Sin(Math.PI * ugao / 180));
                    t4 = centarY - (int)((b*2.5) * Math.Cos(Math.PI * ugao / 180));
                }
                else
                {
                    t1 = centarX - (int)((b*2.5) * -Math.Sin(Math.PI * ugao / 180));
                    t2 = centarY - (int)((b*2.5) *  Math.Cos(Math.PI * ugao / 180));
                    t3 = centarX - (int)((b*2.5) * -Math.Sin(Math.PI * ugao / 180));
                    t4 = centarY - (int)((b*2.5) *  Math.Cos(Math.PI * ugao / 180));
                }
                if (brojac1 == 34) niz[37] = niz[0];
                label2.Text = Convert.ToString(b);
                if (brojac2 > 36) { g.DrawLine(new Pen(Color.Black, 3f), new Point(niz[brojac1].x, niz[brojac1].y), new Point(niz[brojac1+1].x, niz[brojac1+1].y)); }
                g.DrawLine(new Pen(Color.Red, 3f), new Point(prosla1, prosla2), new Point(t1, t2));
                label2.Location = new Point(t1, t2);
                prosla1 = t3;
                prosla2 = t4;
                tacka s = new tacka(t3, t4);
                niz[brojac1] = s;
                brojac1++;
                brojac2++;
            }
            pictureBox1.Image = bmp;
            p.Dispose();
            g.Dispose();
            ugao=ugao+10;
            if (brojac1 == 37) brojac1 = 0;
            if (ugao == 360) { ugao = 0;  }
        }
    }
}