using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebSample.Models.Interface;
using WebSample.Models.Library;
using WebSample.Models.User;

namespace WebSample.Controllers.Api
{
    public class AccountController : ApiController
    {
        static readonly IBaseCRUDRespositories<UserLog> accountRes = new AccountRespositories();

        [HttpGet]
        public IHttpActionResult GetAllUser()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                List<UserLog> users = accountRes.GetAll().ToList();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IHttpActionResult GetUser(UserLog user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                List<UserLog> users = accountRes.GetCollection(user).ToList();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IHttpActionResult Register(UserLog user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                user.password = user.password.EncryptString();
                user = accountRes.Create(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IHttpActionResult UpdateUser(UserLog user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                user.password = user.password.EncryptString();
                bool result = accountRes.Update(user);
                if (result) return Ok(user);
                else return BadRequest("Update user not completed!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public IHttpActionResult DeleteUser(UserLog user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                accountRes.Remove(user.uid);
                return Ok("This user is deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpOptions]
        public IHttpActionResult Login(UserLog user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                user = accountRes.GetItem(user);
                if (user.userActive)
                {
                    user.lastLogin = DateTime.Now;
                    string dataToken = user.username + user.lastLogin.ToString("yyyyMMddHHmmss");
                    user.userToken = dataToken.EncryptString();
                    user.userOnline = true;
                    bool result = accountRes.Update(user);
                    if (result) return Ok(user);
                    else return BadRequest("Login failed!");
                }
                else return BadRequest("User isn't active!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
