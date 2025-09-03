using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        // �ð� UI�ؽ�Ʈ ������Ʈ
        timerText.text = Mathf.FloorToInt(uiManager.remainingTime).ToString();

        // �ð� �����̴� ������Ʈ
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
