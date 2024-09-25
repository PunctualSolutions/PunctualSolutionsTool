using System;

namespace PunctualSolutions.Tool.Singleton
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SingletonAttribute : Attribute
    {
        public SingletonAttribute(bool notAllowedDispose = false, bool disableLazyLoad = false,
            bool disableSealed = false)
        {
            NotAllowedDispose = notAllowedDispose;
            DisableLazyLoad = disableLazyLoad;
            DisableSealed = disableSealed;
        }

        public bool NotAllowedDispose { get; private set; }
        public bool DisableLazyLoad { get; private set; }
        public bool DisableSealed { get; private set; }
    }
}