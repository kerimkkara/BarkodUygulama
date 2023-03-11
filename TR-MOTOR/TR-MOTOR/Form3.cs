using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace TR_MOTOR
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

           
            
        }

      

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }
        Bitmap bmp;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }




        private void Form3_Load(object sender, EventArgs e)
        {

            Label.CheckForIllegalCrossThreadCalls = false;

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


        private void Form3_Shown(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(write);
            thread1.Start();
        }
    }
}
