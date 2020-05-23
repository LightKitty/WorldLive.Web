using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using System.Text;

namespace WorldLive.Core.Helpers
{
    public static class ImageHelper
    {
        public enum ScaleType { Original=0, Small=1 };

        private static Dictionary<string, string> contentTypeDict = new Dictionary<string, string> {
                {"jpg","image/jpeg"},
                {"jpeg","image/jpeg"},
                {"jpe","image/jpeg"},
                {"png","image/png"},
                {"gif","image/gif"},
                {"ico","image/x-ico"},
                {"tif","image/tiff"},
                {"tiff","image/tiff"},
                {"fax","image/fax"},
                {"wbmp","image//vnd.wap.wbmp"},
                {"rp","image/vnd.rn-realpix"}
            };

        public static bool Zoom(string imgPath, ScaleType scaleType, out byte[] imgBytes, out string contentType)
        {
            imgBytes = null;
            contentType = string.Empty;

            var imgType = Path.GetExtension(imgPath).TrimStart('.'); //图片后缀
            if (!contentTypeDict.TryGetValue(imgType, out contentType)) return false; //位置后缀
            switch(scaleType)
            {
                case ScaleType.Small:
                    imgBytes = Zoom(imgPath, 0.5, ImageFormat.Jpeg);
                    return true;
                default:
                    imgBytes = ToBytes(imgPath);
                    return true;
            }
        }

        public static byte[] Zoom(string imgPath, double scale, ImageFormat format)
        {
            using (var imgBmp = new Bitmap(imgPath))
            {
                //找到新尺寸
                int height = Convert.ToInt32(imgBmp.Height * scale);
                int width = Convert.ToInt32(imgBmp.Width * scale);
                var newImg = new Bitmap(imgBmp, width, height);
                //newImg.SetResolution(imgBmp.HorizontalResolution, imgBmp.VerticalResolution);
                var ms = new MemoryStream();
                newImg.Save(ms, format);
                var bytes = ms.GetBuffer();
                ms.Close();
                return bytes;
            }
        }

        public static byte[] ToBytes(string imgPath)
        {
            using (var sw = new FileStream(imgPath, FileMode.Open))
            {
                var bytes = new byte[sw.Length];
                sw.Read(bytes, 0, bytes.Length);
                sw.Close();
                return bytes;
            }
        }
    }
}
