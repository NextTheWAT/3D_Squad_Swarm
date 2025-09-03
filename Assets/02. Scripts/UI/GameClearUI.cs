using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearUI : BaseUI
{
    [SerializeField] private Button backIntroButton;
    [SerializeField] private Button nextStageButton;

    public TextMeshProUGUI gameOverTimeText; // ���� ���� �ð� ǥ�� �ؽ�Ʈ
    public TextMeshProUGUI gameOverInfectionText; // ���� ������ ǥ�� �ؽ�Ʈ
    public TextMeshProUGUI gameOverKillText; // ���� ����� �ΰ� �� ǥ�� �ؽ�Ʈ

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ���(�ν����Ϳ��� ��ư ���� �� �ʿ����)
        backIntroButton.onClick.AddListener(OnClickBackIntroButton);
        nextStageButton.onClick.AddListener(OnClickNextStageButton);
    }

    private void OnEnable()
    {
        // �����ð�, ������, ųī��Ʈ �����ͼ� ������Ʈ
        gameOverTimeText.text = ((int)(uiManager.remainingTime)).ToString();
        gameOverInfectionText.text = uiManager.currentInfection.ToString();
        gameOverKillText.text = uiManager.killCount.ToString();
    }

    // ��Ʈ�ξ����� ���ư���
    public void OnClickBackIntroButton()
    {
        // ��Ʈ�ξ� �ٽ� �ε�
        SceneManager.LoadScene(0);

        Debug.Log("Back to Intro Button Clicked");
    }

    // ���� �������� ���Ӿ� ����
    public void OnClickNextStageButton()
    {
        // ���� ���Ӿ� �ε��� ��������
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // ���� ���� 5�� ��(��������3)�̶��
        if (currentSceneIndex == 5)
        {
            // Home������ ���ư���
            SceneManager.LoadScene(0);
        }

        // ���� �������� ���Ӿ� �ε�
        SceneManager.LoadScene(currentSceneIndex + 1);

        Debug.Log("NextStage Button Clicked");
    }

    protected override UIState GetUIState()
    {
        return UIState.GameClear;
    }
}