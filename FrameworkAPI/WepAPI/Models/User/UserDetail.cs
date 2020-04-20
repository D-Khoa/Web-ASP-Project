using System;

namespace WepAPI.Models.User
{
    /// <summary>
    /// User profile
    /// </summary>
    public class UserDetail
    {
        /// <summary>
        /// User id
        /// </summary>
        public int user_id { get; set; }
        /// <summary>
        /// User code
        /// </summary>
        public string user_cd { get; set; }
        /// <summary>
        /// User fullname
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// User email
        /// </summary>
        public string user_email { get; set; }
        /// <summary>
        /// User telephone
        /// </summary>
        public string user_tel { get; set; }
        /// <summary>
        /// User position in company
        /// </summary>
        public string user_position { get; set; }
        /// <summary>
        /// User department
        /// </summary>
        public string user_dept { get; set; }
        /// <summary>
        /// User factory
        /// </summary>
        public string factory_cd { get; set; }
        /// <summary>
        /// User start work date
        /// </summary>
        public DateTime start_date { get; set; }
        /// <summary>
        /// User registrator date
        /// </summary>
        public DateTime reg_date { get; set; }
    }
}