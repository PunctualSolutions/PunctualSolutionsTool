#region

using System;
using System.IO;
using UnityEngine;

#endregion

namespace PunctualSolutionsTool.Tool
{
    public static class CommonDirectoryTool
    {
        public static string GetHome()   => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static string GetConfig() => Path.Combine(GetHome(), ".config", Application.productName);
    }
}