using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MoRRLibrary.Models
{
    public class Magazine
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نوع مجله")]
        public MagazineType MagazineType { get; set; }

        [Display(Name = "نوع مجله")]
        [Required(ErrorMessage = "نوع مجله لازمی است")]
        public int MagazineTypeId { get; set; }

        [Display(Name="نام")]
        [Required(ErrorMessage ="نام مجله لازمی است")]
        public string Name { get; set; }

        [Display(Name="ناشر")]
        public string Publisher { get; set; }

        [Display(Name = "کود نمبر مجله")]
        [Required(ErrorMessage = "کود نمبر مجله لازمی و باید واحد باشد")]
        [Index(IsUnique = true)]
        [MaxLength(100)]
        public string SerialNumber { get; set; }

        [Display(Name="تاریخ وارده")]
        [Required(ErrorMessage ="تاریخ وارده لازمی است")]
        public string DateEntered { get; set; }

        [Display(Name = "نمبر الماری")]
        [Required(ErrorMessage ="نمبر الماری لازمی است")]
        public int CabinetNumber { get; set; }

        [Display(Name = "نمبر جعبه")]
        [Required(ErrorMessage = "نمبر جعبه لازمی است")]
        public int ShelfNumber { get; set; }

        [Display(Name="تاریخ نشر")]
        [Required(ErrorMessage ="تاریخ نشر لازمی است")]
        public string PublicationDate { get; set; }

        [Display(Name = "تعداد")]
        [Required(ErrorMessage ="تعداد مجله لازمی است")]
        public int Quantity { get; set; }

        [Display(Name = "تعداد موجود")]
        public int AvailableQuantity { get; set; }

        public bool IsActive { get; set; }
    }
}