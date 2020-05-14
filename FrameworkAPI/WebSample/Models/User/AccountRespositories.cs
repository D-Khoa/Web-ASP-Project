using System;
using System.Collections.Generic;
using System.Data;
using WebSample.Models.Interface;
using WebSample.Models.Library;

namespace WebSample.Models.User
{
    public class AccountRespositories : IBaseCRUDRespositories<UserLog>
    {
        public MESSQL mesSQL()
        {
            return new MESSQL();
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="inputItem"></param>
        /// <returns></returns>
        public UserLog Create(UserLog inputItem)
        {
            if (inputItem == null || string.IsNullOrEmpty(inputItem.uid))
                throw new ArgumentNullException("User is not invaild!");
            string query = "INSERT INTO [dbo].[tbl_userlog] ";
            query += string.Format("([uid], [username], [password]) VALUES ('{0}','{1}','{2}')",
                                  inputItem.uid, inputItem.username, inputItem.password);
            mesSQL().OpenConnect();
            int result = mesSQL().DbCommand(query).ExecuteNonQuery();
            mesSQL().CloseConnect();
            if (result > 0) return inputItem;
            else throw new Exception("Can't create new user!");
        }

        /// <summary>
        /// Get all user log info
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserLog> GetAll()
        {
            List<UserLog> listUser = new List<UserLog>();
            string query = "SELECT [uid],[username],[password],[utoken],[last_login],[reg_date],[user_active],[user_online] ";
            query += "FROM [dbo].[tbl_userlog] ";
            mesSQL().OpenConnect();
            IDataReader reader = mesSQL().DbCommand(query).ExecuteReader();
            while (reader.Read())
            {
                listUser.Add(new UserLog()
                {
                    uid = reader["uid"].ToString(),
                    username = reader["username"].ToString(),
                    lastLogin = (DateTime)reader["last_login"],
                    regDate = (DateTime)reader["reg_date"],
                    userActive = (bool)reader["user_active"],
                    userOnline = (bool)reader["user_online"]
                });
            }
            reader.Close();
            mesSQL().CloseConnect();
            return listUser;
        }

        /// <summary>
        /// Get list user with input info
        /// </summary>
        /// <param name="inputItemInfo"></param>
        /// <returns></returns>
        public IEnumerable<UserLog> GetCollection(UserLog inputItemInfo)
        {
            List<UserLog> listUser = new List<UserLog>();
            string query = "SELECT [uid],[username],[password],[utoken],[last_login],[reg_date],[user_active],[user_online] ";
            query += "FROM [dbo].[tbl_userlog] WHERE 1=1 ";
            if (string.IsNullOrEmpty(inputItemInfo.uid))
                query += string.Format("AND [uid] = '{0}' ", inputItemInfo.uid);
            if (string.IsNullOrEmpty(inputItemInfo.username))
                query += string.Format("AND [username] = '{0}' ", inputItemInfo.username);
            mesSQL().OpenConnect();
            IDataReader reader = mesSQL().DbCommand(query).ExecuteReader();
            while (reader.Read())
            {
                listUser.Add(new UserLog()
                {
                    uid = reader["uid"].ToString(),
                    username = reader["username"].ToString(),
                    lastLogin = (DateTime)reader["last_login"],
                    regDate = (DateTime)reader["reg_date"],
                    userActive = (bool)reader["user_active"],
                    userOnline = (bool)reader["user_online"]
                });
            }
            reader.Close();
            mesSQL().CloseConnect();
            return listUser;
        }

        /// <summary>
        /// Get infomation of user
        /// </summary>
        /// <param name="inputItemInfo"></param>
        /// <returns></returns>
        public UserLog GetItem(UserLog inputItemInfo)
        {
            try
            {
                string query = "SELECT [uid],[username],[password],[utoken],[last_login],[reg_date],[user_active],[user_online] ";
                query += string.Format("FROM [dbo].[tbl_userlog] WHERE [username] = '{0}' AND [password] = '{1}'",
                                        inputItemInfo.username, inputItemInfo.password);
                mesSQL().OpenConnect();
                IDataReader reader = mesSQL().DbCommand(query).ExecuteReader();
                reader.Read();
                UserLog outputUser = new UserLog()
                {
                    uid = reader["uid"].ToString(),
                    username = reader["username"].ToString(),
                    lastLogin = (DateTime)reader["last_login"],
                    regDate = (DateTime)reader["reg_date"],
                    userActive = (bool)reader["user_active"],
                    userOnline = (bool)reader["user_online"]
                };
                reader.Close();
                return outputUser;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                throw new Exception("Username and password not match!");
            }
        }

        public void Remove(int inputItemID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete user with uid
        /// </summary>
        /// <param name="inputItemUID"></param>
        public void Remove(string inputItemUID)
        {
            if (!string.IsNullOrEmpty(inputItemUID))
            {
                string query = string.Format("DELETE [dbo].[tbl_userlog] WHERE [uid] = '{0}'", inputItemUID);
                mesSQL().OpenConnect();
                int result = mesSQL().DbCommand(query).ExecuteNonQuery();
                mesSQL().CloseConnect();
                if (result <= 0)
                    throw new Exception("Can't remove this user!");
            }
            else
                throw new Exception("This user isn't exist!");
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="inputItem"></param>
        /// <returns></returns>
        public bool Update(UserLog inputItem)
        {
            if (inputItem == null || string.IsNullOrEmpty(inputItem.uid))
                throw new ArgumentNullException("User is not invaild!");
            string query = "UPDATE [dbo].[tbl_userlog] SET ";
            query += string.Format("[username] = '{0}',[utoken] = '{1}',[last_login] = '{2}',[user_active] = '{3}',[user_online] = '{4}'", inputItem.username, inputItem.userToken, inputItem.lastLogin, inputItem.userActive, inputItem.userOnline);
            query += string.Format("WHERE [uid] = '{0}'", inputItem.uid);
            mesSQL().OpenConnect();
            int result = mesSQL().DbCommand(query).ExecuteNonQuery();
            mesSQL().CloseConnect();
            if (result > 0) return true;
            else throw new Exception("Can't update this user!");
        }
    }
}