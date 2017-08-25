using System.Diagnostics;
using System.IO;

namespace monitor_web_status_code
{
    public static class ProcessHelper
    {
        /// <summary>
        /// 执行进程的主方法
        /// 返回原始的StreamReader
        /// </summary>
        /// <param name="programPath"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static StreamReader RunProcessRaw(string programPath, string args = null)
        {
            Process p = new Process();
            p.StartInfo.FileName = programPath;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.Arguments = args;
            p.Start();

            p.StandardInput.AutoFlush = true;

            return p.StandardOutput;
        }

        /// <summary>
        /// 执行进程的主方法
        /// 返回String类型
        /// </summary>
        /// <param name="programPath"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string RunProcess(string programPath, string args = null)
        {
            return RunProcessRaw(programPath, args).ReadToEnd();
        }

    }
}