using UnityEngine;

public abstract class SingleTon<T> : MonoBehaviour where T : Component
{
    protected static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindAnyObjectByType<T>();
                if(instance == null)
                {
                    GameObject instObj = new GameObject(typeof(T).Name);
                    instance = instObj.AddComponent<T>();
                }
            }
            return instance;
        }
    }
    protected void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
}
