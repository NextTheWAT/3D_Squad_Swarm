using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    public TextMeshProUGUI infectionNumber; // 감염도를 숫자로 표시할 TextMeshPro 컴포넌트
    public TextMeshProUGUI timerText; // 시간을 숫자로 표시할 TextMeshPro 컴포넌트
    public Slider timeSlider; // 시간을 게이지로 표시할 Slider 컴포넌트


    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

    private void Update()
    {
        // UI매니저의 감염도를 가져와 업데이트
        SetInfectionNumber(uiManager.currentInfection);

        // UI텍스트 업데이트
        timerText.text = "남은 시간: " + Mathf.FloorToInt(uiManager.remainingTime).ToString();

        // 슬라이더 업데이트
        timeSlider.value = uiManager.remainingTime / 100f; // 슬라이더 값은 0~1 사이여야 하므로 100으로 나눔
    }

    // 감염도UI 업데이트 함수
    public void SetInfectionNumber(float number)
    {
        infectionNumber.text = number.ToString();
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
