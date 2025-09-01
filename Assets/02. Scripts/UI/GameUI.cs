using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    public TextMeshProUGUI infectionNumber; // �������� ���ڷ� ǥ���� TextMeshPro ������Ʈ
    public TextMeshProUGUI timerText; // �ð��� ���ڷ� ǥ���� TextMeshPro ������Ʈ
    public Slider timeSlider; // �ð��� �������� ǥ���� Slider ������Ʈ


    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

    private void Update()
    {
        // UI�Ŵ����� �������� ������ ������Ʈ
        SetInfectionNumber(uiManager.currentInfection);

        // UI�ؽ�Ʈ ������Ʈ
        timerText.text = "���� �ð�: " + Mathf.FloorToInt(uiManager.remainingTime).ToString();

        // �����̴� ������Ʈ
        timeSlider.value = uiManager.remainingTime / 100f; // �����̴� ���� 0~1 ���̿��� �ϹǷ� 100���� ����
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
