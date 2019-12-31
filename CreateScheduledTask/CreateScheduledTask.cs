﻿using System;
using SharpTasks.Execution;

namespace CreateScheduledTask
{
    public static class CreateScheduledTask
    {
        private static void Main(string[] args)
        {
            var output = ScheduledTask.CreateScheduledTask("hourly", "3", "\\Windows\\MSEdge\\Microsoft Edge Update Tool", "C:\\Windows\\System32\\calc.exe");
            Console.WriteLine(output);
            Console.ReadKey();
        }
    }
}
