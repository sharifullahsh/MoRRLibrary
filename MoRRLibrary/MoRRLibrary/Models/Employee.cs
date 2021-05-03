using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MoRRLibrary.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نام و تخلص مراجع")]
        [Required(ErrorMessage ="نام عضو لازم است")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "کود نمبر / نمبر تذکره")]
        [Required(ErrorMessage = "کود نمبر ویا تذکره عضو لازمی و باید واحد باشد")]
        [Index(IsUnique = true)]
        [MaxLength(100)]
        public string EmployeeCode { get; set; }
        
        [Display(Name = "وظیفه")]
        [Required(ErrorMessage = "نام وظیفه لازمی است")]
        [MaxLength(200)]
        public string Designation { get; set; }
        
        [Display(Name = "ریاست")]
        [IfEmployee]
        public int DepartmentId { get; set; }

        [Required]
        [Display(Name = "ایا کارمند وزارت هست ؟")]
        [DefaultValue(true)]
        public bool IsEmployee { get; set; }

        [Display(Name = "نمبر تماس")]
        [Required(ErrorMessage = "نمبر تلیفون لازمی است")]
        [MaxLength(15)]
        public string Phone { get; set; }

        public bool IsActive { get; set; }
    }

}