using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : BaseUI
{
    [SerializeField] private Button returnButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button backHomeButton;
    [SerializeField] private Button exitButton;

    public GameObject gameStartPanel; // GameUI의 게임 시작 이미지패널

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // 버튼 클릭 이벤트에 함수 등록(인스펙터에서 버튼 연결 할 필요없음)
        returnButton.onClick.AddListener(OnClickReturnButton);
        optionButton.onClick.AddListener(OnClickOptionButton);
        backHomeButton.onClick.AddListener(OnClickBackHomeButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnEnable()
    {
        // 게임 시작 패널 비활성화
        if (gameStartPanel != null)
        {
            gameStartPanel.SetActive(false);
        }
    }

    // 게임으로 돌아가기
    public void OnClickReturnButton()
    {
        Debug.Log("Return Button Clicked");

        // 게임매니저의 일시정지 상태 해제 함수 호출
        GameManager.Instance.OnPause(false);

        // UI 상태를 게임으로 변경
        uiManager.ChangeState(UIState.Game);
    }

    // 옵션창 활성화
    public void OnClickOptionButton()
    {
        Debug.Log("Option Button Clicked");

        // UI매니저의 옵션창 활성화 함수 호출
        uiManager.SetOption();
    }

    // Home으로 돌아가기
    public void OnClickBackHomeButton()
    {
        Debug.Log("BackHome Button Clicked");

        SceneManager.LoadScene(0);
    }

    // 게임 종료
    public void OnClickExitButton()
    {
        Debug.Log("Exit Button Clicked");

        Application.Quit();
    }

    protected override UIState GetUIState()
    {
        return UIState.Pause;
    }
}
