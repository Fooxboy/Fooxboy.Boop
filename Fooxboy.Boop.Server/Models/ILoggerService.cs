using System;
using System.Runtime.CompilerServices;

namespace Fooxboy.Boop.Server.Models
{
    public interface ILoggerService
    {
        bool IsDebug { get; set; }
        void Trace(object msg, [CallerMemberName] string method = null);
        void Error(object msg, Exception e, [CallerMemberName] string method = null);
        void Warning(object msg, [CallerMemberName] string method = null);
        void Debug(object msg, [CallerMemberName] string method = null);
    }
}