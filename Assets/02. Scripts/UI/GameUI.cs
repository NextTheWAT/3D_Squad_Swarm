using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    public TextMeshProUGUI infectionNumber;

    private float maxInfection = 100f;
    private float currentInfection = 0f;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

    // ������ ���� (�ΰ��� ������ ȣ��)
    public void getInfection(float amount)
    {
        // �������� amount��ŭ ������Ű��, maxInfection(100)�� ���� �ʵ��� ����
        currentInfection += amount;
        currentInfection = Mathf.Min(currentInfection, maxInfection);

        // ������ UI ������Ʈ �Լ� ȣ��
        SetInfectionNumber(currentInfection);
    }

    // ������UI ������Ʈ �Լ�
    public void SetInfectionNumber(float number)
    {
        infectionNumber.text = number.ToString();
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
