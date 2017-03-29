using System.Web;
using System.Web.Optimization;

namespace PersonalWebClient
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
                        "~/Scripts/lightbox/lightbox.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/helper").Include(
                      "~/Scripts/Helper.js"));

            bundles.Add(new ScriptBundle("~/bundles/philter").Include(
                      "~/Scripts/philter/jquery.philter.js"));

            bundles.Add(new ScriptBundle("~/bundles/index").Include(
                      "~/Scripts/app/app-index.js"));

            bundles.Add(new ScriptBundle("~/bundles/list").Include(
                      "~/Scripts/app/list.js"));

            bundles.Add(new ScriptBundle("~/bundles/uploadtitleimg").Include(
                      "~/Content/imageUpload/sitelogo/sitelogo.js",
                      "~/Content/imageUpload/cropper/cropper.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/service").Include(
                     "~/Scripts/linkServer/server.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/bootstrap.min.js",
                      "~/Scripts/bootstrap/bootstrap-tooltip.js",
                      "~/Scripts/bootstrap/bootstrap-popover.js",
                      "~/Scripts/bootstrap/bootstrap-affix.js",
                      "~/Scripts/bootstrap/bootstrapValidator.js",
                      "~/Scripts\bootstrap\bootstrap-transition.js",
                      "~/Scripts/bootstrap/bootstrapValidator_zh_CN.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/bootstrap.css",
                      "~/Content/input-style.css",
                      "~/Content/fontAwesome/font-awesome.css",
                      "~/Content/lightbox/lightbox.min.css",
                      "~/Content/bootstrap/bootstrapValidator.css",
                      "~/Content/imgbox.css"));

            bundles.Add(new StyleBundle("~/Content/index").Include(
                      "~/Content/app/index.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-theme").Include(
                       "~/Content/bootstrap/bootstrap-theme.css"));

            bundles.Add(new ScriptBundle("~/Content/uploadtitleimg").Include(
                      "~/Content/imageUpload/sitelogo/sitelogo.css",
                      "~/Content/imageUpload/cropper/cropper.min.css"));

            bundles.Add(new StyleBundle("~/Content/listcss").Include(
                      "~/Content/app/list.css"));

            bundles.Add(new ScriptBundle("~/bundles/btable").Include(
                     "~/Scripts/bootstrap/bootstrap-table.js",
                     "~/Scripts/bootstrap/bootstrap-table-zh-CN.js"));
            bundles.Add(new StyleBundle("~/Content/btablecss").Include(
                     "~/Content/bootstrap/bootstrap-table.css"));

            bundles.Add(new ScriptBundle("~/bundles/fileinput").Include(
                     "~/Scripts/fileinput.min.js",
                     "~/Scripts/fileinput_locale_zh.js"));
            bundles.Add(new StyleBundle("~/Content/fileinputcss").Include(
                     "~/Content/fileinput.css"));

            bundles.Add(new ScriptBundle("~/bundles/summernote").Include(
                    "~/Scripts/summernote/summernote.js",
                    "~/Scripts/summernote/lang/summernote-zh-CN.js"));
            bundles.Add(new StyleBundle("~/Content/summernotecss").Include(
                     "~/Content/summernote/summernote.css"));

            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
                    "~/Scripts/datetimepicker/bootstrap-datetimepicker.js",
                    "~/Scripts/datetimepicker/locales/bootstrap-datetimepicker.zh-CN.js"));
            bundles.Add(new StyleBundle("~/Content/datetimepicker").Include(
                     "~/Content/bootstrap/bootstrap-datetimepicker.css"));

            bundles.Add(new ScriptBundle("~/bundles/sweetalert").Include(
                   "~/Scripts/sweetalert/sweetalert-dev.js"));
            bundles.Add(new StyleBundle("~/Content/sweetalert").Include(
                     "~/Scripts/sweetalert/sweetalert.css"));


            bundles.Add(new ScriptBundle("~/bundles/imgupload").Include(
                   "~/Content/imageUpload/sitelogo/sitelogo.js",
                   "~/Content/imageUpload/cropper/cropper.min.js"));
            bundles.Add(new StyleBundle("~/Content/imgupload").Include(
                     "~/Content/imageUpload/sitelogo/sitelogo.css",
                     "~/Content/imageUpload/cropper/cropper.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/echar").Include(
                   "~/Scripts/echarts/echarts-all.js"));

        }
    }
}
