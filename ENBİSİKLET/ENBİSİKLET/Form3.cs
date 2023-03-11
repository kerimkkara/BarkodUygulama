using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TR_BİSİKLET;
using ZXing;

namespace ENBİSİKLET
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        DataTableCollection dtc;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = dtc[comboBox1.SelectedIndex];
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openfile = new OpenFileDialog())
            {
                if (openfile.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = openfile.FileName;
                    using (var stream = File.Open(openfile.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader excelreader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet reseult = excelreader.AsDataSet(new ExcelDataSetConfiguration()

                            {
                                ConfigureDataTable = (x) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            }
                            );
                            dtc = reseult.Tables;
                            comboBox1.Items.Clear();
                            foreach (DataTable table in dtc) comboBox1.Items.Add(table.TableName);
                        }
                    }
                }
            }
        }

        public void write()
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                Form2 f2 = new Form2();
                f2.textBox4.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                f2.textBox5.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                f2.textBox6.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                f2.textBox7.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
                f2.textBox8.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
                f2.textBox9.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
                f2.textBox11.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
                f2.textBox12.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
                f2.textBox13.Text = dataGridView1.Rows[i].Cells[9].Value.ToString();
            
                BarcodeWriter writer = new BarcodeWriter() { Format = BarcodeFormat.CODE_128 };
                f2.pictureBox1.Image = writer.Write(f2.textBox6.Text);
                Thread.Sleep(2000);
                f2.ShowDialog();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(write);
            thread1.Start();
        }

    }
}
