using MVCSpa.DAL;
using MVCSpa.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCSpa.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
          private readonly AppDbContext db = new AppDbContext();
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<tblUser> list = db.tblUsers.ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tblUser user)
        {
            var count = db.tblUsers.Where(u => u.UserName == user.UserName && u.Password == user.Password).Count();
            if (count <= 0)
            {
                ViewBag.Errors = "Invalid username or password";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return RedirectToAction("Index", "Employees");
            }
           


        }
        [HttpGet]

        public ActionResult Register()
        {
         
            var roles = new List<SelectListItem>
    {
        new SelectListItem { Value = "Admin", Text = "Admin" },
        new SelectListItem { Value = "User", Text = "User" }
    };

            ViewBag.RoleList = new SelectList(roles, "Value", "Text");

            return View();
            

        }
        [HttpPost]
        public ActionResult Register(tblUser user)
        {
            if (ModelState.IsValid)
            {
           
                db.tblUsers.Add(user);
                db.SaveChanges();

                return RedirectToAction("Login", "Account");
            }

      
            ViewBag.RoleList = new SelectList(db.tblRoles.ToList(), "RoleId", "RoleName");
            return View(user);
          
       
        }
        public ActionResult Logout()
        {
           
            FormsAuthentication.SignOut();

          
            return RedirectToAction("Login", "Account");
        }

    }
}