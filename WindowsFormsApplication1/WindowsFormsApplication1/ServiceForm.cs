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
    public partial class ServiceForm : Form
    {
        string ordb = "Data Source = orcl; User Id = scott; Password = tiger;";
        OracleConnection conn;
        public ServiceForm()
        {
            InitializeComponent();
        }

        private void ServiceForm_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into ServiceData values (:Service_ID,:lunchornot,:activity,:User_ID,:Payment_ID,:Booking_ID)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("Service_ID ", textBox1.Text);
            cmd.Parameters.Add("lunchornot", textBox5.Text);
            cmd.Parameters.Add("activity", textBox6.Text);
            cmd.Parameters.Add("User_ID", textBox1.Text);
            cmd.Parameters.Add("Payment_ID", textBox1.Text);
            cmd.Parameters.Add("Booking_ID", textBox1.Text);

            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please Enter Your ID");
            }
            if (String.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Please Select If Want Lunch Or Not");

            }
            if (String.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Please Select If Want To Participate In Activities Or Not");
            }

            int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Service is Done");
                MessageBox.Show("Room is Booked Successfully ! ");
                Welcome welcome = new Welcome();
                welcome.Show();

            }
        }

        
    }
}
