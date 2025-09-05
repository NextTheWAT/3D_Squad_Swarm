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
        // ���콺 Ŀ���� �ö��� �� ��ư ũ�⸦ Ű��
        transform.localScale = originalScale * 1.1f;
        // ��¦�̴� ȿ��: �ڷ�ƾ�� ����ϰų� UI�� ���� ����
        // ��: GetComponent<Image>().color = new Color(1f, 0.8f, 0f); // ��������� ����
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ���콺 Ŀ���� ����� �� ���� ũ��� �ǵ���
        transform.localScale = originalScale;
        // ��: GetComponent<Image>().color = Color.white; // ���� �������� �ǵ���
    }
}