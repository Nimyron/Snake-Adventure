using UnityEngine;

public class SimpleSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// This singleton does not create any GameObject. Instances of T must be manually created.
    /// </summary>

    private static T _instance;
    private static readonly object _lock = new object();
    private static bool _isShuttingDown = false;

    public static T Instance
    {
        get
        {
            return InstanceCheck();
        }
    }

    protected virtual void Awake()
    {
        // Instance assignement is performed within the method.
        InstanceCheck();
    }

    protected void OnApplicationQuit()
    {
        _isShuttingDown = true;
    }

    protected void OnDestroy()
    {
        if (_instance == this as T)
        {
            _instance = null;
        }
    }

    private static T InstanceCheck()
    {
        if (_isShuttingDown)
        {
            Debug.LogWarning($"[{typeof(T).Name}] Instance already destroyed. Returning null.");
            return null;
        }

        Debug.Log($"Thread {System.Threading.Thread.CurrentThread.ManagedThreadId} is attempting to acquire the lock.");
        lock (_lock)
        {
            if (_instance == null)
            {
                T[] _instances = FindObjectsByType<T>(FindObjectsSortMode.None);

                if (_instances.Length != 1)
                {
                    Debug.LogError($"[{typeof(T).Name}] Multiples instances detected. Assigning first one and destroying others.");
                    _instance = _instances[0];

                    for (int i = 1; i < _instances.Length; i++)
                    {
                        Destroy(_instances[i]);
                    }

                    return _instance;
                }
                else
                {
                    _instance = _instances[0];
                    return _instance;
                }
            }
            else
            {
                return _instance;
            }
        }
    }
}
