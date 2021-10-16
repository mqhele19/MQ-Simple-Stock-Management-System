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
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            LoadData();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SqlConnection con = Connection.GetConnection();
            //Insert Logic
            con.Open();
            //
            bool status = false;
            if (comboBox1.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;

            }

            var sqlQuery = "";
            //checking if the record exists
            if (ifProductExists( con, textBox1.Text))
            {
                sqlQuery = @"UPDATE [Products]  SET[ProductName] = '" + textBox2.Text + "',[ProductStatus] ='" + status + "' WHERE <[ProductCode] = '" + textBox1.Text + "',";
                //sqlQuery = @"UPDATE [dbo].[Products]  SET [ProductName] = '" + textBox2.Text + "',[ProductStatus] = '" + status + "'WHERE [ProductCode] = '" + textBox1.Text + "'";

            }
            else 
            {
                sqlQuery= @"INSERT INTO [dbo].[Products] ([ProductCode] ,[ProductName],[ProductStatus])
            VALUES
           ('" + textBox1.Text + "', '" + textBox2.Text + "','" + status + "')";


            }
                SqlDataAdapter sda = new SqlDataAdapter("Select * From [dbo].[Products] ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();
            con.Close();

            //Reading Data
            LoadData();
        }
        private bool ifProductExists(SqlConnection con, string productCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select 1 From [Products] WHERE [ProductCode] =  '" + productCode + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;

        }

        public void LoadData()
        {
            SqlConnection con = Connection.GetConnection();
            SqlDataAdapter sda = new SqlDataAdapter("Select * From [dbo].[Products] ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            //before loading the rows the database must be cleared
            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();


                if ((bool)item["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Deactive";

                }


            }
        }

 

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Active")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;

            }
            
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = Connection.GetConnection();

            var sqlQuery = "";
            //checking if the record exists
            if (ifProductExists(con, textBox1.Text))
            {
                con.Open();
                sqlQuery = @"DELETE FROM  WHERE [ProductCode] = '" + textBox1.Text + "',";
                //sqlQuery = @"UPDATE [dbo].[Products]  SET [ProductName] = '" + textBox2.Text + "',[ProductStatus] = '" + status + "'WHERE [ProductCode] = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {

                MessageBox.Show("Record does not exist");
            }
            
            

            //Reading Data
            LoadData();
        }
    }
    }

