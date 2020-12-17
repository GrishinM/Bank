using System;

namespace Server
{
    internal static class Program
    {
        public static void Main()
        {
            var server = new BankLibrary.Server();
            server.Started += () => Console.WriteLine("Сервер запущен");
            server.Stopped += () => Console.WriteLine("Сервер отсановлен");
            server.Error += e => Console.WriteLine(e.Message);
            server.Start();
        }
    }
}