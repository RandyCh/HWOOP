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
    public partial class FrmNewUser : Form
    {
        public FrmNewUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyMember newusers = new MyMember();
            bool logon = false;
            if (textBox2.Text == textBox3.Text)
            {
                newusers.CreateUser(textBox1.Text, textBox2.Text, textBox4.Text, out logon);
                if (logon == true)
                {
                    MessageBox.Show("logon successfully ");
                }
            }
        }
    }
}

