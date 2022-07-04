using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WorldLive.Core.Helpers
{
    public static class IOHelper
    {
        /// <summary>
        /// C#按文件名排序（顺序）
        /// </summary>
        /// <param name="fileInfos">待排序数组</param>
        /// <param name="order">正序？</param>
        public static void SortAsFileName(FileInfo[] fileInfos, bool order = true)
        {
            Array.Sort(fileInfos, delegate (FileInfo x, FileInfo y) { return order ? x.Name.CompareTo(y.Name) : y.Name.CompareTo(x.Name); });
        }

        /// <summary>
        /// C#按文件夹名称排序（顺序）
        /// </summary>
        /// <param name="directoryInfos">待排序文件夹数组</param>
        /// <param name="asc">正序？</param>
        public static void SortAsFolderName(DirectoryInfo[] directoryInfos, bool asc = true)
        {
            Array.Sort(directoryInfos, delegate (DirectoryInfo x, DirectoryInfo y) { return asc ? x.Name.CompareTo(y.Name) : y.Name.CompareTo(x.Name); });
        }
    }
}
