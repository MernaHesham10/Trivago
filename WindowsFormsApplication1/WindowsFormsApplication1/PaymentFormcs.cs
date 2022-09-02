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
    public partial class PaymentFormcs : Form
    {
        string ordb = "Data Source = orcl; User Id = scott; Password = tiger;";
        OracleConnection conn;

        public PaymentFormcs()
        {
            InitializeComponent();
        }

        private void PaymentFormcs_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            comboBox1.Items.Add("Cash");
            comboBox1.Items.Add("Visa");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into PaymentData values (:Pay_ID,:Payment_Method,:pay_date,:user_id)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("Pay_ID ", textBox1.Text);
            cmd.Parameters.Add("Payment_Method", comboBox1.Text);
            cmd.Parameters.Add("pay_date", textBox2.Text);
            cmd.Parameters.Add("user_id", textBox1.Text);

            if (String.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Please Enter Your ID");
            }
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please Enter Payment Method");
            }
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please Enter Payment Date");
            }

            int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Payment is Done");
                BookingForm log = new BookingForm();
                log.Show();
            }
        }
    }
}
