using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button exitButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // 버튼 클릭 이벤트에 함수 등록(인스펙터에서 버튼 연결 할 필요없음)
        startButton.onClick.AddListener(OnClickStartButton);
        optionButton.onClick.AddListener(OnClickOptionButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    // 게임 시작
    public void OnClickStartButton()
    {
        // 게임씬 로드
        // SceneManager.LoadScene("GameScene");
        Debug.Log("Start Button Clicked - Load Game Scene");
    }

    // 옵션창 활성화
    public void OnClickOptionButton()
    {
        // UI매니저의 옵션창 활성화 함수 호출
        uiManager.SetOption();
    }

    // 게임 종료
    public void OnClickExitButton()
    {
        Application.Quit();
    }

    protected override UIState GetUIState()
    {
        return UIState.Intro;
    }
}