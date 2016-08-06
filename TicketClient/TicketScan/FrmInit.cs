using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicketScan.Scan;

namespace TicketScan
{
    public partial class FrmInit : Form, IPlugin.IForm
    {
        private String text;
        private Boolean isOver;

        public FrmInit()
        {
            InitializeComponent();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            FrmTicket ticket = new FrmTicket();
            ticket.Show();
        }

        public void Start()
        {
            this.Show();
        }

        public void Stop()
        {
            Application.Exit();
        }

        public string GetProcessText()
        {
            return text;
        }

        public bool IsNext()
        {
            return isOver;
        }
    }
}
