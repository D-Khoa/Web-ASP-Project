using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WepAPI.Models;
using WepAPI.Models.Factory;

namespace WepAPI.Controllers.Api
{
    public class MesFactoryController : ApiController
    {
        static readonly IBaseCRUDRespository<MesFactory> factoryRes = new FactoryRespository();
        /// <summary>
        /// Get all factory
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllFactory()
        {
            List<MesFactory> factories = factoryRes.GetAll().ToList();
            if (factories.Count > 0) return Ok(factories);
            else return NotFound();
        }
        /// <summary>
        /// Get an factory by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetFactoryByID(int id)
        {
            MesFactory factory = new MesFactory { factory_id = id };
            factory = factoryRes.Get(factory);
            if (factory == null) return NotFound();
            return Ok(factory);
        }
        /// <summary>
        /// Add new factory
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostFactory(MesFactory factory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                factory = factoryRes.Create(factory);
                string apiname = WebApiConfig.DEFAULT_ROUTE_NAME;
                var respone = this.Request.CreateResponse<MesFactory>(HttpStatusCode.Created, factory);
                string uri = Url.Link(apiname, new { id = factory.factory_id });
                respone.Headers.Location = new Uri(uri);
                return Ok(factory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Update an factory info
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult PutFactory(MesFactory factory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!factoryRes.Update(factory)) return NotFound();
                return Ok(factory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Delete an factory with id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public IHttpActionResult DeleteFactory(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                MesFactory control = new MesFactory { factory_id = id };
                control = factoryRes.Get(control);
                if (control == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                factoryRes.Remove(id);
                return Ok("Deleted factory!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
