using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fooxboy.Boop.Client.WpfApp.Services;

namespace Fooxboy.Boop.Client.WpfApp.Helpers
{
    public static class ImageHelper
    {

        public async static Task<string> GetImage(string url, string filename= null)
        {
            url = $"http://{ApiService.Get().Address}/" + url;
            var client = new WebClient();
            if (!Directory.Exists("cache"))
            {
                Directory.CreateDirectory("cache");
            }

            var fileName = url.GetHashCode();
            
            string ext=url.Substring(url.LastIndexOf('.'));

            var localUrl = $"cache/{fileName}{ext}";

            if (File.Exists(localUrl))
            {
                var file = new FileInfo(localUrl);
                var fullPatch = file.DirectoryName + file.FullName;
                return fullPatch;
            }

            try
            {
                await client.DownloadFileTaskAsync(url, localUrl);
                var file = new FileInfo(localUrl);
                var fullPatch = file.FullName;
                return fullPatch;
            }
            catch (Exception e)
            {
                return "Controls/user.jpg";
            }

            
        }
    }
}
