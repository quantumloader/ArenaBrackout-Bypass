using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AB_Bypass_Gui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            checknames();
        }

        private void checknames()
        {
            // FIND PC INFO
            try
            {
                // chat gpt :(
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");

                ManagementObjectSearcher processorSearcher =
                    new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

                foreach (ManagementObject processor in processorSearcher.Get())
                {
                    string processorName = processor["Name"].ToString();

                    // write new data
                    label2.Text = "CPU: " + processorName;
                }
                foreach (ManagementObject videoController in searcher.Get())
                {
                    string gpuName = videoController["Name"].ToString();
                    string driverVersion = videoController["DriverVersion"].ToString();

                    // write new data
                    label1.Text = "GPU: " + gpuName;
                    label4.Text = "Driver version: " + driverVersion;
                }
            }
            catch (ManagementException ex)
            {
                MessageBox.Show("Fail to dump PC DATA: " + ex.Message);
            }
        }


        // MISSCLICK
        private void label1_Click(object sender, EventArgs e)
        {

        }


        // BYPASS
        private void button1_Click(object sender, EventArgs e)
        {

            // path for gpu name
            string gpuPath = @"SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}";
            string direct = "0000";

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(gpuPath + "\\" + direct, true))
            {
                if (key != null)
                {
                    // change names to (NVIDIA GeForce RTX 2080)
                    key.SetValue("HardwareInformation.ChipType", "NVIDIA GeForce RTX 2080", RegistryValueKind.String);
                    key.SetValue("HardwareInformation.AdapterString", "NVIDIA GeForce RTX 2080", RegistryValueKind.String);
                    key.SetValue("DriverDesc", "NVIDIA GeForce RTX 2080", RegistryValueKind.String);

                    // Show done message box
                    MessageBox.Show($"Bypassed!");

                    newinfo();
                }
                else
                {
                    // idk but show message box
                    MessageBox.Show($"i cant bypass it wtf");
                }
            }
        }

        private void newinfo()
        {
            // path for new gpu name
            string gpuPath = @"SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}";
            string direct = "0000";

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(gpuPath + "\\" + direct, true))
            {
                if (key != null)
                {
                    // refresh name
                    object t = key.GetValue("HardwareInformation.ChipType");

                    label1.Text = "GPU: " + t.ToString();
                }
            }
        }

        // OPEN LINK #1

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
