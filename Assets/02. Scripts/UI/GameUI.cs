using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    private Slider infectionSlider;
    private float maxInfection = 100f;
    private float currentInfection = 0f;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // �ڽ� ������Ʈ���� ������ Slider ������Ʈ ã��
        infectionSlider = GetComponentInChildren<Slider>();
    }

    // ������ ���� �Լ� (�ΰ�/��ɲ� ���� �� ȣ��)
    public void IncreasInfection(float amount)
    {
        currentInfection += amount;
        
        // �������� �ִ�ġ�� ���� �ʵ��� ����
        currentInfection = Mathf.Min(currentInfection, maxInfection);

        Debug.Log($"Infection Increased : {currentInfection}");
    }

    // ���� ������ % ��
    public float GetPercentage()
    {
        return currentInfection / maxInfection;
    }

    private void Update()
    {
        // �ǽð� ������ ������Ʈ
        if (infectionSlider != null)
        {
            infectionSlider.value = GetPercentage();
        }
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
