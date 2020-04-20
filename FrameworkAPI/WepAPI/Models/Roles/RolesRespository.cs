using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WepAPI.Models.Library;

namespace WepAPI.Models.Roles
{
    public class RolesRespository : IBaseCRUDRespository<MesRoles>
    {
        /// <summary>
        /// Call SQL base
        /// </summary>
        private static MESSQL mESSQL;
        /// <summary>
        /// List users
        /// </summary>
        private List<MesRoles> roles { get; set; }
        /// <summary>
        /// Control respository
        /// </summary>
        public RolesRespository()
        {
            mESSQL = new MESSQL();
        }
        /// <summary>
        /// Control repository with custom connectionstring
        /// </summary>
        /// <param name="connectionString"></param>
        public RolesRespository(string connectionString)
        {
            mESSQL = new MESSQL(connectionString);
        }

        public MesRoles Create(MesRoles item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            string query = "INSERT INTO dbo.MesRoles ";
            query += "(role_cd, role_name, reg_user) VALUES ";
            query += string.Format("('{0}','{1}','{2}')", item.role_cd, item.role_name, item.reg_user);
            mESSQL.OpenConnect();
            int result = mESSQL.DbCommand(query).ExecuteNonQuery();
            mESSQL.CloseConnect();
            if (result > 0) return item;
            else throw new Exception("Can't create new role!");
        }

        public MesRoles Get(MesRoles item)
        {
            roles = GetAll().ToList();
            if (item.role_id > 0)
                return roles.Find(x => x.role_id == item.role_id);
            else
                return roles.Find(x => x.role_cd == item.role_cd);
        }

        public IEnumerable<MesRoles> GetAll()
        {
            roles = new List<MesRoles>();
            string query = "SELECT * FROM dbo.MesRoles";
            mESSQL.OpenConnect();
            IDataReader reader = mESSQL.DbCommand(query).ExecuteReader();
            while (reader.Read())
            {
                roles.Add(new MesRoles()
                {
                    role_id = (int)reader["role_id"],
                    role_cd = reader["role_cd"].ToString(),
                    role_name = reader["role_name"].ToString(),
                    reg_user = reader["reg_user"].ToString(),
                    reg_date = (DateTime)reader["reg_date"]
                });
            }
            reader.Close();
            mESSQL.CloseConnect();
            return roles;
        }

        public IEnumerable<MesRoles> GetList(MesRoles item)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            if (id > 0)
            {
                string query = string.Format("DELETE dbo.MesRoles WHERE role_id = '{0}'", id);
                mESSQL.OpenConnect();
                int result = mESSQL.DbCommand(query).ExecuteNonQuery();
                mESSQL.CloseConnect();
                if (result <= 0)
                    throw new Exception("Can't remove this role!");
            }
            else
                throw new Exception("This role isn't exist!");
        }

        public bool Update(MesRoles item)
        {
            if (item == null || item.role_id == 0)
                throw new ArgumentNullException("item");
            string query = "UPDATE dbo.MesRoles SET ";
            query += string.Format("role_cd = '{0}', role_name = '{1}', reg_user = '{2}' ",
                                   item.role_cd, item.role_name, item.reg_user);
            query += string.Format("WHERE user_id = '{0}'", item.role_id);
            mESSQL.OpenConnect();
            int result = mESSQL.DbCommand(query).ExecuteNonQuery();
            mESSQL.CloseConnect();
            if (result > 0) return true;
            else throw new Exception("Can't update this role!");
        }
    }
}