#region

using System.Diagnostics;

#endregion

namespace PunctualSolutionsTool.Tool
{
    public static class CmdTool
    {
        public static void RunCmd(string command, string argument = "", bool wait = false, bool useWindow = false)
        {
            var info = new ProcessStartInfo(command)
            {
                    Arguments       = argument,
                    CreateNoWindow  = !useWindow,
                    UseShellExecute = true,
            };
            var process = Process.Start(info);
            if (process == null || !wait) return;
            process.WaitForExit();
            process.Close();
        }
    }
}