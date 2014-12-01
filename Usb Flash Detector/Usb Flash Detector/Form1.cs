using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Usb_Flash_Detector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        delegate void BindMessageBox(string text);
        private void UpdateTextbox(string _Text)
        {

            MessageBox.Show(_Text);

        }
        clsUsbFlashDetector detector = new clsUsbFlashDetector();
        private void button1_Click(object sender, EventArgs e)
        {
            
            detector.DoWorkEvent += new DoWorkEventHandler(backgroundWorker1_DoWork);
            detector.start();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            bool DriveLetter = detector.verifyDriver();
            if(DriveLetter)
                this.Invoke(new BindMessageBox(UpdateTextbox), new object[] { "Pendrive Real" });
            else
                this.Invoke(new BindMessageBox(UpdateTextbox), new object[] { "Pendrive no" });

            string letter = detector.GetDrivers() + " - " + detector.GetDriverSerial() + " - " + detector.GetDriverKey();
            this.Invoke(new BindMessageBox(UpdateTextbox), new object[] { letter });
        }
    }
}
