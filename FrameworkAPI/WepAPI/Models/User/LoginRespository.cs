using System;
using System.Collections.Generic;
using System.Data;
using WepAPI.Models.Library;

namespace WepAPI.Models.User
{
    public class LoginRespository : IBaseCRUDRespository<UserLog>
    {
        /// <summary>
        /// Call SQL base
        /// </summary>
        private static MESSQL mESSQL;
        public LoginRespository()
        {
            mESSQL = new MESSQL();
        }
        public LoginRespository(string connectionString)
        {
            mESSQL = new MESSQL(connectionString);
        }

        public UserLog Create(UserLog user)
        {
            throw new NotImplementedException();
        }

        public UserLog Get(UserLog user)
        {
            try
            {
                string query = "SELECT userlog_id, user_cd, user_active, user_online, last_ipadd, last_login, reg_date ";
                query += string.Format("FROM dbo.UserLog WHERE user_cd = '{0}' AND user_password = '{1}'",
                                        user.user_cd, user.user_password);
                mESSQL.OpenConnect();
                IDataReader reader = mESSQL.DbCommand(query).ExecuteReader();
                reader.Read();
                user = new UserLog()
                {
                    userlog_id = (int)reader["userlog_id"],
                    user_cd = reader["user_cd"].ToString(),
                    user_active = (bool)reader["user_active"],
                    user_online = (bool)reader["user_online"],
                    last_ipadd = reader["last_ipadd"].ToString(),
                    last_login = (DateTime)reader["last_login"],
                    reg_date = (DateTime)reader["reg_date"]
                };
                reader.Close();
                List<string> userRoles = new List<string>();
                query = "SELECT [control_cd] FROM [dbo].[MesControl] WHERE[parent_cd] in ";
                query += "(SELECT[control_cd] FROM[dbo].[ControlRoles] WHERE[role_cd] in ";
                query += string.Format("(SELECT[role_cd]  FROM[dbo].[UserRoles]  WHERE[user_cd] = '{0}'))", user.user_cd);
                reader = mESSQL.DbCommand(query).ExecuteReader();
                while (reader.Read())
                {
                    userRoles.Add(reader["control_cd"].ToString());
                }
                reader.Close();
                mESSQL.CloseConnect();
                user.userRoles = string.Join(",", userRoles.ToArray());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                throw new Exception("Invalid user or password!");
            }
            if (user.user_active) return user;
            else throw new Exception("This user hasn't been actived!");
        }

        public IEnumerable<UserLog> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserLog> GetList(UserLog item)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(UserLog user)
        {
            if (user == null || user.userlog_id == 0)
                throw new ArgumentNullException("user");
            string query = "UPDATE dbo.UserLog SET ";
            query += string.Format("user_cd = '{0}', user_active = '{1}', user_online = '{2}'" +
                                   ", last_ipadd = '{3}', last_login = '{4}'",
                                   user.user_cd, user.user_active, user.user_online,
                                   user.last_ipadd, user.last_login);
            query += string.Format("WHERE userlog_id = '{0}'", user.userlog_id);
            mESSQL.OpenConnect();
            int result = mESSQL.DbCommand(query).ExecuteNonQuery();
            mESSQL.CloseConnect();
            if (result > 0) return true;
            else throw new Exception("Can't update this user!");
        }
    }
}