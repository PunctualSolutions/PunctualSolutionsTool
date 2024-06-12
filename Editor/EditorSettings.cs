using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace PunctualSolutionsTool.Tool.Editor
{
    internal class EditorSettings : ScriptableObject
    {
        private const string Path = "Assets/Editor/EditorSettings.asset";

        [SerializeField] private FontAsset fontAsset;
        [SerializeField] private string font;

        internal static SerializedObject Get() => EditorTool.GetEditorScriptableObject<EditorSettings>(Path);
    }
}