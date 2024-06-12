using System;
using System.Threading;
using System.Threading.Tasks;

namespace PunctualSolutionsTool.Tool
{
    public class TaskConvert
    {
        private readonly Action<Action> _setAction;
        private readonly Action<Action> _cancel;

        public TaskConvert(Action<Action> setAction, Action<Action> cancel)
        {
            _setAction = setAction;
            _cancel = cancel;
        }

        public async Task Start(CancellationTokenSource tokenSource = null)
        {
            tokenSource ??= new CancellationTokenSource();
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
        private readonly Action<Action<T>> _setAction;
        private readonly Action<Action<T>> _cancel;

        public TaskConvert(Action<Action<T>> setAction, Action<Action<T>> cancel)
        {
            _setAction = setAction;
            _cancel = cancel;
        }

        public async Task<T> Start(CancellationTokenSource tokenSource = null)
        {
            tokenSource ??= new CancellationTokenSource();

            TaskCompletionSource<bool> completion = new(tokenSource);
            T value = default;
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