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

    // �̱���
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // ���� Manager�� ������ ������ �߻����� ������ �˸�
                Debug.LogError("UIManager is not found in the scene.");
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
    private OptionUI optionUI;
    private UIState currentState;

    private void Awake()
    {
        // �̱��� ���� �ʱ�ȭ (�ߺ� ���� ����)
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
        optionUI = GetComponentInChildren<OptionUI>(true);
        optionUI.Init(this);

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
    }

    // ���ӽ��� �� ���ӸŴ������� ȣ��
    public void SetPlayGame()
    {
        // enum ���¸� Game���� ����
        ChangeState(UIState.Game);
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

    // �� �ɼǹ�ư���� ȣ�� (��Ʈ��, �Ͻ�����)
    public void SetOption()
    {
        // enum ���¸� Option�� ����
        ChangeState(UIState.Option);
    }

    // UI ���� ���� �޼���
    public void ChangeState(UIState state)
    {
        // ���� ���¸� ������Ʈ
        currentState = state;

        // �� UI�� SetActive ����� ������ ���� enum ���¿� ��ġ�ϴ��� ���� Ȱ��ȭ ���� ����
        introUI.SetActive(currentState);
        stageSelectUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        pauseUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
        optionUI.SetActive(currentState);
    }
}