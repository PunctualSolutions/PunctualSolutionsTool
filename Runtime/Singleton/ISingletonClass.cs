namespace PunctualSolutions.Tool.Singleton
{
    public interface ISingletonClass<out T> : ISingleton where T : class, ISingletonClass<T>
    {
        public void Dispose() => Singleton<T>.Dispose();
    }
}