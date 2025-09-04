using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum UIState
{
    None,
    Intro,
    StageSelect,
    Game,
    Pause,
    GameOver,
    GameClear,
    Option,
    TimeUP,
    Info,
}

public class UIManager : Singleton<UIManager>
{
    // UI 요소들을 참조할 변수들
    private IntroUI introUI;
    private StageSelectUI stageSelectUI;
    private GameUI gameUI;
    private PauseUI pauseUI;
    private GameOverUI gameOverUI;
    private GameClearUI gameClearUI;
    private OptionUI optionUI;
    private TimeUPUI timeUPUI;
    public GameObject stageSelectCarObject;

    private CameraManager cameraManager;

    // 페이드 효과에 사용할 UI Image
    [SerializeField] private Image fadePanel;

    // 페이드 시간 (몇 초 동안 페이드할지)
    public float fadeDuration = 5.0f;

    private float maxInfection = 100f; // 최대 감염도
    public float currentInfection = 0f; // 현재 감염도
    public float remainingTime = 100f; // 초기 시간
    public float killCount = 0; // 사냥한 인간 수

    private int _selectedStageIndex; // 선택된 스테이지 인덱스를 저장할 변수

    private Coroutine _timerRoutine;

    public int SelectedStageIndex
    {
        get { return _selectedStageIndex; }
        set { _selectedStageIndex = value; }
    }

    // 현재 상태와 이전 UI상태를 저장할 변수
    private UIState _currentState; // 현재 상태를 저장할 변수
    private UIState _previousState; // 이전 상태를 저장할 변수

    public UIState PreviousState
    {
        get { return _previousState; }
        private set { _previousState = value; }
    }

    // 현재 상태와 이전 씬넘버를 저장할 변수
    private int _currentSceneIndex; // 현재 씬넘버를 저장할 변수
    private int _previousSceneIndex; // 이전 씬넘버를 저장할 변수

    public int PreviousSceneIndex
    {
        get { return _previousSceneIndex; }
        private set { _previousSceneIndex = value; }
    }

    protected override void Awake()
    {
        base.Awake();

        // 부모의 Awake에서 중복으로 파괴된 경우, 이 아래 코드가 실행되지 않음
        if (this == null)
            return;

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
        timeUPUI = GetComponentInChildren<TimeUPUI>(true);
        timeUPUI.Init(this);
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
        timeUPUI.SetActive(_currentState);
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

        if (cameraManager == null)
        {
            cameraManager = CameraManager.Instance;
        }
        cameraManager.SetStageSelectVirtualCamera();
    }

    // 게임시작 시 게임매니저에서 호출
    public void SetPlayGame()
    {
        // enum 상태를 Game으로 변경
        ChangeState(UIState.Game);

        // 게임 시작시 타이머 코루틴 시작
        _timerRoutine = StartCoroutine(Countdown());
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

        if (_timerRoutine != null)
        {
            StopCoroutine(_timerRoutine);
            _timerRoutine = null;
        }

        // 게임오버시 타이머 코루틴 정지
        //StopCoroutine(Countdown());
        //_timerRoutine = null;
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

    // UI매니저의 타임업시 호출
    public void SetTimeUP()
    {
        // enum 상태를 TimeUP으로 변경
        ChangeState(UIState.TimeUP);

        // 타임오버시 타이머 코루틴 정지
        StopCoroutine(Countdown());
    }

    // 감염도 증가 (인간이 죽을때 호출)
    public void getInfection(float amount)
    {
        // 감염도를 amount만큼 증가시키고, maxInfection(100)을 넘지 않도록 제한
        currentInfection += amount;
        currentInfection = Mathf.Min(currentInfection, maxInfection);

        // 사냥한 인간 수 1증가
        killCount++;

        if (currentInfection >= maxInfection)
        {
            // 타임오버시 타이머 코루틴 정지
            StopCoroutine(Countdown());

            // 감염도가 최대치에 도달하면 게임 오버 처리
            SetGameClear();
        }
    }

    IEnumerator Countdown()
    {
        // 최초 시간을 00으로 다시 설정
        remainingTime = 100f;

        while (remainingTime > 0)
        {
            // 1초 대기
            yield return new WaitForSeconds(1f);

            // 남은 시간을 1초 감소
            remainingTime--;
        }

        // 시간이 0 이하가 되면 타임업 처리
        if (remainingTime <= 0)
        {
            remainingTime = Mathf.Max(remainingTime, 0); // 시간을 0 이하로 떨어뜨리지 않음

            SetTimeUP(); // 시간이 0이 되면 타임업 UI로 전환
        }
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

    // 씬 로드시 호출되는 함수
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 인덱스가 0이면 (Home 씬)
        if (scene.buildIndex == 0)
        {
            // UI 상태를 Intro로 변경하여 UI 초기화
            ChangeState(UIState.None);

            // 스테이지 선택 차량 오브젝트 활성화
            stageSelectCarObject.SetActive(true);
            SetIntro();

            StartCoroutine(FadeAndLoadScene());
        }
        else if (scene.buildIndex == 3 || scene.buildIndex == 4 || scene.buildIndex == 5)
        {
            // UI 상태를 GameUI로 변경
            ChangeState(UIState.Game);

            // 시작시 카운트다운 코루틴 시작
            StartCoroutine(Countdown());
        }

        // 현재 상태를 이전 상태에 저장하고,
        // 새로운 상태로 업데이트
        _previousSceneIndex = _currentSceneIndex;
        _currentSceneIndex = scene.buildIndex;
    }

    // 일부 씬시작 시 페이드인(화면이 점점 밝아지는 효과) 코루틴
    private IEnumerator FadeAndLoadScene()
    {
        // 1. 페이드 인 (화면이 어두워짐)
        float timer = 0f;
        Color color = fadePanel.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, timer / fadeDuration); // 알파 값 1 -> 0로 서서히 변경
            fadePanel.color = color;
            yield return null;
        }
    }
}