using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WepAPI.Models;
using WepAPI.Models.User;

namespace WepAPI.Controllers.Api
{
    public class UserLogController : ApiController
    {
        static readonly IBaseCRUDRespository<UserLog> userLogRes = new UserLogRespository();
        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllUser()
        {
            List<UserLog> users = userLogRes.GetAll().ToList();
            if (users.Count > 0) return Ok(users);
            else return NotFound();
        }
        /// <summary>
        /// Get an user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserByID(int id)
        {
            UserLog user = new UserLog { userlog_id = id };
            user = userLogRes.Get(user);
            if (user == null) return NotFound();
            return Ok(user);
        }
        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostUserLog(UserLog user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                user = userLogRes.Create(user);
                string apiname = WebApiConfig.DEFAULT_ROUTE_NAME;
                var respone = this.Request.CreateResponse<UserLog>(HttpStatusCode.Created, user);
                string uri = Url.Link(apiname, new { id = user.userlog_id });
                respone.Headers.Location = new Uri(uri);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Update an user info
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult PutUserLog(UserLog user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!userLogRes.Update(user)) return NotFound();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Delete an user with id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public IHttpActionResult DeleteUserLog(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                UserLog user = new UserLog { userlog_id = id };
                user = userLogRes.Get(user);
                if (user == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                userLogRes.Remove(id);
                return Ok("Deleted user!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
