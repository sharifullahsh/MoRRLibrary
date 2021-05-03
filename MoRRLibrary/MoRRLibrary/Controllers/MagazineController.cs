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
    public class MagazineController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Magazine
        public ActionResult Index()
        {
            ViewBag.MagazineTypeId = new SelectList(db.MagazineTypes, "Id", "Name");
            return View();
        }

        public JsonResult getMagazines()
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
            var magazineName = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            var magazineTypeId = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt16(start) : 0;
            int recordsTotal = 0;
            var v = (from m in db.Magazines
                     select new
                     {
                         m.Id,
                         m.Name,
                         MagazineType = m.MagazineType.Name,
                         MagazineTypeId = m.MagazineTypeId,
                         m.Publisher,
                         PublicationDate = m.PublicationDate,
                         m.SerialNumber,
                         DateEntered = m.DateEntered,
                         m.CabinetNumber,
                         m.ShelfNumber,
                         m.Quantity,
                         m.AvailableQuantity,
                         m.IsActive
                     }).Where(m=>m.IsActive);

            if (!string.IsNullOrEmpty(magazineName))
            {
                v = v.Where(a => a.Name.Contains(magazineName));
            }
            if (!string.IsNullOrEmpty(magazineTypeId))
            {
                int mTypeId = int.Parse(magazineTypeId);
                v = v.Where(a => a.MagazineTypeId == mTypeId);
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                //v = v.OrderBy(sortColumn + " " + sortColumnDir);

            }
            recordsTotal = v.Count();
            v = v.OrderByDescending(e => e.Id);
            var data = v.Skip(skip).Take(pageSize);
            var DataToBeSend = data.AsEnumerable().Select(m=>
                new
                {
                    m.Id,
                    m.Name,
                    m.MagazineType,
                    m.MagazineTypeId,
                    m.Publisher,
                    PublicationDate = m.PublicationDate,
                    m.SerialNumber,
                    DateEntered = m.DateEntered,
                    m.CabinetNumber,
                    m.ShelfNumber,
                    m.Quantity,
                    m.AvailableQuantity,
                    m.IsActive
                }
            );
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = DataToBeSend },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info     
                Console.Write(ex);
            }     
            return result;
        }


        // GET: Magazine/Create
        public ActionResult Create()
        {
            ViewBag.MagazineTypeId = new SelectList(db.MagazineTypes, "Id", "Name");
            return View();
        }

        // POST: Magazine/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MagazineTypeId,Name,Publisher,SerialNumber,DateEntered,CabinetNumber,ShelfNumber,PublicationDate,Quantity,AvailableQuantity,IsActive")] Magazine magazine)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Magazine mObj = new Magazine();
                    mObj.MagazineTypeId = magazine.MagazineTypeId;
                    mObj.Name = magazine.Name;
                    mObj.Publisher = magazine.Publisher;
                    mObj.SerialNumber = magazine.SerialNumber;
                    mObj.DateEntered = magazine.DateEntered;
                    mObj.CabinetNumber = magazine.CabinetNumber;
                    mObj.ShelfNumber = magazine.ShelfNumber;
                    mObj.PublicationDate = magazine.PublicationDate;
                    mObj.Quantity = magazine.Quantity;
                    mObj.AvailableQuantity = magazine.Quantity;
                    mObj.IsActive = true;
                    db.Magazines.Add(mObj);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    if (ExceptionHelper.IsUniqueConstraintViolation(ex))
                    {
                        ModelState.AddModelError("SerialNumber", $"مجله به این سریال نمبر در سیستم موجود است");
                        ViewBag.MagazineTypeId = new SelectList(db.MagazineTypes, "Id", "Name", magazine.MagazineTypeId);
                        return View(magazine);
                    }
                }
            }

            ViewBag.MagazineTypeId = new SelectList(db.MagazineTypes, "Id", "Name", magazine.MagazineTypeId);
            return View(magazine);
        }

        // GET: Magazine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magazine magazine = db.Magazines.Find(id);
            if (magazine == null)
            {
                return HttpNotFound();
            }
            ViewBag.MagazineTypeId = new SelectList(db.MagazineTypes, "Id", "Name", magazine.MagazineTypeId);
            return View(magazine);
        }

        // POST: Magazine/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MagazineTypeId,Name,Publisher,SerialNumber,DateEntered,CabinetNumber,ShelfNumber,PublicationDate,Quantity,AvailableQuantity,IsActive")] Magazine magazine)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(magazine).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    if (ExceptionHelper.IsUniqueConstraintViolation(ex))
                    {
                        ModelState.AddModelError("SerialNumber", $"مجله به این سریال نمبر در سیستم موجود است");
                        ViewBag.MagazineTypeId = new SelectList(db.MagazineTypes, "Id", "Name", magazine.MagazineTypeId);
                        return View(magazine);
                    }
                }

            }
            ViewBag.MagazineTypeId = new SelectList(db.MagazineTypes, "Id", "Name", magazine.MagazineTypeId);
            return View(magazine);
        }

        // POST: Magazine/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Magazine magazine = db.Magazines.Find(id);
            if (magazine == null)
            {
                return HttpNotFound();
            }
            magazine.IsActive = false;
            db.SaveChanges();
            return RedirectToAction("Index");
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
