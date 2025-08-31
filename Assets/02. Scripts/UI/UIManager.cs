using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Intro,
    StageSelect,
    Game,
    Pause,
    GameOver,
    GameClear,
    Option,
}

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    // 싱글톤
    public static UIManager Instance
    {
        get
        {
            // 인스턴스가 존재하지 않으면 씬에서 찾거나 새로 생성
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();

                // 씬에 없으면 새로 게임 오브젝트를 만들어 컴포넌트 추가
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(UIManager).Name);
                    _instance = singletonObject.AddComponent<UIManager>();
                }
            }
            return _instance;
        }
    }

    // UI 요소들을 참조할 변수들
    private IntroUI introUI;
    private StageSelectUI stageSelectUI;
    private GameUI gameUI;
    private PauseUI pauseUI;
    private GameOverUI gameOverUI;
    private GameClearUI gameClearUI;
    private OptionUI optionUI;
    
    private CameraManager cameraManager;

    private float maxInfection = 100f; // 최대 감염도
    public float currentInfection = 0f; // 현재 감염도
    public float remainingTime = 100f; // 초기 시간
    public float killCount = 0; // 사냥한 인간 수

    // 현재 상태와 이전 상태를 저장할 변수
    private UIState _currentState;
    private UIState _previousState;

    public UIState PreviousState
    {
        get { return _previousState; }
        private set { _previousState = value; }
    }

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

        // 각 UI 요소들을 찾아 초기화
        introUI = GetComponentInChildren<IntroUI>(true);
        introUI.Init(this);
        stageSelectUI = GetComponentInChildren<StageSelectUI>(true);
        stageSelectUI.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.Init(this);
        pauseUI = GetComponentInChildren<PauseUI>(true);
        pauseUI.Init(this);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI.Init(this);
        gameClearUI = GetComponentInChildren<GameClearUI>(true);
        gameClearUI.Init(this);
        optionUI = GetComponentInChildren<OptionUI>(true);
        optionUI.Init(this);

        cameraManager = CameraManager.Instance;

        // 최초 enum 상태를 Intro로 설정
        SetIntro();
    }

    // 인트로 화면으로 전환
    public void SetIntro()
    {
        // enum 상태를 Game으로 변경
        ChangeState(UIState.Intro);
    }

    // 스테이지 선택 화면으로 전환
    public void SetStageSelect()
    {
        // enum 상태를 StageSelect로 변경
        ChangeState(UIState.StageSelect);

        cameraManager.SetStageSelectVirtualCamera();
    }

    // 게임시작 시 게임매니저에서 호출
    public void SetPlayGame()
    {
        // enum 상태를 Game으로 변경
        ChangeState(UIState.Game);

        // 게임 시작시 타이머 코루틴 시작
        StartCoroutine(Countdown());
    }

    // 일시정지 키입력하는 곳에서 호출
    // (게임매니저 등에서 게임 정지되도록 처리 후 호출)
    public void SetPause()
    {
        // enum 상태를 Pause로 변경
        ChangeState(UIState.Pause);
    }

    // 플레이어 사망 시 적절한 곳(예:플레이어)에서 호출
    public void SetGameOver()
    {
        // enum 상태를 GameOver로 변경
        ChangeState(UIState.GameOver);
    }

    // 게임 클리어 시 호출
    public void SetGameClear()
    {
        // enum 상태를 GameClear로 변경
        ChangeState(UIState.GameClear);
    }

    // 각 옵션버튼에서 호출 (인트로, 일시정지)
    public void SetOption()
    {
        // enum 상태를 Option로 변경
        ChangeState(UIState.Option);
    }

    // UI 상태 변경 메서드
    public void ChangeState(UIState newState)
    {
        // 현재 상태를 이전 상태에 저장하고,
        // 새로운 상태로 업데이트
        _previousState = _currentState;
        _currentState = newState;

        // 각 UI에 SetActive 명령을 보내서 현재 enum 상태와 일치하는지 비교후 활성화 여부 결정
        introUI.SetActive(_currentState);
        stageSelectUI.SetActive(_currentState);
        gameUI.SetActive(_currentState);
        pauseUI.SetActive(_currentState);
        gameOverUI.SetActive(_currentState);
        gameClearUI.SetActive(_currentState);
        optionUI.SetActive(_currentState);
    }

    // 감염도 증가 (인간이 죽을때 호출)
    public void getInfection(float amount)
    {
        // 감염도를 amount만큼 증가시키고, maxInfection(100)을 넘지 않도록 제한
        currentInfection += amount;
        currentInfection = Mathf.Min(currentInfection, maxInfection);

        // 사냥한 인간 수 1증가
        killCount++;
    }

    IEnumerator Countdown()
    {
        // 최초 시간을 100으로 다시 설정
        remainingTime = 100f;

        while (remainingTime > 0)
        {
            // 1초 대기
            yield return new WaitForSeconds(1f);

            // 남은 시간을 1초 감소
            remainingTime--;
        }

        // 게임오버 (남은시간0)
        SetGameOver();
        Debug.Log("시간 종료!");
    }
}