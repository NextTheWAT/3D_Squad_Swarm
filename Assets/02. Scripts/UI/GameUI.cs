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
    public Image gameStartPanel; // 게임 스타트 패널 이미지
    
    private float maxTime; // 최대 시간을 저장할 변수

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

    private void Start()
    {
        // 게임 시작 시 페이드 아웃 코루틴 시작
        StartCoroutine(GameStartFadeOut());
    }

    private IEnumerator GameStartFadeOut()
    {
        float duration = 1.5f; // 페이드 아웃 지속 시간
        float elapsed = 0.0f; // 경과 시간
        Color originalColor = gameStartPanel.color; // 원래 색상 저장
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime; // 경과 시간 업데이트
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsed / duration); // 알파 값 계산
            gameStartPanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); // 알파 값 적용
            yield return null; // 다음 프레임까지 대기
        }
        // 페이드 아웃이 완료되면 패널을 완전히 투명하게 설정하고 비활성화
        gameStartPanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.0f);
        gameStartPanel.gameObject.SetActive(false);
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
