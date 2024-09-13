using System;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Execution;
using Loxodon.Framework.Views;
using PunctualSolutions.Tool.Addressables;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PunctualSolutions.Tool.UGUI
{
    public class AddressablesUIViewLocator : UIViewLocatorBase
    {
        protected virtual IWindowManager GetDefaultWindowManager()                                => UGUIManager.Instance.GlobalWindowManager;
        public override   T              LoadView<T>(string           name)                       => throw new NotImplementedException();
        public override   T              LoadWindow<T>(string         name)                       => throw new NotImplementedException();
        public override   T              LoadWindow<T>(IWindowManager windowManager, string name) => throw new NotImplementedException();

        public override IProgressResult<float, T> LoadViewAsync<T>(string name) => Load<T>(name);

        static ProgressResult<float, T> Load<T>(string name, IWindowManager windowManager = null)
        {
            var result = new ProgressResult<float, T>();
            Executors.RunAsync(async () =>
            {
                var value   = await AddressablesTool.Get<GameObject>(name);
                var @object = await Object.InstantiateAsync(value);
                var view    = @object[0].GetComponent<T>();
                if (windowManager != null && view is IWindow window)
                    window.WindowManager = windowManager;
                result.SetResult(view);
                return result;
            });
            return result;
        }

        public override IProgressResult<float, T> LoadWindowAsync<T>(string         name)                       => Load<T>(name);
        public override IProgressResult<float, T> LoadWindowAsync<T>(IWindowManager windowManager, string name) => Load<T>(name, windowManager);
    }
}