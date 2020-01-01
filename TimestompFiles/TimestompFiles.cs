using System;
using SharpTasks.Evasion;

namespace TimestompFiles
{
    public static class Task
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Timestomp.SetFileTimestamp(args[0], args[1], args[2]));
            Console.ReadKey();
        }
    }
}
