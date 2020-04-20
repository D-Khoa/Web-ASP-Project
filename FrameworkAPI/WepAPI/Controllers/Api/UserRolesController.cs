using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WepAPI.Models;
using WepAPI.Models.Roles;

namespace WepAPI.Controllers.Api
{
    public class UserRolesController : ApiController
    {
        static readonly IBaseCRUDRespository<UserRoles> roleRes = new UserRolesRespository();
        /// <summary>
        /// Get all user role
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllUserRoles()
        {
            List<UserRoles> roles = roleRes.GetAll().ToList();
            if (roles.Count > 0) return Ok(roles);
            else return NotFound();
        }
        /// <summary>
        /// Get list user role
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetListUserRoles(UserRoles item)
        {
            List<UserRoles> roles = roleRes.GetList(item).ToList();
            if (roles.Count > 0) return Ok(roles);
            else return NotFound();
        }
        /// <summary>
        /// Get an user role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserRoleByID(int id)
        {
            UserRoles role = new UserRoles { user_role_id = id };
            role = roleRes.Get(role);
            if (role == null) return NotFound();
            return Ok(role);
        }
        /// <summary>
        /// Add new user role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostUserRole(UserRoles role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                role = roleRes.Create(role);
                string apiname = WebApiConfig.DEFAULT_ROUTE_NAME;
                var respone = this.Request.CreateResponse<UserRoles>(HttpStatusCode.Created, role);
                string uri = Url.Link(apiname, new { id = role.user_role_id });
                respone.Headers.Location = new Uri(uri);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Update an user role info
        /// </summary>
        /// <param name="contrl"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult PutUserRole(UserRoles role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!roleRes.Update(role)) return NotFound();
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Delete an control with id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public IHttpActionResult DeleteUserRole(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                UserRoles role = new UserRoles { user_role_id = id };
                role = roleRes.Get(role);
                if (role == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                roleRes.Remove(id);
                return Ok("Deleted role!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
