using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

/// <summary>
/// Summary description for BundleConfig
/// </summary>
public class BundleConfig
{
    // For more information on Bundling, visit https://go.microsoft.com/fwlink/?LinkID=303951
    public static void RegisterBundles(BundleCollection bundles)
    {
        bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                        "~/Script/WebForms/WebForms.js",
                        "~/Script/WebForms/WebUIValidation.js",
                        "~/Script/WebForms/MenuStandards.js",
                        "~/Script/WebForms/Focus.js",
                        "~/Script/WebForms/GridView.js",
                        "~/Script/WebForms/DetailsView.js",
                        "~/Script/WebForms/TreeView.js",
                        "~/Script/WebForms/WebParts.js"));

        // Order is very important for these files to work, they have explicit dependencies
        bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                "~/Script/WebForms/MsAjax/MicrosoftAjax.js",
                "~/Script/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                "~/Script/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                "~/Script/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

        // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
        // ready for production, use the build tool at https://modernizr.com to pick only the tests you need
        bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Script/modernizr-*"));
        bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Script/jquery.touchSwipe.min.js",
                      "~/Script/jquery-1.11.2.min.js"));

        bundles.Add(new ScriptBundle("~/bundles/utility").Include(
                   "~/Script/Utility.js"));
        bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                  "~/Script/bootstrap.js"));
        bundles.Add(new ScriptBundle("~/bundles/owl").Include(
                  "~/Script/owl.carousel.min.js"));

        bundles.Add(new StyleBundle("~/bundles/css").Include(
             "~/StyleSheet/bootstrap.css",
             "~/StyleSheet/Style.css",
             "~/StyleSheet/owl.theme.default.min.css",
             "~/StyleSheet/owl.carousel.min.css",
            "~/StyleSheet/fontawesome-all.css"));
    }
}