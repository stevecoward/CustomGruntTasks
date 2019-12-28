using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace GetScheduledTask
{
    public static class Task
    {
        static void Main(string[] args)
        {
            string result = Execute(args[0], args[1]);
            Console.WriteLine(result);
            Console.ReadKey();
        }

        private static string stringFormat = "{0,-100} {1,-10} {2,-20}";

        public sealed class ScheduledTaskResult
        {
            private string formattedName;

            public string Name { get; set; } = "";
            public string Status { get; set; } = "";
            public string NextRun { get; set; } = "";

            public override string ToString()
            {
                formattedName = Name.Length > 97 ? Name.Substring(0, 97) + "..." : Name;
                return String.Format(stringFormat, formattedName, Status, NextRun);
            }
        }

        public static string Execute(string Folder, string Name)
        {
            try
            {
                List<ScheduledTaskResult> tasks = new List<ScheduledTaskResult>();

                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "schtasks.exe";
                    process.StartInfo.Arguments = String.Format("/query /nh /fo csv /tn {0}", Folder.Equals("\\") ? "\\" : Folder + "\\");
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.Start();

                    string processOutput = process.StandardOutput.ReadToEnd();
                    string processStdErr = process.StandardError.ReadToEnd();

                    foreach (string line in processOutput.Split('\n'))
                    {
                        string[] lineData = line.Split(',');
                        if (lineData.Length > 2)
                        {
                            tasks.Add(new ScheduledTaskResult
                            {
                                Name = lineData[0].Replace("\"", ""),
                                NextRun = lineData[1].Replace("\"", ""),
                                Status = lineData[2].Replace("\"", "").Replace("\r", ""),
                            });
                        }
                    }

                    process.WaitForExit();
                }

                if (tasks.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(String.Format(stringFormat + Environment.NewLine, "Task", "Status", "Next Run"));
                    
                    if (!Name.Equals(""))
                    {
                        ScheduledTaskResult foundTask = tasks.Where(task => task.Name.Contains(Name)).FirstOrDefault();
                        if (foundTask != null)
                            sb.Append(foundTask.ToString() + Environment.NewLine);
                    }
                    else
                    {
                        foreach (ScheduledTaskResult task in tasks)
                        {
                            sb.Append(task.ToString() + Environment.NewLine);
                        }
                    }

                    return sb.ToString();
                }

                return String.Format("Failed to find task or folder: {0}\\", Folder);
            }
            catch (Exception e) { return e.GetType().FullName + ": " + e.Message + Environment.NewLine + e.StackTrace; }
        }
    }
}
