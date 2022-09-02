using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class AdminChoices : Form
    {
        public AdminChoices()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();
            adminForm.Show();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            UserInfoForm searchForm = new UserInfoForm();
            searchForm.Show();
        }
    }
}
