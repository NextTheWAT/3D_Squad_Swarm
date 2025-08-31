using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearUI : BaseUI
{
    [SerializeField] private Button backIntroButton;
    [SerializeField] private Button nextStageButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // 버튼 클릭 이벤트에 함수 등록(인스펙터에서 버튼 연결 할 필요없음)
        backIntroButton.onClick.AddListener(OnClickBackIntroButton);
        nextStageButton.onClick.AddListener(OnClickNextStageButton);
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
        // 다음 스테이지 게임씬 로드
        SceneManager.LoadScene(currentSceneIndex + 1);

        Debug.Log("NextStage Button Clicked");
    }

    protected override UIState GetUIState()
    {
        return UIState.GameClear;
    }
}