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

namespace HowToConnectToaDB_basics
{
    public partial class ConnectToAdb_Window : Form
    {
        string string ConStr =
            "Server=DatabaseSRFN.mssql.somee.com;" +
                "Database=DatabaseSRFN;" +
                "User Id=serafin;" +
                "Password = 19771977; ";
        public ConnectToAdb_Window()
        {
            InitializeComponent();
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection();
            sqlcon.ConnectionString = ConStr;
            try
            {
                sqlcon.Open();
                //this part changes accordingly
                //Select
                //update
                //insert
                //delete

                //Step 1: Define the query
                string query = "SELECT * FROM Contact ORDER BY LastName";

                //Step 2: Create and inizializate the command object 
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandText = query;
                myCommand.Connection = sqlcon;
                //Step 3: Create datatable that will contain the data
                DataTable myTable = new DataTable();
                myTable.Columns.Add("ID");
                myTable.Columns.Add(new DataColumn("FirstName"));
                myTable.Columns.Add(new DataColumn("LastName"));
                myTable.Columns.Add(new DataColumn("ID"));
                myTable.Columns.Add(new DataColumn("Phone"));
                myTable.Columns.Add(new DataColumn("Email"));

                //Step 4: Fill in the datatable with information from the DataBase
                SqlDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    DataRow contact = myTable.NewRow();
                    contact["ID"] = myReader["ID"];
                    contact["FirstName"] = myReader["FirstName"];
                    contact["LastName"] = myReader["LastName"];
                    contact["Phone"] = myReader["Phone"];
                    contact["Email"] = myReader["Email"];
                    myTable.Rows.Add(contact);
                }

                //Step 5: Bind datatable to datagridview
                //IMPORTANTTTTTTTTTTTTTTTTTTTTTTT
                dgContacts.DataSource = myTable;

            }
            catch (Exception exc123)
            {
                MessageBox.Show(exc123.Message);
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
        }
    }
}
