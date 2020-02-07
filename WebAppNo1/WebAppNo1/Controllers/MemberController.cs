using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppNo1.Models;

namespace WebAppNo1.Controllers
{
    public class MemberController : ApiController
    {
        m_user[] member = new m_user[]
        {
            new m_user {user_id = 1, user_name = "DKhoa", password ="123", email ="DKhoa@123",
                        phone ="0123456789", reg_date = DateTime.Now, is_active = false },
            new m_user {user_id = 2, user_name = "TKhoa", password ="456", email ="TKhoa@456",
                        phone ="0987654321", reg_date = DateTime.Now, is_active = false },
            new m_user {user_id = 3, user_name = "NKhoa", password ="789", email ="NKhoa@789",
                        phone ="0999999999", reg_date = DateTime.Now, is_active = false },
        };

        public IEnumerable<m_user> GetAllMember()
        {
            return member;
        }

        public IHttpActionResult GetMember(int id)
        {
            var mem = member.FirstOrDefault(p => p.user_id == id);
            if (mem == null)
                return NotFound();
            else
                return Ok(mem);
        }
    }
}
