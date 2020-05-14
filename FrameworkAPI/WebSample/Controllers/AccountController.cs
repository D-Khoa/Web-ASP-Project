using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using WebSample.Models.Library;
using WebSample.Models.User;

namespace WebSample.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLog user)
        {
            //Mã hóa mật khẩu
            if (!string.IsNullOrEmpty(user.password))
            {
                user.password = user.password.EncryptString();
                user.confirmPassword = user.password;
            }
            //Chuyển đổi thông tin user sang dạng json
            string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            //Khai báo client api
            var client = new RestClient(WebAPIUrl.apiURL + "account/login");
            client.Timeout = -1;
            //Khai báo dạng request là put
            var request = new RestRequest(Method.OPTIONS);
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
                WebAPIUrl.userName = user.username;
                WebAPIUrl.userToken = user.userToken;
                return RedirectToAction("Login");
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
        public ActionResult Logout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Logout(UserLog user)
        {
            if (user.userOnline)
            {
                user.userOnline = false;
                //Chuyển đổi thông tin user sang dạng json
                string jsonUser = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                //Khai báo client api
                var client = new RestClient(WebAPIUrl.apiURL + "account/UpdateUser");
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
                    WebAPIUrl.userName = user.username;
                    WebAPIUrl.userToken = user.userToken;
                    return RedirectToAction("Login");
                }
                else
                {
                    ErrorMessage errorMessage = new ErrorMessage();
                    errorMessage = errorMessage.jsonConvert(response.Content);
                    ModelState.AddModelError("Error", errorMessage.Message);
                }
                //Không chuyển trang khi lỗi
                return View();
            }
            else return RedirectToAction("Login");
        }
    }
}