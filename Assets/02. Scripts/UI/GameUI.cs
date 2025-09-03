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
    public TextMeshProUGUI infectionNumber; // 감염도를 숫자로 표시할 TextMeshPro 컴포넌트
    public TextMeshProUGUI timerText; // 시간을 숫자로 표시할 TextMeshPro 컴포넌트
    public Slider timeSlider; // 시간을 게이지로 표시할 Slider 컴포넌트

    public float maxTime;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // 슬라이더의 최대값을 1로 고정
        timeSlider.maxValue = 1f;

        maxTime = UIManager.Instance.remainingTime; // maxTime을 UIManager의 초기 시간으로 설정
    }

    private void Update()
    {
        // UI매니저의 감염도를 가져와 업데이트
        SetInfectionNumber(uiManager.currentInfection);

        // 시간 UI텍스트 업데이트
        timerText.text = Mathf.FloorToInt(uiManager.remainingTime).ToString();

        // 시간 슬라이더 업데이트: remainingTime을 maxTime으로 나누어 0~1 사이의 값
        float elapsedTime = maxTime - uiManager.remainingTime;
        timeSlider.value = elapsedTime / maxTime;
    }

    // 감염도UI 업데이트 함수
    public void SetInfectionNumber(float number)
    {
        infectionNumber.text = $"{Mathf.FloorToInt(number)}%";
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
