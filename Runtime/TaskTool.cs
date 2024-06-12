using System.Threading;

namespace PunctualSolutionsTool.Tool
{
    public static class TaskTool
    {
        /// <summary>
        /// 创建关联取消令牌
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static CancellationTokenSource CreateLinkedTokenSource(this CancellationToken token) =>
            CancellationTokenSource.CreateLinkedTokenSource(token);
    }
}