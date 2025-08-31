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

    // �̱���
    public static UIManager Instance
    {
        get
        {
            // �ν��Ͻ��� �������� ������ ������ ã�ų� ���� ����
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();

                // ���� ������ ���� ���� ������Ʈ�� ����� ������Ʈ �߰�
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(UIManager).Name);
                    _instance = singletonObject.AddComponent<UIManager>();
                }
            }
            return _instance;
        }
    }

    // UI ��ҵ��� ������ ������
    private IntroUI introUI;
    private StageSelectUI stageSelectUI;
    private GameUI gameUI;
    private PauseUI pauseUI;
    private GameOverUI gameOverUI;
    private GameClearUI gameClearUI;
    private OptionUI optionUI;
    
    private CameraManager cameraManager;

    private float maxInfection = 100f; // �ִ� ������
    public float currentInfection = 0f; // ���� ������
    public float remainingTime = 100f; // �ʱ� �ð�
    public float killCount = 0; // ����� �ΰ� ��

    // ���� ���¿� ���� ���¸� ������ ����
    private UIState _currentState;
    private UIState _previousState;

    public UIState PreviousState
    {
        get { return _previousState; }
        private set { _previousState = value; }
    }

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

        cameraManager = CameraManager.Instance;

        // ���� enum ���¸� Intro�� ����
        SetIntro();
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

        cameraManager.SetStageSelectVirtualCamera();
    }

    // ���ӽ��� �� ���ӸŴ������� ȣ��
    public void SetPlayGame()
    {
        // enum ���¸� Game���� ����
        ChangeState(UIState.Game);

        // ���� ���۽� Ÿ�̸� �ڷ�ƾ ����
        StartCoroutine(Countdown());
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
    }

    // ������ ���� (�ΰ��� ������ ȣ��)
    public void getInfection(float amount)
    {
        // �������� amount��ŭ ������Ű��, maxInfection(100)�� ���� �ʵ��� ����
        currentInfection += amount;
        currentInfection = Mathf.Min(currentInfection, maxInfection);

        // ����� �ΰ� �� 1����
        killCount++;
    }

    IEnumerator Countdown()
    {
        // ���� �ð��� 100���� �ٽ� ����
        remainingTime = 100f;

        while (remainingTime > 0)
        {
            // 1�� ���
            yield return new WaitForSeconds(1f);

            // ���� �ð��� 1�� ����
            remainingTime--;
        }

        // ���ӿ��� (�����ð�0)
        SetGameOver();
        Debug.Log("�ð� ����!");
    }
}