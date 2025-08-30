using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : BaseUI
{
    [SerializeField] private Button returnButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // 버튼 클릭 이벤트에 함수 등록(인스펙터에서 버튼 연결 할 필요없음)
        returnButton.onClick.AddListener(OnClickReturnButton);
    }

    // 게임으로 돌아가기
    public void OnClickReturnButton()
    {
        // UI매니저의 이전 상태를 가져옴
        UIState previousState = uiManager.PreviousState;

        // 돌아가기 누르면 이전 상태로 돌아가도록 구현
        uiManager.ChangeState(previousState);
    }

    // 키입력에 접근해서 변경 가능하도록 구현

    protected override UIState GetUIState()
    {
        return UIState.Option;
    }
}
