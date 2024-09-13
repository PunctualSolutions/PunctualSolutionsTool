using UnityEngine;
using UnityEngine.AddressableAssets;

namespace PunctualSolutions.Tool.UGUI
{
    [CreateAssetMenu(fileName = "UGUI", menuName = "PunctualSolutions/UGUI/Settings", order = 1000)]
    public class UGUISettings : ScriptableObject
    {
        [field: SerializeField]
        public AssetReferenceT<AudioClip> DefaultButtonClickSound { get; private set; }
    }
}