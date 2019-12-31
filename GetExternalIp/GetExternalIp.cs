using System;
using SharpTasks.Reconnaissance;

namespace GetExternalIp
{
    public static class Task
    {
        private static void Main(string[] args)
        {
            var ip = IpAddress.GetExternalIp();
            Console.WriteLine(ip);
        }
    }
}