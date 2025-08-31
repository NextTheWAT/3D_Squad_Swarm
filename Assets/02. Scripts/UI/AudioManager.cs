using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            // 인스턴스가 존재하지 않으면 씬에서 찾거나 새로 생성
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();

                // 씬에 없으면 새로 게임 오브젝트를 만들어 컴포넌트 추가
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(AudioManager).Name);
                    _instance = singletonObject.AddComponent<AudioManager>();
                }
            }
            return _instance;
        }
    }

    private AudioSource musicAudioSource; // 배경 음악용 AudioSource
    public AudioClip musicClip; // 기본 배경 음악 클립

    private void Awake()
    {
        // 인스턴스가 이미 존재하고, 나 자신이 아니라면 파괴
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 인스턴스 초기화 및 씬 전환 시 파괴 방지
        _instance = this;
        DontDestroyOnLoad(gameObject);

        // 배경음 재생용 AudioSource 설정
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.loop = true;
    }

    private void Start()
    {
        // 기본 배경 음악 시작
        ChangeBackGroundMusic(musicClip);
    }

    // 배경 음악을 다른 클립으로 교체하는 함수
    public void ChangeBackGroundMusic(AudioClip clip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }
}