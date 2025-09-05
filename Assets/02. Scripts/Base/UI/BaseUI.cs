using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    // UIManager 인스턴스를 저장할 변수
    protected UIManager uiManager;

    // 순서 꼬임방지를 위해 Awake 대신 UIManager의 Awake에서 직접 생성 호출
    public virtual void Init(UIManager uiManager)
    {
        // uiManager 변수에 UIManager에서 받아온 인스턴스를 저장
        this.uiManager = uiManager;
    }

    // 아래 SetActive에서 현재 자신의 enum 상태가 뭔지 가져오기 위한 추상메서드 (각 상태UI에서 구현)
    protected abstract UIState GetUIState();

    // 받은 매개변수(예:Intro)와 현재 UI의 상태가 일치하면 활성화, 아니면 비활성화
    public void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
}
