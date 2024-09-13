using PunctualSolutions.Tool.Singleton;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace PunctualSolutions.Tool.UGUI
{
    public class UGUIManager : MonoBehaviour, IMonoSingleton<UGUIManager>
    {
        public static UGUIManager Instance => MonoSingleton<UGUIManager>.Instance;

        [field: SerializeField]
        public UGUISettings Settings { get; private set; }
    }

    [CreateAssetMenu(fileName = "UGUI", menuName = "PunctualSolutions/UGUI/Settings", order = 1000)]
    public class UGUISettings : ScriptableObject
    {
        [field: SerializeField]
        public AssetReferenceT<AudioClip> DefaultButtonClickSound { get; private set; }
    }
}