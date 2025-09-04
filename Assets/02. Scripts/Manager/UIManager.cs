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
    // UI ��ҵ��� ������ ������
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

    // ���̵� ȿ���� ����� UI Image
    [SerializeField] private Image fadePanel;

    // ���̵� �ð� (�� �� ���� ���̵�����)
    public float fadeDuration = 5.0f;

    private float maxInfection = 100f; // �ִ� ������
    public float currentInfection = 0f; // ���� ������
    public float remainingTime = 100f; // �ʱ� �ð�
    public float killCount = 0; // ����� �ΰ� ��

    private int _selectedStageIndex; // ���õ� �������� �ε����� ������ ����

    private Coroutine _timerRoutine;

    public int SelectedStageIndex
    {
        get { return _selectedStageIndex; }
        set { _selectedStageIndex = value; }
    }

    // ���� ���¿� ���� UI���¸� ������ ����
    private UIState _currentState; // ���� ���¸� ������ ����
    private UIState _previousState; // ���� ���¸� ������ ����

    public UIState PreviousState
    {
        get { return _previousState; }
        private set { _previousState = value; }
    }

    // ���� ���¿� ���� ���ѹ��� ������ ����
    private int _currentSceneIndex; // ���� ���ѹ��� ������ ����
    private int _previousSceneIndex; // ���� ���ѹ��� ������ ����

    public int PreviousSceneIndex
    {
        get { return _previousSceneIndex; }
        private set { _previousSceneIndex = value; }
    }

    protected override void Awake()
    {
        base.Awake();

        // �θ��� Awake���� �ߺ����� �ı��� ���, �� �Ʒ� �ڵ尡 ������� ����
        if (this == null)
            return;

        // �� UI ��ҵ��� ã�� �ʱ�ȭ
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

    // UI ���� ���� �޼���
    public void ChangeState(UIState newState)
    {
        // ���� ���¸� ���� ���¿� �����ϰ�,
        // ���ο� ���·� ������Ʈ
        _previousState = _currentState;
        _currentState = newState;

        // �� UI�� SetActive ����� ������ ���� enum ���¿� ��ġ�ϴ��� ���� Ȱ��ȭ ���� ����
        introUI.SetActive(_currentState);
        stageSelectUI.SetActive(_currentState);
        gameUI.SetActive(_currentState);
        pauseUI.SetActive(_currentState);
        gameOverUI.SetActive(_currentState);
        gameClearUI.SetActive(_currentState);
        optionUI.SetActive(_currentState);
        timeUPUI.SetActive(_currentState);
    }

    // ��Ʈ�� ȭ������ ��ȯ
    public void SetIntro()
    {
        // enum ���¸� Game���� ����
        ChangeState(UIState.Intro);
    }

    // �������� ���� ȭ������ ��ȯ
    public void SetStageSelect()
    {
        // enum ���¸� StageSelect�� ����
        ChangeState(UIState.StageSelect);

        if (cameraManager == null)
        {
            cameraManager = CameraManager.Instance;
        }
        cameraManager.SetStageSelectVirtualCamera();
    }

    // ���ӽ��� �� ���ӸŴ������� ȣ��
    public void SetPlayGame()
    {
        // enum ���¸� Game���� ����
        ChangeState(UIState.Game);

        // ���� ���۽� Ÿ�̸� �ڷ�ƾ ����
        _timerRoutine = StartCoroutine(Countdown());
    }

    // �Ͻ����� Ű�Է��ϴ� ������ ȣ��
    // (���ӸŴ��� ��� ���� �����ǵ��� ó�� �� ȣ��)
    public void SetPause()
    {
        // enum ���¸� Pause�� ����
        ChangeState(UIState.Pause);
    }

    // �÷��̾� ��� �� ������ ��(��:�÷��̾�)���� ȣ��
    public void SetGameOver()
    {
        // enum ���¸� GameOver�� ����
        ChangeState(UIState.GameOver);

        if (_timerRoutine != null)
        {
            StopCoroutine(_timerRoutine);
            _timerRoutine = null;
        }

        // ���ӿ����� Ÿ�̸� �ڷ�ƾ ����
        //StopCoroutine(Countdown());
        //_timerRoutine = null;
    }

    // ���� Ŭ���� �� ȣ��
    public void SetGameClear()
    {
        // enum ���¸� GameClear�� ����
        ChangeState(UIState.GameClear);
    }

    // �� �ɼǹ�ư���� ȣ�� (��Ʈ��, �Ͻ�����)
    public void SetOption()
    {
        // enum ���¸� Option�� ����
        ChangeState(UIState.Option);
    }

    // UI�Ŵ����� Ÿ�Ӿ��� ȣ��
    public void SetTimeUP()
    {
        // enum ���¸� TimeUP���� ����
        ChangeState(UIState.TimeUP);

        // Ÿ�ӿ����� Ÿ�̸� �ڷ�ƾ ����
        StopCoroutine(Countdown());
    }

    // ������ ���� (�ΰ��� ������ ȣ��)
    public void getInfection(float amount)
    {
        // �������� amount��ŭ ������Ű��, maxInfection(100)�� ���� �ʵ��� ����
        currentInfection += amount;
        currentInfection = Mathf.Min(currentInfection, maxInfection);

        // ����� �ΰ� �� 1����
        killCount++;

        if (currentInfection >= maxInfection)
        {
            // Ÿ�ӿ����� Ÿ�̸� �ڷ�ƾ ����
            StopCoroutine(Countdown());

            // �������� �ִ�ġ�� �����ϸ� ���� ���� ó��
            SetGameClear();
        }
    }

    IEnumerator Countdown()
    {
        // ���� �ð��� 00���� �ٽ� ����
        remainingTime = 100f;

        while (remainingTime > 0)
        {
            // 1�� ���
            yield return new WaitForSeconds(1f);

            // ���� �ð��� 1�� ����
            remainingTime--;
        }

        // �ð��� 0 ���ϰ� �Ǹ� Ÿ�Ӿ� ó��
        if (remainingTime <= 0)
        {
            remainingTime = Mathf.Max(remainingTime, 0); // �ð��� 0 ���Ϸ� ����߸��� ����

            SetTimeUP(); // �ð��� 0�� �Ǹ� Ÿ�Ӿ� UI�� ��ȯ
        }
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

    // �� �ε�� ȣ��Ǵ� �Լ�
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� �ε����� 0�̸� (Home ��)
        if (scene.buildIndex == 0)
        {
            // UI ���¸� Intro�� �����Ͽ� UI �ʱ�ȭ
            ChangeState(UIState.None);

            // �������� ���� ���� ������Ʈ Ȱ��ȭ
            stageSelectCarObject.SetActive(true);
            SetIntro();

            StartCoroutine(FadeAndLoadScene());
        }
        else if (scene.buildIndex == 3 || scene.buildIndex == 4 || scene.buildIndex == 5)
        {
            // UI ���¸� GameUI�� ����
            ChangeState(UIState.Game);

            // ���۽� ī��Ʈ�ٿ� �ڷ�ƾ ����
            StartCoroutine(Countdown());
        }

        // ���� ���¸� ���� ���¿� �����ϰ�,
        // ���ο� ���·� ������Ʈ
        _previousSceneIndex = _currentSceneIndex;
        _currentSceneIndex = scene.buildIndex;
    }

    // �Ϻ� ������ �� ���̵���(ȭ���� ���� ������� ȿ��) �ڷ�ƾ
    private IEnumerator FadeAndLoadScene()
    {
        // 1. ���̵� �� (ȭ���� ��ο���)
        float timer = 0f;
        Color color = fadePanel.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, timer / fadeDuration); // ���� �� 1 -> 0�� ������ ����
            fadePanel.color = color;
            yield return null;
        }
    }
}