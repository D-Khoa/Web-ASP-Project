using System.Data;
using MySql.Data.MySqlClient;

namespace WebAppNo1.Models
{
    /// <summary>
    /// CONNECT MYSQL CLASS
    /// </summary>
    public class MySQLDao
    {
        MySqlConnection connection;
        private string ConnectionString { get; set; }
        private string server = "localhost";
        private string database = "maindb";
        private string uid = "root";
        private string password = "4865awdS";
        public MySQLDao()
        {
            ConnectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";"
                             + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(ConnectionString);
        }

        /// <summary>
        /// Open connection
        /// </summary>
        public void OpenConnection()
        {
            connection.Open();
        }

        /// <summary>
        /// Close connection
        /// </summary>
        public void CloseConnection()
        {
            connection.Dispose();
            connection.Close();
        }

        /// <summary>
        /// SQL command
        /// </summary>
        /// <param name="query">query string</param>
        /// <returns></returns>
        public IDbCommand Command(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            return command;
        }

        /// <summary>
        /// SQL adapter
        /// </summary>
        /// <param name="query">query string</param>
        /// <returns></returns>
        public IDataAdapter Adapter(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            return adapter;
        }
    }
}