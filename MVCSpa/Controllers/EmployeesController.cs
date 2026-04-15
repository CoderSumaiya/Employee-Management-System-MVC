using MVCSpa.DAL;
using MVCSpa.Models;
using MVCSpa.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSpa.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        public ActionResult Index(string sortOrder, string currentFilter, string
        searchString, int? page)
        {
            //IEnumerable<Employee> employees = db.Employees
            //    .Include(d => d.Department)
            //    .Include(a => a.AcademicDetails)
            //    .ToList();

            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {

                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var employees = from e in db.Employees
                           select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e =>
                e.EmployeeName.ToUpper().Contains(searchString.ToUpper()));
              
            }
            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.EmployeeName);
                    break;
                case "Date":
                    employees = employees.OrderBy(e => e.JoiningDate);
                    break;
                case "date_desc":
                    employees = employees.OrderByDescending(e => e.JoiningDate);
                    break;
                default:
                    employees = employees.OrderBy(s => s.EmployeeName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(employees.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public JsonResult CheckMobileExists(string MobileNo, int? EmployeeId)
        {
            bool exists = db.Employees.Any(e => e.MobileNo == MobileNo && e.EmployeeId != EmployeeId);

            if (exists)
            {
                return Json("Mobile number is already in use.", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult CreatePartial()
        {
            EmployeeViewModel employee = new EmployeeViewModel
            {
                Departments = db.Departments.ToList(),
                AcademicDetails = new List<AcademicDetail>()
            };

            employee.AcademicDetails.Add(new AcademicDetail());
            return PartialView("_CreateEmployeePartial", employee);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEmployee(EmployeeViewModel vobj)
        {
            if (vobj.AcademicDetails == null)
            {
                vobj.AcademicDetails = new List<AcademicDetail>();
            }

            if (!ModelState.IsValid)
            {
                vobj.Departments = db.Departments.ToList();
                return PartialView("_CreateEmployeePartial", vobj);
            }

            Employee employee = new Employee
            {
                EmployeeName = vobj.EmployeeName,
                JoiningDate = vobj.JoiningDate,
                MobileNo = vobj.MobileNo,
                Salary = vobj.Salary,
                DepartmentId = vobj.DepartmentId,
                IsPermanent = vobj.IsPermanent,
                AcademicDetails = vobj.AcademicDetails
            };

            if (vobj.ProfileFile != null)
            {
                string uniqueFileName = GetFileName(vobj.ProfileFile);
                employee.ImageUrl = uniqueFileName;
            }
            else
            {
                employee.ImageUrl = "noimage.png";
            }

            db.Employees.Add(employee);

            try
            {
                db.SaveChanges();
                return Json(new { success = true, redirectUrl = Url.Action("Index") });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                vobj.Departments = db.Departments.ToList();
                return PartialView("_CreateEmployeePartial", vobj);
            }
        }

        private string GetFileName(HttpPostedFileBase file)
        {
            string fileName = "";
            if (file != null)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string path = Path.Combine(Server.MapPath("~/images/"), fileName);
                file.SaveAs(path);
            }
            return fileName;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);

            if (employee != null)
            {
                var academics = db.AcademicDetail.Where(e => e.EmployeeId == id).ToList();
                db.AcademicDetail.RemoveRange(academics);

                db.Entry(employee).State = EntityState.Deleted;
                db.SaveChanges();

                return Json(new { success = true, redirectUrl = Url.Action("Index") });
            }

            return Json(new { success = false, message = "Employee not found." });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditPartial(int id)
        {
            var employee = db.Employees
                .Include(d => d.Department)
                .Include(a => a.AcademicDetails)
                .FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
            {
                return HttpNotFound("Employee not found.");
            }

            var vObj = new EmployeeViewModel
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                JoiningDate = employee.JoiningDate,
                MobileNo = employee.MobileNo,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId,
                IsPermanent = employee.IsPermanent,
                ImageUrl = employee.ImageUrl,
                AcademicDetails = employee.AcademicDetails != null
                    ? employee.AcademicDetails.ToList()
                    : new List<AcademicDetail>(),
                Departments = db.Departments.ToList()
            };

            if (vObj.AcademicDetails.Count == 0)
            {
                vObj.AcademicDetails.Add(new AcademicDetail());
            }

            return PartialView("_EditEmployeePartial", vObj);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(EmployeeViewModel vobj, string OldImageUrl)
        {
            if (vobj.AcademicDetails == null)
            {
                vobj.AcademicDetails = new List<AcademicDetail>();
            }

            if (!ModelState.IsValid)
            {
                vobj.Departments = db.Departments.ToList();
                return PartialView("_EditEmployeePartial", vobj);
            }

            Employee obj = db.Employees
                .Include(a => a.AcademicDetails)
                .FirstOrDefault(e => e.EmployeeId == vobj.EmployeeId);

            if (obj == null)
            {
                return Json(new { success = false, message = "Employee not found." });
            }

            obj.EmployeeName = vobj.EmployeeName;
            obj.DepartmentId = vobj.DepartmentId;
            obj.MobileNo = vobj.MobileNo;
            obj.Salary = vobj.Salary;
            obj.IsPermanent = vobj.IsPermanent;
            obj.JoiningDate = vobj.JoiningDate;

            if (vobj.ProfileFile != null)
            {
                obj.ImageUrl = GetFileName(vobj.ProfileFile);
            }
            else
            {
                obj.ImageUrl = OldImageUrl;
            }

            var incomingIds = vobj.AcademicDetails
                .Where(a => a.AcademicDetailId > 0)
                .Select(a => a.AcademicDetailId)
                .ToList();

            var academicsToRemove = obj.AcademicDetails
                .Where(a => !incomingIds.Contains(a.AcademicDetailId))
                .ToList();

            foreach (var academicToRemove in academicsToRemove)
            {
                db.AcademicDetail.Remove(academicToRemove);
            }

            foreach (var item in vobj.AcademicDetails)
            {
                if (item.AcademicDetailId > 0)
                {
                    var existingAcademic = obj.AcademicDetails
                        .FirstOrDefault(a => a.AcademicDetailId == item.AcademicDetailId);

                    if (existingAcademic != null)
                    {
                        existingAcademic.DegreeName = item.DegreeName;
                        existingAcademic.CGPA = item.CGPA;
                    }
                }
                else
                {
                    obj.AcademicDetails.Add(new AcademicDetail
                    {
                        DegreeName = item.DegreeName,
                        CGPA = item.CGPA
                    });
                }
            }

            try
            {
                db.SaveChanges();
                return Json(new { success = true, message = "Employee updated successfully!" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                vobj.Departments = db.Departments.ToList();
                return PartialView("_EditEmployeePartial", vobj);
            }
        }
    }
}