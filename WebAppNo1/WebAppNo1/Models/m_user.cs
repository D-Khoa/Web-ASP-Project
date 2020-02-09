using System;
using System.Collections.Generic;
using System.Data;

namespace WebAppNo1.Models
{
    /// <summary>
    /// USER INFO
    /// </summary>
    public class m_user
    {
        #region ALL FIELDS
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public DateTime reg_date { get; set; }
        public bool is_active { get; set; }
        public List<m_user> listUser { get; set; }
        public m_user()
        {
            listUser = new List<m_user>();
        }
        #endregion

        #region QUERY DATABASE
        /// <summary>
        /// Get user info when login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public m_user LoginUser(string username, string pass)
        {
            MySQLDao SQL = new MySQLDao();
            string query = string.Empty;
            query = "SELECT `m_user`.`user_id`,`m_user`.`user_name`,`m_user`.`password`,`m_user`.`email`,`m_user`.`phone`,";
            query += "`m_user`.`reg_date`,`m_user`.`is_active` FROM `maindb`.`m_user` ";
            query += "WHERE `m_user`.`user_name` ='" + username + "' ";
            query += "AND `m_user`.`password` ='" + pass + "'";
            SQL.OpenConnection();
            IDataReader reader = SQL.Command(query).ExecuteReader();
            reader.Read();
            m_user outItem = new m_user
            {
                user_id = (int)reader["user_id"],
                user_name = reader["user_name"].ToString(),
                password = reader["password"].ToString(),
                email = reader["email"].ToString(),
                phone = reader["phone"].ToString(),
                reg_date = (DateTime)reader["reg_date"],
                is_active = reader.GetBoolean(reader.GetOrdinal("is_active")),
            };
            reader.Close();
            SQL.CloseConnection();
            query = string.Empty;
            return outItem;
        }

        /// <summary>
        /// Update state of user when login or log out
        /// </summary>
        /// <param name="isActive">true: login, false: log out</param>
        public void UpdateUserState(bool isActive)
        {
            MySQLDao SQL = new MySQLDao();
            string query = string.Empty;
            SQL.OpenConnection();
            query = "UPDATE `maindb`.`m_user` SET `is_active` = '" + isActive + "' WHERE `user_id` ='" + user_id + "'";
            int result = SQL.Command(query).ExecuteNonQuery();
            SQL.CloseConnection();
            query = string.Empty;
        }

        /// <summary>
        /// Get user info in database into current user list
        /// </summary>
        /// <param name="user">input user info need search (id: 0 if skip, name, email, phone)</param>
        /// <param name="checkState">true: check state of user</param>
        public void SearchUser(m_user user, bool checkState)
        {
            MySQLDao SQL = new MySQLDao();
            string query = string.Empty;
            listUser = new List<m_user>();
            query = "SELECT `m_user`.`user_id`,`m_user`.`user_name`,`m_user`.`password`,`m_user`.`email`,";
            query += @"`m_user`.`phone`,`m_user`.`reg_date`,`m_user`.`is_active` ";
            query += "FROM `maindb`.`m_user` WHERE 1=1 ";
            if (user.user_id > 0)
                query += "AND `user_id` ='" + user.user_id + "' ";
            if (!string.IsNullOrEmpty(user.user_name))
                query += "AND `user_name` ='" + user.user_name + "' ";
            if (!string.IsNullOrEmpty(user.email))
                query += "AND `email` ='" + user.email + "' ";
            if (!string.IsNullOrEmpty(user.phone))
                query += "AND `phone` ='" + user.phone + "' ";
            if (checkState)
                query += "AND `is_active` ='" + user.is_active + "' ";
            SQL.OpenConnection();
            IDataReader reader = SQL.Command(query).ExecuteReader();
            while (reader.Read())
            {
                m_user outItem = new m_user()
                {
                    user_id = (int)reader["user_id"],
                    user_name = reader["user_name"].ToString(),
                    password = reader["password"].ToString(),
                    email = reader["email"].ToString(),
                    phone = reader["phone"].ToString(),
                    reg_date = (DateTime)reader["reg_date"],
                    is_active = reader.GetBoolean(reader.GetOrdinal("is_active")),
                };
                listUser.Add(outItem);
            }
            reader.Close();
            SQL.CloseConnection();
            query = string.Empty;
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="inItem">input user (username, pass, email, phone)</param>
        /// <returns></returns>
        public int AddUser(m_user inItem)
        {
            MySQLDao SQL = new MySQLDao();
            string query = string.Empty;
            SQL.OpenConnection();
            query = "INSERT INTO `maindb`.`m_user`(`user_name`,`password`,`email`,`phone`)";
            query += "VALUES('" + inItem.user_name + "','" + inItem.password + "','" + inItem.email + "','";
            query += inItem.phone + "')";
            int result = SQL.Command(query).ExecuteNonQuery();
            SQL.CloseConnection();
            query = string.Empty;
            return result;
        }

        /// <summary>
        /// Update an user
        /// </summary>
        /// <param name="inItem">input user (user id, user_name, password, email, phone, reg_date)</param>
        /// <returns></returns>
        public int UpdateUser(m_user inItem)
        {
            MySQLDao SQL = new MySQLDao();
            string query = string.Empty;
            SQL.OpenConnection();
            query = "UPDATE `maindb`.`m_user` SET `user_name` = '" + inItem.user_name + "', `password`='" + inItem.password;
            query += "',`email`='" + inItem.email + "',`phone`='" + inItem.phone + "',`reg_date` ='" + DateTime.Now + "' ";
            query += "WHERE `user_id` ='" + user_id + "'";
            int result = SQL.Command(query).ExecuteNonQuery();
            SQL.CloseConnection();
            query = string.Empty;
            return result;
        }

        /// <summary>
        /// Delete current user
        /// </summary>
        /// <returns></returns>
        public int DeleteUser()
        {
            MySQLDao SQL = new MySQLDao();
            string query = string.Empty;
            SQL.OpenConnection();
            query = "DELETE FROM `maindb`.`m_user` WHERE `user_id` ='" + user_id + "'";
            int result = SQL.Command(query).ExecuteNonQuery();
            SQL.CloseConnection();
            query = string.Empty;
            return result;
        }
        #endregion
    }
}