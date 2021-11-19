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

namespace ANONEN
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            var drivers = Drivers.GetDirectories(@"E:\Drivers\");
            for (int i = 0; i < drivers.Length; i++)
            {
                drivers[i] = new DirectoryInfo(drivers[i]).Name;
            }
            cBoxDriver.Items.AddRange(drivers);
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (validateSelection()) return;
            installDrivers();
        }

        private bool validateSelection()
        {
            if (cBoxDriver.SelectedIndex == -1)
            {
                lblStatus.Text = "Driver not selected!";
                return true;
            }
            return false;
        }

        private bool installDrivers()
        {
            Drivers drvinstall = new Drivers();

            drvinstall.StartInstallation(drvInstallProgressChanged, drvInstallEnd, Drivers.GetDirectories(@"E:\Drivers\")[cBoxDriver.SelectedIndex]);
            return false;
        }

        private void drvInstallProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            var x = ((bool, string))e.UserState;
            lblStatus.Text = x.Item2;
        }

        private void drvInstallEnd(object sender, RunWorkerCompletedEventArgs e)
        {
            lblStatus.Text = "Drivers installed";
        }
    }
}

