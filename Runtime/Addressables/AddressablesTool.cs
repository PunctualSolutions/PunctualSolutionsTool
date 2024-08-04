#if Addressables
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace PunctualSolutions.Tool.Addressables
{
    public static class AddressablesTool
    {
        static readonly List<AssetReference> WaitAsset = new();

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
            var loadAssetAsync = await value.LoadAssetAsync();
            return loadAssetAsync;
        }
    }
}
#endif