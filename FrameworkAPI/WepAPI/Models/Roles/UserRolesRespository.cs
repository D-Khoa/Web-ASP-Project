using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using WepAPI.Models.Library;

namespace WepAPI.Models.Roles
{
    public class UserRolesRespository : IBaseCRUDRespository<UserRoles>
    {
        /// <summary>
        /// Call SQL base
        /// </summary>
        private static MESSQL mESSQL;
        /// <summary>
        /// List users
        /// </summary>
        private List<UserRoles> roles { get; set; }
        /// <summary>
        /// Control respository
        /// </summary>
        public UserRolesRespository()
        {
            mESSQL = new MESSQL();
        }
        /// <summary>
        /// Control repository with custom connectionstring
        /// </summary>
        /// <param name="connectionString"></param>
        public UserRolesRespository(string connectionString)
        {
            mESSQL = new MESSQL(connectionString);
        }

        public UserRoles Create(UserRoles item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            string query = "INSERT INTO dbo.UserRoles ";
            query += "(user_cd, role_cd, reg_user) VALUES ";
            query += string.Format("('{0}','{1}','{2}')", item.user_cd, item.role_cd, item.reg_user);
            mESSQL.OpenConnect();
            int result = mESSQL.DbCommand(query).ExecuteNonQuery();
            mESSQL.CloseConnect();
            if (result > 0) return item;
            else throw new Exception("Can't create new role!");
        }

        public UserRoles Get(UserRoles item)
        {
            roles = GetAll().ToList();
            if (item.user_role_id > 0)
                return roles.Find(x => x.user_role_id == item.user_role_id);
            else
                throw new ArgumentNullException("item");
        }

        public IEnumerable<UserRoles> GetAll()
        {
            roles = new List<UserRoles>();
            string query = "SELECT * FROM dbo.UserRoles";
            mESSQL.OpenConnect();
            IDataReader reader = mESSQL.DbCommand(query).ExecuteReader();
            while (reader.Read())
            {
                roles.Add(new UserRoles()
                {
                    user_role_id = (int)reader["user_role_id"],
                    user_cd = reader["user_cd"].ToString(),
                    role_cd = reader["role_cd"].ToString(),
                    reg_user = reader["reg_user"].ToString(),
                    reg_date = (DateTime)reader["reg_date"]
                });
            }
            reader.Close();
            mESSQL.CloseConnect();
            return roles;
        }

        public IEnumerable<UserRoles> GetList(UserRoles item)
        {
            roles = new List<UserRoles>();
            string query = "SELECT * FROM dbo.UserRoles WHERE 1=1 ";
            if (!string.IsNullOrEmpty(item.user_cd))
                query += string.Format("AND user_cd='{0}' ", item.user_cd);
            if (!string.IsNullOrEmpty(item.role_cd))
                query += string.Format("AND role_cd='{0}' ", item.role_cd);
            if (!string.IsNullOrEmpty(item.reg_user))
                query += string.Format("AND reg_user='{0}' ", item.reg_user);
            mESSQL.OpenConnect();
            IDataReader reader = mESSQL.DbCommand(query).ExecuteReader();
            while (reader.Read())
            {
                roles.Add(new UserRoles()
                {
                    user_role_id = (int)reader["user_role_id"],
                    user_cd = reader["user_cd"].ToString(),
                    role_cd = reader["role_cd"].ToString(),
                    reg_user = reader["reg_user"].ToString(),
                    reg_date = (DateTime)reader["reg_date"]
                });
            }
            reader.Close();
            mESSQL.CloseConnect();
            return roles;
        }

        public void Remove(int id)
        {
            if (id > 0)
            {
                string query = string.Format("DELETE dbo.UserRoles WHERE user_role_id = '{0}'", id);
                mESSQL.OpenConnect();
                int result = mESSQL.DbCommand(query).ExecuteNonQuery();
                mESSQL.CloseConnect();
                if (result <= 0)
                    throw new Exception("Can't remove this role!");
            }
            else
                throw new Exception("This role isn't exist!");
        }

        public bool Update(UserRoles item)
        {
            if (item == null || item.user_role_id == 0)
                throw new ArgumentNullException("item");
            string query = "UPDATE dbo.UserRoles SET ";
            query += string.Format("user_cd = '{0}', role_cd = '{1}', reg_user = '{2}' ",
                                   item.user_cd, item.role_cd, item.reg_user);
            query += string.Format("WHERE user_id = '{0}'", item.user_role_id);
            mESSQL.OpenConnect();
            int result = mESSQL.DbCommand(query).ExecuteNonQuery();
            mESSQL.CloseConnect();
            if (result > 0) return true;
            else throw new Exception("Can't update this role!");
        }
    }
}