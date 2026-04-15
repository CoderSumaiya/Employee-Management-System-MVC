using MVCSpa.DAL;
using MVCSpa.Models;
using MVCSpa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MVCSpa.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var roles = db.tblRoles.Include("tblUser").ToList();
            return View(roles);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RoleCreate()
        {
            UserRoleViewModel role = new UserRoleViewModel();
            role.UserList = db.tblUsers.ToList();
            ViewBag.UserId = new SelectList(role.UserList, "Id", "UserName", role.UserId);

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult RoleCreate(UserRoleViewModel vobj)
        {
            if (!ModelState.IsValid)
            {
                vobj.UserList = db.tblUsers.ToList();
                ViewBag.UserId = new SelectList(vobj.UserList, "Id", "UserName", vobj.UserId);
                return View(vobj);
            }

            var userExists = db.tblUsers.Any(u => u.Id == vobj.UserId);
            if (!userExists)
            {
                ModelState.AddModelError("UserId", "Selected user not found.");
                vobj.UserList = db.tblUsers.ToList();
                ViewBag.UserId = new SelectList(vobj.UserList, "Id", "UserName", vobj.UserId);
                return View(vobj);
            }

            tblRole role = new tblRole
            {
                RoleName = vobj.RoleName,
                UserId = vobj.UserId
            };

            db.tblRoles.Add(role);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}