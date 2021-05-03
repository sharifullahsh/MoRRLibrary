using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoRRLibrary.Models;

namespace MoRRLibrary.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Book
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult getBooks()
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
            var bookName = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            var auther = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            var serialNumber = Request.Form.GetValues("columns[5][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt16(start) : 0;
            int recordsTotal = 0;
            var v = (from b in db.Books select new {
                b.Id,
                b.Name,
                b.Author,
                Language = b.Language.Name,
                b.CabinetNumber,
                b.ShelfNumber,
                b.SerialNumber,
                b.Translator,
                b.Publisher,
                b.Quantity,
                b.AvailableQuantity,
                LanguageId = b.Language.Id,
                b.IsActive
            }).Where(b => b.IsActive == true);

            if (!string.IsNullOrEmpty(bookName))
            {
                v = v.Where(a => a.Name.Contains(bookName));
            }
            if (!string.IsNullOrEmpty(auther))
            {
                v = v.Where(a => a.Author.Contains(auther));
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
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data },
                JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                     
                Console.Write(ex);
            }    
            return result;
        }

        [HttpGet]
        public ActionResult ViewBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Include(b=>b.Language).SingleOrDefault(b=>b.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ViewBook",book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Name");
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Author,Translator,Publisher,LanguageId,CabinetNumber,ShelfNumber,Quantity,SerialNumber")] Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Book bookObj = new Book();
                    bookObj.Name = book.Name;
                    bookObj.Author = book.Author;
                    bookObj.Translator = book.Translator;
                    bookObj.Publisher = book.Publisher;
                    bookObj.LanguageId = book.LanguageId;
                    bookObj.CabinetNumber = book.CabinetNumber;
                    bookObj.ShelfNumber = book.ShelfNumber;
                    bookObj.Quantity = book.Quantity;
                    bookObj.AvailableQuantity = book.Quantity;
                    bookObj.SerialNumber = book.SerialNumber;
                    bookObj.IsActive = true;
                    db.Books.Add(bookObj);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    if (ExceptionHelper.IsUniqueConstraintViolation(ex))
                    {
                        ModelState.AddModelError("EmployeeCode", $"کتاب به این سریال نمبر در سیستم موجود است");
                        ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Name", book.LanguageId);
                        return View(book);
                    }
                }
                
            }
            var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
            ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Name", book.LanguageId);
            return View(book);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Name", book.LanguageId);
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Author,Translator,Publisher,LanguageId,CabinetNumber,ShelfNumber,Quantity,AvailableQuantity,SerialNumber,IsActive")] Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(book).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    if (ExceptionHelper.IsUniqueConstraintViolation(ex))
                    {
                        ModelState.AddModelError("EmployeeCode", $"کتاب به این سریال نمبر در سیستم موجود است");
                        ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Name", book.LanguageId);
                        return View(book);
                    }
                }

            }
            ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Name", book.LanguageId);
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            Book book = db.Books.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (book == null)
            {
                return Json(new { success = false, responseText = "این ریکارد در سیستم موجود نیست" }, JsonRequestBehavior.AllowGet);
            }
            book.IsActive = false;
            db.Books.Remove(book);
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
