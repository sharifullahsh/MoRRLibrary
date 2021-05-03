using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MoRRLibrary.Models
{
    public class Borrow
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="کارمند")]
        public Employee Employee { get; set; }

        [Display(Name = "کارمند")]
        public int EmployeeId { get; set; }

        [Display(Name ="نوع کتاب")]
        public ItemType ItemType { get; set; }

        [Display(Name = "نوع کتاب")]
        public int ItemTypeId { get; set; }

        public int ItemId { get; set; }

        [Display(Name ="تاریخ گرفتن")]
        public string BorrowingDate { get; set; }

        [Display(Name ="برگردیده")]
        public bool IsReturned { get; set; }

        [Display(Name = "تاریخ برگشت")]
        public string RetrunDate { get; set; }
    }
}