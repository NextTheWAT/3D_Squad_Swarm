using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : BaseUI
{
    // 게임으로 돌아가기
    public void OnClickReturnButton()
    {
        // X버튼을 누르면 이전 상태로 돌아가도록 구현
    }

    // 오디오매니저 볼륨에 접근해서 조절 가능하도록 구현

    // 키입력에 접근해서 변경 가능하도록 구현

    protected override UIState GetUIState()
    {
        return UIState.Option;
    }
}
