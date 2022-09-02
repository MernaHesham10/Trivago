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
    public partial class UserLogincs : Form
    {
        string ordb = "Data Source = orcl; User Id = scott; Password = tiger;";
        OracleConnection conn;
        public UserLogincs()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select UserID from UserInfo where UserName =: InputName and UserPassword  =: InputPass";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("InputName", textBox1.Text);
            cmd.Parameters.Add("InputPass", textBox2.Text);


            if (String.IsNullOrEmpty(textBox1.Text)) {
                MessageBox.Show("Please Enter Your Name");
            }
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please Enter Your Password");
            }

            OracleDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    MessageBox.Show("LogIn Is Done", "Hello User " + dr[0].ToString());
                    if (MessageBox.Show("Do You Want To Go to Payment Page","", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        PaymentFormcs paymentFormcs = new PaymentFormcs();
                        paymentFormcs.Show();
                    }
                    else
                    {
                        Welcome welcome = new Welcome();
                        welcome.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Data Doesn't Found");
            }
            dr.Close();       
        }       
    }
}
