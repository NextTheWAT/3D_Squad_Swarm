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
    [SerializeField] private Button infoButton;
    [SerializeField] private Button infoExitButton;
    [SerializeField] private Button pauseButton;

    public TextMeshProUGUI infectionNumber; // 감염도를 숫자로 표시할 TextMeshPro 컴포넌트
    public TextMeshProUGUI timerText; // 시간을 숫자로 표시할 TextMeshPro 컴포넌트
    public Slider timeSlider; // 시간을 게이지로 표시할 Slider 컴포넌트
    
    public GameObject infoPanel; // 게임 정보 패널
    public Image gameStartPanel; // 게임 스타트 패널 이미지
    
    private float maxTime; // 최대 시간을 저장할 변수

    public bool gameInfoChecked; // 게임시작시 정보창 볼지 여부

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // 버튼 클릭 이벤트에 함수 등록(인스펙터에서 버튼 연결 할 필요없음)
        infoButton.onClick.AddListener(OnClickInfoButton);
        infoExitButton.onClick.AddListener(OnClickInfoExitButton);
        pauseButton.onClick.AddListener(OnClickPauseButton);

        // 슬라이더의 최대값을 1로 고정
        timeSlider.maxValue = 1f;

        maxTime = UIManager.Instance.remainingTime; // maxTime을 UIManager의 초기 시간으로 설정

        gameInfoChecked = true;
    }

    // UI 활성화 시 감염도 초기화
    private void OnEnable()
    {
        if (uiManager.PreviousState == UIState.Pause)
        {
            return; // 이전 상태가 일시정지 상태라면 초기화하지 않음
        }

        // 게임매니저의 게임시간 일시정지 함수 호출(게임일시정지)
        GameManager.Instance.OnPause(true);

        // UI매니저의 현재감염도를 0으로 초기화
        uiManager.currentInfection = 0f;

        // UI매니저의 킬카운트를 0으로 초기화
        uiManager.killCount = 0f;

        // UI매니저의 남은시간을 초기화
        uiManager.remainingTime = maxTime;

        Debug.Log($"gameInfoChecked : {gameInfoChecked}");

        // 게임 시작 시 gameInfoChecked가 true면
        if (gameInfoChecked == true)
        {
            Debug.Log("게임정보창 활성화");

            // 게임매니저의 게임시간 일시정지 함수 호출(게임일시정지)
            GameManager.Instance.OnPause(true);

            // 정보창 UI 활성화
            infoPanel.SetActive(true);
        }

        // false면
        else if (gameInfoChecked == false)
        {
            Debug.Log("게임정보창 비활성화");

            GameManager.Instance.OnPause(false);

            // 게임 시작 시 페이드 아웃 코루틴 시작
            StartCoroutine(GameStartFadeInOut());
        }
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

    // 게임인포 버튼 클릭 함수
    public void OnClickInfoButton()
    {
        // 게임매니저의 게임시간 일시정지 함수 호출(게임일시정지)
        GameManager.Instance.OnPause(true);

        // 게임정보 UI 활성화
        infoPanel.SetActive(true);
    }

    // 게임인포 닫기 버튼 클릭 함수
    public void OnClickInfoExitButton()
    {
        // 게임매니저의 게임시간 일시정지 함수 호출(게임재개)
        GameManager.Instance.OnPause(false);

        // 게임정보 UI 비활성화
        infoPanel.SetActive(false);

        if (gameInfoChecked == true)
        {
            // 게임 시작 시 페이드 아웃 코루틴 시작
            StartCoroutine(GameStartFadeInOut());
        }

        // 게임정보창을 다시 열었다가 껐을때는 다시 안뜨게 false로 변경
        gameInfoChecked = false;
    }
    
    // 일시정지 버튼 클릭 함수
    public void OnClickPauseButton()
    {
        // 일시정지 UI 활성화 함수 호출
        uiManager.SetPause();
    }

    // 감염도UI 업데이트 함수
    public void SetInfectionNumber(float number)
    {
        infectionNumber.text = $"{Mathf.FloorToInt(number)}%";
    }

    // 게임 시작 시 페이드 인 / 아웃 코루틴
    private IEnumerator GameStartFadeInOut()
    {
        // 시작 시 패널 활성화
        gameStartPanel.gameObject.SetActive(true);

        float duration = 1.5f; // 페이드 지속 시간
        float elapsed = 0.0f; // 경과 시간
        Color originalColor = gameStartPanel.color; // 원래 색상 저장

        // --- Fade In ---
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime; // 경과 시간 업데이트
            float alpha = Mathf.Lerp(0.0f, 1.0f, elapsed / duration); // 알파 값 계산 (0.0 -> 1.0)
            gameStartPanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); // 알파 값 적용
            yield return null; // 다음 프레임까지 대기
        }
        // Fade In 완료 후 완전히 불투명하게 설정
        gameStartPanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1.0f);

        // Fade Out 시작 전 경과 시간 초기화
        elapsed = 0.0f;

        // --- Fade Out ---
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime; // 경과 시간 업데이트
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsed / duration); // 알파 값 계산 (1.0 -> 0.0)
            gameStartPanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); // 알파 값 적용
            yield return null; // 다음 프레임까지 대기
        }

        // 페이드 아웃 완료 후 패널을 비활성화
        gameStartPanel.gameObject.SetActive(false);
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
