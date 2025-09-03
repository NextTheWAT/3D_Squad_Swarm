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
    private Animator timesUpAnimator; // Ÿ����� �ִϸ�����
    private AudioSource audioSource; // Ÿ����� �����

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        timesUpAnimator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Ÿ����� �ִϸ��̼� ��� �Լ� (�ٷ� ���)
    public void PlayTimesUpAnimation()
    {
        // Ÿ����� �ִϸ��̼� ��� (Ÿ�Ӿ� ���� ���)
        timesUpAnimator.SetTrigger("TimesUP");

        // Ÿ����� ����� ��� (ȣ���� �Ҹ�)
        audioSource.PlayOneShot(audioSource.clip);

        StartCoroutine(TimesUpAnimtionDelay(audioSource.clip));
    }

    // Ÿ����� �ִϸ��̼� �ڷ�ƾ (������ �� ���)
    private IEnumerator TimesUpAnimtionDelay(AudioClip audioClip)
    {
        // ����� ���̸�ŭ ���
        yield return new WaitForSeconds(audioClip.length);

        // 2���� �ε� (Ÿ�Ӿ� ���ӿ�����)
        SceneManager.LoadScene(2);
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
