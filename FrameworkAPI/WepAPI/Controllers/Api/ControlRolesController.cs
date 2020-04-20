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
    public class ControlRolesController : ApiController
    {
        static readonly IBaseCRUDRespository<ControlRoles> roleRes = new ControlRolesRespository();
        /// <summary>
        /// Get all control
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllControl()
        {
            List<ControlRoles> roles = roleRes.GetAll().ToList();
            if (roles.Count > 0) return Ok(roles);
            else return NotFound();
        }
        /// <summary>
        /// Get list control role
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ControlRoles> GetListUser(ControlRoles item)
        {
            return roleRes.GetList(item);
        }
        /// <summary>
        /// Get an control by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetControlByID(int id)
        {
            ControlRoles role = new ControlRoles { control_role_id = id };
            role = roleRes.Get(role);
            if (role == null) return NotFound();
            return Ok(role);
        }
        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostControl(ControlRoles role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                role = roleRes.Create(role);
                string apiname = WebApiConfig.DEFAULT_ROUTE_NAME;
                var respone = this.Request.CreateResponse<ControlRoles>(HttpStatusCode.Created, role);
                string uri = Url.Link(apiname, new { id = role.control_role_id });
                respone.Headers.Location = new Uri(uri);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Update an control info
        /// </summary>
        /// <param name="contrl"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult PutControl(ControlRoles role)
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
        public IHttpActionResult DeleteControl(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                ControlRoles role = new ControlRoles { control_role_id = id };
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
