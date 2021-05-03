using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MoRRLibrary.Models
{
    public class OfficialMagazine
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="نمبر مسلسل")]
        [Required(ErrorMessage ="نمبر مسلسل لازمی است")]
        [Index(IsUnique = true)]
        [MaxLength(100)]
        public string SerialNumber { get; set; }

        [Display(Name="ناشر")]
        [Required(ErrorMessage ="نام ناشر لازمی است")]
        public string Publisher { get; set; }

        [Display(Name = "نام جریده")]
        [Required(ErrorMessage ="نام جریده لازم است")]
        public string Name { get; set; }

        [Display(Name = "تاریخ وارده")]
        [Required(ErrorMessage ="تاریخ وارده لازمی است")]
        public string DateEntered { get; set; }

        [Display(Name = "نمبر الماری")]
        [Required(ErrorMessage = "نمبر الماری لازمی است")]
        public int CabinetNumber { get; set; }

        [Display(Name = "نمبر جعبه")]
        [Required(ErrorMessage = "نمبر جعبه لازمی است")]
        public int ShelfNumber { get; set; }

        [Display(Name = "کود نمبر جریده")]
        [Required(ErrorMessage = "کود نمبر جریده لازمی و باید واحد باشد")]
        [Index(IsUnique = true)]
        [MaxLength(100)]
        public string CodeNumber { get; set; }

        [Display(Name = "تاریخ نشر")]
        [Required(ErrorMessage ="تاریخ نشر لازمی است")]
        public string PublicationDate { get; set; }

        [Display(Name = "تعداد")]
        [Required(ErrorMessage ="تعداد جریده لازمی است")]
        public int Quantity { get; set; }

        [Display(Name = "تعداد موجود")]
        public int AvailableQuantity { get; set; }

        public bool IsActive { get; set; }
    }
}