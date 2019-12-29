using SharpTasks.Execution;
using System;
using System.Collections.Generic;

namespace GetScheduledTask
{
    public static class Task
    {
        static void Main(string[] args)
        {
            List<ScheduledTask.ScheduledTaskResult> tasks = ScheduledTask.GetScheduledTasks(args[0]);

            if (tasks != null && args.Length > 1)
            {
                Console.WriteLine(ScheduledTask.FilterTasksByName(tasks, args[1]));
                return;
            }

            Console.WriteLine(ScheduledTask.GetResultsReport(tasks));
        }
    }
}
