using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace WebAppNo1.Models
{
    public class m_user
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public DateTime reg_date { get; set; }
        public bool is_active { get; set; }

        //public string ConnectionString { get; set; }
        //public m_user(string connectionString)
        //{
        //    this.ConnectionString = connectionString;
        //}

        //private MySqlConnection GetConnection()
        //{
        //    return new MySqlConnection(ConnectionString);
        //}
    }
}