using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource musicAudioSource; // ��� ���ǿ� AudioSource
    public AudioClip[] musicClip; // �⺻ ��� ���� Ŭ��

    protected override void Awake()
    {
        base.Awake();

        // �θ��� Awake���� �ߺ����� �ı��� ���, �� �Ʒ� �ڵ尡 ������� ����
        if (this == null) 
            return;

        // ����� ����� AudioSource ����
        musicAudioSource = GetComponent<AudioSource>();

        if (musicAudioSource == null)
        {
            // AudioSource ������Ʈ�� ���ٸ� �߰�
            musicAudioSource = gameObject.AddComponent<AudioSource>();
        }

        musicAudioSource.loop = true;
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
        switch (scene.buildIndex)
        {
            case 0: // Home ��
                ChangeBackGroundMusic(musicClip[0]);
                break;

            case 1: // Intro ��
                musicAudioSource.Stop();
                break;

            case 2: // TimeUP GameOver ��
                musicAudioSource.Stop();
                break;

            case 3: // Stage1 ��
                ChangeBackGroundMusic(musicClip[1]);
                break;

            case 4: // Stage2 ��
                ChangeBackGroundMusic(musicClip[2]);
                break;

            case 5: // Stage3 ��
                ChangeBackGroundMusic(musicClip[3]);
                break;

            default:
                break;
        }
    }

    // ��� ������ �ٸ� Ŭ������ ��ü�ϴ� �Լ�
    public void ChangeBackGroundMusic(AudioClip clip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }
}