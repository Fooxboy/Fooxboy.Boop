using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Fooxboy.Boop.Server.Services
{
    public class LoggerService
    {
        public bool IsDebug = true;
        public void Trace(object msg, [CallerMemberName]string method = null)
        {
            Write($"[{method}] {msg}");
        }

        public void Error(object msg, Exception e, [CallerMemberName]string method = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Write($"[{method}] {msg}");

        }

        public void Warning(object msg, [CallerMemberName]string method = null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Write($"[{method}] {msg}");
        }

        public void Debug(object msg, [CallerMemberName] string method = null)
        {
            if (IsDebug)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Write($"[DEBUG][{method}] {msg}");
            }
        }

        private void Write(string msg)
        {
            var text = $"({DateTime.Now.Hour}:{DateTime.Now.Minute})=> {msg}";
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
