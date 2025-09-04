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

    public TextMeshProUGUI gameOverTimeText; // 최종 생존 시간 표시 텍스트
    public TextMeshProUGUI gameOverInfectionText; // 최종 감염도 표시 텍스트
    public TextMeshProUGUI gameOverKillText; // 최종 사냥한 인간 수 표시 텍스트

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // 버튼 클릭 이벤트에 함수 등록(인스펙터에서 버튼 연결 할 필요없음)
        restartButton.onClick.AddListener(OnClickRestartButton);
        backIntroButton.onClick.AddListener(OnClickBackIntroButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnEnable()
    {
        // 남은시간, 감염도, 킬카운트 가져와서 업데이트
        gameOverTimeText.text = ((int)(uiManager.remainingTime)).ToString();
        gameOverInfectionText.text = uiManager.currentInfection.ToString();
        gameOverKillText.text = uiManager.killCount.ToString();
    }

    // 인트로씬으로 돌아가기
    public void OnClickBackIntroButton()
    {
        // 인트로씬 다시 로드
        SceneManager.LoadScene(0);

        Debug.Log("Back to Intro Button Clicked");
    }

    // 현재 게임씬 다시 시작
    public void OnClickRestartButton()
    {
        // 현재 씬이 2번씬이라면
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            // 이전 스테이지 씬으로 이동
            SceneManager.LoadScene(uiManager.SelectedStageIndex);
            return;
        }
        // 현재 게임씬 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Debug.Log("Restart Button Clicked");
    }

    // 게임 종료
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