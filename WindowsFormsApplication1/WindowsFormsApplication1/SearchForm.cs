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
    public partial class SearchForm : Form
    {
        string ordb = "Data Source = orcl; User Id = scott; Password = tiger;";
        OracleConnection conn;
        
      
        public SearchForm()
        {
            InitializeComponent();
        }

        private void Connected_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select DestName from DestinationData";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            dr.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select h.HotelName from HotelInfo h , DestinationData d where h.HotelID = d.HotelID and h.HotelLocation =: DestName" ;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("DestName", comboBox1.SelectedItem.ToString());

            OracleDataReader dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                textBox3.Text = dr[0].ToString();
            }
            dr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "GETROOMS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("DestName", comboBox1.SelectedItem.ToString());
            cmd.Parameters.Add("HotelName", textBox3.Text);
            cmd.Parameters.Add("idout", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    comboBox3.Items.Add(dr[0]);
                }
            }
            else
            {
                MessageBox.Show("Data Doesn't Found");
            }
            dr.Close();
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

                try {
                    conn = new OracleConnection(ordb);
                    conn.Open();
                    int price;
                    int capacity;
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "GETROOMDATA";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Room_ID", comboBox3.SelectedItem.ToString());
                    cmd.Parameters.Add("Hotel_Name", textBox3.Text);
                    cmd.Parameters.Add("Room_Price", OracleDbType.Int32, ParameterDirection.Output);
                    cmd.Parameters.Add("Room_Capacity", OracleDbType.Int32, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();
                    price = Convert.ToInt32(cmd.Parameters["Room_Price"].Value.ToString());
                    capacity = Convert.ToInt32(cmd.Parameters["Room_Capacity"].Value.ToString());

                    textBox1.Text = price.ToString();
                    textBox2.Text = capacity.ToString();

                    if (MessageBox.Show("Do You Want To Book This Room", "Welcome To Payment", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        UserLogincs userLogincs = new UserLogincs();
                        userLogincs.Show();
                    }
                }
            catch 
                {
                    MessageBox.Show("Data Doesn't Found");
                }     
        }
    }
}
