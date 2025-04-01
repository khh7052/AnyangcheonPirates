using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static bool applicationIsQuitting = false;

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance " +
                    typeof(T) +
                    " already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance)
                    DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    protected virtual void OnEnable()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        applicationIsQuitting = true;
    }
}
