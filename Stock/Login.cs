using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";//the method clear cears the textfield
            textBox2.Clear();
            textBox1.Focus();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            //TO : DO Check login username and password
            //step 1 writing the code for getting the sql server
            SqlConnection con = Connection.GetConnection();
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT *
            FROM[dbo].[Login]  Where Username = '"+ textBox1.Text +"' and Password = '"+ textBox2.Text +"'",con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if(dt.Rows.Count == 1)
            {
                this.Hide(); //this code hides the login form
                StockMain main = new StockMain();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid Username and Password...!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonClear_Click(sender, e);
            }



              

        }
    }
}
