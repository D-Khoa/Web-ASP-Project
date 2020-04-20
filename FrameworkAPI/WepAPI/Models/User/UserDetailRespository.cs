using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WepAPI.Models.Library;

namespace WepAPI.Models.User
{
    public class UserDetailRespository : IBaseCRUDRespository<UserDetail>
    {
        /// <summary>
        /// Call SQL base
        /// </summary>
        private static MESSQL mESSQL;
        /// <summary>
        /// List users
        /// </summary>
        private List<UserDetail> Users { get; set; }
        /// <summary>
        /// User detail respository
        /// </summary>
        public UserDetailRespository()
        {
            mESSQL = new MESSQL();
        }
        /// <summary>
        /// User detail repository with custom connectionstring
        /// </summary>
        /// <param name="connectionString"></param>
        public UserDetailRespository(string connectionString)
        {
            mESSQL = new MESSQL(connectionString);
        }
        /// <summary>
        /// Get list all user
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserDetail> GetAll()
        {
            Users = new List<UserDetail>();
            string query = "SELECT * FROM dbo.UserDetail";
            mESSQL.OpenConnect();
            IDataReader reader = mESSQL.DbCommand(query).ExecuteReader();
            while (reader.Read())
            {
                Users.Add(new UserDetail()
                {
                    user_id = (int)reader["user_id"],
                    user_cd = reader["user_cd"].ToString(),
                    user_name = reader["user_name"].ToString(),
                    user_position = reader["user_position"].ToString(),
                    user_dept = reader["user_dept"].ToString(),
                    user_email = reader["user_email"].ToString(),
                    user_tel = reader["user_tel"].ToString(),
                    factory_cd = reader["factory_cd"].ToString(),
                    start_date = (DateTime)reader["start_date"],
                    reg_date = (DateTime)reader["reg_date"]
                });
            }
            reader.Close();
            mESSQL.CloseConnect();
            return Users;
        }
        /// <summary>
        /// Get list user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<UserDetail> GetList(UserDetail user)
        {
            Users = new List<UserDetail>();
            string query = "SELECT * FROM dbo.UserDetail WHERE 1=1 ";
            if (!string.IsNullOrEmpty(user.user_position))
                query += string.Format("AND user_position='{0}' ", user.user_position);
            if (!string.IsNullOrEmpty(user.user_dept))
                query += string.Format("AND user_dept='{0}' ", user.user_dept);
            if (!string.IsNullOrEmpty(user.factory_cd))
                query += string.Format("AND factory_cd='{0}' ", user.factory_cd);
            mESSQL.OpenConnect();
            IDataReader reader = mESSQL.DbCommand(query).ExecuteReader();
            while (reader.Read())
            {
                Users.Add(new UserDetail()
                {
                    user_id = (int)reader["user_id"],
                    user_cd = reader["user_cd"].ToString(),
                    user_name = reader["user_name"].ToString(),
                    user_position = reader["user_position"].ToString(),
                    user_dept = reader["user_dept"].ToString(),
                    user_email = reader["user_email"].ToString(),
                    user_tel = reader["user_tel"].ToString(),
                    factory_cd = reader["factory_cd"].ToString(),
                    start_date = (DateTime)reader["start_date"],
                    reg_date = (DateTime)reader["reg_date"]
                });
            }
            reader.Close();
            mESSQL.CloseConnect();
            return Users;
        }
        /// <summary>
        /// Get an user with id
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserDetail Get(UserDetail user)
        {
            Users = GetAll().ToList();
            if (user.user_id > 0)
                return Users.Find(x => x.user_id == user.user_id);
            else
                return Users.Find(x => x.user_cd == user.user_cd);
        }
        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserDetail Create(UserDetail user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            string query = "INSERT INTO dbo.UserDetail ";
            query += "(user_cd, user_name, user_position, user_dept, user_email, user_tel, factory_cd, start_date) VALUES ";
            query += string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                                    user.user_cd, user.user_name, user.user_position, user.user_dept, user.user_email,
                                    user.user_tel, user.factory_cd, user.start_date);
            mESSQL.OpenConnect();
            int result = mESSQL.DbCommand(query).ExecuteNonQuery();
            mESSQL.CloseConnect();
            if (result > 0) return user;
            else throw new Exception("Can't create new user!");
        }
        /// <summary>
        /// Update an user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Update(UserDetail user)
        {
            if (user == null || user.user_id == 0)
                throw new ArgumentNullException("user");
            string query = "UPDATE dbo.UserDetail SET ";
            query += string.Format("user_cd = '{0}', user_name = '{1}', user_position = '{2}', user_dept = '{3}'" +
                                   ", user_email = '{4}', user_tel = '{5}', factory_cd = '{6}', start_date = '{7}' ",
                                   user.user_cd, user.user_name, user.user_position, user.user_dept, user.user_email,
                                   user.user_tel, user.factory_cd, user.start_date);
            query += string.Format("WHERE user_id = '{0}'", user.user_id);
            mESSQL.OpenConnect();
            int result = mESSQL.DbCommand(query).ExecuteNonQuery();
            mESSQL.CloseConnect();
            if (result > 0) return true;
            else throw new Exception("Can't update this user!");
        }
        /// <summary>
        /// Remove an user with id
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            if (id > 0)
            {
                string query = string.Format("DELETE dbo.UserDetail WHERE user_id = '{0}'", id);
                mESSQL.OpenConnect();
                int result = mESSQL.DbCommand(query).ExecuteNonQuery();
                mESSQL.CloseConnect();
                if (result <= 0)
                    throw new Exception("Can't remove this user!");
            }
            else
                throw new Exception("This user isn't exist!");
        }
    }
}