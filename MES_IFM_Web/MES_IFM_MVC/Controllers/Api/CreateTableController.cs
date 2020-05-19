using System;
using MES_IFM_MVC.Models.dbTable;
using Microsoft.AspNetCore.Mvc;

namespace MES_IFM_MVC.Controllers.Api
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