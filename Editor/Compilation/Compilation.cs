#region

using UnityEditor;
using UnityEditor.Compilation;

#endregion

namespace PunctualSolutionsTool.Tool.Editor.Compilation
{
    public static class Compilation
    {
        [MenuItem("Tool/CleanAndCompilation")]
        static void CleanAndCompilation() => CompilationPipeline.RequestScriptCompilation(RequestScriptCompilationOptions.CleanBuildCache);
    }
}