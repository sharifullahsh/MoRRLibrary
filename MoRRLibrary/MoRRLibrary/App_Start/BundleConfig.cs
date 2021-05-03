using System.Web;
using System.Web.Optimization;

namespace MoRRLibrary
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        //"~/Scripts/jquery-{version}.js",

                        "~/Content/vendor/jquery/jquery.js",
                        "~/Content/MdBootstrapPDateTimePicker/popper.js",
                        "~/Content/MdBootstrapPDateTimePicker/bootstrap.4.3.1.js",
                        //"~/Content/MdBootstrapPDateTimePicker/jquery.md.bootstrap.datetimepicker.js",

                        //"~/Scripts/popper.min.js",
                        //"~/Scripts/bootstrap.js",
                        //"~/Content/vendor/bootstrap/js/bootstrap.bundle.js",
                        //"~/Scripts/bootstrap.js",  
                        "~/Scripts/js/sb-admin-2.min.js",
                        "~/Content/vendor/jquery-easing/jquery.easing.js",
                        "~/Content/vendor/datatables/jquery.dataTables.js",
                        "~/Content/vendor/datatables/dataTables.bootstrap4.js",
                        "~/Scripts/bootbox/Script/bootbox.all.js",
                        "~/Scripts/toastr.js",
                        "~/Scripts/customization/validationmessages.js",
                        //"~/Scripts/bootbox/local-fa.js"
                        //"~/Content/vendor/chart.js/Chart.js",
                        "~/Scripts/common/dateTimePicker.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                      //"~/Content/bootstrap.css",
                        //"~/Content/site.css",
                        "~/Content/css/sb-admin-2-rtl.css",
                        //"~/Content/vendor/fontawesome-free/css/all.css",
                        "~/Content/vendor/datatables/dataTables.bootstrap4.css",
                         //"~/Content/MdBootstrapPDateTimePicker/bootstrap.4.3.1.css",
                        "~/Content/MdBootstrapPDateTimePicker/css.css",
                        "~/Content/MdBootstrapPDateTimePicker/jquery.md.bootstrap.datetimepicker.style.css",

                        "~/Content/toastr.css",
                       "~/Content/css/custome.css"
                      ));
        }
    }
}
