using Loxodon.Framework.Contexts;
using Loxodon.Framework.Views;
using PunctualSolutions.Tool.Singleton;
using UnityEngine;

namespace PunctualSolutions.Tool.UGUI
{
    public class UGUIManager : MonoBehaviour, IMonoSingleton<UGUIManager>
    {
        public static UGUIManager Instance => MonoSingleton<UGUIManager>.Instance;

        [field: SerializeField]
        public UGUISettings Settings { get; private set; }

        [field: SerializeField]
        public GlobalWindowManagerBase GlobalWindowManager { get; private set; }

        void Start()
        {
            var context   = Context.GetApplicationContext();
            var container = context.GetContainer();
            container.Register<IUIViewLocator>(new DefaultUIViewLocator());
        }
    }
}