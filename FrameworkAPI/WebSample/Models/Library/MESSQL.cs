using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebSample.Models.Library
{
    public class MESSQL
    {
        public string ConnectionString { get; set; }
        private SqlConnection _dbContext;
        /// <summary>
        /// Call and set connectionstring for SQL connection
        /// </summary>
        /// <param name="connectionstring"></param>
        public MESSQL()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
            _dbContext = new SqlConnection(ConnectionString);
        }
        /// <summary>
        /// Call and set connectionstring for SQL connection
        /// </summary>
        /// <param name="connectionstring">custom connection string</param>
        public MESSQL(string connectionstring)
        {
            ConnectionString = connectionstring;
            _dbContext = new SqlConnection(ConnectionString);
        }
        /// <summary>
        /// Get database connection
        /// </summary>
        /// <returns></returns>
        public IDbConnection DbContext()
        {
            return _dbContext;
        }
        /// <summary>
        /// Open database connection
        /// </summary>
        public void OpenConnect()
        {
            if (_dbContext.State != ConnectionState.Open)
                _dbContext.Open();
        }
        /// <summary>
        /// Close database connection
        /// </summary>
        public void CloseConnect()
        {
            _dbContext.Close();
        }
        /// <summary>
        /// Set SQL command without transaction
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public IDbCommand DbCommand(string cmdText)
        {
            SqlCommand sqlCommand = new SqlCommand(cmdText, _dbContext);
            return sqlCommand;
        }
        /// <summary>
        /// Set SQL command with transaction
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        public IDbCommand DbCommand(string cmdText, IDbTransaction dbTransaction)
        {
            SqlCommand sqlCommand = new SqlCommand(cmdText, _dbContext, (SqlTransaction)dbTransaction);
            return sqlCommand;
        }
    }
}