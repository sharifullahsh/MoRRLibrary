using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoRRLibrary.ViewModels
{
    public class DashboardViewModel
    {
        public int NoRegisteredEmployees { get; set; }
        public int NoBooksRegistered { get; set; }
        public int NoMagazineRegistered { get; set; }
        public int NoOfficialMagazineRegistered { get; set; }
        public int NoIssueBooks { get; set; }
        public int NoIssuedMagazine { get; set; }
        public int NoIssuedOfficialMagazine { get; set; }
    }

}