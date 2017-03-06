using System.Web;
using System.Web.Optimization;

namespace PersonalWebClientAsTwo
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            //瀑布流布局
            bundles.Add(new ScriptBundle("~/bundles/waterfallFlowLayout").Include(
                        "~/Scripts/masonry.pkgd.js",
                        "~/Scripts/imagesloaded.pkgd.js"));

            bundles.Add(new ScriptBundle("~/bundles/lightbox").Include(
                        "~/Scripts/lightbox.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/helper").Include(
                      "~/Scripts/Helper.js"));

            bundles.Add(new ScriptBundle("~/bundles/philter").Include(
                      "~/Scripts/philter/jquery.philter.js"));

            bundles.Add(new ScriptBundle("~/bundles/index").Include(
                      "~/Scripts/app/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/service").Include(
                     "~/Scripts/linkServer/server.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/bootstrap.min.js",
                      "~/Scripts/bootstrap/bootstrap-tooltip.js",
                      "~/Scripts/bootstrap/bootstrap-popover.js",
                      "~/Scripts/bootstrap/bootstrap-affix.js",
                      "~/Scripts/bootstrap/bootstrapValidator.js",
                      "~/Scripts/bootstrap/bootstrapValidator_zh_CN.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/bootstrap.css",
                      "~/Content/input-style.css",
                      "~/Content/fontAwesome/font-awesome.css",
                      "~/Content/lightbox.css",
                      "~/Content/imgbox.css"));

            bundles.Add(new StyleBundle("~/Content/index").Include(
                      "~/Content/app/index.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-theme").Include(
                       "~/Content/bootstrap/bootstrap-theme.css"));
        }
    }
}
