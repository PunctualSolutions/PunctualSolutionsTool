using UnityEditor;
using UnityEditor.Compilation;

namespace ZhengDianWaiBao.Tool.Editor.Compilation
{
    public static class Compilation
    {
        [MenuItem("Assets/CleanAndCompilation")]
        private static void CleanAndCompilation() => CompilationPipeline.RequestScriptCompilation(RequestScriptCompilationOptions.CleanBuildCache);
    }
}