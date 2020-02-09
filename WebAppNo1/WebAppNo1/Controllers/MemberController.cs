using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppNo1.Models;

namespace WebAppNo1.Controllers
{
    /// <summary>
    /// Member management
    /// </summary>
    public class MemberController : ApiController
    {
        m_user userData { get; set; }

        public MemberController()
        {
            userData = new m_user();
        }

        public IEnumerable<m_user> Get()
        {
            userData.SearchUser(new m_user
            {
                user_id = 0,
                user_name = string.Empty,
                email = string.Empty,
                phone = string.Empty,
            }, false);
            return userData.listUser;
        }

        [HttpPost]
        [Route("api/member/SignUp")]
        public string SignUp(m_user inuser)
        {
            try
            {
                var n = userData.AddUser(new m_user
                {
                    user_name = inuser.user_name,
                    password = inuser.password,
                    email = inuser.email,
                    phone = inuser.phone,
                });
                return "Add " + n.ToString() + " member succesed!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        [Route("api/member/SignIn")]
        public string SignIn(m_user loginUser)
        {
            try
            {
                userData = userData.LoginUser(loginUser.user_name, loginUser.password);
                return "Welcome " + loginUser.user_name;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
