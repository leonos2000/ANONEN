using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Collections.ObjectModel;

namespace ANONEN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Form2 frm2 = new Form2();
            frm2.TopLevel = false;
            panel1.Controls.Add(frm2);
            frm2.Show();
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Form3 frm2 = new Form3();
            frm2.TopLevel = false;
            panel1.Controls.Add(frm2);
            frm2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string help = @"D:\ANON\Instructions\Help.txt";
            Process.Start(help);
        }

        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Form4 frm2 = new Form4();
            frm2.TopLevel = false;
            panel1.Controls.Add(frm2);
            frm2.Show();
        }
    }
}
