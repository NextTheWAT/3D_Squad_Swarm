using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backIntroButton;
    [SerializeField] private Button exitButton;

    public TextMeshProUGUI gameOverTimeText; // ���� ���� �ð� ǥ�� �ؽ�Ʈ
    public TextMeshProUGUI gameOverInfectionText; // ���� ������ ǥ�� �ؽ�Ʈ
    public TextMeshProUGUI gameOverKillText; // ���� ����� �ΰ� �� ǥ�� �ؽ�Ʈ

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ���(�ν����Ϳ��� ��ư ���� �� �ʿ����)
        restartButton.onClick.AddListener(OnClickRestartButton);
        backIntroButton.onClick.AddListener(OnClickBackIntroButton);
        exitButton.onClick.AddListener(OnClickExitButton);
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

    // ���� ���Ӿ� �ٽ� ����
    public void OnClickRestartButton()
    {
        // ���� ���� 2�����̶��
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            // ���� �������� ������ �̵�
            SceneManager.LoadScene(uiManager.SelectedStageIndex);
            return;
        }
        // ���� ���Ӿ� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Debug.Log("Restart Button Clicked");
    }

    // ���� ����
    public void OnClickExitButton()
    {
        Application.Quit();
        
        Debug.Log("Exit Button Clicked");
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}