using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    // UIManager �ν��Ͻ��� ������ ����
    protected UIManager uiManager;

    // ���� ���ӹ����� ���� Awake ��� UIManager�� Awake���� ���� ���� ȣ��
    public virtual void Init(UIManager uiManager)
    {
        // uiManager ������ UIManager���� �޾ƿ� �ν��Ͻ��� ����
        this.uiManager = uiManager;
    }

    // �Ʒ� SetActive���� ���� �ڽ��� enum ���°� ���� �������� ���� �߻�޼��� (�� ����UI���� ����)
    protected abstract UIState GetUIState();

    // ���� �Ű�����(��:Intro)�� ���� UI�� ���°� ��ġ�ϸ� Ȱ��ȭ, �ƴϸ� ��Ȱ��ȭ
    public void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
}
