using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 마우스 커서가 올라갔을 때 버튼 크기를 키움
        transform.localScale = originalScale * 1.1f;
        // 반짝이는 효과: 코루틴을 사용하거나 UI의 색상 변경
        // 예: GetComponent<Image>().color = new Color(1f, 0.8f, 0f); // 노란색으로 변경
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 마우스 커서가 벗어났을 때 원래 크기로 되돌림
        transform.localScale = originalScale;
        // 예: GetComponent<Image>().color = Color.white; // 원래 색상으로 되돌림
    }
}