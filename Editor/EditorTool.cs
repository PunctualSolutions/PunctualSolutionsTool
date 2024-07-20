#region

using UnityEditor;
using UnityEngine;

#endregion

namespace PunctualSolutionsTool.Tool.Editor
{
    public static class EditorTool
    {
        public static void Play() => EditorApplication.EnterPlaymode();

        public static SerializedObject GetEditorScriptableObject<T>(string path) where T : ScriptableObject
        {
            var settings = AssetDatabase.LoadAssetAtPath<T>(path);
            if (settings != null) return new(settings);
            settings = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(settings, path);
            AssetDatabase.SaveAssets();
            return new(settings);
        }
    }
}