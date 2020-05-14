using System;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using RestSharp;
using WepAPI.Models;
using WepAPI.Models.Library;
using WepAPI.Models.Roles;
using WepAPI.Models.User;

namespace WepAPI.Controllers
{
    public class AccountController : Controller
    {
        // GET: LogIn
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLog user)
        {
            //Mã hóa mật khẩu
            if (!string.IsNullOrEmpty(user.user_password))
            {
                user.user_password = user.user_password.Encrypt();
                user.confirm_password = user.user_password;
                user.user_online = true;
                user.last_login = DateTime.Now;
            }
            //Chuyển đổi thông tin user sang dạng json
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            //Khai báo client api
            var client = new RestClient(WebInfo.apiURL + "login/logio");
            client.Timeout = -1;
            //Khai báo dạng request là put
            var request = new RestRequest(Method.PUT);
            //Thêm chuỗi thông tin dạng json để put dữ liệu user
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jsonUser, ParameterType.RequestBody);
            //Xuất response từ api
            IRestResponse response = client.Execute(request);
            //Chuyển đổi chuỗi json trả về thành dạng object (UserLog)
            user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserLog>(response.Content);
            //Nếu trả về code OK thì chuyển sang trang chủ
            if (response.IsSuccessful)
            {
                //Lấy usercode
                WebInfo.loginUserCD = user.user_cd;
                return RedirectToAction("Index", "Main", new { role = user.userRoles, username = user.user_cd });
            }
            else
            {
                ErrorMessage errorMessage = new ErrorMessage();
                errorMessage = errorMessage.jsonConvert(response.Content);
                ModelState.AddModelError("Error", errorMessage.Message);
            }
            //Không chuyển trang khi lỗi
            return View(user);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserLog user)
        {
            if (!string.IsNullOrEmpty(user.user_password))
            {
                user.user_password = user.user_password.Encrypt();
                user.confirm_password = user.confirm_password.Encrypt();
            }
            //Chuyển đổi thông tin user sang dạng json
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            //Khai báo client api
            var client = new RestClient(WebInfo.apiURL + "UserLog/PostUserLog");
            client.Timeout = -1;
            //Khai báo dạng request là post
            var request = new RestRequest(Method.POST);
            //Thêm chuỗi thông tin dạng json để post dữ liệu user
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jsonUser, ParameterType.RequestBody);
            //Xuất response từ api
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                ViewBag.message = "User has successfully registered!";
                return View();
            }
            else
            {
                ErrorMessage errorMessage = new ErrorMessage();
                errorMessage = errorMessage.jsonConvert(response.Content);
                ModelState.AddModelError("Error", errorMessage.Message);
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logout(UserLog user)
        {
            //Mã hóa mật khẩu
            if (!string.IsNullOrEmpty(user.user_password))
            {
                user.user_password = user.user_password.Encrypt();
                user.confirm_password = user.user_password;
                user.user_online = false;
            }
            //Chuyển đổi thông tin user sang dạng json
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            //Khai báo client api
            var client = new RestClient(WebInfo.apiURL + "login/logio");
            client.Timeout = -1;
            //Khai báo dạng request là put
            var request = new RestRequest(Method.PUT);
            //Thêm chuỗi thông tin dạng json để put dữ liệu user
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jsonUser, ParameterType.RequestBody);
            //Xuất response từ api
            IRestResponse response = client.Execute(request);
            //Chuyển đổi chuỗi json trả về thành dạng object (UserLog)
            user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserLog>(response.Content);
            //Nếu trả về code OK thì chuyển sang trang chủ
            if (response.IsSuccessful)
                return RedirectToAction("Index", "Main", new { role = user.userRoles, username = "Good bye " + user.user_cd });
            else
            {
                ErrorMessage errorMessage = new ErrorMessage();
                errorMessage = errorMessage.jsonConvert(response.Content);
                ModelState.AddModelError("Error", errorMessage.Message);
            }
            //Không chuyển trang khi lỗi
            return View(user);
        }
    }
}