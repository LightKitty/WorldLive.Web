using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WorldLive.Core.Consts;
using WorldLive.Core.Helpers;
using WorldLive.Web.Models.Home;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorldLive.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            DirectoryInfo root = new DirectoryInfo(CommonConst.ScreenshotsPath);
            DirectoryInfo[] dics = root.GetDirectories();
            IOHelper.SortAsFolderName(dics, false); //排序
            IndexModel homeModel = new IndexModel
            {
                Folders = dics
            };

            return View(homeModel);
        }

        public IActionResult Screenshots(string folder, int page)
        {
            string folderPath = CommonConst.ScreenshotsPath + "/" + folder;
            if (!Directory.Exists(folderPath))
            {//不存在
                return Index();
            }

            DirectoryInfo root = new DirectoryInfo(folderPath);
            FileInfo[] files = root.GetFiles();
            IOHelper.SortAsFileName(files); //排序

            int maxPage = Convert.ToInt32(Math.Ceiling((double)files.Length / CommonConst.ScreenshotsPageSize));
            if (page < 1) page = 1;
            else if (page > maxPage) page = maxPage;

            int startIndex = files.Length - 1 - (page - 1) * CommonConst.ScreenshotsPageSize;
            int endIndex = files.Length - 1 - page * CommonConst.ScreenshotsPageSize + 1;
            if (endIndex < 0) endIndex = 0;

            List<string> screenshotNames = new List<string>();

            for (int i = startIndex; i >= endIndex; i--)
            {
                screenshotNames.Add(files[i].Name);
            }

            ScreenshotsModel screenshotsModel = new ScreenshotsModel
            {
                FolderName = folder,
                ScreenshotNames = screenshotNames,
                MaxPage = maxPage,
                CurrentPage = page
            };

            if (page > 1)
            {
                screenshotsModel.LastPage = page - 1;
            }
            if (page < maxPage)
            {
                screenshotsModel.NextPage = page + 1;
            }

            return View(screenshotsModel);
        }

        [HttpGet]
        public IActionResult Screenshot(string folder, string name, ImageHelper.ScaleType scaleType)
        {
            string path = CommonConst.WebRootPath + "/" + "screenshots" + "/" + folder + "/" + name;
            byte[] bytes;
            string contentType;
            ImageHelper.Zoom(path, scaleType, out bytes, out contentType);
            return new FileContentResult(bytes, contentType);
        }
    }
}
