using System;
using System.Collections.Generic;
using System.Data;

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
        public List<m_user> listUser { get; set; }

        public m_user()
        {
            listUser = new List<m_user>();
        }

        public void GetAllUser()
        {
            MySQLDao SQL = new MySQLDao();
            string query = string.Empty;
            listUser = new List<m_user>();
            query = @"SELECT `m_user`.`user_id`,`m_user`.`user_name`,`m_user`.`password`,`m_user`.`email`,`m_user`.`phone`,
                             `m_user`.`reg_date`,`m_user`.`is_active` FROM `maindb`.`m_user`;";
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

        public m_user LoginUser(string username, string pass)
        {
            MySQLDao SQL = new MySQLDao();
            string query = string.Empty;
            query = @"SELECT `m_user`.`user_id`,`m_user`.`user_name`,`m_user`.`password`,`m_user`.`email`,`m_user`.`phone`,
                             `m_user`.`reg_date`,`m_user`.`is_active` FROM `maindb`.`m_user`;";
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
    }
}