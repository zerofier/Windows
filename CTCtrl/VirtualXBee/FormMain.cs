using System;
using System.Threading;
using System.Windows.Forms;
using NETMF.OpenSource.XBee;
using NETMF.OpenSource.XBee.Api.Zigbee;

namespace VirtualXBee
{
    public partial class FormMain : Form
    {
        private System.Threading.Thread sendTread;
        private System.Threading.Thread recvTread;
        private XBeeApi xbeeApi;

        public FormMain()
        {
            InitializeComponent();

            //
            // xbeeApi
            //
            this.sendTread = new System.Threading.Thread(this.sendProc);
            this.recvTread = new System.Threading.Thread(this.recvProc);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        

        private void sendProc()
        {
            for (; ; Thread.Sleep(10 * 1000))
            {
                RxResponse res = new RxResponse();
            }
        }

        private void recvProc()
        {

        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            if (this.xbeeApi != null)
            {
                this.xbeeApi.Close();
                this.xbeeApi = null;
            }
            this.Close();
        }

        private void menuStart_Click(object sender, EventArgs e)
        {
            this.menuStart.Enabled = false;
            this.menuEnd.Enabled = true;
            this.menuPortSetting.Enabled = false;

            this.xbeeApi = new XBeeApi(Properties.Settings.Default.PortName, Properties.Settings.Default.BaudRate);
            this.xbeeApi.Open();
        }

        private void menuEnd_Click(object sender, EventArgs e)
        {
            this.menuStart.Enabled = true;
            this.menuEnd.Enabled = false;
            this.menuPortSetting.Enabled = true;
            if (this.xbeeApi != null)
            {
                this.xbeeApi.Close();
                this.xbeeApi = null;
            }
        }

        private void menuPortSetting_Click(object sender, EventArgs e)
        {
            DialogPortConfig dlg = new DialogPortConfig();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Properties.Settings.Default.PortName = dlg.PortName;
                Properties.Settings.Default.BaudRate = dlg.BaudRate;
                Properties.Settings.Default.DataBits = dlg.DataBits;
                Properties.Settings.Default.Parity = dlg.Parity;
                Properties.Settings.Default.StopBits = dlg.StopBits;
                Properties.Settings.Default.Save();
            }
        }

        private void timerSender_Tick(object sender, EventArgs e)
        {

        }
    }
}
