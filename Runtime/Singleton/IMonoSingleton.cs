using UnityEngine;

namespace PunctualSolutions.Tool.Singleton
{
    public interface IMonoSingleton<T> : ISingleton where T : MonoBehaviour, ISingleton
    {
        public void Dispose() => MonoSingleton<T>.Dispose();
    }
}