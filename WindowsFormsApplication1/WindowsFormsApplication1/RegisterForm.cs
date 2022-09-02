using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace WindowsFormsApplication1
{
    public partial class RegisterForm : Form
    {
        string ordb = "Data Source = orcl; User Id = scott; Password = tiger;";
        OracleConnection conn;
        public RegisterForm()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into UserInfo values (:UserID, :UserName, :UserPassword , :UserGender, :UserPhone)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("UserID", textBox1.Text);
            cmd.Parameters.Add("UserName", textBox2.Text);
            cmd.Parameters.Add("UserPassword", textBox5.Text);
            cmd.Parameters.Add("UserGender", textBox3.Text);
            cmd.Parameters.Add("UserPhone", textBox4.Text);

            if (String.IsNullOrEmpty(textBox1.Text)){
                MessageBox.Show("Please Enter Your ID");
            }
            if (String.IsNullOrEmpty(textBox2.Text)){
                MessageBox.Show("Please Enter Your Name");
            }
            if (String.IsNullOrEmpty(textBox3.Text)){
                MessageBox.Show("Please Enter Your Password");
            }
            if (String.IsNullOrEmpty(textBox4.Text)){
                MessageBox.Show("Please Enter Your Gender");
            }
            if (String.IsNullOrEmpty(textBox5.Text))
            { 
                MessageBox.Show("Please Enter Your Phone Number");
            }
            int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Welcome New User");
                Welcome welcome = new Welcome();
                welcome.Show();
            }
        }
    }
}
