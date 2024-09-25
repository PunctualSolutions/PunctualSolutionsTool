#region

using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using CyUniTask = Cysharp.Threading.Tasks.UniTask;

#endregion

namespace PunctualSolutions.Tool.UniTask
{
    public static class TaskTool
    {
        /// <summary>
        ///     创建关联取消令牌
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static CancellationTokenSource CreateLinkedTokenSource(this CancellationToken token) =>
            CancellationTokenSource.CreateLinkedTokenSource(token);

        public static CyUniTask ToUniTaskX(this IEnumerator enumerator, MonoBehaviour coroutineRunner = null)
        {
            if (!coroutineRunner) coroutineRunner = UniTaskManager.Instance;
            return enumerator.ToUniTask(coroutineRunner);
        }
    }
}