using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.ViewModels;
using WebApplication1.Models;
using System.Data.Entity.Validation;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            Session["UserId"] = 0;
            return View();
        }

        // GET: Account/Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public ActionResult Register(RegisterVM obj)
        {
            bool UserExistis = db.Users.Any(x => x.Username == obj.Username);
            if(UserExistis)
            {
                ViewBag.UsernameMessage = "This username is already in use, try another";
                return View();
            }
            bool EmailExistis = db.Users.Any(y => y.Email == obj.Email);
            if(EmailExistis)
            {
                ViewBag.EmailMessage = "This Email is already in use, try another";
                return View();
            }
            User u = new User();
            u.Username = obj.Username;
            u.Password = obj.Password;
            u.Email = obj.Email;
            u.ImageUrl = "";
            u.CreatedOn = DateTime.Now;
            db.Users.Add(u);
            db.SaveChanges();

            return RedirectToAction("Index", "ChatRoom");
        }

        // GET: Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(LoginVM obj)
        {
            bool exists = db.Users.Any(u => u.Username == obj.Username && u.Password == obj.Password);
            if (exists)
            {
                Session["UserId"] = db.Users.Single(x => x.Username == obj.Username).Id;
                return RedirectToAction("Index", "ChatRoom");
            }
            ViewBag.Message = "Invalid Credentials!";
            return View();
        }

        // GET: Account/Logout
        [HttpGet]
        public ActionResult Logout()
        {
            Session["UserId"] = 0;
            return RedirectToAction("Index","Home");
        }



    }
}