using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WepAPI.Models.Library;

namespace WepAPI.Models.Factory
{
    public class FactoryRespository : IBaseCRUDRespository<MesFactory>
    {
        /// <summary>
        /// Call SQL base
        /// </summary>
        private static MESSQL mESSQL;
        /// <summary>
        /// List users
        /// </summary>
        private List<MesFactory> factories { get; set; }
        /// <summary>
        /// Control respository
        /// </summary>
        public FactoryRespository()
        {
            mESSQL = new MESSQL();
        }
        /// <summary>
        /// Control repository with custom connectionstring
        /// </summary>
        /// <param name="connectionString"></param>
        public FactoryRespository(string connectionString)
        {
            mESSQL = new MESSQL(connectionString);
        }

        public MesFactory Create(MesFactory item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            string query = "INSERT INTO dbo.MesFactory ";
            query += "(factory_cd, factory_name, reg_user) VALUES ";
            query += string.Format("('{0}','{1}','{2}')", item.factory_cd, item.factory_name, item.reg_user);
            mESSQL.OpenConnect();
            int result = mESSQL.DbCommand(query).ExecuteNonQuery();
            mESSQL.CloseConnect();
            if (result > 0) return item;
            else throw new Exception("Can't create new factory!");
        }

        public MesFactory Get(MesFactory item)
        {
            factories = GetAll().ToList();
            return factories.Find(x => x.factory_id == item.factory_id);
        }

        public IEnumerable<MesFactory> GetAll()
        {
            factories = new List<MesFactory>();
            string query = "SELECT * FROM dbo.MesFactory";
            mESSQL.OpenConnect();
            IDataReader reader = mESSQL.DbCommand(query).ExecuteReader();
            while (reader.Read())
            {
                factories.Add(new MesFactory()
                {
                    factory_id = (int)reader["factory_id"],
                    factory_cd = reader["factory_cd"].ToString(),
                    factory_name = reader["factory_name"].ToString(),
                    reg_user = reader["reg_user"].ToString(),
                    reg_date = (DateTime)reader["reg_date"]
                });
            }
            reader.Close();
            mESSQL.CloseConnect();
            return factories;
        }

        public IEnumerable<MesFactory> GetList(MesFactory item)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            if (id > 0)
            {
                string query = string.Format("DELETE dbo.MesFactory WHERE factory_id = '{0}'", id);
                mESSQL.OpenConnect();
                int result = mESSQL.DbCommand(query).ExecuteNonQuery();
                mESSQL.CloseConnect();
                if (result <= 0)
                    throw new Exception("Can't remove this factory!");
            }
            else
                throw new Exception("This factory isn't exist!");
        }

        public bool Update(MesFactory item)
        {
            if (item == null || item.factory_id == 0)
                throw new ArgumentNullException("item");
            string query = "UPDATE dbo.MesFactory SET ";
            query += string.Format("factory_cd = '{0}', factory_name = '{1}', reg_user = '{2}' ",
                                   item.factory_cd, item.factory_name, item.reg_user);
            query += string.Format("WHERE factory_id = '{0}'", item.factory_id);
            mESSQL.OpenConnect();
            int result = mESSQL.DbCommand(query).ExecuteNonQuery();
            mESSQL.CloseConnect();
            if (result > 0) return true;
            else throw new Exception("Can't update this factory!");
        }
    }
}