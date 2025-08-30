using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUI : BaseUI
{
    [SerializeField] private Button returnButton;
    [SerializeField] private Button stage1Button;
    [SerializeField] private Button stage2Button;
    [SerializeField] private Button stage3Button;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // 버튼 클릭 이벤트에 함수 등록(인스펙터에서 버튼 연결 할 필요없음)
        returnButton.onClick.AddListener(OnClickReturnButton);
        stage1Button.onClick.AddListener(() => OnClickStartStageButton(1));
        stage2Button.onClick.AddListener(() => OnClickStartStageButton(2));
        stage3Button.onClick.AddListener(() => OnClickStartStageButton(3));
    }

    public void OnClickReturnButton()
    {
        uiManager.SetIntro();
    }

    // 스테이지 인덱스를 받는 단일 함수
    public void OnClickStartStageButton(int stageIndex)
    {
        switch (stageIndex)
        {
            case 1:
                Debug.Log("스테이지 1 시작");
                // SceneManager.LoadScene("Stage1Scene");
                break;
            case 2:
                Debug.Log("스테이지 2 시작");
                // SceneManager.LoadScene("Stage2Scene");
                break;
            case 3:
                Debug.Log("스테이지 3 시작");
                // SceneManager.LoadScene("Stage3Scene");
                break;
        }
    }

    protected override UIState GetUIState()
    {
        return UIState.StageSelect;
    }
}