using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XBee
{
    public partial class FormMain : Form
    {
        System.IO.Ports.SerialPort serialPort;

        public FormMain()
        {
            InitializeComponent();
            //
            // serialPort
            //
            this.serialPort = new System.IO.Ports.SerialPort();
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort_DataReceived);
        }

        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            XBee.Frame frame;
            switch (e.EventType)
            {
                case System.IO.Ports.SerialData.Chars:
                    frame = XBee.RecvFrame(this.serialPort);
                    System.Diagnostics.Debug.WriteLine(frame.FrameType);
                    if (frame.FrameType == XBee.FrameType.ReceivePacket) {
                        XBee.ReceivePacket receivePacket = new XBee.ReceivePacket(frame);
                        System.Diagnostics.Debug.WriteLine(receivePacket.Address64.ToString("X16"));
                        System.Diagnostics.Debug.WriteLine(receivePacket.Address16.ToString("X4"));
                        System.Diagnostics.Debug.WriteLine(receivePacket.Options.ToString("X2"));
                        System.Diagnostics.Debug.WriteLine(System.Text.Encoding.ASCII.GetString(receivePacket.Data));
                    }
                    break;
                case System.IO.Ports.SerialData.Eof:
                    break;
                default:
                    break;
            }
        }

        private void menuPortSetting_Click(object sender, EventArgs e)
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

        private void menuStart_Click(object sender, EventArgs e)
        {
            this.menuStart.Enabled = false;
            this.menuEnd.Enabled = true;
            this.menuPortSetting.Enabled = false;
            if (this.serialPort.IsOpen == false)
                this.serialPort.Open();
        }

        private void menuEnd_Click(object sender, EventArgs e)
        {
            this.menuStart.Enabled = true;
            this.menuEnd.Enabled = false;
            this.menuPortSetting.Enabled = true;

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
