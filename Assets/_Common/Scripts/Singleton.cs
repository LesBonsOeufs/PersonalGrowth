using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    /// <summary>
    /// The instance.
    /// </summary>
    private static T instance;

    /// <summary>
    /// Gets the instance of the Singleton.
    /// </summary>
    /// <value>The instance.</value>
    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<T>();

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
            instance = this as T;
        else if (instance != this)
            Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}