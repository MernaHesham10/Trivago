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
    public partial class AdminLogin : Form
    {
        string ordb = "Data Source = orcl; User Id = scott; Password = tiger;";
        OracleConnection conn;
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select AdminID from AdminInfo where AdminName =: InputName and AdminPassword  =: InputPass";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("InputName", textBox1.Text);
            cmd.Parameters.Add("InputPass", textBox2.Text);


            if(String.IsNullOrEmpty(textBox1.Text))
            {
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
                    MessageBox.Show("Hello Admin " + dr[0].ToString(), "LogIn Is Done");
                    AdminChoices adminChoices = new AdminChoices();
                    adminChoices.Show();
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
