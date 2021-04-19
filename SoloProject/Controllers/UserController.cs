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
    public class UserController : Controller
    {
        private MyContext _context;

        public UserController(MyContext context)
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
        [HttpGet("edit")]
        public IActionResult EditAccount()
        {
            if(HttpContext.Session.GetInt32("userId") == null){
                return RedirectToAction("Login", "Access");
            }
            ViewBag.User = userIn.UserId;
            RegisterUser model = _context.Users.FirstOrDefault(d => d.UserId == userIn.UserId);
            if(model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost("updateUsers")]
        public IActionResult UpdateUser(EditUser user)
        {
            RegisterUser toUpdate = _context.Users.FirstOrDefault(d => d.UserId == userIn.UserId);
            if (ModelState.IsValid)
            {
                toUpdate.FirstName = user.FirstName;
                toUpdate.LastName = user.LastName;
                toUpdate.Email = user.Email;
                toUpdate.Address = user.Address;
                toUpdate.State = user.State;
                toUpdate.City = user.City;
                toUpdate.UpdatedAt = DateTime.Now;

                _context.SaveChanges();
                return RedirectToAction("EditAccount");
            }
            return View("EditAccount", user);
        }

        [HttpPost("updatePasss")]
        public IActionResult UpdatePass(EditUser user)
        {
            RegisterUser u = _context.Users.FirstOrDefault(d => d.UserId == user.UserId);
            RegisterUser toUpdate = _context.Users.FirstOrDefault(d => d.UserId == userIn.UserId);
            PasswordHasher<RegisterUser> Hasher = new PasswordHasher<RegisterUser>();
            if (ModelState.IsValid)
            {
                toUpdate.Password = user.Password = Hasher.HashPassword(u, user.Password);

                _context.SaveChanges();
                return RedirectToAction("EditAccount");
            }
            return View("EditAccount", user);
        }
        
        
    }
}
