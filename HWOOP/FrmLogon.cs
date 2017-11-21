using HWOOP.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HWOOP
{
    public partial class FrmLogon : Form
    {

        int count = 0;
        public FrmLogon()
        {
            InitializeComponent();
        }
        private void btuCheck_click(object sender, EventArgs e)
        {
            MyMember y = new MyMember();
            y.m_username = textBox1.Text;
            y.m_Password = textBox2.Text;
            
            if (checkBox1.Checked==true)
            {
                Settings.Default.username = textBox1.Text;
                Settings.Default.password = textBox2.Text;
                Settings.Default.Save();
            }
        

            y.ValidateUser(out bool checkresult);

            if (checkresult)
            {
                FrmMain frmMain = new FrmMain();//產生Form2的物件，才可以使用它所提供的Method
                this.Visible = false;//將Form1隱藏。由於在Form1的程式碼內使用this，所以this為Form1的物件本身
                frmMain.Visible = true;//顯示第二個視窗
            }
            else
            {
                MessageBox.Show("logon failed 失敗次數" + count + @"/3");
                count++;
            }
            if (count > 3)
            {
                Application.Exit();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmForgetPassword f = new FrmForgetPassword();
            f.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FrmLogon_Load(object sender, EventArgs e)
        {
            textBox2.Text = Settings.Default.password;
            textBox1.Text = Settings.Default.username;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MyMember g = new MyMember();
            textBox1.Text= g.FindUsersByEmail(textBox3.Text);
        }
    }
}
