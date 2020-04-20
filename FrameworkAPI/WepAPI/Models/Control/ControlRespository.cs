using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WepAPI.Models.Library;

namespace WepAPI.Models.Control
{
    public class ControlRespository : IBaseCRUDRespository<MesControl>
    {
        /// <summary>
        /// Call SQL base
        /// </summary>
        private static MESSQL mESSQL;
        /// <summary>
        /// List users
        /// </summary>
        private List<MesControl> controls { get; set; }
        /// <summary>
        /// Control respository
        /// </summary>
        public ControlRespository()
        {
            mESSQL = new MESSQL();
        }
        /// <summary>
        /// Control repository with custom connectionstring
        /// </summary>
        /// <param name="connectionString"></param>
        public ControlRespository(string connectionString)
        {
            mESSQL = new MESSQL(connectionString);
        }

        public MesControl Create(MesControl item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            string query = "INSERT INTO dbo.MesControl ";
            query += "(control_cd, control_name, parent_cd) VALUES ";
            query += string.Format("('{0}','{1}','{2}')", item.control_cd, item.control_name, item.parent_cd);
            mESSQL.OpenConnect();
            int result = mESSQL.DbCommand(query).ExecuteNonQuery();
            mESSQL.CloseConnect();
            if (result > 0) return item;
            else throw new Exception("Can't create new control!");
        }

        public MesControl Get(MesControl item)
        {
            controls = GetAll().ToList();
            if (item.control_id > 0)
                return controls.Find(x => x.control_id == item.control_id);
            if (!string.IsNullOrEmpty(item.control_cd))
                return controls.Find(x => x.control_cd == item.control_cd);
            else throw new ArgumentNullException("item");
        }

        public IEnumerable<MesControl> GetAll()
        {
            controls = new List<MesControl>();
            string query = "SELECT * FROM dbo.MesControl";
            mESSQL.OpenConnect();
            IDataReader reader = mESSQL.DbCommand(query).ExecuteReader();
            while (reader.Read())
            {
                controls.Add(new MesControl()
                {
                    control_id = (int)reader["control_id"],
                    control_cd = reader["control_cd"].ToString(),
                    control_name = reader["control_name"].ToString(),
                    parent_cd = reader["parent_cd"].ToString(),
                    reg_date = (DateTime)reader["reg_date"]
                });
            }
            reader.Close();
            mESSQL.CloseConnect();
            return controls;
        }

        public IEnumerable<MesControl> GetList(MesControl item)
        {
            controls = new List<MesControl>();
            string query = "SELECT * FROM dbo.MesControl WHERE 1=1 ";
            if (!string.IsNullOrEmpty(item.parent_cd))
                query += string.Format("AND parent_cd ='{0}' ", item.parent_cd);
            mESSQL.OpenConnect();
            IDataReader reader = mESSQL.DbCommand(query).ExecuteReader();
            while (reader.Read())
            {
                controls.Add(new MesControl()
                {
                    control_id = (int)reader["control_id"],
                    control_cd = reader["control_cd"].ToString(),
                    control_name = reader["control_name"].ToString(),
                    parent_cd = reader["parent_cd"].ToString(),
                    reg_date = (DateTime)reader["reg_date"]
                });
            }
            reader.Close();
            mESSQL.CloseConnect();
            return controls;
        }

        public void Remove(int id)
        {
            if (id > 0)
            {
                string query = string.Format("DELETE dbo.MesControl WHERE control_id = '{0}'", id);
                mESSQL.OpenConnect();
                int result = mESSQL.DbCommand(query).ExecuteNonQuery();
                mESSQL.CloseConnect();
                if (result <= 0)
                    throw new Exception("Can't remove this control!");
            }
            else
                throw new Exception("This control isn't exist!");
        }

        public bool Update(MesControl item)
        {
            if (item == null || item.control_id == 0)
                throw new ArgumentNullException("item");
            string query = "UPDATE dbo.MesControl SET ";
            query += string.Format("control_cd = '{0}', control_name = '{1}', parent_cd = '{2}' ",
                                   item.control_cd, item.control_name, item.parent_cd);
            query += string.Format("WHERE user_id = '{0}'", item.control_id);
            mESSQL.OpenConnect();
            int result = mESSQL.DbCommand(query).ExecuteNonQuery();
            mESSQL.CloseConnect();
            if (result > 0) return true;
            else throw new Exception("Can't update this control!");
        }
    }
}