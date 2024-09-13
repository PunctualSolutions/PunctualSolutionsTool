#region

using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

#endregion

namespace PunctualSolutionsTool.Tool.Editor
{
    class EditorSettings : ScriptableObject
    {
        const string Path = "Assets/Editor/EditorSettings.asset";

        [SerializeField]
        FontAsset fontAsset;

        [SerializeField]
        string font;

        internal static SerializedObject Get() => EditorTool.GetEditorScriptableObject<EditorSettings>(Path);
    }
}