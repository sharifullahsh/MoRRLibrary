using MD.PersianDateTime;
using MoRRLibrary.Models;
using MoRRLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoRRLibrary.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            DashboardViewModel model = new DashboardViewModel();
            model.NoRegisteredEmployees = db.Employees.Where(e=>e.IsActive == true).Count();
            model.NoBooksRegistered = db.Books.Where(b => b.IsActive == true).Count();
            model.NoMagazineRegistered = db.Magazines.Where(m => m.IsActive == true).Count();
            model.NoOfficialMagazineRegistered = db.OfficialMagazines.Where(o => o.IsActive == true).Count();
            var bookQuantity = db.Books.Where(b => b.IsActive == true).Sum(b => (int?)b.Quantity) ?? 0;
            var bookAvailableQuantity = db.Books.Where(b=>b.IsActive == true).Sum(b => (int?)b.AvailableQuantity) ?? 0;
            model.NoIssueBooks = bookQuantity - bookAvailableQuantity;
            var magazineQuantity = db.Magazines.Where(m=>m.IsActive == true).Sum(m => (int?)m.Quantity) ?? 0;
            var magazineAvailable = db.Magazines.Where(m=>m.IsActive == true).Sum(m => (int?)m.AvailableQuantity) ?? 0;
            model.NoIssuedMagazine = magazineQuantity - magazineAvailable;
            var oMagazineQuantity = db.OfficialMagazines.Where(o=>o.IsActive == true).Sum(om => (int?)om.Quantity) ?? 0;
            var oMagazineAvailable = db.OfficialMagazines.Where(o=>o.IsActive == true).Sum(om => (int?)om.AvailableQuantity) ?? 0;
            model.NoIssuedOfficialMagazine = oMagazineQuantity - oMagazineAvailable;
            return View(model);
        }
        public JsonResult getStudyEvaluation()
        {
            // Initialization.     
            var thisYear = new PersianDateTime(PersianDateTime.Now.Year,01,01);

            JsonResult result = new JsonResult();
            try
            {
            var data = db.Borrows.Select(b=> new { BorrowingDate = b.BorrowingDate }).ToList().AsEnumerable();
            var thisYearData = (from b in data
                            select new
                            {
                                BorrowingDate = (PersianDateTime.Parse(b.BorrowingDate))
                            }
                            ).Where(b=> b.BorrowingDate >= thisYear).ToList(); 
                var groupByMonth = (from b in thisYearData group b by b.BorrowingDate.Month into mData
                                select new { monthNo = mData.Key, monthRecords = mData.Count()}).ToList();

            return Json(new {  data = groupByMonth },
                JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {     
                Console.Write(ex);
            }   
            return result;
        }
    }
}