using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CTCtrl
{
    public partial class DialogPortConfig : Form
    {
        private object[] baudRate = {
            1200,
            2400,
            4800,
            7200,
            9600,
            14400,
            19200,
            38400,
            57600,
            115200,
            230400,
            460800,
            921600
        };

        private object[] dataBits = { 7, 8 };

        public string PortName
        {
            get { return (string)this.cmbPortName.SelectedItem; }
        }

        public int BaudRate 
        {
            get { return (int)this.cmbBaudRate.SelectedItem; }
        }

        public int DataBits
        {
            get { return (int)this.cmbDataBits.SelectedItem; }
        }

        public System.IO.Ports.Parity Parity
        {
            get { return (System.IO.Ports.Parity)this.cmbParity.SelectedItem; }
        }

        public System.IO.Ports.StopBits StopBits
        {
            get { return (System.IO.Ports.StopBits)this.cmbStopBits.SelectedItem; }
        }

        public DialogPortConfig()
        {
            InitializeComponent();

            //
            // cmbProtName
            //
            this.cmbPortName.Items.AddRange((object[])System.IO.Ports.SerialPort.GetPortNames());
            this.cmbPortName.SelectedIndex = 0;
            //
            // cmbBaudRate
            //
            this.cmbBaudRate.Items.AddRange(baudRate);
            //
            // cmbDataBits
            //
            this.cmbDataBits.Items.AddRange(dataBits);
            //
            // cmbParity
            //
            this.cmbParity.DataSource = Enum.GetValues(typeof(System.IO.Ports.Parity));
            //
            // cmbStopBits
            //
            this.cmbStopBits.DataSource = Enum.GetValues(typeof(System.IO.Ports.StopBits));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.cmbPortName.SelectedItem = Properties.Settings.Default.PortName;
            this.cmbBaudRate.SelectedItem = Properties.Settings.Default.BaudRate;
            this.cmbDataBits.SelectedItem = Properties.Settings.Default.DataBits;
            this.cmbParity.SelectedItem = Properties.Settings.Default.Parity;
            this.cmbStopBits.SelectedItem = Properties.Settings.Default.StopBits;
        }
    }
}
