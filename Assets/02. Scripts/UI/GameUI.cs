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

    public float maxTime;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // �����̴��� �ִ밪�� 1�� ����
        timeSlider.maxValue = 1f;

        maxTime = UIManager.Instance.remainingTime; // maxTime�� UIManager�� �ʱ� �ð����� ����
    }

    private void Update()
    {
        // UI�Ŵ����� �������� ������ ������Ʈ
        SetInfectionNumber(uiManager.currentInfection);

        // �ð� UI�ؽ�Ʈ ������Ʈ
        timerText.text = Mathf.FloorToInt(uiManager.remainingTime).ToString();

        // �ð� �����̴� ������Ʈ: remainingTime�� maxTime���� ������ 0~1 ������ ��
        float elapsedTime = maxTime - uiManager.remainingTime;
        timeSlider.value = elapsedTime / maxTime;
    }

    // ������UI ������Ʈ �Լ�
    public void SetInfectionNumber(float number)
    {
        infectionNumber.text = $"{Mathf.FloorToInt(number)}%";
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
