using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorldLive.Core.Consts
{
    public static class CommonConst
    {
        public const string UseUrls =
#if DEBUG
            "http://*:5000";
#else
            "http://*";
#endif
        public static string WebRootPath { get; private set; }

        private const string ScreenshotsFolder = "screenshots";

        public static string ScreenshotsPath { get { return WebRootPath + "/" + ScreenshotsFolder; } }

        public static int ScreenshotsPageSize = 10;

        private const string FilesFolder = "files";

        public static string FilesPath { get { return WebRootPath + "/" + FilesFolder; } }

        public static void WebInit(string webRootPath)
        {
            WebRootPath = webRootPath;
            if (!Directory.Exists(ScreenshotsPath)) Directory.CreateDirectory(ScreenshotsPath);
            if (!Directory.Exists(FilesPath)) Directory.CreateDirectory(FilesPath);
            ClearFiles();
        }

        private static void ClearFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(FilesPath);
            FileSystemInfo[] fileinfo = dir?.GetFileSystemInfos();  //返回目录中所有文件和子目录
            if (fileinfo?.Count() <= 0) return;
            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo)            //判断是否文件夹
                {
                    DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                    subdir.Delete(true);          //删除子目录和文件
                }
                else
                {
                    //如果 使用了 streamreader 在删除前 必须先关闭流 ，否则无法删除 sr.close();
                    File.Delete(i.FullName);      //删除指定文件
                }
            }
        }
    }
}
