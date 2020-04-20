using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WepAPI.Models;
using WepAPI.Models.User;

namespace WepAPI.Controllers.Api
{
    public class UserDetailController : ApiController
    {
        static readonly IBaseCRUDRespository<UserDetail> userDetailRes = new UserDetailRespository();

        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<UserDetail> GetAllUser()
        {
            return userDetailRes.GetAll();
        }
        /// <summary>
        /// Get list user detail
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<UserDetail> GetListUser(UserDetail user)
        {
            return userDetailRes.GetList(user);
        }
        /// <summary>
        /// Get an user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public UserDetail GetUserByID(int id)
        {
            UserDetail user = new UserDetail { user_id = id };
            user = userDetailRes.Get(user);
            if (user == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return user;
        }
        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostUserDetail(UserDetail user)
        {
            user = userDetailRes.Create(user);
            string apiname = WebApiConfig.DEFAULT_ROUTE_NAME;
            var respone = this.Request.CreateResponse<UserDetail>(HttpStatusCode.Created, user);
            string uri = Url.Link(apiname, new { id = user.user_id });
            respone.Headers.Location = new Uri(uri);
            return respone;
        }
        /// <summary>
        /// Update an user info
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public bool PutUserDetail(UserDetail user)
        {
            if (!userDetailRes.Update(user))
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return true;
        }
        /// <summary>
        /// Delete an user with id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public void DeleteUserDetail(int id)
        {
            UserDetail user = new UserDetail { user_id = id };
            user = userDetailRes.Get(user);
            if (user == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            userDetailRes.Remove(id);
        }
    }
}
