using MVCSpa.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;

namespace MVCSpa.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db = new AppDbContext();


        [AllowAnonymous]
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Departments = new SelectList(db.Departments, "DepartmentId", "DepartmentName");

            return View();
         
        }

       


    }
}