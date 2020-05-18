using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MES_IFM_Web.Models.dbTable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MES_IFM_Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateTableController : ControllerBase
    {
        private static readonly CreateTableResposity createTableResposity = new CreateTableResposity();

        [HttpPost]
        public ActionResult<int> PostCreateTable(string tableName, int qtyTable)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return createTableResposity.CreateTable(tableName, qtyTable);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}