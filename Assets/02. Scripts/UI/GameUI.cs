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

        // 자식 오브젝트에서 감염도 Slider 컴포넌트 찾기
        infectionSlider = GetComponentInChildren<Slider>();
    }

    // 감염도 증가 함수 (인간/사냥꾼 죽을 때 호출)
    public void IncreasInfection(float amount)
    {
        currentInfection += amount;
        
        // 감염도가 최대치를 넘지 않도록 제한
        currentInfection = Mathf.Min(currentInfection, maxInfection);

        Debug.Log($"Infection Increased : {currentInfection}");
    }

    // 현재 감염도 % 비교
    public float GetPercentage()
    {
        return currentInfection / maxInfection;
    }

    private void Update()
    {
        // 실시간 감염도 업데이트
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
