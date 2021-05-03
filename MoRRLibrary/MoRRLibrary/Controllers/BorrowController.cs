using MD.PersianDateTime;
using MoRRLibrary.Helpers;
using MoRRLibrary.Models;
using MoRRLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace MoRRLibrary.Controllers
{
    public class BorrowController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        int bookTypeId;
        int magazineTypeId;
        int officialMagazineTypeId;
        public BorrowController()
        {
            bookTypeId = db.ItemTypes.Where(t => t.Name == "کتاب").Select(t => t.Id).FirstOrDefault();
            magazineTypeId = db.ItemTypes.Where(t => t.Name == "مجله").Select(t => t.Id).FirstOrDefault();
            officialMagazineTypeId = db.ItemTypes.Where(t => t.Name == "جریده").Select(t => t.Id).FirstOrDefault();
        }
        // GET: Borrow
        public ActionResult Index()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            var model = new EmployeeViewModel();
            return View(model);
        }

        public ActionResult BorrowItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var selectedEmployee = (from e in db.Employees
                                    join d in db.Departments on e.DepartmentId equals d.Id
                                    select new
                                    {
                                        e.Id,
                                        e.Name,
                                        e.EmployeeCode,
                                        e.Designation,
                                        e.Phone,
                                        e.IsActive,
                                        e.IsEmployee,
                                        DepartmentId = d.Id,
                                        DepartmentName = d.Name,
                                    }).FirstOrDefault(e => e.Id == id);
            if (selectedEmployee == null)
            {
                return HttpNotFound();
            }
            var model = new BorrowPublicationViewModel();
            var employee = new EmployeeViewModel();
            if(selectedEmployee != null)
            {
                employee.Id = selectedEmployee.Id;
                employee.Name = selectedEmployee.Name;
                employee.EmployeeCode = selectedEmployee.EmployeeCode;
                employee.Designation = selectedEmployee.Designation;
                employee.Phone = selectedEmployee.Phone;
                employee.IsActive = selectedEmployee.IsActive;
                employee.IsEmployee = selectedEmployee.IsEmployee;
                employee.DepartmentId = selectedEmployee.DepartmentId;
                employee.DepartmentName = selectedEmployee.DepartmentName;
            }
            model.Employee = employee;
            ViewBag.MagazineTypeId = new SelectList(db.MagazineTypes, "Id", "Name");

            return View(model);
        }

        //Get employee ID and return its borrowed books
        public ActionResult getEmployeeBorrowedBooks(int? id)
        {
            // Initialization.     
            JsonResult result = new JsonResult();
            try
            {

                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = db.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            var borrowedBooks = (from borrow in db.Borrows
                                 join book in db.Books on borrow.ItemId equals book.Id
                                 join itemType in db.ItemTypes on borrow.ItemTypeId equals itemType.Id
                                 select new
                                 {
                                     bookName = book.Name,
                                     author = book.Author,
                                     language = book.Language.Name,
                                     cabinetNumber = book.CabinetNumber,
                                     shelfNumber = book.ShelfNumber,
                                     serialNumber = book.SerialNumber,
                                     borrowId = borrow.Id,
                                     borrow.IsReturned,
                                     itemTypeId = borrow.ItemTypeId,
                                     employeeId = borrow.EmployeeId,
                                 }).Where(b => b.itemTypeId == bookTypeId && b.employeeId == employee.Id && b.IsReturned == false).ToList();
            return Json(new { data = borrowedBooks }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {    
                Console.Write(ex);
            }  
            return result;
        }

        //Add book to employee
        [HttpPost]
        public ActionResult BorrowBook(int? id, int? employeeId)
        {
            if (id == null && employeeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var alreadyBorrowed = db.Borrows.Where(b => b.ItemTypeId == bookTypeId && b.ItemId == id && b.EmployeeId == employeeId).FirstOrDefault();
            if (alreadyBorrowed != null)
            {
                return Json(new { success = false, responseText = "فعلاً این کتاب را با خود دارد" }, JsonRequestBehavior.AllowGet);
            }
            Book book = db.Books.Where(b => b.Id == id).FirstOrDefault();
            Employee employee = db.Employees.Where(e => e.Id == employeeId).FirstOrDefault();
            if (book == null && employee == null && bookTypeId == 0)
            {
                return Json(new { success = false, responseText = "کتاب و یا اخذ کننده در سیستم موجود نیست" }, JsonRequestBehavior.AllowGet);
            }
            if (book.AvailableQuantity <= 0)
            {
                return Json(new { success = false, responseText = "این کتاب فعلاً در کتابخانه موجودنیست" }, JsonRequestBehavior.AllowGet);
            }
            Borrow borrowObj = new Borrow()
            {
                EmployeeId = employee.Id,
                ItemTypeId = bookTypeId,
                ItemId = book.Id,
                BorrowingDate = DateHelper.PersionDateNow(),
                IsReturned = false
            };
            db.Borrows.Add(borrowObj);
            db.SaveChanges();
            //Reduce the availability number
            int availibleyQuantity = book.AvailableQuantity - 1;
            book.AvailableQuantity = availibleyQuantity;
            db.SaveChanges();
            return Json(new { success = true, responseText = "کتاب موافقانه به کارمند تسلیم شود" }, JsonRequestBehavior.AllowGet);
        }

        //Return book, by passing borrow record id
        public ActionResult ReturnBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrow borrow = db.Borrows.Where(b => b.Id == id).FirstOrDefault();

            if (borrow == null)
            {
                return Json(new { success = false, responseText = "این ریکارد در سیستم موجود نیست" }, JsonRequestBehavior.AllowGet);
            }
            borrow.IsReturned = true;
            borrow.RetrunDate = DateHelper.PersionDateNow();
            //Add book back to availablity
            Book book = db.Books.Where(b => b.Id == borrow.ItemId).FirstOrDefault();
            int availibleyQuantity = book.AvailableQuantity + 1;
            book.AvailableQuantity = availibleyQuantity;
            db.SaveChanges();
            return Json(new { success = true, responseText = "کتاب موافقانه به کتابخانه تحویل داده شود" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getDistributedMagazine(int? id)
        {
            // Initialization.     
            JsonResult result = new JsonResult();
            //try
            //{

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = db.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            var borrowedMagazines = (from borrow in db.Borrows
                                     join magazine in db.Magazines on borrow.ItemId equals magazine.Id
                                     join itemType in db.ItemTypes on borrow.ItemTypeId equals itemType.Id
                                     join magazineT in db.MagazineTypes on magazine.MagazineTypeId equals magazineT.Id
                                     select new
                                     {
                                         Name = magazine.Name,
                                         MagazineType = magazineT.Name,
                                         magazine.Publisher,
                                         PublicationDate = magazine.PublicationDate,
                                         magazine.SerialNumber,
                                         DateEntered = magazine.DateEntered,
                                         borrow.EmployeeId,
                                         borrow.IsReturned,
                                         borrowId = borrow.Id,
                                         BorrowedItemTypeId = borrow.ItemTypeId
                                     }).Where(b => b.BorrowedItemTypeId == magazineTypeId && b.EmployeeId == employee.Id && b.IsReturned == false).ToList();
            return Json(new { data = borrowedMagazines }, JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception ex)
            //{
            //    // Info     
            //    Console.Write(ex);
            //}
            // Return info.     
            //return result;B
        }

        //Add magazine to employee
        [HttpPost]
        public ActionResult BorrowMagazine(int? id, int? employeeId)
        {
            if (id == null && employeeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var alreadyBorrowed = db.Borrows.Where(b => b.ItemTypeId == magazineTypeId && b.ItemId == id && b.EmployeeId == employeeId).FirstOrDefault();
            if (alreadyBorrowed != null)
            {
                return Json(new { success = false, responseText = "کارمند این مجله را با خود دارد" }, JsonRequestBehavior.AllowGet);
            }
            Magazine magazine = db.Magazines.Where(b => b.Id == id).FirstOrDefault();
            Employee employee = db.Employees.Where(e => e.Id == employeeId).FirstOrDefault();
            if (magazine == null && employee == null && bookTypeId == 0)
            {
                return Json(new { success = false, responseText = "مجله و یا کارمند در سیستم موجود نیست" }, JsonRequestBehavior.AllowGet);
            }
            if (magazine.AvailableQuantity <= 0)
            {
                return Json(new { success = false, responseText = "این مجله فعلاً در کتابخانه موجودنیست" }, JsonRequestBehavior.AllowGet);
            }
            Borrow borrowObj = new Borrow()
            {
                EmployeeId = employee.Id,
                ItemTypeId = magazineTypeId,
                ItemId = magazine.Id,
                BorrowingDate = DateHelper.PersionDateNow(),
                IsReturned = false
            };
            db.Borrows.Add(borrowObj);
            db.SaveChanges();
            //Reduce the availability number
            int availibleyQuantity = magazine.AvailableQuantity - 1;
            magazine.AvailableQuantity = availibleyQuantity;
            db.SaveChanges();
            return Json(new { success = true, responseText = "مجله موافقانه به کارمند تسلیم شود" }, JsonRequestBehavior.AllowGet);
        }

        //Pass emplyee id and return all official magazine borrowed by employee
        public ActionResult getBorrowedOfficialMagazines(int? id)
        {
            // Initialization.     
            JsonResult result = new JsonResult();
            //try
            //{

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = db.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            var borrowedOfficialMagazines = (from borrow in db.Borrows
                                             join oMagazine in db.OfficialMagazines on borrow.ItemId equals oMagazine.Id
                                             join itemType in db.ItemTypes on borrow.ItemTypeId equals itemType.Id
                                             select new
                                             {
                                                 oMagazine.Publisher,
                                                 PublicationDate = oMagazine.PublicationDate,
                                                 oMagazine.Name,
                                                 oMagazine.SerialNumber,
                                                 DateEntered = oMagazine.DateEntered,
                                                 oMagazine.CodeNumber,
                                                 oMagazine.CabinetNumber,
                                                 oMagazine.ShelfNumber,
                                                 borrow.EmployeeId,
                                                 borrowId = borrow.Id,
                                                 borrow.IsReturned,
                                                 BorrowedItemTypeId = borrow.ItemTypeId
                                             }).Where(b => b.BorrowedItemTypeId == officialMagazineTypeId && b.EmployeeId == employee.Id && b.IsReturned == false).ToList();
            return Json(new { data = borrowedOfficialMagazines }, JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception ex)
            //{
            //    // Info     
            //    Console.Write(ex);
            //}
            // Return info.     
            //return result;B
        }

        //Add Official magazine to employee
        [HttpPost]
        public ActionResult BorrowOfficialMagazine(int? id, int? employeeId)
        {
            if (id == null && employeeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var alreadyBorrowed = db.Borrows.Where(b => b.ItemTypeId == officialMagazineTypeId && b.ItemId == id &&
            b.EmployeeId == employeeId && b.IsReturned == false).FirstOrDefault();
            if (alreadyBorrowed != null)
            {
                return Json(new { success = false, responseText = "کارمند این جریده را با خود دارد" }, JsonRequestBehavior.AllowGet);
            }
            OfficialMagazine OMagazine = db.OfficialMagazines.Where(b => b.Id == id).FirstOrDefault();
            Employee employee = db.Employees.Where(e => e.Id == employeeId).FirstOrDefault();
            if (OMagazine == null && employee == null && officialMagazineTypeId == 0)
            {
                return Json(new { success = false, responseText = "چریده و یا کارمند در سیستم موجود نیست" }, JsonRequestBehavior.AllowGet);
            }
            if (OMagazine.AvailableQuantity <= 0)
            {
                return Json(new { success = false, responseText = "این جریده فعلاً در کتابخانه موجودنیست" }, JsonRequestBehavior.AllowGet);
            }
            Borrow borrowObj = new Borrow()
            {
                EmployeeId = employee.Id,
                ItemTypeId = officialMagazineTypeId,
                ItemId = OMagazine.Id,
                BorrowingDate = DateHelper.PersionDateNow(),
                IsReturned = false
            };
            db.Borrows.Add(borrowObj);
            db.SaveChanges();
            //Reduce the availability number
            int availibleyQuantity = OMagazine.AvailableQuantity - 1;
            OMagazine.AvailableQuantity = availibleyQuantity;
            db.SaveChanges();
            return Json(new { success = true, responseText = "جریده موافقانه به کارمند تسلیم شود" }, JsonRequestBehavior.AllowGet);
        }

        //Return book, by passing borrow record id
        public ActionResult ReturnOfficialMagazine(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrow borrow = db.Borrows.Where(b => b.Id == id).FirstOrDefault();

            if (borrow == null)
            {
                return Json(new { success = false, responseText = "این ریکارد در سیستم موجود نیست" }, JsonRequestBehavior.AllowGet);
            }
            borrow.IsReturned = true;
            borrow.RetrunDate = DateHelper.PersionDateNow();
            //Add official Magazine back to availablity
            OfficialMagazine oMagazine = db.OfficialMagazines.Where(b => b.Id == borrow.ItemId).FirstOrDefault();
            int availibleyQuantity = oMagazine.AvailableQuantity + 1;
            oMagazine.AvailableQuantity = availibleyQuantity;
            db.SaveChanges();
            return Json(new { success = true, responseText = "جریده موافقانه به کتابخانه تحویل داده شود" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logs()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            ViewBag.ItemTypeId = new SelectList(db.ItemTypes.Where(t => t.IsActive == true), "Id", "Name");
            return View();
        }
        public JsonResult getLogs()
        {
            // Initialization.     
            JsonResult result = new JsonResult();
            //try
            //{
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault()
                                    + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var employeeName = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            var employeeCode = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            var publicationTypeId = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
            var publicationName = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
            var isReturned = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt16(start) : 0;
            int recordsTotal = 0;


            var borrowedLogsTemp = db.Borrows.Include(b => b.ItemType).Include(b => b.Employee)
                .Select(b => new
                {
                    employeeName = b.Employee.Name,
                    employeeCode = b.Employee.EmployeeCode,
                    publicationTypeName = b.ItemType.Name,
                    BorrowingDate = b.BorrowingDate,
                    b.IsReturned,
                    RetrunDate = b.RetrunDate,
                    borrowId = b.Id,
                    b.ItemTypeId,
                    b.ItemId
                });

            var tempDate1 = borrowedLogsTemp.ToList();

            var borrowedLogs = (from b in borrowedLogsTemp
                                join book in db.Books on b.ItemId equals book.Id into tempBooks
                                from pb in tempBooks.DefaultIfEmpty()
                                join m in db.Magazines on b.ItemId equals m.Id into tempMagazines
                                from pm in tempMagazines.DefaultIfEmpty()
                                    //join o in db.OfficialMagazines on b.ItemId equals o.Id into tempOfficalMagazines from pom in tempOfficalMagazines.DefaultIfEmpty()
                                select new
                                {
                                    employeeName = b.employeeName,
                                    employeeCode = b.employeeCode,
                                    publicationType = b.publicationTypeName,
                                    publicationName = (b.ItemTypeId == bookTypeId) ? pb.Name : pm.Name,
                                    BorrowingDate = b.BorrowingDate,
                                    b.IsReturned,
                                    RetrunDate = b.RetrunDate,
                                    borrowId = b.borrowId,
                                    b.ItemTypeId,
                                    b.ItemId
                                }
                                );

            var tempDate2 = borrowedLogs.ToList();
            borrowedLogs = borrowedLogs.AsQueryable();
            if (!string.IsNullOrEmpty(employeeCode))
            {
                borrowedLogs = borrowedLogs.Where(a => a.employeeCode.Contains(employeeCode));
            }
            if (!string.IsNullOrEmpty(employeeName))
            {
                borrowedLogs = borrowedLogs.Where(a => a.employeeName.Contains(employeeName));
            }
            if (!string.IsNullOrEmpty(publicationTypeId))
            {
                int pbTypeId = int.Parse(publicationTypeId);
                borrowedLogs = borrowedLogs.Where(a => a.ItemTypeId == pbTypeId);
            }
            if (!string.IsNullOrEmpty(publicationName))
            {
                borrowedLogs = borrowedLogs.Where(a => a.publicationName == publicationName);
            }
            if (!string.IsNullOrEmpty(isReturned) && isReturned == "true")
            {
                var tempIsReturned = bool.Parse(isReturned);
                borrowedLogs = borrowedLogs.Where(a => a.IsReturned == tempIsReturned);
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                //v = v.OrderBy(sortColumn + " " + sortColumnDir); 
            }
            recordsTotal = borrowedLogs.ToList().Count();
            borrowedLogs = borrowedLogs.OrderByDescending(e => e.borrowId);
            var data = borrowedLogs.Skip(skip).Take(pageSize);

            var dataToBeSend = data.AsEnumerable().Select(b =>
                               new
                               {
                                   employeeName = b.employeeName,
                                   employeeCode = b.employeeCode,
                                   publicationType = b.publicationType,
                                   publicationName = b.publicationName,
                                   BorrowingDate = b.BorrowingDate,
                                   IsReturned = b.IsReturned == true ? "بلی" : "نخیر",
                                   RetrunDate = b.RetrunDate,
                                   borrowId = b.borrowId,
                                   b.ItemTypeId,
                                   b.ItemId
                               }
                            ).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = dataToBeSend },
                JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception ex)
            //{
            //    // Info     
            //    Console.Write(ex);
            //}
            // Return info.     
            //return result;


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
