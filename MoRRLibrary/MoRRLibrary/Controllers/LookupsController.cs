using MoRRLibrary.Models;
using MoRRLibrary.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoRRLibrary.Controllers
{
    public class LookupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Lookups
        public ActionResult Index()
        {
            LookupsViewModel model = new LookupsViewModel()
            {
                Language = new Language(),
                Department =  new Department()

            };
            return View(model);
        }

        [HttpGet]
        public ActionResult getLanguages()
        {
            // Initialization.     
            JsonResult result = new JsonResult();
            try
            {
                var languages = (from l in db.Languages
                     select new
                     {
                         l.Id,
                         l.Name,
                         l.IsActive
                     }).OrderBy(l=>l.Id).Where(l=>l.IsActive == true);
            return Json(new { data = languages },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }     
            return result;

        }

        [HttpGet]
        public ActionResult getLanguage(int Id) {
            Language lang = db.Languages.Where(l => l.Id == Id && l.IsActive == true).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(lang, Formatting.Indented, new JsonSerializerSettings {
                ReferenceLoopHandling =  ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public ActionResult SaveLanguage(Language language)
        {
            try
            {
                if(language.Id > 0 && ModelState.IsValid)
                {
                    Language lang = db.Languages.Where(l => l.Id == language.Id && l.IsActive == true).SingleOrDefault();
                    if(lang == null)
                    {
                        return Json(new { success = false, responseText = "این زبان در سیستم ثبت نیست" }, JsonRequestBehavior.AllowGet);
                    }
                    lang.Name = language.Name;
                    db.SaveChanges();
                    return Json(new { success = true, responseText = "نام زبان موافقانه تغیر داده شود" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Language lang = new Language();
                    lang.Name = language.Name;
                    lang.IsActive = true;
                    db.Languages.Add(lang);
                    db.SaveChanges();
                    return Json(new { success = true, responseText = "زبان موافقانه به سیستم اضافه شود" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e) {
                return Json(new { success = false, responseText = "سیستم در اضافه نمودن زبان مشکل دارد" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult DeleteLanguage(int? Id)
        {
            if(Id == null)
            {
                return Json(new { success = false, responseText = "این زبان در سیستم موجود نیست" }, JsonRequestBehavior.AllowGet);
            }
            Language lang = db.Languages.SingleOrDefault(l => l.Id == Id);
            if (lang == null)
            {
                return Json(new { success = false, responseText = "این زبان در سیستم موجود نیست" }, JsonRequestBehavior.AllowGet);
            }
            lang.IsActive = false;
            db.SaveChanges();
            return Json(new { success = false, responseText = "زبان موافقانه خذب شود" }, JsonRequestBehavior.AllowGet);

        }


        //Department start
        [HttpGet]
        public ActionResult getDepartments()
        {
            // Initialization.     
            JsonResult result = new JsonResult();
            try
            {
                var departments = (from d in db.Departments
                             select new
                             {
                                 d.Id,
                                 d.Name,
                                 d.IsActive
                             }).OrderBy(d => d.Id).Where(d => d.IsActive == true);
            return Json(new { data = departments },
                JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            { 
                Console.Write(ex);
            }     
            return result;

        }
        [HttpGet]
        public ActionResult getDepartment(int Id)
        {
            Department department = db.Departments.Where(d => d.Id == Id && d.IsActive == true).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(department, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveDepartment(Department department)
        {
            try
            {
                if (department.Id != 0 && ModelState.IsValid)
                {
                    Department dep = db.Departments.Where(d => d.Id == department.Id && d.IsActive == true).SingleOrDefault();
                    if (dep == null)
                    {
                        return Json(new { success = false, responseText = "این ریاست در سیستم ثبت نیست" }, JsonRequestBehavior.AllowGet);
                    }
                    dep.Name = department.Name;
                    db.SaveChanges();
                    return Json(new { success = true, responseText = "نام ریاست موافقانه تغیر داده شود" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Department dep = new Department();
                    dep.Name = department.Name;
                    dep.IsActive = true;
                    db.Departments.Add(dep);
                    db.SaveChanges();
                    return Json(new { success = true, responseText = "ریاست موافقانه به سیستم اضافه شود" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, responseText = "سیستم در اضافه نمودن زبان مشکل دارد" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult DeleteDepartment(int? Id)
        {
            if (Id == null)
            {
                return Json(new { success = false, responseText = "این ریاست در سیستم موجود نیست" }, JsonRequestBehavior.AllowGet);
            }
            Department dep = db.Departments.SingleOrDefault(d => d.Id == Id);
            if (dep == null)
            {
                return Json(new { success = false, responseText = "این ریاست در سیستم موجود نیست" }, JsonRequestBehavior.AllowGet);
            }
            dep.IsActive = false;
            db.SaveChanges();
            return Json(new { success = false, responseText = "ریاست موافقانه خذب شود" }, JsonRequestBehavior.AllowGet);

        }
    }
}
