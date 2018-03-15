using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DbConnection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using the_wall.Models;

namespace the_wall.Controllers {
    public class HomeController : Controller {
        [HttpGet]
        [Route("wall")]
        public IActionResult Index() {
            if (HttpContext.Session.GetInt32("userID") != null) {
                string allMessages = $"SELECT * FROM messages";
                string allComments = $"SELECT * FROM comments";

                ViewBag.allMessages = DbConnector.Execute(allMessages);
                return View("Wall");
            }
            return RedirectToAction("Index", "Login");
        }

        public IActionResult SignOut() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult NewMessage(string Message) {
            int userID = (int)HttpContext.Session.GetInt32("userID");
            string newMessageQuery = $"INSERT INTO messages (message, created_at, updated_at, user_id) VALUES ('{Message}', NOW(), NOW(), {userID})";
            DbConnector.Execute(newMessageQuery);
            return RedirectToAction("Index");
        }
    }
}