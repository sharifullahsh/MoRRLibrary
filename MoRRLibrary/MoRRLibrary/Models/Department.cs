using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoRRLibrary.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="ریاست")]
        [Required]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}