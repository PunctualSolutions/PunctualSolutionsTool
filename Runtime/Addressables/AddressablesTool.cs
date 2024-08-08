#if Addressables
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UAddressable = UnityEngine.AddressableAssets.Addressables;

namespace PunctualSolutions.Tool.Addressables
{
    public static class AddressablesTool
    {
        static readonly List<AssetReference> WaitAsset       = new();
        static readonly List<string>         WaitAssetString = new();

        public static async UniTask<T> Get<T>(this AssetReferenceT<T> value) where T : Object
        {
            if (value.OperationHandle.IsValid())
            {
                await value.OperationHandle;
                return value.OperationHandle.Convert<T>().Result;
            }

            WaitAsset.Add(value);
            await UniTask.NextFrame();
            while (WaitAsset.First() != value)
                await UniTask.NextFrame();
            WaitAsset.Remove(value);
            return await value.LoadAssetAsync();
        }

        public static async UniTask<T> Get<T>(string key) where T : Object
        {
            WaitAssetString.Add(key);
            await UniTask.NextFrame();
            while (WaitAssetString.First() != key)
                await UniTask.NextFrame();
            WaitAssetString.Remove(key);
            return await UAddressable.LoadAssetAsync<T>(key);
        }
    }
}
#endif