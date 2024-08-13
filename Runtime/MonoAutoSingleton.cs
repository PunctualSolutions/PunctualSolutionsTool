using UnityEngine;

namespace PunctualSolutions.RideBicycle
{
    public class MonoAutoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _instance;

        public static T Instance
        {
            get
            {
                _instance ??= new GameObject(typeof(T).Name).AddComponent<T>();
                return _instance;
            }
            private set => _instance = value;
        }

        protected virtual bool IsDontDestroyOnLoad => true;
    }
}