using System;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected virtual bool IsDontDestroyOnLoad => true;
    public static T Instance;

    public virtual void Awake()
    {
        if (Instance is null)
        {
            Instance = GetComponent<T>();
            if (IsDontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this) throw new Exception("Instance already exists");
    }
}