#region

using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

#endregion

namespace PunctualSolutions.Tool.Pool
{
    public abstract class ObjectPool<T>
    {
        readonly List<T>  _activeObjects = new();
        readonly Queue<T> _pool          = new();
        readonly uint?    _warningExceedsSizeRange;


        /// <summary>
        /// </summary>
        /// <param name="warningExceedsSizeRange">When the number of objects in the pool exceeds the limit, a warning will be issued each time they are added</param>
        public ObjectPool(uint? warningExceedsSizeRange)
        {
            if (warningExceedsSizeRange == 0) throw new($"{nameof(warningExceedsSizeRange)} cannot be zero");
            _warningExceedsSizeRange = warningExceedsSizeRange;
        }

        public             uint       Total       => (uint)(_activeObjects.Count + _pool.Count);
        public             uint       UseCount    => (uint)_pool.Count;
        public             uint       UnusedCount => (uint)_activeObjects.Count;
        protected abstract UniTask<T> OnCreate();
        protected abstract UniTask    OnGet(T     @object);
        protected abstract UniTask    OnRelease(T @object);
        protected abstract UniTask    OnDestroy(T @object);

        public async UniTask<T> Get()
        {
            var @object = _pool.Count == 0 ? await OnCreate() : _pool.Dequeue();
            await OnGet(@object);
            _activeObjects.Add(@object);
            if (_warningExceedsSizeRange != null && Total > _warningExceedsSizeRange) Debug.LogWarning($"{typeof(T).Name} object Pool Size exceeds warning range");
            return @object;
        }

        public async UniTask Release(T @object)
        {
            await OnRelease(@object);
            if (_activeObjects.Contains(@object)) _activeObjects.Remove(@object);
            _pool.Enqueue(@object);
        }

        public async UniTask Clear()
        {
            foreach (var @object in _pool) await OnDestroy(@object);
            foreach (var @object in _activeObjects) await OnDestroy(@object);
            _pool.Clear();
            _activeObjects.Clear();
        }
    }
}