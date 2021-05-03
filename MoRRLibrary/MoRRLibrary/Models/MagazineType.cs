using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoRRLibrary.Models
{
    public class MagazineType
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="نوع مجله")]
        [Required(ErrorMessage ="نوع مجله لازمی است")]
        public string Name { get; set; }
        public ICollection<Magazine> Magazines { get; set; }

        public bool IsActive { get; set; }
    }
}