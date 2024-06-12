using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace PunctualSolutionsTool.Tool.Editor
{
    public class EditorSettingsWindow
    {
        private readonly SerializedObject _settings = EditorSettings.Get();
        public EditorSettingsWindow(VisualElement element)
        {
            var asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>
                (PackageInfo.FindForAssembly(typeof(EditorSettingsWindow).Assembly).assetPath);
            asset.CloneTree(element);
        }
    }
}