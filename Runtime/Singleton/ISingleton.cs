namespace PunctualSolutions.Tool.Singleton
{
    public interface ISingleton
    {
        void OnSingletonInit();
        void Dispose();
    }
}