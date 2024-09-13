#region

using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using CyUniTask = Cysharp.Threading.Tasks.UniTask;

#endregion

namespace PunctualSolutions.Tool.UniTask
{
    public static class UniTaskTool
    {
        public static CyUniTask Delay(this int seconds, CancellationToken token = default) => CyUniTask.Delay(TimeSpan.FromSeconds(seconds), DelayType.DeltaTime, PlayerLoopTiming.Update, token);

        public static CyUniTask Delay(this double seconds, CancellationToken token = default) => CyUniTask.Delay(TimeSpan.FromSeconds(seconds), DelayType.DeltaTime, PlayerLoopTiming.Update, token);

        public static CyUniTask Delay(this float seconds, CancellationToken token = default) => CyUniTask.Delay(TimeSpan.FromSeconds(seconds), DelayType.DeltaTime, PlayerLoopTiming.Update, token);
    }
}