#region

using System;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace PunctualSolutionsTool.Tool
{
    public class TaskConvert
    {
        readonly Action<Action> _cancel;
        readonly Action<Action> _setAction;

        public TaskConvert(Action<Action> setAction, Action<Action> cancel)
        {
            _setAction = setAction;
            _cancel    = cancel;
        }

        public async Task Start(CancellationTokenSource tokenSource = null)
        {
            tokenSource ??= new();
            TaskCompletionSource<bool> completion = new(tokenSource);
            try
            {
                _setAction(C);
                await using (tokenSource.Token.Register(() => completion.TrySetCanceled())) await completion.Task;
            }
            catch
            {
                completion.TrySetResult(false);
            }
            finally
            {
                _cancel(C);
            }

            return;

            void C() => completion.TrySetResult(true);
        }
    }

    public class TaskConvert<T>
    {
        readonly Action<Action<T>> _cancel;
        readonly Action<Action<T>> _setAction;

        public TaskConvert(Action<Action<T>> setAction, Action<Action<T>> cancel)
        {
            _setAction = setAction;
            _cancel    = cancel;
        }

        public async Task<T> Start(CancellationTokenSource tokenSource = null)
        {
            tokenSource ??= new();

            TaskCompletionSource<bool> completion = new(tokenSource);
            T                          value      = default;
            try
            {
                _setAction(C);
                await using (tokenSource.Token.Register(() => completion.TrySetCanceled())) await completion.Task;
            }
            catch
            {
                completion.TrySetResult(false);
            }
            finally
            {
                _cancel(C);
            }

            return value;

            void C(T inValue)
            {
                value = inValue;
                completion.TrySetResult(true);
            }
        }
    }
}