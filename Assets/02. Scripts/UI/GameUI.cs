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
    public TextMeshProUGUI infectionNumber; // �������� ���ڷ� ǥ���� TextMeshPro ������Ʈ
    public TextMeshProUGUI timerText; // �ð��� ���ڷ� ǥ���� TextMeshPro ������Ʈ
    public Slider timeSlider; // �ð��� �������� ǥ���� Slider ������Ʈ
    public Image gameStartPanel; // ���� ��ŸƮ �г� �̹���
    
    private float maxTime; // �ִ� �ð��� ������ ����

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // �����̴��� �ִ밪�� 1�� ����
        timeSlider.maxValue = 1f;

        maxTime = UIManager.Instance.remainingTime; // maxTime�� UIManager�� �ʱ� �ð����� ����
    }

    private void Update()
    {
        // UI�Ŵ����� �������� ������ ������Ʈ
        SetInfectionNumber(uiManager.currentInfection);

        // �ð� UI�ؽ�Ʈ ������Ʈ
        timerText.text = Mathf.FloorToInt(uiManager.remainingTime).ToString();

        // �ð� �����̴� ������Ʈ: remainingTime�� maxTime���� ������ 0~1 ������ ��
        float elapsedTime = maxTime - uiManager.remainingTime;
        timeSlider.value = elapsedTime / maxTime;
    }

    // ������UI ������Ʈ �Լ�
    public void SetInfectionNumber(float number)
    {
        infectionNumber.text = $"{Mathf.FloorToInt(number)}%";
    }

    private void Start()
    {
        // ���� ���� �� ���̵� �ƿ� �ڷ�ƾ ����
        StartCoroutine(GameStartFadeOut());
    }

    private IEnumerator GameStartFadeOut()
    {
        float duration = 1.5f; // ���̵� �ƿ� ���� �ð�
        float elapsed = 0.0f; // ��� �ð�
        Color originalColor = gameStartPanel.color; // ���� ���� ����
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime; // ��� �ð� ������Ʈ
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsed / duration); // ���� �� ���
            gameStartPanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); // ���� �� ����
            yield return null; // ���� �����ӱ��� ���
        }
        // ���̵� �ƿ��� �Ϸ�Ǹ� �г��� ������ �����ϰ� �����ϰ� ��Ȱ��ȭ
        gameStartPanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.0f);
        gameStartPanel.gameObject.SetActive(false);
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
