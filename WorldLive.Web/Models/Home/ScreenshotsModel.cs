using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WorldLive.Web.Models.Home
{
    public class ScreenshotsModel
    {
        public string FolderName { get; set; }
        public List<string> ScreenshotNames { get; set; }
        public int LastPage { get; set; }
        public int NextPage { get; set; }
        public int MaxPage { get; set; }
        public int CurrentPage { get; set; }
        public DateTime Date { get; set; }
    }
}
