using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Optimization;

namespace PersianPortal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/News.js",
                      "~/Scripts/Poem.js",
                      "~/Scripts/respond.js",
                     "~/Scripts/bootstrap-datepicker.min.js",
                     "~/Scripts/bootstrap-datepicker.fa.min.js",
                     "~/Scripts/persianDatepicker.js"));

            var bundle = new ScriptBundle("~/bundles/ckeditor").Include(
                "~/Scripts/ckeditor/ckeditor.js",
                "~/Scripts/ckeditor/config.js",
                "~/Scripts/ckeditor/styles.js",
                "~/Scripts/ckeditor/adapters/jquery.js");
            bundle.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(bundle);

                     //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/styles.css",
                      "~/Content/social-net-style.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/bootstrap-glyphicons.css",
                //"~/Content/bootstrap-rtl.min.css",
                      "~/Content/bootstrap-rtl.css",
                      "~/Content/toggle-switch-px.css",
                      "~/Content/toggle-switch-rem.css",
                      "~/Content/toggle-switch.css",
                //"~/Content/bootstrap-theme.css",
                      "~/Content/bootstrap-datepicker.min.css"));

        }
    }
    class NonOrderingBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}
