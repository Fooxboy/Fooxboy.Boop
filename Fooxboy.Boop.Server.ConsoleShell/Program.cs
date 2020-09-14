using System;

namespace Fooxboy.Boop.Server.ConsoleShell
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Startup();
            server.Run();
            Console.ReadLine();
        }
    }
}