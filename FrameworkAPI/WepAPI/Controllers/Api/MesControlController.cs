using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WepAPI.Models;
using WepAPI.Models.Control;

namespace WepAPI.Controllers.Api
{
    public class MesControlController : ApiController
    {
        static readonly IBaseCRUDRespository<MesControl> controlRes = new ControlRespository();
        /// <summary>
        /// Get all control
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllControl ()
        {
            List<MesControl> controls = controlRes.GetAll().ToList();
            if (controls.Count > 0) return Ok(controls);
            else return NotFound();
        }
        /// <summary>
        /// Get list control
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<MesControl> GetListUserRoles(MesControl item)
        {
            return controlRes.GetList(item);
        }
        /// <summary>
        /// Get an control by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetControlByID(int id)
        {
            MesControl control = new MesControl { control_id = id };
            control = controlRes.Get(control);
            if (control == null) return NotFound();
            return Ok(control);
        }
        /// <summary>
        /// Add new control
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostControl(MesControl control)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                control = controlRes.Create(control);
                string apiname = WebApiConfig.DEFAULT_ROUTE_NAME;
                var respone = this.Request.CreateResponse<MesControl>(HttpStatusCode.Created, control);
                string uri = Url.Link(apiname, new { id = control.control_id });
                respone.Headers.Location = new Uri(uri);
                return Ok(control);
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
        public IHttpActionResult PutControl(MesControl control)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!controlRes.Update(control)) return NotFound();
                return Ok(control);
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
                MesControl control = new MesControl { control_id = id };
                control = controlRes.Get(control);
                if (control == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                controlRes.Remove(id);
                return Ok("Deleted control!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
