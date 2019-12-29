using System;
using SharpTasks.Reconnaissance;

namespace GetExternalIp
{
    public static class Task
    {
        static void Main(string[] args)
        {
            string ip = IPAddress.GetExternalIP();
            Console.WriteLine(ip);
        }
    }
}