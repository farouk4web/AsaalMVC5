using System.Web;
using System.Web.Optimization;

namespace Asaal
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
    

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            // JQUERY  AND jquery validation
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            //BOOTSTRAP JS FILES
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));



            // BOOTBOX LIBRARY TO CREATE ALERT AND CONFIRM ETC IN BOOTSTRAP DESIGN
            bundles.Add(new ScriptBundle("~/bundles/bootbox").Include(
                        "~/Scripts/bootbox.js"));


            // CUSTOM JS FILES CREATED BY ME ---> FAROUK
            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                      "~/Scripts/main.js",
                      "~/Scripts/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/questionInDetails").Include(
                        "~/Scripts/questionInDetails.js"));
           
            bundles.Add(new ScriptBundle("~/bundles/populateCountries").Include(
                        "~/Scripts/populateCountries.js"));

            bundles.Add(new ScriptBundle("~/bundles/controlPanel").Include(
                        "~/Scripts/controlPanel.js"));

            bundles.Add(new ScriptBundle("~/bundles/removeUser").Include(
                        "~/Scripts/removeUser.js"));

            bundles.Add(new ScriptBundle("~/bundles/changeUserPicture").Include(
                        "~/Scripts/changeUserPicture.js"));

            bundles.Add(new ScriptBundle("~/bundles/pagenationNav").Include(
                        "~/Scripts/pagenationNav.js"));



            // CSS DEFAULT FILES TOGETHER
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            // css rtl files
            bundles.Add(new StyleBundle("~/Content/css-rtl").Include(
                      "~/Content/bootstrap-rtl.css",
                      "~/Content/site.css",
                      "~/Content/site-rtl.css"));
        }
    }
}
