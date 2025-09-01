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

        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ���(�ν����Ϳ��� ��ư ���� �� �ʿ����)
        returnButton.onClick.AddListener(OnClickReturnButton);
    }

    // �������� ���ư���
    public void OnClickReturnButton()
    {
        // UI�Ŵ����� ���� ���¸� ������
        UIState previousState = uiManager.PreviousState;

        // ���ư��� ������ ���� ���·� ���ư����� ����
        uiManager.ChangeState(previousState);
    }

    // Ű�Է¿� �����ؼ� ���� �����ϵ��� ����

    protected override UIState GetUIState()
    {
        return UIState.Option;
    }
}
