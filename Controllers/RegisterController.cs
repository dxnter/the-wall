using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DbConnection;
using Microsoft.AspNetCore.Mvc;
using the_wall.Models;

namespace the_wall.Controllers {
    public class RegisterController : Controller {

        [HttpGet]
        [Route("")]
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        [Route("/create")]
        public IActionResult Create(Register newUser) {
            if (ModelState.IsValid) {
                string emailExistsQuery = $"SELECT * FROM users WHERE Email = '{newUser.Email}'";
                var matchedEmails = DbConnector.Query(emailExistsQuery);
                if (matchedEmails.Count > 0) {
                    ViewBag.emailExists = "A user with that email already exists";
                    return View("Index");
                }
                DbConnector.Execute($"INSERT INTO users (FirstName, LastName, Email, Password) VALUES ('{newUser.firstName}', '{newUser.lastName}', '{newUser.Email}', '{newUser.Password}')");
                return RedirectToAction("Index", "Wall");
            }
            return View("Wall");
        }
    }
}