using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fooxboy.Boop.Client.WpfApp.Helpers
{
    public class ImageHelper
    {

        public async static Task<string> GetImage(string url, string filename= null)
        {
            var client = new WebClient();
            if (!Directory.Exists("cache"))
            {
                Directory.CreateDirectory("cache");

            }

            var fileName = url.GetHashCode();

            try
            {
                await client.DownloadFileTaskAsync(url, $"cache/{fileName}.jpg");
            }
            catch (Exception e)
            {
                //todo: flex
            }

            

            return $"cache/{fileName}.jpg";
        }
    }
}
