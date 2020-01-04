using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Commerce1.Models;

namespace Commerce1.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
     
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
             if(ModelState.IsValid)
            {
                
                // If a User exists with provided email
                if(dbContext.Users.Any(u => u.Email == user.Email))
                {
                // Manually add a ModelState error to the Email field, with provided
                // error message
                 ModelState.AddModelError("Email", "Email already in use!");
            
                 // You may consider returning to the View at this point
                 return View("Index");
                }
        
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                dbContext.Add(user);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("loggedinUser", user.UserId);
                return Redirect("/home");
                
                
            }
            
            else 
            {
                return View("Index");
            }
        }
        [HttpPost("login")]
        public IActionResult Login(LoginUser userSubmission)
        {
            if(ModelState.IsValid)
            {
                // If inital ModelState is valid, query for a user with provided email
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == userSubmission.Email);
                

                
                // If no user exists with provided email
                if(userInDb == null)
                {
                    // Add an error to ModelState and return to View!
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Index");
                }
                
                // Initialize hasher object
                var hasher = new PasswordHasher<LoginUser>();
                
                // verify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);
                
                // result can be compared to 0 for failure
                if(result == 0)
                {
                    // handle failure (this should be similar to how "existing email" is handled)
                    ModelState.AddModelError("Password", "Incorrect Password");
                    return View("Index");
                }
            
                HttpContext.Session.SetInt32("loggedinUser", userInDb.UserId);
                return Redirect("/home");
                
            }
            return View("Index");
        }

        [HttpGet("/home")]
        public IActionResult Home()
        {
              int? session = HttpContext.Session.GetInt32("loggedinUser");
             
            if (session == null)
            {
                
                return RedirectToAction("Index");
            }

            ViewBag.LoggedIn = dbContext.Users.FirstOrDefault(i => i.UserId == (int)session);
            
            return View();
            
        }
        [HttpPost("/post")]
        public IActionResult Plan(Beat beat)
        {
            int? session = HttpContext.Session.GetInt32("loggedinUser");
             if(ModelState.IsValid)
            {
                              
                    beat.UserId = (int)session;
                    dbContext.Add(beat);
                    dbContext.SaveChanges();
                   
                return Redirect("/beats");
            }
            else
                {
                    return View("Home");
                }
            
        }

        [HttpGet("/beats")]
        public IActionResult Info(int BeatId)
        {
            int? session = HttpContext.Session.GetInt32("loggedinUser");
            
            if (session == null)
            {
                HttpContext.Session.Clear();
                return View("Index");
            }

            ViewBag.UserBeats = dbContext.Beats.Where(i => i.UserId == (int)session);
            ViewBag.LoggedIn = dbContext.Users.FirstOrDefault(i => i.UserId == (int)session);
            
            return View("Info");
        }

    }
}
