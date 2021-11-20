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

        private void Form3_Load(object sender, EventArgs e)
        {
            Config.loadConfig();
            fillComboDriver();
            fillComboDepartment();
        }

        private void fillComboDepartment()
        {
            List<string> items = new List<string>();
            for (int i = 0; i < Config.all.presets.Count; i++)
            {
                items.Add(Config.all.presets[i].name);
            }
            comboDepartment.Items.AddRange(items.ToArray());
        }

        private void fillComboDriver()
        {
            var drivers = Drivers.GetDirectories(Config.all.driversLocation);
            for (int i = 0; i<drivers.Length; i++)
            {
                drivers[i] = new DirectoryInfo(drivers[i]).Name;
            }
            comboDriver.Items.AddRange(drivers);
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (validateSelection()) return;
            installDrivers();
        }

        private bool validateSelection()
        {
            if (comboDriver.SelectedIndex == -1)
            {
                lblStatus.Text = "Driver not selected!";
                return true;
            }
            return false;
        }

        private bool installDrivers()
        {
            Drivers drvinstall = new Drivers();

            drvinstall.StartInstallation(drvInstallProgressChanged, drvInstallEnd, Drivers.GetDirectories(Config.all.driversLocation)[comboDriver.SelectedIndex]);
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

