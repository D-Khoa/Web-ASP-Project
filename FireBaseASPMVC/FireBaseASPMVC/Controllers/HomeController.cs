using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using FireBaseASPMVC.Models;

namespace FireBaseASPMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [Authorize]
        public async Task<ActionResult> About()
        {
            //Get Okta user data
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var userEmail = claims.Where(x => x.Type == "email").FirstOrDefault().Value;
            var userOktaId = claims.Where(x => x.Type == "sub").FirstOrDefault().Value;
            var currentLoginTime = DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss");

            //Save non identifying data to Firebase
            var currentUserLogin = new LoginData() { TimestampUTC = currentLoginTime };
            var firebaseClient = new FirebaseClient("https://pencil-76ef8.firebaseio.com/");
            var result = await firebaseClient
              .Child("Users/" + userOktaId + "/Logins")
              .PostAsync(currentUserLogin);

            //Retrieve data from Firebase
            var dbLogins = await firebaseClient
              .Child("Users")
              .Child(userOktaId)
              .Child("Logins")
              .OnceAsync<LoginData>();

            var timestampList = new List<DateTime>();

            //Convert JSON data to original datatype
            foreach (var login in dbLogins)
            {
                timestampList.Add(Convert.ToDateTime(login.Object.TimestampUTC).ToLocalTime());
            }

            //Pass data to the view
            ViewBag.CurrentUser = userEmail;
            ViewBag.Logins = timestampList.OrderByDescending(x => x);
            return View();
        }
    }
}
