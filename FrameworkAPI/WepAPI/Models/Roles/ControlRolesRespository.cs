using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WepAPI.Models.Library;

namespace WepAPI.Models.Roles
{
    public class ControlRolesRespository : IBaseCRUDRespository<ControlRoles>
    {
        /// <summary>
        /// Call SQL base
        /// </summary>
        private static MESSQL mESSQL;
        /// <summary>
        /// List users
        /// </summary>
        private List<ControlRoles> roles { get; set; }
        /// <summary>
        /// Control respository
        /// </summary>
        public ControlRolesRespository()
        {
            mESSQL = new MESSQL();
        }
        /// <summary>
        /// Control repository with custom connectionstring
        /// </summary>
        /// <param name="connectionString"></param>
        public ControlRolesRespository(string connectionString)
        {
            mESSQL = new MESSQL(connectionString);
        }

        public ControlRoles Create(ControlRoles item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            string query = "INSERT INTO dbo.ControlRoles ";
            query += "(control_cd, role_cd, reg_user) VALUES ";
            query += string.Format("('{0}','{1}','{2}')", item.control_cd, item.role_cd, item.reg_user);
            mESSQL.OpenConnect();
            int result = mESSQL.DbCommand(query).ExecuteNonQuery();
            mESSQL.CloseConnect();
            if (result > 0) return item;
            else throw new Exception("Can't create new role!");
        }

        public ControlRoles Get(ControlRoles item)
        {
            roles = GetAll().ToList();
            return roles.Find(x => x.control_role_id == item.control_role_id);
        }

        public IEnumerable<ControlRoles> GetAll()
        {
            roles = new List<ControlRoles>();
            string query = "SELECT * FROM dbo.ControlRoles";
            mESSQL.OpenConnect();
            IDataReader reader = mESSQL.DbCommand(query).ExecuteReader();
            while (reader.Read())
            {
                roles.Add(new ControlRoles()
                {
                    control_role_id = (int)reader["control_role_id"],
                    control_cd = reader["control_cd"].ToString(),
                    role_cd = reader["role_cd"].ToString(),
                    reg_user = reader["reg_user"].ToString(),
                    reg_date = (DateTime)reader["reg_date"]
                });
            }
            reader.Close();
            mESSQL.CloseConnect();
            return roles;
        }

        public IEnumerable<ControlRoles> GetList(ControlRoles item)
        {
            roles = new List<ControlRoles>();
            string query = "SELECT * FROM dbo.ControlRoles WHERE 1=1 ";
            if (!string.IsNullOrEmpty(item.control_cd))
                query += string.Format("AND control_cd='{0}' ", item.control_cd);
            if (!string.IsNullOrEmpty(item.role_cd))
                query += string.Format("AND role_cd='{0}' ", item.role_cd);
            mESSQL.OpenConnect();
            IDataReader reader = mESSQL.DbCommand(query).ExecuteReader();
            while (reader.Read())
            {
                roles.Add(new ControlRoles()
                {
                    control_role_id = (int)reader["control_role_id"],
                    control_cd = reader["control_cd"].ToString(),
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
                string query = string.Format("DELETE dbo.ControlRoles WHERE control_role_id = '{0}'", id);
                mESSQL.OpenConnect();
                int result = mESSQL.DbCommand(query).ExecuteNonQuery();
                mESSQL.CloseConnect();
                if (result <= 0)
                    throw new Exception("Can't remove this role!");
            }
            else
                throw new Exception("This role isn't exist!");
        }

        public bool Update(ControlRoles item)
        {
            if (item == null || item.control_role_id == 0)
                throw new ArgumentNullException("item");
            string query = "UPDATE dbo.UserRoles SET ";
            query += string.Format("control_cd = '{0}', role_cd = '{1}', reg_user = '{2}' ",
                                   item.control_cd, item.role_cd, item.reg_user);
            query += string.Format("WHERE user_id = '{0}'", item.control_role_id);
            mESSQL.OpenConnect();
            int result = mESSQL.DbCommand(query).ExecuteNonQuery();
            mESSQL.CloseConnect();
            if (result > 0) return true;
            else throw new Exception("Can't update this role!");
        }
    }
}