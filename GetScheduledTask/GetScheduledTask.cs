using SharpTasks.Execution;
using System;

namespace GetScheduledTask
{
    public static class Task
    {
        private static void Main(string[] args)
        {
            var tasks = ScheduledTask.GetScheduledTasks(args[0]);

            if (tasks != null && args.Length > 1)
            {
                Console.WriteLine(ScheduledTask.FilterTasksByName(tasks, args[1]));
                return;
            }

            Console.WriteLine(ScheduledTask.GetResultsReport(tasks));
        }
    }
}
