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

    // 감염도 증가 (인간이 죽을때 호출)
    public void getInfection(float amount)
    {
        // 감염도를 amount만큼 증가시키고, maxInfection(100)을 넘지 않도록 제한
        currentInfection += amount;
        currentInfection = Mathf.Min(currentInfection, maxInfection);

        // 감염도 UI 업데이트 함수 호출
        SetInfectionNumber(currentInfection);
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
