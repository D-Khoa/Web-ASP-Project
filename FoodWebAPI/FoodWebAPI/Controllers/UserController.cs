using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodWebAPI.Class;

namespace FoodWebAPI.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public List<UserProfile> GetAllUser()
        {
            DBFoodDataContext db = new DBFoodDataContext();
            return db.UserProfiles.ToList();
        }

        [HttpGet]
        public UserProfile GetUser(int id)
        {
            DBFoodDataContext db = new DBFoodDataContext();
            return db.UserProfiles.FirstOrDefault(x => x.UserId == id);
        }

        [HttpGet]
        public bool Login(string username, string password)
        {
            try
            {
                DBFoodDataContext db = new DBFoodDataContext();
                UserProfile userinfo = db.UserProfiles.FirstOrDefault(x => x.UserName == username);
                if (userinfo == null) return false;
                if (userinfo.Password == password.Encrypt()) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public bool AddUser(string username, string password, bool isactive)
        {
            try
            {
                DBFoodDataContext db = new DBFoodDataContext();
                UserProfile userinfo = new UserProfile();
                userinfo.UserName = username;
                userinfo.Password = password.Encrypt();
                userinfo.IsActive = isactive;
                db.UserProfiles.InsertOnSubmit(userinfo);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut]
        public bool UpdateUser(int userid, string username, string password, bool isactive)
        {
            try
            {
                DBFoodDataContext db = new DBFoodDataContext();
                UserProfile userinfo = db.UserProfiles.FirstOrDefault(x => x.UserId == userid);
                if (userinfo == null) return false;
                userinfo.UserName = username;
                userinfo.Password = password;
                userinfo.IsActive = isactive;
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        public bool DeleteUser(int userid)
        {
            DBFoodDataContext db = new DBFoodDataContext();
            UserProfile userinfo = db.UserProfiles.FirstOrDefault(x => x.UserId == userid);
            if (userinfo == null) return false;
            db.UserProfiles.DeleteOnSubmit(userinfo);
            db.SubmitChanges();
            return true;
        }
    }
}
