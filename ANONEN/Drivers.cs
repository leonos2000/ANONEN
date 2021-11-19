using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace ANONEN
{
    public class Drivers
    {
        private string exec;
        private Logger logger;
        private BackgroundWorker worker;
        public Drivers()
        {
            logger = new Logger("Driver install", false);
        }
        public void StartInstallation(ProgressChangedEventHandler callbackFunc, RunWorkerCompletedEventHandler finishedFunc, string driverPath, int timeout = 10000)
        {
            exec = driverPath;

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker;
            worker.ProgressChanged += callbackFunc;
            worker.RunWorkerCompleted += finishedFunc;

            worker.RunWorkerAsync(timeout);
        }

        public static string[] GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }
        private string[] GetDriversInfPath()
        {
            return Directory.GetFiles(exec, "*.inf", SearchOption.AllDirectories);
        }
        protected void Worker(object sender, DoWorkEventArgs e)
        {
            var driversInfPath = GetDriversInfPath();
            float driversCount = driversInfPath.Length;

            var i = 0F;
            foreach (var path in driversInfPath)
            {
                i++;
                var proc = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = @"C:\Tools",
                        FileName = @"C:\Windows\Sysnative\pnputil.exe",
                        Arguments = $" -i -a {path} ",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                    }
                };
                if (!proc.Start())
                {
                    throw new ApplicationException("Driver installation process not started correctly.");
                }

                bool status = false;
                while (!proc.StandardOutput.EndOfStream)
                {
                    var line = proc.StandardOutput.ReadLine();
                    logger.Log(line);

                    if (line.Contains("Number successfully imported"))
                    {
                        if (line.Contains("1"))
                        {
                            status = true;
                        }
                        else status = false;
                    }
                }

                int progress = (int)(i / driversCount * 100);

                if (!proc.WaitForExit(1000 * 60 * 30))
                {
                    throw new ApplicationException("Driver installation process timeout exceeded.");
                }
                worker.ReportProgress(progress, (status, path));
            }
        }
    }
}
