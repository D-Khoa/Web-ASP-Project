using System;
using System.Collections.Generic;
using System.Data;

namespace WebAppNo1.Models
{
    /// <summary>
    /// LAMP DATA
    /// </summary>
    public class m_lamp
    {
        #region ALL FIELDS
        public int lamp_id { get; set; }
        public string lamp_cd { get; set; }
        public bool lamp_state { get; set; }
        public double wattage { get; set; }
        public DateTime reg_date { get; set; }
        public string reg_user { get; set; }
        public List<m_lamp> listLamp { get; set; }
        public m_lamp()
        {
            listLamp = new List<m_lamp>();
        }
        #endregion

        #region QUERY DATABASE
        /// <summary>
        /// Get lamp info into current lamp list
        /// </summary>
        /// <param name="lamp">input lamp info need search</param>
        /// <param name="checkState">true: check state of lamp</param>
        public void SearchLamp(m_lamp lamp, bool checkState)
        {
            MySQLDao SQL = new MySQLDao();
            string query = string.Empty;
            listLamp = new List<m_lamp>();
            query = "SELECT `m_lamp`.`lamp_id`,`m_lamp`.`lamp_cd`,`m_lamp`.`lamp_state`,`m_lamp`.`wattage` ";
            query += "FROM `maindb`.`m_lamp` WHERE 1=1 ";
            if (lamp.lamp_id > 0)
                query += "AND `lamp_id` ='" + lamp.lamp_id + "' ";
            if (!string.IsNullOrEmpty(lamp.lamp_cd))
                query += "AND `lamp_cd` ='" + lamp.lamp_cd + "' ";
            if (checkState)
                query += "AND `lamp_state` ='" + lamp.lamp_state + "' ";
            SQL.OpenConnection();
            IDataReader reader = SQL.Command(query).ExecuteReader();
            while (reader.Read())
            {
                m_lamp outItem = new m_lamp()
                {
                    lamp_id = (int)reader["lamp_id"],
                    lamp_cd = reader["lamp_cd"].ToString(),
                    lamp_state = reader.GetBoolean(reader.GetOrdinal("lamp_state")),
                    wattage = (double)reader["wattage"],
                    reg_date = (DateTime)reader["reg_date"],
                    reg_user = reader["reg_user"].ToString(),
                };
                listLamp.Add(outItem);
            }
            reader.Close();
            SQL.CloseConnection();
            query = string.Empty;
        }

        /// <summary>
        /// Add a lamp into database
        /// </summary>
        /// <param name="inItem">input lamp (lamp_cd, lamp_state, wattage, reg_user)</param>
        /// <returns></returns>
        public int AddLamp(m_lamp inItem)
        {
            MySQLDao SQL = new MySQLDao();
            string query = string.Empty;
            SQL.OpenConnection();
            query = @"INSERT INTO `maindb`.`m_lamp`(`lamp_cd`,`lamp_state`,`wattage`,`reg_user`)";
            query += "VALUES('" + inItem.lamp_cd + "','" + inItem.lamp_state + "','" + inItem.wattage + "','";
            query += inItem.reg_user + "')";
            int result = SQL.Command(query).ExecuteNonQuery();
            SQL.CloseConnection();
            query = string.Empty;
            return result;
        }

        /// <summary>
        /// Update a lamp
        /// </summary>
        /// <param name="inItem">input lamp (lamp_cd: empty if skip, lamp_state, wattage)</param>
        /// <param name="changeState">true: change state of lamp</param>
        /// <param name="changeValue">true: change wattage of lamp</param>
        /// <returns></returns>
        public int UpdateLamp(m_lamp inItem, bool changeState, bool changeValue)
        {
            MySQLDao SQL = new MySQLDao();
            string query = string.Empty;
            SQL.OpenConnection();
            query = "UPDATE `maindb`.`m_lamp` SET `reg_date` ='" + DateTime.Now + "' ";
            if (!string.IsNullOrEmpty(inItem.lamp_cd))
                query += ",`lamp_cd` ='" + inItem.lamp_cd + "' ";
            if (changeState)
                query += ",`lamp_state`='" + inItem.lamp_state + "' ";
            if (changeValue)
                query += ",`wattage`='" + inItem.wattage + "' ";
            query += "WHERE `lamp_id` ='" + inItem.lamp_id + "'";
            int result = SQL.Command(query).ExecuteNonQuery();
            SQL.CloseConnection();
            query = string.Empty;
            return result;
        }

        /// <summary>
        /// Delete current lamp
        /// </summary>
        /// <returns></returns>
        public int DeleteLamp()
        {
            MySQLDao SQL = new MySQLDao();
            string query = string.Empty;
            SQL.OpenConnection();
            query = "DELETE FROM `maindb`.`m_lamp` WHERE `lamp_id` ='" + lamp_id + "'";
            int result = SQL.Command(query).ExecuteNonQuery();
            SQL.CloseConnection();
            query = string.Empty;
            return result;
        }
        #endregion
    }
}