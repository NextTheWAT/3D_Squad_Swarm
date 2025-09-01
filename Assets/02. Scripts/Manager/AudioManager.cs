using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            // �ν��Ͻ��� �������� ������ ������ ã�ų� ���� ����
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();

                // ���� ������ ���� ���� ������Ʈ�� ����� ������Ʈ �߰�
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(AudioManager).Name);
                    _instance = singletonObject.AddComponent<AudioManager>();
                }
            }
            return _instance;
        }
    }

    private AudioSource musicAudioSource; // ��� ���ǿ� AudioSource
    public AudioClip[] musicClip; // �⺻ ��� ���� Ŭ��

    private void Awake()
    {
        // �ν��Ͻ��� �̹� �����ϰ�, �� �ڽ��� �ƴ϶�� �ı�
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // �ν��Ͻ� �ʱ�ȭ �� �� ��ȯ �� �ı� ����
        _instance = this;
        DontDestroyOnLoad(gameObject);

        // ����� ����� AudioSource ����
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.loop = true;
    }

    // ��� ������ �ٸ� Ŭ������ ��ü�ϴ� �Լ�
    public void ChangeBackGroundMusic(AudioClip clip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    private void OnEnable()
    {
        // �� �ε� �Ϸ� �̺�Ʈ�� �Լ� ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // �� �ε� �Ϸ� �̺�Ʈ���� �Լ� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // �� �ε� �Ϸ� �� ȣ��Ǵ� �Լ�
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch(scene.buildIndex)
        {
            case 0: // Home ��
                ChangeBackGroundMusic(musicClip[0]);
                break;

            //case 1: // Game ��
            //    // ChangeBackGroundMusic(musicClip[2]);
            //    break;
            default:
                break;
        }
    }
}