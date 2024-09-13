namespace PunctualSolutions.Tool.Singleton
{
    public interface ISingletonClass<out T> : ISingleton where T : class, ISingleton
    {
        public void Dispose() => Singleton<T>.Dispose();
    }
}