using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : BaseUI
{
    // �������� ���ư���
    public void OnClickReturnButton()
    {
        // X��ư�� ������ ���� ���·� ���ư����� ����
    }

    // ������Ŵ��� ������ �����ؼ� ���� �����ϵ��� ����

    // Ű�Է¿� �����ؼ� ���� �����ϵ��� ����

    protected override UIState GetUIState()
    {
        return UIState.Option;
    }
}
