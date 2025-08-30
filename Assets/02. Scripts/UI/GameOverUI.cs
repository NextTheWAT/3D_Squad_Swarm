using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backIntroButton;
    [SerializeField] private Button exitButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ���(�ν����Ϳ��� ��ư ���� �� �ʿ����)
        restartButton.onClick.AddListener(OnClickRestartButton);
        backIntroButton.onClick.AddListener(OnClickBackIntroButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    // ���� ���Ӿ� �ٽ� ����
    public void OnClickRestartButton()
    {
        // ���� ���Ӿ� �ٽ� �ε�
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Debug.Log("Restart Button Clicked");
    }

    // ��Ʈ�ξ����� ���ư���
    public void OnClickBackIntroButton()
    {
        // ��Ʈ�ξ� �ٽ� �ε�
        // SceneManager.LoadScene(0);

        Debug.Log("Back to Intro Button Clicked");
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