using System;
using System.IO;

namespace ANONEN
{
    public class Logger
    {
        public string Prefix { get; set; }
        private string logsFullPath;
        private bool disabled;
        public Logger(string prefix, bool disabled = false, bool overwrite = true, string logsPath = "", string logsFileName = "log.txt")
        {
            logsFullPath = Path.Combine(logsPath, logsFileName);
            Prefix = "Logger";
            this.disabled = disabled;

            if ((!File.Exists(logsPath)) || overwrite)
            {
                StreamWriter sw = File.CreateText(logsFullPath);
                sw.WriteLine(Ts() + "Log file created, started logging...");
                sw.Close();
            }
            else
            {
                StreamWriter sw = File.AppendText(logsFullPath);
                sw.WriteLine(Ts() + "Started logging...");
                sw.Close();
            }
            Prefix = prefix;
        }
        public static string Ts()
        {
            return DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString() + " ";
        }
        public void Log(string message, string prefix = "")
        {
            if (disabled) return;
            if (prefix != "") this.Prefix = prefix;
            StreamWriter tw = File.AppendText(logsFullPath);
            tw.WriteLine(Ts() + "[ " + this.Prefix + " ]" + message);
            tw.Close();
        }
    }
}
