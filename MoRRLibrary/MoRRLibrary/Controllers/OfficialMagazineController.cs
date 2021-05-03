using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoRRLibrary.Helpers;
using MoRRLibrary.Models;
using MoRRLibrary.ViewModels;

namespace MoRRLibrary.Controllers
{
    public class OfficialMagazineController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OfficialMagazine
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getOfficialMagazines()
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
            var publisher = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            var name = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
            var serialNumber = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt16(start) : 0;
            int recordsTotal = 0;
            var v = (from o in db.OfficialMagazines
                     select new
                     {
                         o.Id,
                         o.Publisher,
                         PublicationDate = o.PublicationDate,
                         o.Name,
                         DateEntered = o.DateEntered,
                         o.CodeNumber,
                         o.CabinetNumber,
                         o.ShelfNumber,
                         o.Quantity,
                         o.AvailableQuantity,
                         o.SerialNumber,
                         o.IsActive
                     }).Where(o => o.IsActive == true);

            if (!string.IsNullOrEmpty(name))
            {
                v = v.Where(a => a.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(publisher))
            {
                v = v.Where(a => a.Publisher.Contains(publisher));
            }
            if (!string.IsNullOrEmpty(serialNumber))
            {
                v = v.Where(a => a.SerialNumber.Contains(serialNumber));
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                //v = v.OrderBy(sortColumn + " " + sortColumnDir);

            }
            recordsTotal = v.Count();
            v = v.OrderByDescending(e => e.Id);
            var data = v.Skip(skip).Take(pageSize);
            var dataToBeSend = data.AsEnumerable().Select(o=>
            new
            {
                o.Id,
                o.Publisher,
                PublicationDate = o.PublicationDate,
                o.Name,
                DateEntered = o.DateEntered,
                o.CodeNumber,
                o.CabinetNumber,
                o.ShelfNumber,
                o.Quantity,
                o.AvailableQuantity,
                o.SerialNumber,
                o.IsActive
            }
            ).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = dataToBeSend },
                JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {    
                Console.Write(ex);
            }   
            return result;
        }

        // GET: OfficialMagazine/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OfficialMagazine/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Publisher,DateEntered,CabinetNumber,ShelfNumber,CodeNumber,PublicationDate,Quantity,SerialNumber,SerialNumber,IsActiv")] OfficialMagazine officialMagazine)
        {
            //var persianDateTime = new PersianDateTime();
            //if(officialMagazine.PublicationDate)
            if (ModelState.IsValid)
            {
                try
                {
                    OfficialMagazine obj = new OfficialMagazine();
                    obj.Publisher = officialMagazine.Publisher;
                    obj.Name = officialMagazine.Name;
                    obj.DateEntered = officialMagazine.DateEntered;
                    obj.CabinetNumber = officialMagazine.CabinetNumber;
                    obj.ShelfNumber = officialMagazine.ShelfNumber;
                    obj.CodeNumber = officialMagazine.CodeNumber;
                    obj.PublicationDate = officialMagazine.PublicationDate;
                    obj.Quantity = officialMagazine.Quantity;
                    obj.AvailableQuantity = officialMagazine.Quantity;
                    obj.SerialNumber = officialMagazine.SerialNumber;
                    obj.IsActive = true;
                    db.OfficialMagazines.Add(obj);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    if (ExceptionHelper.IsUniqueConstraintViolation(ex))
                    {
                        ModelState.AddModelError("CodeNumber", $"جریده به این کود نمبر در سیستم موجود است");
                        ModelState.AddModelError("SerialNumber", $"جریده به این سریال نمبر در سیستم موجود است");
                        return View(officialMagazine);
                    }
                }
            }

            return View(officialMagazine);
        }

        // GET: OfficialMagazine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficialMagazine officialMagazine = db.OfficialMagazines.Find(id);
            if (officialMagazine == null)
            {
                return HttpNotFound();
            }
            return View(officialMagazine);
        }

        // POST: OfficialMagazine/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Publisher,DateEntered,CabinetNumber,ShelfNumber,CodeNumber,PublicationDate,Quantity,AvailableQuantity,SerialNumber,IsActive")] OfficialMagazine officialMagazine)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(officialMagazine).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    if (ExceptionHelper.IsUniqueConstraintViolation(ex))
                    {
                        ModelState.AddModelError("CodeNumber", $"جریده به این کود نمبر در سیستم موجود است");
                        ModelState.AddModelError("SerialNumber", $"جریده به این سریال نمبر در سیستم موجود است");
                        return View(officialMagazine);
                    }
                }

            }
            return View(officialMagazine);
        }

        // POST: OfficialMagazine/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficialMagazine officialMagazine = db.OfficialMagazines.Find(id);
            if (officialMagazine == null)
            {
                return Json(new { success = false, responseText = "این ریکارد در سیستم موجود نیست" }, JsonRequestBehavior.AllowGet);
            }
            officialMagazine.IsActive = false;
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
