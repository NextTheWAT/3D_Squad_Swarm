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

public class TimeUPUI : BaseUI
{
    private Animator timesUpAnimator; // 타임즈업 애니메이터
    private AudioSource audioSource; // 타임즈업 오디오

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        timesUpAnimator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
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
