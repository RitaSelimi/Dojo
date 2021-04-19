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

namespace SoloProject.Controllers
{
    public class AccessController : Controller
    {
        private MyContext _context;

        public AccessController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register2(RegisterUser user)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Register");
                }
                PasswordHasher<RegisterUser> Hasher = new PasswordHasher<RegisterUser>();
                user.Password = Hasher.HashPassword(user, user.Password);

                var newUser = _context.Users.Add(user).Entity;
                _context.SaveChanges();

                HttpContext.Session.SetInt32("userId", newUser.UserId);

                return RedirectToAction("AboutUs", "Home");
            }
            return View("Register");
        }

        [HttpPost("login")]
        public IActionResult Login2(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                RegisterUser userLog = _context.Users.FirstOrDefault(u => u.Email == user.LoginEmail);
                if (userLog == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                    return View("Login");
                }
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                
                var result = hasher.VerifyHashedPassword(user, userLog.Password, user.LoginPassword);

                if(result == PasswordVerificationResult.Failed){
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                    return View("Login");
                }
                HttpContext.Session.SetInt32("userId", userLog.UserId);
                return RedirectToAction("AboutUs", "Home");
            }
            return View("Login");
        }

        [HttpGet("logout")]
        public RedirectToActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Register");
        }
    }
}
