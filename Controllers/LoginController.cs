using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DbConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using the_wall.Models;

namespace the_wall.Controllers {
    public class LoginController : Controller {

        [HttpGet]
        [Route("/login")]
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Register user) {
            string userAttempting = $"SELECT * FROM users WHERE Email = '{user.Email}'";
            var matchedUser = DbConnector.Query(userAttempting);
            if (matchedUser.Count > 0) {
                string comparePasswordQuery = $"SELECT * FROM users WHERE Password = '{user.Password}'";
                var matchedPassword = DbConnector.Query(comparePasswordQuery);
                if (matchedPassword.Count > 0) {
                    int userID = (int) matchedPassword[0]["id"];
                    HttpContext.Session.SetInt32("userID", userID);
                    return RedirectToAction("Index", "Wall");
                } else {
                    TempData["incorrectPassword"] = "Password is incorrect";
                }
            } else {
                TempData["invalidEmail"] = "Invalid email";
            }
            return RedirectToAction("Index");
        }
    }
}