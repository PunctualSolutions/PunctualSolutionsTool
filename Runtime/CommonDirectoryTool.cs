using System;
using System.IO;
using UnityEngine;

namespace ZhengDianWaiBao.Tool
{
    public static class CommonDirectoryTool
    {
        public static string GetHome() =>
            Environment.OSVersion.Platform switch
            {
                PlatformID.Unix or PlatformID.MacOSX => Environment.GetEnvironmentVariable("$HOME"),
                PlatformID.Win32NT => Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%"),
                _ => throw new Exception()
            };

        public static string GetConfig() => Path.Combine(GetHome(), ".config", Application.productName);
    }
}