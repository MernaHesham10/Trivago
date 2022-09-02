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
    public partial class BookingForm : Form
    {
        string ordb = "Data Source = orcl; User Id = scott; Password = tiger;";
        OracleConnection conn;
        int price;
        public BookingForm()
        {

            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into BookingD values (:Book_ID,:BookADate,:BookDDate,:bookCost,:user_id,:payID)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("Book_ID ", textBox1.Text);
            cmd.Parameters.Add("BookADate", textBox6.Text);
            cmd.Parameters.Add("BookDDate", textBox7.Text);
            cmd.Parameters.Add("bookCost", textBox4.Text);
            cmd.Parameters.Add("user_id", textBox1.Text);
            cmd.Parameters.Add("payID", textBox1.Text);

            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please Enter Your ID");
            }
            if (String.IsNullOrEmpty(textBox6.Text)){

                MessageBox.Show("Please Enter Booking Arrival Date ");
            }
            if (String.IsNullOrEmpty(textBox7.Text))
            {
                MessageBox.Show("Please Enter Booking Departure Date ");
            }

            int r = cmd.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Booking is Done");
                ServiceForm log = new ServiceForm();
                log.Show();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select r.RoomPrice from RoomData r where r.RoomID =: inputID";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("inputID", textBox3.Text);

            if (String.IsNullOrEmpty(textBox3.Text)){
                MessageBox.Show("Please Enter Room ID");
            }

            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox4.Text = dr[0].ToString();
            }
            dr.Close();
        }

        
    }
}
