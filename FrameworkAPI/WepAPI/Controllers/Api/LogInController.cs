using System;
using System.Web.Http;
using WepAPI.Models;
using WepAPI.Models.User;

namespace WepAPI.Controllers.Api
{
    public class LogInController : ApiController
    {
        static readonly IBaseCRUDRespository<UserLog> loginRes = new LoginRespository();
        [HttpPost]
        public IHttpActionResult Get(UserLog user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                user = loginRes.Get(user);
                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IHttpActionResult Logio(UserLog user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                user = loginRes.Get(user);
                loginRes.Update(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
