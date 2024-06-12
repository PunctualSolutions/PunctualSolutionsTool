using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace PunctualSolutionsTool.Tool
{
    public static class UniTaskTool
    {
        public static UniTask Delay(this int seconds, CancellationToken token = default) =>
            UniTask.Delay(TimeSpan.FromSeconds(seconds), DelayType.DeltaTime, PlayerLoopTiming.Update, token);

        public static UniTask Delay(this double seconds, CancellationToken token = default) =>
            UniTask.Delay(TimeSpan.FromSeconds(seconds), DelayType.DeltaTime, PlayerLoopTiming.Update, token);

        public static UniTask Delay(this float seconds, CancellationToken token = default) =>
            UniTask.Delay(TimeSpan.FromSeconds(seconds), DelayType.DeltaTime, PlayerLoopTiming.Update, token);
    }
}