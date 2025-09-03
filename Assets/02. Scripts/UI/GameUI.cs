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
    public TextMeshProUGUI infectionNumber; // �������� ���ڷ� ǥ���� TextMeshPro ������Ʈ
    public TextMeshProUGUI timerText; // �ð��� ���ڷ� ǥ���� TextMeshPro ������Ʈ
    public Slider timeSlider; // �ð��� �������� ǥ���� Slider ������Ʈ

    public Animator timesUpAnimator; // Ÿ����� �ִϸ�����
    private AudioSource audioSource; // Ÿ����� �����


    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // UI�Ŵ����� �������� ������ ������Ʈ
        SetInfectionNumber(uiManager.currentInfection);

        // �ð� UI�ؽ�Ʈ ������Ʈ
        timerText.text = Mathf.FloorToInt(uiManager.remainingTime).ToString();

        // �ð� �����̴� ������Ʈ
        timeSlider.value = uiManager.remainingTime / 100f; // �����̴� ���� 0~1 ���̿��� �ϹǷ� 100���� ����
    }

    // ������UI ������Ʈ �Լ�
    public void SetInfectionNumber(float number)
    {
        infectionNumber.text = $"{Mathf.FloorToInt(number)}%";
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
