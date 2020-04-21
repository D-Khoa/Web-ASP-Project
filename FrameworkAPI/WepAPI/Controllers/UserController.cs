using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using WepAPI.Models;
using WepAPI.Models.User;

namespace WepAPI.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserDetail user)
        {
            //Chuyển đổi thông tin user sang dạng json
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            //Khai báo client api
            var client = new RestClient(WebInfo.apiURL + "userdetail/GetListUser");
            client.Timeout = -1;
            //Khai báo dạng request là get
            var request = new RestRequest(Method.GET);
            //Thêm chuỗi thông tin dạng json để get dữ liệu user
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jsonUser, ParameterType.RequestBody);
            //Xuất response từ api
            IRestResponse response = client.Execute(request);
            //Chuyển đổi chuỗi json trả về thành dạng object (UserLog)
            List<UserDetail> users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserDetail>>(response.Content);
            //Nếu trả về code OK thì chuyển sang trang chủ
            if (response.IsSuccessful)
            {
                return View(users);
            }
            else
            {
                ErrorMessage errorMessage = new ErrorMessage();
                errorMessage = errorMessage.jsonConvert(response.Content);
                ModelState.AddModelError("Error", errorMessage.Message);
            }
            return View();
        }

        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(UserDetail user)
        {
            return View(user);
        }

        public ActionResult UpdateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateUser(UserDetail user)
        {
            return View(user);
        }

        public ActionResult DeleteUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            return View(id);
        }
    }
}