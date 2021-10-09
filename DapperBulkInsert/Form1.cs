using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace DapperBulkInsert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Location");

            dt.Rows.Add("Mark", "London");
            dt.Rows.Add("John", "NY");
            dt.Rows.Add("James", "Paris");

            using (IDbConnection conn = Connection)
            {
                conn.Execute("InsertUsers", new { users = dt.AsTableValuedParameter("UserTableType") }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
