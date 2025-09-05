using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource musicAudioSource; // 배경 음악용 AudioSource
    public AudioClip[] musicClip; // 기본 배경 음악 클립

    protected override void Awake()
    {
        base.Awake();

        // 부모의 Awake에서 중복으로 파괴된 경우, 이 아래 코드가 실행되지 않음
        if (this == null) 
            return;

        // 배경음 재생용 AudioSource 설정
        musicAudioSource = GetComponent<AudioSource>();

        if (musicAudioSource == null)
        {
            // AudioSource 컴포넌트가 없다면 추가
            musicAudioSource = gameObject.AddComponent<AudioSource>();
        }

        musicAudioSource.loop = true;
    }

    private void OnEnable()
    {
        // 씬 로드 완료 이벤트에 함수 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 씬 로드 완료 이벤트에서 함수 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬 로드 완료 시 호출되는 함수
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 0: // Home 씬
                ChangeBackGroundMusic(musicClip[0]);
                break;

            case 1: // Intro 씬
                musicAudioSource.Stop();
                break;

            case 2: // TimeUP GameOver 씬
                musicAudioSource.Stop();
                break;

            case 3: // Stage1 씬
                ChangeBackGroundMusic(musicClip[1]);
                break;

            case 4: // Stage2 씬
                ChangeBackGroundMusic(musicClip[2]);
                break;

            case 5: // Stage3 씬
                ChangeBackGroundMusic(musicClip[3]);
                break;

            default:
                break;
        }
    }

    // 배경 음악을 다른 클립으로 교체하는 함수
    public void ChangeBackGroundMusic(AudioClip clip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }
}