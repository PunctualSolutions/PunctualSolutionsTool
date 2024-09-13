namespace PunctualSolutions.Tool.Singleton
{
    public static class Singleton<T> where T : class, ISingleton
    {
        static          T      _instance;
        static readonly object Lock = new();

        public static T Instance
        {
            get
            {
                lock (Lock) _instance ??= SingletonCreator.CreateSingleton<T>();
                return _instance;
            }
        }

        public static void Dispose() => _instance = null;
    }
}