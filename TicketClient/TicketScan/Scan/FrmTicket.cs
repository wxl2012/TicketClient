using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using DAL;
using Models;

namespace TicketScan.Scan
{
    public partial class FrmTicket : Form, IPlugin.IForm
    {
        private String text;
        private Boolean isOver;

        private String command = "";
        private DateTime lastTime = DateTime.Now;
        private int pointTop = 20;  //按钮起始top
        private int x = 0;          //起始位置
        private int rowButtonCount = 0;
        private int timeSpanSeconds = 0;
        private Size buttonSize = new Size(80, 90); //定义按钮尺寸

        private const String AVATAR_DEFAULT_FILE = "\\Assets\\Image\\avatar.jpg";

        private PeopleTicket ticket = null;


        public FrmTicket()
        {
            InitializeComponent();
            initUI();
        }

        private void FrmTicket_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            Tools.AppConfigHelper.RootPath = Application.ExecutablePath;
            try
            {
                this.timeSpanSeconds = Convert.ToInt32(Tools.AppConfigHelper.GetAppSettingsValue("NewCommandTimeSpan"));
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            
        }

        private void FrmTicket_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if ((DateTime.Now - lastTime).Seconds > timeSpanSeconds)
            {
                this.Text = "门票扫码";
                command = "";
            }

            lastTime = DateTime.Now;
            if (isKeyword(e.KeyChar)) {
                if (e.KeyChar == 13) {
                    this.Text += command;
                    Console.WriteLine(command);
                    execute(command);
                }
                return;
            }
            
            command += e.KeyChar.ToString();
        
        }

        private bool isKeyword(char keyChar)
        {
            bool flag = false;

            int key = (int)keyChar;
            switch (key)
            {
                case 13:
                    flag = true;
                    break;
                case 8:
                    flag = true;
                    break;
                case 27:
                    flag = true;
                    break;
            }

            return flag;
        }

        private void refreshTicket(int count)
        {
            this.panelButtons.Controls.Clear();
            pointTop = 20;
            x = 10;
            rowButtonCount = Convert.ToInt32(this.panelButtons.Width / (buttonSize.Width + 30));

            //添加全部按钮
            if (count > 1)
            {
                Button btnAll = getTicketButton("使用全部票");
                btnAll.BackColor = Color.Aquamarine;
                btnAll.Click -= Button_Click;
                btnAll.Click += BtnAll_Click;
                this.panelButtons.Controls.Add(btnAll);
            }
            

            for (int i = 0; i < count; i++)
            {
                Button button = getTicketButton(String.Format("观演票{0}", i));
                this.panelButtons.Controls.Add(button);
            }
        }

        private void BtnAll_Click(object sender, EventArgs e)
        {
            if (ticket == null)
            {
                return;
            }

            ticket.Num = 0;
            PeopleTicketDAL.Update(ticket);
            this.panelButtons.Controls.Clear();
        }

        private Button getTicketButton(String text)
        {
            Button button = new Button();
            button.Click += Button_Click;
            button.Text = text;

            //计算显示位置
            if (this.panelButtons.Controls.Count != 0 && this.panelButtons.Controls.Count % rowButtonCount == 0) {
                x = 0;
                pointTop = Convert.ToInt32(this.panelButtons.Controls.Count / rowButtonCount) * (buttonSize.Height + 30);
            }
            x = x == 0 ? 10 : x;
            Point point = new Point(x, pointTop);
            button.Bounds = new Rectangle(point, buttonSize);

            x += buttonSize.Width + 30;
            return button;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ticket.Num -= 1;
            PeopleTicketDAL.Update(ticket);

            Button button = sender as Button;
            this.panelButtons.Controls.Remove(button);
            if (this.panelButtons.Controls.Count == 1)
            {
                this.panelButtons.Controls.Clear();
            }
        }

        private void initUI()
        {
            this.panelButtons.AutoScroll = true;
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


        #region 业务方法

        private bool execute(String command) {
            command = "app://scan/ticket/MQ==";
            if (command.IndexOf("app://") != 0) {
                return false;
            }
            command = command.Replace("app://", "");
            String[] commands = command.Split('/');

            if (commands.Length < 1) {
                return false;
            }

            if (commands[0] != "scan") {
                return false;
            }

            switch (commands[1]) {
                case "ticket":
                    ticket = this.GetTicketById(commands[2]);
                    this.labBrithday.Text = ticket.People.Birthday.ToString();
                    this.labGender.Text = ticket.People.Gender;
                    this.labIdentity.Text = ticket.People.Identity;
                    this.labName.Text = ticket.People.FirstName + ticket.People.LastName;
                    this.labPhone.Text = ticket.People.Phone;
                    this.labWechat.Text = ticket.Wechat == null ? "" : ticket.Wechat.NickName;
                    this.labNo.Text = ticket.Member == null ? "" : ticket.Member.No;
                    
                    try {
                        Stream stream = WebRequest.Create(ticket.Wechat.Headimgurl).GetResponse().GetResponseStream();
                        Image image = Image.FromStream(stream);
                        this.imgPortrait.BackgroundImage = image;
                    } catch (Exception error) {
                        //LogHelper.WriteLog(this.GetType(), error);
                        this.imgPortrait.BackgroundImage = Image.FromFile(Application.StartupPath + AVATAR_DEFAULT_FILE);
                    }
                    this.refreshTicket(ticket.Num);
                    break;
            }

            return true;
        }

        private PeopleTicket GetTicketById(String id)
        {
            byte[] byteId = Convert.FromBase64String(id);
            String value = ASCIIEncoding.Default.GetString(byteId);
            PeopleTicket ticket = PeopleTicketDAL.GetPeopleTicketById(Convert.ToInt32(value));
            return ticket;
        }

        #endregion
    }
}
