using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PunctualSolutions.Tool
{
    public static class MainManagerTool
    {
        public static UniTask ToUniTaskX(this IEnumerator enumerator, MonoBehaviour coroutineRunner = null)
        {
            if(!coroutineRunner) coroutineRunner = MainManager.Instance;
            return enumerator.ToUniTask(coroutineRunner);
        }
    }
}