using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance => _instance; // 없으면 null

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            // 같은 타입이 이미 있으면 자신 제거
            Destroy(gameObject);
            return;
        }

        _instance = this as T;
        DontDestroyOnLoad(gameObject); // 씬 넘어가도 유지
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }
}
