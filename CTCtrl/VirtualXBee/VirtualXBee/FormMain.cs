using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VirtualXBee
{
    public partial class FormMain : Form
    {
        private System.IO.Ports.SerialPort serialPort;
        private System.Threading.Thread sendTread;
        private System.Threading.Thread recvTread;

        public FormMain()
        {
            InitializeComponent();

            //
            // serialPort
            //
            this.serialPort = new System.IO.Ports.SerialPort();
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort_DataReceived);

            this.sendTread = new System.Threading.Thread(this.sendProc);
            this.recvTread = new System.Threading.Thread(this.recvProc);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        private void menuPortConfig_Click(object sender, EventArgs e)
        {
            DialogPortConfig dlg = new DialogPortConfig();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Properties.Settings.Default.PortName = this.serialPort.PortName = dlg.PortName;
                Properties.Settings.Default.BaudRate = this.serialPort.BaudRate = dlg.BaudRate;
                Properties.Settings.Default.DataBits = this.serialPort.DataBits = dlg.DataBits;
                Properties.Settings.Default.Parity = this.serialPort.Parity = dlg.Parity;
                Properties.Settings.Default.StopBits = this.serialPort.StopBits = dlg.StopBits;
                Properties.Settings.Default.Save();
            }
        }

        private void sendProc()
        {

        }

        private void recvProc()
        {

        }

        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            XBee.Frame frame;
            switch (e.EventType)
            {
                case System.IO.Ports.SerialData.Chars:
                    frame = XBee.recvFrame(this.serialPort);
                    System.Diagnostics.Debug.WriteLine(frame.FrameType);
                    break;
                case System.IO.Ports.SerialData.Eof:
                    break;
                default:
                    break;
            }
        }

        private void menuStart_Click(object sender, EventArgs e)
        {
            this.menuStart.Enabled = false;
            this.menuEnd.Enabled = true;
            this.menuPortConfig.Enabled = false;
            if (this.serialPort.IsOpen == false)
                this.serialPort.Open();
        }

        private void menuEnd_Click(object sender, EventArgs e)
        {
            this.menuStart.Enabled = true;
            this.menuEnd.Enabled = false;
            this.menuPortConfig.Enabled = true;

            if (this.serialPort.IsOpen == true)
                this.serialPort.Close();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            this.serialPort.Close();
            this.Close();
        }
    }
}
