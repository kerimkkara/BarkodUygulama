using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace BarkodUygulama
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        Bitmap bmp;

        private void Form2_Load(object sender, EventArgs e)
        {
            Label.CheckForIllegalCrossThreadCalls = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

     

        public void write()
        {

            Graphics g = this.CreateGraphics();
            Thread.Sleep(2000);
            bmp = new Bitmap(this.Size.Width, this.Size.Height, g);
            Graphics mg = Graphics.FromImage(bmp);
            mg.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, this.Size);
            printDocument1.Print();
            try
            {
                this.Close();
            }
            catch (Exception ex) { Console.Write("Error info:" + ex.Message); }

        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(write);
            thread1.Start();
        }
    }
}
