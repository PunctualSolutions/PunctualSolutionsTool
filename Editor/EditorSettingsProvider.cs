using UnityEditor;
using UnityEngine.UIElements;

namespace ZhengDianWaiBao.Tool.Editor
{
    internal class EditorSettingsProvider : SettingsProvider
    {
        private static readonly string[] Keywords = { "ZhengDian", "Font" };


        private EditorSettingsProvider(string path)
            : base(path, SettingsScope.User, Keywords)
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _ = new EditorSettingsWindow(rootElement);
        }

        [SettingsProvider]
        public static SettingsProvider Create()
        {
            var provider = new EditorSettingsProvider("Preferences/ZhengDian")
            {
                // TODO 添加参数进行处理
                // Automatically extract all keywords from the Styles.
                // Keywords = GetSearchKeywordsFromGUIContentProperties<Styles>()
            };

            return provider;
        }
    }
}