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
        restartButton.onClick.AddListener(OnClickRestartButton);
        backIntroButton.onClick.AddListener(OnClickBackIntroButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    // ���� ���Ӿ� �ٽ� ����
    public void OnClickRestartButton()
    {
        // ���� ���Ӿ� �ٽ� �ε�
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ��Ʈ�ξ����� ���ư���
    public void OnClickBackIntroButton()
    {
        // ��Ʈ�ξ� �ٽ� �ε�
        // SceneManager.LoadScene(0);
    }

    // ���� ����
    public void OnClickExitButton()
    {
        Application.Quit();
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}