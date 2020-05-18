using MES_IFM_Web.Models.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MES_IFM_Web.Models.Account
{
    public class UserResposity : IBaseAPI<aa0001>
    {
        private MESSQL mESSQL;
        private readonly MESContext _db;
        /// <summary>
        /// CRUD for user profile
        /// </summary>
        public UserResposity(MESContext db)
        {
            _db = db;
            mESSQL = new MESSQL();
        }
        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public aa0001 Get(aa0001 item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<aa0001> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<aa0001> GetList()
        {
            throw new NotImplementedException();
        }

        public aa0001 Post(aa0001 item)
        {
            throw new NotImplementedException();
        }

        public bool Put(aa0001 item)
        {
            throw new NotImplementedException();
        }
    }
}
