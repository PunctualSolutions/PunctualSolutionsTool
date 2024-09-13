namespace PunctualSolutions.Tool.Singleton
{
    using UnityEngine;

    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance) return _instance;
                _instance = FindAnyObjectByType<T>();
                if (_instance) return _instance;
                var singleton = new GameObject();
                var component = singleton.AddComponent<T>();
                singleton.name = "Singleton" + typeof(T);
                if (component.IsDontDestroyOnLoad)
                    DontDestroyOnLoad(singleton);
                return _instance;
            }
        }

        void Awake()
        {
            if (!_instance)
            {
                _instance = this as T;
                if (IsDontDestroyOnLoad)
                    DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this) throw new("MonoSingleton already exists!");

            InAwake();
        }

        void OnDestroy()
        {
            _instance = null;
            InDestroy();
        }

        protected virtual bool IsDontDestroyOnLoad => true;

        protected virtual void InAwake()
        {
        }

        protected virtual void InDestroy()
        {
        }
    }
}