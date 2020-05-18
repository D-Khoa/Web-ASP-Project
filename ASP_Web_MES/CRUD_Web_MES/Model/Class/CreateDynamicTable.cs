using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Web_MES.Model.Class
{
    public class CreateDynamicTable
    {
        private readonly ApplicationDbContext _db;
        public CreateDynamicTable(ApplicationDbContext db)
        {
            _db = db;
        }

        public void CreateTable(string typeName, int qtyOfTable)
        {

        }
    }
}
