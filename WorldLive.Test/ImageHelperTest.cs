using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorldLive.Core.Helpers;

namespace WorldLive.Test
{
    [TestClass]
    public class ImageHelperTest
    {
        [TestMethod]
        public void ZoomTest()
        {
            string path = "e:/iis_live_pac.jpg";
            string type;
            byte[] data;
            var result = ImageHelper.Zoom(path, ImageHelper.ScaleType.Small, out data, out type);
        }
    }
}
