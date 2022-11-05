using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;



namespace WinFormsApp1
    {
    public partial class Form1 : Form
    {
        private readonly IList<Snow> snowflakes;
        private readonly Timer timer;
        private Graphics buffer;
        private Bitmap map, snoww, phonee, mainBitmap;
        private int speed = 30;
        public Form1()
        {
            InitializeComponent();
            snowflakes = new List<Snow>();
            phonee = new Bitmap(Properties.Resources.zima);
            map = new Bitmap(phonee,
                Screen.PrimaryScreen.WorkingArea.Width,
                Screen.PrimaryScreen.WorkingArea.Height);
            snoww = new Bitmap(Properties.Resources.snow3);
            mainBitmap = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width,
                Screen.PrimaryScreen.WorkingArea.Height);
            buffer = Graphics.FromImage(mainBitmap);

            AddCreateSnow();
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Timer_Tick;
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            foreach (var snowflake in snowflakes)
            {
                snowflake.Y += snowflake.Severity + speed;
                snowflake.X += snowflake.Severity;
                if (snowflake.Y > Screen.PrimaryScreen.WorkingArea.Height)
                {
                    snowflake.Y = -snowflake.Severity;
                }
                if (snowflake.X > Screen.PrimaryScreen.WorkingArea.Width)
                {
                    snowflake.X = -snowflake.Severity;
                }
            }
            Draw();
            timer.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void AddCreateSnow()
        {
            var rnd = new Random();

            for (int i = 0; i < 100; i++)
            {
                snowflakes.Add(new Snow
                {
                    X = rnd.Next(Screen.PrimaryScreen.WorkingArea.Width),
                    Y = -rnd.Next(Screen.PrimaryScreen.WorkingArea.Height),
                    Severity = rnd.Next(5, 35)
                });
            }
        }



        private void Draw()
        {
            buffer.DrawImage(map, new Rectangle(0, 0,
            Screen.PrimaryScreen.WorkingArea.Width,
            Screen.PrimaryScreen.WorkingArea.Height));
            foreach (var snowflake in snowflakes)
            {
                if (snowflake.Y > 0)
                {
                    buffer.DrawImage(snoww, new Rectangle(
                        snowflake.X,
                        snowflake.Y,
                        snowflake.Severity,
                        snowflake.Severity));
                }
            }
            var g = this.CreateGraphics();
            g.DrawImage(mainBitmap, 0, 0);
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }
        }
    }
} 