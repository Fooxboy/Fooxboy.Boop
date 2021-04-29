using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using LettuceEncrypt;
using LettuceEncrypt.Accounts;
using LettuceEncrypt.Acme;
using LettuceEncrypt.Internal;
using LettuceEncrypt.Internal.IO;

namespace Fooxboy.Boop.BackendServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Генерация кода регистрации.
            var code = new Random().Next(111111, 999999);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"КОД РЕГИСТРАЦИИ АДМИНИСТРАТОРА: {code}");
            Console.ResetColor();
            StaticData.Code = code;
            
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseUrls("http://0.0.0.0:2020")
                .Build();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public static class StaticData
    {
        public static long Code { get; set; }
    }
}
