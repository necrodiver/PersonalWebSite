using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonalWebService.Helper
{
    public static class Utility_Helper
    {
        /// <summary>
        /// 判断是否是分类的Id（是否为GUID字符串）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsClassIds(string[] str)
        {
            Regex re = new Regex(@"^[a-zA-Z0-9]+$");
            foreach (var item in str)
            {
                if (string.IsNullOrEmpty(item))
                    return false;
                if (!re.IsMatch(item))
                    return false;
            }
            return true;
        }
    }

    public class ImageConvert
    {
        /// <summary>
        /// Image转字节流
        /// </summary>
        /// <param name="img">Image</param>
        /// <param name="format">Image格式</param>
        /// <returns></returns>
        public static string ToBase64String(Image img, ImageFormat format)
        {
            if (img != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, format);
                    byte[] buffer = ms.ToArray();
                    return Convert.ToBase64String(buffer);
                }
            }
            return string.Empty;
        }
        public static string ToBase64HtmlString(Image img, ImageFormat format)
        {
            string type = "jpeg";
            if (format.Guid == ImageFormat.Png.Guid)
            {
                type = "png";
            }
            else if (format.Guid == ImageFormat.Gif.Guid)
            {
                type = "gif";
            }
            return string.Format("data:image/{0};base64,", type) + ToBase64String(img, format);
        }

        public static Image FromBase64String(string base64Str)
        {
            Bitmap bitmap = null;
            Image img = null;
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] buffer = Convert.FromBase64String(base64Str);
                ms.Write(buffer, 0, buffer.Length);
                try
                {
                    img = Image.FromStream(ms);
                    if (img != null)
                    {
                        bitmap = new Bitmap(img.Width, img.Height);
                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            g.DrawImage(img, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                        }
                    }
                }
                catch { }
            }
            return bitmap;
        }

        /// <summary>
        /// 图片字节转Image
        /// </summary>
        /// <param name="str">图片字节</param>
        /// <returns></returns>
        public static Image FromBase64HtmlString(string str)
        {
            string[] strs = str.Split(',');
            if (strs.Length > 0)
            {
                return FromBase64String(strs[strs.Length - 1]);
            }
            else
            {
                return FromBase64String(str);
            }
        }
    }
}
