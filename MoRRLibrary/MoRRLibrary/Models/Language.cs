using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoRRLibrary.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نام زبان")]
        [Required(ErrorMessage = "نام زبان لازمی است")]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }

        public bool IsActive { get; set; }
    }
}