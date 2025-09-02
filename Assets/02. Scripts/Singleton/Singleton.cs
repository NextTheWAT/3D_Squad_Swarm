using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance => _instance; // ������ null

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            // ���� Ÿ���� �̹� ������ �ڽ� ����
            Destroy(gameObject);
            return;
        }

        _instance = this as T;
        DontDestroyOnLoad(gameObject); // �� �Ѿ�� ����
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }
}
