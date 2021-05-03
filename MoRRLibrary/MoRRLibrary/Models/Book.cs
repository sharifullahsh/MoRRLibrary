using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MoRRLibrary.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="نام کتاب")]
        [Required(ErrorMessage ="نام لازمی است")]
        public string Name { get; set; }

        [Required(ErrorMessage = "نام نویسنده لازمی است")]
        [Display(Name = "نویسنده")]
        public string Author { get; set; }

        [Display(Name = "مترجم")]
        public string Translator { get; set; }

        [Display(Name = "ناشر")]
        public string Publisher { get; set; }

        [Display(Name="زبان")]
        public Language Language { get; set; }

        [Display(Name = "زبان")]
        [Required(ErrorMessage = "نام زبان لازمی است")]
        public int LanguageId { get; set; }

        [Display(Name = "نمبر الماری")]
        [Required(ErrorMessage = "نمبر الماری لازمی است")]
        public int CabinetNumber { get; set; }

        [Display(Name = "نمبر جعبه")]
        [Required(ErrorMessage = "نمبر جعبه لازمی است")]
        public int ShelfNumber { get; set; }

        [Display(Name = "تعداد جلد")]
        [Required(ErrorMessage = "تعداد جلد لازمی است")]
        public int Quantity { get; set; }

        [Display(Name = "کود نمبر کتاب")]
        [Required(ErrorMessage = "کود نمبر کتاب لازمی و باید واحد باشد")]
        [Index(IsUnique = true)]
        [MaxLength(100)]
        public string SerialNumber { get; set; }

        [Display(Name="تعداد موجود")]
        public int AvailableQuantity { get; set; }

        public bool IsActive { get; set; }
    }
}