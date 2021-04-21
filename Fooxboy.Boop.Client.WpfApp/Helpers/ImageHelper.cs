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

        public async static Task<string> GetImage(string url, string fileName= null)
        {
            if(url[0] == '/')
            {
                url = $"http://{ApiService.Get().Address}" + url;

            }else
            {
                url = $"http://{ApiService.Get().Address}/" + url;

            }
            var client = new WebClient();
            if (!Directory.Exists("cache"))
            {
                Directory.CreateDirectory("cache");
            }
            var localUrl = string.Empty;

            if(fileName == null)
            {
                fileName = Path.GetFileName(new Uri(url).LocalPath);

                localUrl = $"cache/{fileName}";

            }else
            {
                localUrl = $"cache/{fileName}";
            }

            if (File.Exists(localUrl))
            {
                var file = new FileInfo(localUrl);
                var fullPatch = file.FullName;
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
