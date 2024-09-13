using UnityEngine;

namespace PunctualSolutions.Tool.Singleton
{
    public static class MonoSingleton<T> where T : MonoBehaviour, ISingleton
    {
        static T _instance;

        public static bool DontDestroyOnLoad { get; set; } = true;

        public static T Instance
        {
            get
            {
                if (!_instance) _instance = SingletonCreator.CreateMonoSingleton<T>(DontDestroyOnLoad);
                return _instance;
            }
        }

        public static void Dispose()
        {
            if (SingletonCreator.IsUnitTestMode)
                Object.DestroyImmediate(_instance.gameObject);
            else
                Object.Destroy(_instance.gameObject);
            _instance = null;
        }
    }
}