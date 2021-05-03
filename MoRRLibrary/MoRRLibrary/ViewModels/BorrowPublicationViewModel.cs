using MoRRLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoRRLibrary.ViewModels
{
    public class BorrowPublicationViewModel
    {
        public EmployeeViewModel Employee { get; set; }
        public List<Book> Books { get; set; }
        public List<Magazine> Magazines { get; set; }
        public List<OfficialMagazine> OfficalMagazines { get; set; }
    }
}