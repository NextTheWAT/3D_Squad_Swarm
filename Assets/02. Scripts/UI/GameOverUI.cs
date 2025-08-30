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

    // 현재 게임씬 다시 시작
    public void OnClickRestartButton()
    {
        // 현재 게임씬 다시 로드
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 인트로씬으로 돌아가기
    public void OnClickBackIntroButton()
    {
        // 인트로씬 다시 로드
        // SceneManager.LoadScene(0);
    }

    // 게임 종료
    public void OnClickExitButton()
    {
        Application.Quit();
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}