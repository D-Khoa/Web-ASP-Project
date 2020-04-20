using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WepAPI.Models.Library;

namespace WepAPI.Models.User
{
    public class UserLogRespository : IBaseCRUDRespository<UserLog>
    {
        /// <summary>
        /// Call SQL base
        /// </summary>
        private static MESSQL mESSQL;
        /// <summary>
        /// List user
        /// </summary>
        private List<UserLog> Users { get; set; }
        /// <summary>
        /// User log respository
        /// </summary>
        public UserLogRespository()
        {
            mESSQL = new MESSQL();
        }
        /// <summary>
        /// User log respository with custom connectionstring
        /// </summary>
        /// <param name="connectionString"></param>
        public UserLogRespository(string connectionString)
        {
            mESSQL = new MESSQL(connectionString);
        }
        /// <summary>
        /// Create new user user log
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserLog Create(UserLog user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            string query = "INSERT INTO dbo.UserLog ";
            query += string.Format("(user_cd, user_password) VALUES ('{0}','{1}')", user.user_cd, user.user_password);
            mESSQL.OpenConnect();
            int result = mESSQL.DbCommand(query).ExecuteNonQuery();
            mESSQL.CloseConnect();
            if (result > 0) return user;
            else throw new Exception("Can't create new user!");
        }
        /// <summary>
        /// Get user log by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserLog Get(UserLog user)
        {
            Users = GetAll().ToList();
            if (user.userlog_id > 0)
                return Users.Find(x => x.userlog_id == user.userlog_id);
            else
                return Users.Find(x => x.user_cd == user.user_cd);
        }
        /// <summary>
        /// Get all user log
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserLog> GetAll()
        {
            Users = new List<UserLog>();
            string query = "SELECT userlog_id, user_cd, user_active, user_online, last_ipadd, last_login, reg_date ";
            query += "FROM dbo.UserLog";
            mESSQL.OpenConnect();
            IDataReader reader = mESSQL.DbCommand(query).ExecuteReader();
            while (reader.Read())
            {
                Users.Add(new UserLog()
                {
                    userlog_id = (int)reader["userlog_id"],
                    user_cd = reader["user_cd"].ToString(),
                    user_active = (bool)reader["user_active"],
                    user_online = (bool)reader["user_online"],
                    last_ipadd = reader["last_ipadd"].ToString(),
                    last_login = (DateTime)reader["last_login"],
                    reg_date = (DateTime)reader["reg_date"]
                });
            }
            reader.Close();
            mESSQL.CloseConnect();
            return Users;
        }
        /// <summary>
        /// Remove an user
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            if (id > 0)
            {
                string query = string.Format("DELETE dbo.UserLog WHERE userlog_id = '{0}'", id);
                mESSQL.OpenConnect();
                int result = mESSQL.DbCommand(query).ExecuteNonQuery();
                mESSQL.CloseConnect();
                if (result <= 0)
                    throw new Exception("Can't remove this user!");
            }
            else
                throw new Exception("This user isn't exist!");
        }
        /// <summary>
        /// Update an user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Update(UserLog user)
        {
            if (user == null || user.userlog_id == 0)
                throw new ArgumentNullException("user");
            user.user_password = user.user_password.Encrypt();
            string query = "UPDATE dbo.UserLog SET ";
            query += string.Format("user_cd = '{0}', user_password = '{1}', user_active = '{2}', user_online = '{3}'" +
                                   ", last_ipadd = '{4}', last_login = '{5}'",
                                   user.user_cd, user.user_password, user.user_active, user.user_online,
                                   user.last_ipadd, user.last_login);
            query += string.Format("WHERE userlog_id = '{0}'", user.userlog_id);
            mESSQL.OpenConnect();
            int result = mESSQL.DbCommand(query).ExecuteNonQuery();
            mESSQL.CloseConnect();
            if (result > 0) return true;
            else throw new Exception("Can't update this user!");
        }

        public IEnumerable<UserLog> GetList(UserLog item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Login/logout
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isLogin"></param>
        //public bool LogIO(UserLog user, bool isLogin)
        //{
        //    user.user_password = user.user_password.Encrypt();
        //    string query = "SELECT userlog_id, user_cd, user_active, user_online, last_ipadd, last_login, reg_date ";
        //    query += string.Format("FROM dbo.UserLog WHERE user_cd = '{0}' AND user_password = '{1}'",
        //                            user.user_cd, user.user_password);
        //    mESSQL.OpenConnect();
        //    try
        //    {
        //        IDataReader reader = mESSQL.DbCommand(query).ExecuteReader();
        //        reader.Read();
        //        user = new UserLog()
        //        {
        //            userlog_id = (int)reader["userlog_id"],
        //            user_cd = reader["user_cd"].ToString(),
        //            user_active = (bool)reader["user_active"],
        //            user_online = (bool)reader["user_online"],
        //            last_ipadd = reader["last_ipadd"].ToString(),
        //            last_login = (DateTime)reader["last_login"],
        //            reg_date = (DateTime)reader["reg_date"]
        //        };
        //        reader.Close();
        //    }
        //    catch
        //    {
        //        throw new Exception("Invalid user code or password!");
        //    }
        //    if (user.user_active)
        //        throw new Exception("This user hasn't been actived!");
        //    if (user.user_online == isLogin)
        //        throw new Exception("This user is online!");
        //    user.user_online = isLogin;
        //    if (isLogin) user.last_login = DateTime.Now;
        //    Update(user);
        //    mESSQL.CloseConnect();
        //    return true;
        //}
    }
}