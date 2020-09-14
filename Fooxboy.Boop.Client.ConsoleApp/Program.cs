using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Fooxboy.Boop.Client.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var bytes = new byte[1024];
            
            var ipAddress = IPAddress.Parse("127.0.0.1");
            var endPoint = new IPEndPoint(ipAddress, 2020);
            
            var sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            sender.Connect(endPoint);

            while (true)
            {
                Console.WriteLine("Введите сообщение");
                var msg = Console.ReadLine();
                var msgByte = Encoding.UTF8.GetBytes(msg);

                var sendingBytes = sender.Send(msgByte);

                var bytesRec = sender.Receive(bytes);
            
                Console.WriteLine($"Ответ: {Encoding.UTF8.GetString(bytes, 0, bytesRec)}");
            }
           

            Console.ReadLine();
        }
    }
}