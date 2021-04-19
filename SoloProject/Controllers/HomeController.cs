using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoloProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace SoloProject.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }
        private RegisterUser userIn
        {
            get
            {
                return _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
            }
        }

        [HttpGet("menu")]
        public IActionResult Menu()
        {
            if(HttpContext.Session.GetInt32("userId") == null){
                return RedirectToAction("Login", "Access");
            }
            ViewBag.User = userIn.UserId;
            var model = _context.ProductUsers.Include(w => w.Product).Include(x => x.User).Where(x => x.UserId == userIn.UserId).Take(5)
                .ToList();
            
            return View(model);
        }

        [HttpGet("aboutus")]
        public IActionResult AboutUs()
        {
            if(HttpContext.Session.GetInt32("userId") == null){
                return RedirectToAction("Login", "Access");
            }
            ViewBag.User = userIn;
            var comment = _context.Comments.Include(u => u.User)
                .OrderByDescending(t => t.CommentId).Take(4).ToList();
            var model = new CommentList(){
                ListComment = comment
            };
            return View(model);
        }

        [HttpPost("newComment")]
        public IActionResult CreateComment(CommentList newComment)
        {
            if(HttpContext.Session.GetInt32("userId") == null){
                return RedirectToAction("Login", "Access");
            }
            if(ModelState.IsValid)
            {
                newComment.OneComment.UserId = userIn.UserId;
                _context.Comments.Add(newComment.OneComment);
                _context.SaveChanges();
                return RedirectToAction("AboutUs");
            }
            ViewBag.UserId = userIn.UserId;
            return View("AboutUs");
        }
    }
}
