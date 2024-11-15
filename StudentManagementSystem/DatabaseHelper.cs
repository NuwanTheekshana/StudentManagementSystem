using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem
{
    internal class DatabaseHelper
    {
        private static readonly string connectionString = "Data Source=IT-NBNUWAN\\SQLEXPRESS;Initial Catalog=ticketing_system_db;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
