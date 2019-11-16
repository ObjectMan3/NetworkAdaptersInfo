using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Net;
namespace WindowsFormsApplication8
{
    public partial class Form1 : Form
    {
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            FormWindowState previousWindowState = this.WindowState;

            base.WndProc(ref m);

            FormWindowState currentWindowState = this.WindowState;

            if (previousWindowState != currentWindowState && currentWindowState == FormWindowState.Maximized)
            {
                this.Close();
            }
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    if ((int)m.Result == HTCLIENT)
                        m.Result = (IntPtr)HTCAPTION;
                    return;
             }

        }

        public Form1()
        {
            InitializeComponent();
            listBox1.SendToBack();
            listBox2.SendToBack();
            this.FormBorderStyle = FormBorderStyle.None;

            this.SizeChanged += new EventHandler(Form1_SizeChanged);
            FormMaximized += new EventHandler(Form1_FormMaximized);

            _CurrentWindowState = this.WindowState;
            if (_CurrentWindowState == FormWindowState.Maximized)
            {
                this.Close();
            }
        }

        private void FillUps()
        {
            listBox2.Items.Clear();
            listBox1.Items.Clear();
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            listBox2.Items.Add(" ");
            listBox1.Items.Add(" ");
            foreach (NetworkInterface adapter in interfaces)
            {
                var ipProps = adapter.GetIPProperties();

                foreach (var ip in ipProps.UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        listBox2.Items.Add(adapter.Description.ToString());
                        if (adapter.OperationalStatus.ToString() == "Up")
                        { listBox1.Items.Add(ip.Address.ToString()); }
                        else { listBox1.Items.Add(adapter.OperationalStatus.ToString()); }
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FillUps();
        }
        public event EventHandler FormMaximized;
        private void FireFormMaximized()
        {
            if (FormMaximized != null)
            {
                FormMaximized(this, EventArgs.Empty);
            }
        }

        private FormWindowState _CurrentWindowState;
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized && _CurrentWindowState != FormWindowState.Maximized)
            {
                FireFormMaximized();
            }
            _CurrentWindowState = this.WindowState;
        }

        void Form1_FormMaximized(object sender, EventArgs e)
        {
        
            this.Close();
        }
        private void Form1_Maximize(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Close();
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void ListBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            FillUps();
            listBox1.Refresh();
        }
        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void listBox1_mouseclick(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            FillUps();
            listBox1.Refresh();
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
