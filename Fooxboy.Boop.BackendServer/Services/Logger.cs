using System;
using Microsoft.Extensions.Logging;

namespace Fooxboy.Boop.BackendServer.Services
{
    public class Logger
    {
        public void Info(object message)
        {
            var msg = $"[INFO]=> {message}";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Write(msg);
        }

        public void Error(string method, Exception e)
        {
            var msg = $"[ERROR]=> [{method}] {e.Message}";
            Console.ForegroundColor = ConsoleColor.Red;
            Write(msg);
        }

        public void Debug(string message)
        {
            var msg = $"[DEBUG]=> {message}";
            Console.ForegroundColor = ConsoleColor.Magenta;
            Write(msg);
        }

        private void Write(string message)
        {
            var msg = $"({DateTime.Now.Hour}:{DateTime.Now.Minute}) {message}";
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}