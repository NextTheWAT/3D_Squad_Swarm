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
            if (_instance == null)
            {
                // 씬에 Manager가 없으면 에러를 발생시켜 문제를 알림
                Debug.LogError("UIManager is not found in the scene.");
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
    private OptionUI optionUI;

    // 현재 상태와 이전 상태를 저장할 변수
    private UIState _currentState;
    private UIState _previousState;

    private CameraManager cameraManager;

    public UIState PreviousState
    {
        get { return _previousState; }
        private set { _previousState = value; }
    }

    private void Awake()
    {
        // 싱글톤 패턴 초기화 (중복 로직 제거)
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

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
        optionUI.SetActive(_currentState);
    }
}