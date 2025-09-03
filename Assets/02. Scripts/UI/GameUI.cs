using System;
using System.Collections;
using System.Collections.Generic;
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

    public Animator timesUpAnimator; // 타임즈업 애니메이터
    private AudioSource audioSource; // 타임즈업 오디오


    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // UI매니저의 감염도를 가져와 업데이트
        SetInfectionNumber(uiManager.currentInfection);

        // 시간 UI텍스트 업데이트
        timerText.text = Mathf.FloorToInt(uiManager.remainingTime).ToString();

        // 시간 슬라이더 업데이트
        timeSlider.value = uiManager.remainingTime / 100f; // 슬라이더 값은 0~1 사이여야 하므로 100으로 나눔
    }

    // 감염도UI 업데이트 함수
    public void SetInfectionNumber(float number)
    {
        infectionNumber.text = $"{Mathf.FloorToInt(number)}%";
    }

    // 타임즈업 애니메이션 재생 함수 (바로 재생)
    public void PlayTimesUpAnimation()
    {
        // 타임즈업 애니메이션 재생 (타임업 글자 띄움)
        timesUpAnimator.SetTrigger("TimesUP");
        // 타임즈업 오디오 재생 (호루라기 소리)
        audioSource.PlayOneShot(audioSource.clip);

        StartCoroutine(TimesUpAnimtionDelay(audioSource.clip));
    }

    // 타임즈업 애니메이션 코루틴 (딜레이 후 재생)
    private IEnumerator TimesUpAnimtionDelay(AudioClip audioClip)
    {
        // 오디오 길이만큼 대기
        yield return new WaitForSeconds(audioClip.length);

        // 2번씬 로드 (타임업 게임오버씬)
        SceneManager.LoadScene(2);
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
