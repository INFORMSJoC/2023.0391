using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace IpInputExt.Ctrls
{
    public partial class IpInputTextbox : UserControl
    {
        /// <summary>
        /// Ip地址
        /// </summary>
        public string IP
        {
            get
            {
                return this.ToString();
            }
            set
            {
                string ipStr = value;
                if (string.IsNullOrEmpty(ipStr))
                {
                    Ip1.Text = "";
                    Ip2.Text = "";
                    Ip3.Text = "";
                    Ip4.Text = "";
                    ip = "";
                }
                else
                {
                    try
                    {
                        IPAddress ipValue;
                        ipValue = IPAddress.Parse(ipStr);
                        string[] ips = ipStr.Split('.');
                        Ip1.Text = ips[0];
                        Ip2.Text = ips[1];
                        Ip3.Text = ips[2];
                        Ip4.Text = ips[3];
                        ip = ipStr;
                    }
                    catch
                    {
                        Ip1.Text = "";
                        Ip2.Text = "";
                        Ip3.Text = "";
                        Ip4.Text = "";
                        ip = "";
                    }
                }
            }
        }
        private string ip;

        public IpInputTextbox()
        {
            InitializeComponent();
        }

        private void Ip1_TextChanged(object sender, EventArgs e)
        {
            if (Ip1.Text.Length == 3 && Ip1.Text.Length > 0 && Ip1.SelectionLength == 0)
            {
                Ip2.Focus();
                Ip2.Select(0, Ip2.Text.Length);
            }
        }

        private void Ip2_TextChanged(object sender, EventArgs e)
        {
            if (Ip2.Text.Length == 3 && Ip2.Text.Length > 0 && Ip2.SelectionLength == 0)
            {
                Ip3.Focus();
                Ip3.Select(0, Ip3.Text.Length);
            }
        }

        private void Ip3_TextChanged(object sender, EventArgs e)
        {
            if (Ip3.Text.Length == 3 && Ip3.Text.Length > 0 && Ip3.SelectionLength == 0)
            {
                Ip4.Focus();
                Ip4.Select(0, Ip4.Text.Length);
            }
        }

        /// <summary>
        /// ToString重写
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string Ipstr = Ip1.Text + "." + Ip2.Text + "." + Ip3.Text + "." + Ip4.Text;
            try
            {
                IPAddress.Parse(Ipstr);
            }
            catch
            {
                return "IP地址格式不正确";
            }
            this.ip = Ipstr;
            return this.ip;
        }

        private void Ip1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 9 && Ip1.Text.Length > 0 && Ip1.SelectionLength == 0)
            {
                Ip2.Focus();
                Ip2.Select(0, Ip2.Text.Length);
            }
        }

        private void Ip2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 9 && Ip2.Text.Length > 0 && Ip2.SelectionLength == 0)
            {
                Ip3.Focus();
                Ip3.Select(0, Ip3.Text.Length);
            }
        }

        private void Ip3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 9 && Ip3.Text.Length > 0 && Ip3.SelectionLength == 0)
            {
                Ip4.Focus();
                Ip4.Select(0, Ip4.Text.Length);
            }
        }
    }
}
