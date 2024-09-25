using System;

namespace PunctualSolutions.Tool.Singleton
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SingletonMonoAttribute : Attribute
    {
        public SingletonMonoAttribute(bool notAllowedRelease = false, bool disableSealed = false,bool allowAutoDestroy = false)
        {
            NotAllowedRelease = notAllowedRelease;
            DisableSealed = disableSealed;
            AllowAutoDestroy = allowAutoDestroy;
        }

        public bool NotAllowedRelease { get; private set; }
        public bool DisableSealed { get; private set; }
        public bool AllowAutoDestroy { get; private set; }
    }
}