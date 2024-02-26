using System.Diagnostics;

namespace ZhengDianWaiBao.Tool
{
    public class CmdTool
    {
        public static void RunCmd(string command, string argument, bool wait = false)
        {
            var info = new ProcessStartInfo(command)
            {
                Arguments = argument,
                CreateNoWindow = true,
                UseShellExecute = false
            };
            var process = Process.Start(info);
            if (process == null || !wait) return;
            process.WaitForExit();
            process.Close();
        }
    }
}