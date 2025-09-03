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

    public TextMeshProUGUI gameOverTimeText; // 최종 생존 시간 표시 텍스트
    public TextMeshProUGUI gameOverInfectionText; // 최종 감염도 표시 텍스트
    public TextMeshProUGUI gameOverKillText; // 최종 사냥한 인간 수 표시 텍스트

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // 버튼 클릭 이벤트에 함수 등록(인스펙터에서 버튼 연결 할 필요없음)
        backIntroButton.onClick.AddListener(OnClickBackIntroButton);
        nextStageButton.onClick.AddListener(OnClickNextStageButton);
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

    // 다음 스테이지 게임씬 시작
    public void OnClickNextStageButton()
    {
        // 현재 게임씬 인덱스 가져오기
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 현재 씬이 5번 씬(스테이지3)이라면
        if (currentSceneIndex == 5)
        {
            // Home씬으로 돌아가기
            SceneManager.LoadScene(0);
        }

        // 다음 스테이지 게임씬 로드
        SceneManager.LoadScene(currentSceneIndex + 1);

        Debug.Log("NextStage Button Clicked");
    }

    protected override UIState GetUIState()
    {
        return UIState.GameClear;
    }
}