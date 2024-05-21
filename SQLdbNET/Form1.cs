using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


namespace SQLdbNET
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection=null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString);
            sqlConnection.Open();
            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Вы подключены. Все ОК.", "Connection");
            }
            else
            {
                MessageBox.Show("ERROR", "Connection",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            textBox7.Text = "SELECT ProductName, UnitPrice FROM Products";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(
               textBox7.Text, sqlConnection
               );
                DataSet ds = new DataSet();
                sqlDataAdapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Что-то пошло не так!", MessageBoxButtons.OK,MessageBoxIcon.Error);  
            }
           


        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            SqlDataReader dataReader = null;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT ProductName, QuantityPerUnit, UnitPrice FROM Products", sqlConnection);
                
                dataReader= cmd.ExecuteReader();
                ListViewItem item = null;
                while (dataReader.Read())
                {
                   item = new ListViewItem(new string[] 
                   {    Convert.ToString(dataReader["ProductName"]),
                        Convert.ToString(dataReader["QuantityPerUnit"]),
                        Convert.ToString(dataReader["UnitPrice"]),
                   });
                    listView1.Items.Add(item);
                }
            
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dataReader!=null && !dataReader.IsClosed)
                {
                    dataReader.Close();
                }
            }

        }
    }
}
