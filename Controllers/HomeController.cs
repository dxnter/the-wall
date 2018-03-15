using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DbConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using the_wall.Models;

namespace the_wall.Controllers {
    public class HomeController : Controller {
        [HttpGet]
        [Route("wall")]
        public IActionResult Index() {
            if (HttpContext.Session.GetInt32("userID") != null) {
                ViewBag.currentUser = DbConnector.Query($"SELECT * FROM users WHERE id = {HttpContext.Session.GetInt32("userID")}");
                ViewBag.allMessages = DbConnector.Query($"SELECT messages.message, messages.id, messages.created_at, users.FirstName, users.LastName FROM messages INNER JOIN users ON messages.user_id=users.id ORDER BY created_at DESC");
                ViewBag.allComments = DbConnector.Query($"SELECT comments.comment, comments.created_at, comments.message_id, users.FirstName, users.LastName, comments.user_id, users.id FROM comments JOIN users ON users.id = user_id ORDER BY created_at DESC");
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
            int userID = (int) HttpContext.Session.GetInt32("userID");
            string newMessageQuery = $"INSERT INTO messages (message, created_at, updated_at, user_id) VALUES ('{Message}', NOW(), NOW(), {userID})";
            DbConnector.Execute(newMessageQuery);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult NewComment(string Comment, int id) {
            int userID = (int) HttpContext.Session.GetInt32("userID");
            string newCommentQuery = $"INSERT INTO comments (comment, created_at, updated_at, user_id, message_id) VALUES ('{Comment}', NOW(), NOW(), {userID}, {id})";
            DbConnector.Execute(newCommentQuery);
            return RedirectToAction("Index");
        }
    }
}