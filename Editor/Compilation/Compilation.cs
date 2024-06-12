using UnityEditor;
using UnityEditor.Compilation;

namespace PunctualSolutionsTool.Tool.Editor.Compilation
{
    public static class Compilation
    {
        [MenuItem("Tool/CleanAndCompilation")]
        private static void CleanAndCompilation() => CompilationPipeline.RequestScriptCompilation(RequestScriptCompilationOptions.CleanBuildCache);
    }
}