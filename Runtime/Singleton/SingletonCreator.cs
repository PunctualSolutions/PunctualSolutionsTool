using System;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PunctualSolutions.Tool.Singleton
{
    static class SingletonCreator
    {
        static T CreateNonPublicConstructorObject<T>() where T : class
        {
            var type             = typeof(T);
            var constructorInfos = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            var ctor             = Array.Find(constructorInfos, c => c.GetParameters().Length == 0);
            if (ctor == null) throw new("Non-Public Constructor() not found! in " + type);
            return ctor.Invoke(null) as T;
        }

        public static T CreateSingleton<T>() where T : class, ISingleton
        {
            var type = typeof(T);
            if (typeof(MonoBehaviour).IsAssignableFrom(type))
                throw new("MonoBehaviours cannot be created with this method. Use CreateMonoSingleton() instead.");
            var instance = CreateNonPublicConstructorObject<T>();
            instance.OnSingletonInit();
            return instance;
        }

        public static bool IsUnitTestMode { get; set; }

        public static T CreateMonoSingleton<T>(bool dontDestroyOnLoad) where T : Component, ISingleton
        {
            if (!IsUnitTestMode && !Application.isPlaying)
                return null;
            var instance = Object.FindAnyObjectByType<T>();
            if (!instance)
            {
                var componentType = typeof(T);
                var obj           = new GameObject(componentType.Name);
                if (!IsUnitTestMode && dontDestroyOnLoad) Object.DontDestroyOnLoad(obj);
                instance = obj.AddComponent<T>();
            }

            if (instance)
                instance.OnSingletonInit();
            return null;
        }
    }
}