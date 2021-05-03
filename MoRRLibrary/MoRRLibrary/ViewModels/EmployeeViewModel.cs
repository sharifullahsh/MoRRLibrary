using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoRRLibrary.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "نام و تخلص عضو")]
        public string Name { get; set; }

        [Display(Name = "کود نمبر / نمبر تذکره")]
        public string EmployeeCode { get; set; }
        
        [Display(Name = "وظیفه")]
        public string Designation { get; set; }
        
        [Display(Name = "ریاست")]
        public int DepartmentId { get; set; }

        [Display(Name = "ریاست")]
        public string DepartmentName { get; set; }

        [Display(Name = "آیا کارمند وزارت هست ؟")]
        public bool IsEmployee { get; set; }

        [Display(Name = "نمبر تماس")]
        public string Phone { get; set; }

        public bool IsActive { get; set; }
    }
}