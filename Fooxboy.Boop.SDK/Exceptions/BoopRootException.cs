using System;

namespace Fooxboy.Boop.SDK.Exceptions
{
    public class BoopRootException :Exception
    {
        public string Message { get; }
        public int Code { get; }
        public BoopRootException(string message, int code) : base(message)
        {
            Code = code;
            Message = message;
        }
    }
}