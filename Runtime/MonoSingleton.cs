#region

using UnityEngine;

#endregion

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static     T    Instance;
    protected virtual bool IsDontDestroyOnLoad => true;

    public void Awake()
    {
        if (Instance is null)
        {
            Instance = GetComponent<T>();
            if (IsDontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
            InAwake();
        }
        else if (Instance != this) throw new("Instance already exists");
    }

    public virtual void InAwake();

    void OnDestroy()
    {
        Instance = null;
    }
}