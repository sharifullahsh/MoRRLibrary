using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoRRLibrary.Models;
using MoRRLibrary.ViewModels;

namespace MoRRLibrary.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            var model = new EmployeeViewModel();
            return View(model);
        }
        public JsonResult getEmployees()
        {
            // Initialization.     
            JsonResult result = new JsonResult();
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault()
                                    + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var employeeName = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            var employeeCode = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            var depId = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt16(start) : 0;
            int recordsTotal = 0;
            var v = (from e in db.Employees
                     join d in db.Departments on e.DepartmentId equals d.Id into employeeWithDepartment
                     from empDep in employeeWithDepartment.DefaultIfEmpty()
                     select new
                     {
                         e.Id,
                         e.Name,
                         e.EmployeeCode,
                         e.Designation,
                         DepartmentName = empDep.Name,
                         DepartmentId =(int?) empDep.Id,
                         IsEmployee = e.IsEmployee == true ? "بلی" : "نخیر",
                         e.Phone,
                         e.IsActive
                     }).Where(e => e.IsActive == true);

            if (!string.IsNullOrEmpty(employeeCode))
            {
                v = v.Where(a => a.EmployeeCode.Contains(employeeCode));
            }
            if (!string.IsNullOrEmpty(employeeName))
            {
                v = v.Where(a => a.Name.Contains(employeeName));
            }
            if (!string.IsNullOrEmpty(depId))
            {
                var departmentId = int.Parse(depId);
                v = v.Where(a => a.DepartmentId == departmentId);
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                //v = v.OrderBy(sortColumn + " " + sortColumnDir); 
            }
            recordsTotal = v.Count();
            v = v.OrderByDescending(e => e.Id);
            var data = v.Skip(skip).Take(pageSize);
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }    
            return result;
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments.ToList(), "Id", "Name");
            Employee model = new Employee() { IsEmployee = true };
            return View(model);
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Employee emp = new Employee();
                    emp.EmployeeCode = employee.EmployeeCode;
                    emp.Name = employee.Name;
                    emp.Designation = employee.Designation;
                    emp.Phone = employee.Phone;
                    emp.IsActive = true;
                    emp.IsEmployee = employee.IsEmployee;
                    emp.DepartmentId = employee.DepartmentId;
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    if (ExceptionHelper.IsUniqueConstraintViolation(ex))
                    {
                        ModelState.AddModelError("EmployeeCode", $"کارمند به این کود نمبر در سیستم موجود است");
                        ViewBag.DepartmentId = new SelectList(db.Departments.ToList(), "Id", "Name");
                        return View(employee);
                    }
                }
            }
            ViewBag.DepartmentId = new SelectList(db.Departments.ToList(), "Id", "Name");
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    if (ExceptionHelper.IsUniqueConstraintViolation(ex))
                    {
                        ModelState.AddModelError("EmployeeCode", $"کارمند به این کود نمبر در سیستم موجود است");
                        ViewBag.DepartmentId = new SelectList(db.Departments.ToList(), "Id", "Name");
                        return View(employee);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Employee employee = db.Employees.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (employee == null)
            {
                return Json(new { success = false, responseText = "این ریکارد در سیستم موجود نیست" }, JsonRequestBehavior.AllowGet);
            }
            employee.IsActive = false;
            db.SaveChanges();
            return Json(new { success = true, responseText = "ریکارد موافقانه حذب شد" }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
