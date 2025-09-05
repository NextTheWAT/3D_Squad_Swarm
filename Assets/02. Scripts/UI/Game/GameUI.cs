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

    public TextMeshProUGUI infectionNumber; // �������� ���ڷ� ǥ���� TextMeshPro ������Ʈ
    public TextMeshProUGUI timerText; // �ð��� ���ڷ� ǥ���� TextMeshPro ������Ʈ
    public Slider timeSlider; // �ð��� �������� ǥ���� Slider ������Ʈ
    
    public GameObject infoPanel; // ���� ���� �г�
    public Image gameStartPanel; // ���� ��ŸƮ �г� �̹���
    
    private float maxTime; // �ִ� �ð��� ������ ����

    public bool gameInfoChecked; // ���ӽ��۽� ����â ���� ����

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ���(�ν����Ϳ��� ��ư ���� �� �ʿ����)
        infoButton.onClick.AddListener(OnClickInfoButton);
        infoExitButton.onClick.AddListener(OnClickInfoExitButton);
        pauseButton.onClick.AddListener(OnClickPauseButton);

        // �����̴��� �ִ밪�� 1�� ����
        timeSlider.maxValue = 1f;

        maxTime = UIManager.Instance.remainingTime; // maxTime�� UIManager�� �ʱ� �ð����� ����

        gameInfoChecked = true;
    }

    // UI Ȱ��ȭ �� ������ �ʱ�ȭ
    private void OnEnable()
    {
        if (uiManager.PreviousState == UIState.Pause)
        {
            return; // ���� ���°� �Ͻ����� ���¶�� �ʱ�ȭ���� ����
        }

        // ���ӸŴ����� ���ӽð� �Ͻ����� �Լ� ȣ��(�����Ͻ�����)
        GameManager.Instance.OnPause(true);

        // UI�Ŵ����� ���簨������ 0���� �ʱ�ȭ
        uiManager.currentInfection = 0f;

        // UI�Ŵ����� ųī��Ʈ�� 0���� �ʱ�ȭ
        uiManager.killCount = 0f;

        // UI�Ŵ����� �����ð��� �ʱ�ȭ
        uiManager.remainingTime = maxTime;

        Debug.Log($"gameInfoChecked : {gameInfoChecked}");

        // ���� ���� �� gameInfoChecked�� true��
        if (gameInfoChecked == true)
        {
            Debug.Log("��������â Ȱ��ȭ");

            // ���ӸŴ����� ���ӽð� �Ͻ����� �Լ� ȣ��(�����Ͻ�����)
            GameManager.Instance.OnPause(true);

            // ����â UI Ȱ��ȭ
            infoPanel.SetActive(true);
        }

        // false��
        else if (gameInfoChecked == false)
        {
            Debug.Log("��������â ��Ȱ��ȭ");

            GameManager.Instance.OnPause(false);

            // ���� ���� �� ���̵� �ƿ� �ڷ�ƾ ����
            StartCoroutine(GameStartFadeInOut());
        }
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

    // �������� ��ư Ŭ�� �Լ�
    public void OnClickInfoButton()
    {
        // ���ӸŴ����� ���ӽð� �Ͻ����� �Լ� ȣ��(�����Ͻ�����)
        GameManager.Instance.OnPause(true);

        // �������� UI Ȱ��ȭ
        infoPanel.SetActive(true);
    }

    // �������� �ݱ� ��ư Ŭ�� �Լ�
    public void OnClickInfoExitButton()
    {
        // ���ӸŴ����� ���ӽð� �Ͻ����� �Լ� ȣ��(�����簳)
        GameManager.Instance.OnPause(false);

        // �������� UI ��Ȱ��ȭ
        infoPanel.SetActive(false);

        if (gameInfoChecked == true)
        {
            // ���� ���� �� ���̵� �ƿ� �ڷ�ƾ ����
            StartCoroutine(GameStartFadeInOut());
        }

        // ��������â�� �ٽ� �����ٰ� �������� �ٽ� �ȶ߰� false�� ����
        gameInfoChecked = false;
    }
    
    // �Ͻ����� ��ư Ŭ�� �Լ�
    public void OnClickPauseButton()
    {
        // �Ͻ����� UI Ȱ��ȭ �Լ� ȣ��
        uiManager.SetPause();
    }

    // ������UI ������Ʈ �Լ�
    public void SetInfectionNumber(float number)
    {
        infectionNumber.text = $"{Mathf.FloorToInt(number)}%";
    }

    // ���� ���� �� ���̵� �� / �ƿ� �ڷ�ƾ
    private IEnumerator GameStartFadeInOut()
    {
        // ���� �� �г� Ȱ��ȭ
        gameStartPanel.gameObject.SetActive(true);

        float duration = 1.5f; // ���̵� ���� �ð�
        float elapsed = 0.0f; // ��� �ð�
        Color originalColor = gameStartPanel.color; // ���� ���� ����

        // --- Fade In ---
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime; // ��� �ð� ������Ʈ
            float alpha = Mathf.Lerp(0.0f, 1.0f, elapsed / duration); // ���� �� ��� (0.0 -> 1.0)
            gameStartPanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); // ���� �� ����
            yield return null; // ���� �����ӱ��� ���
        }
        // Fade In �Ϸ� �� ������ �������ϰ� ����
        gameStartPanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1.0f);

        // Fade Out ���� �� ��� �ð� �ʱ�ȭ
        elapsed = 0.0f;

        // --- Fade Out ---
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime; // ��� �ð� ������Ʈ
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsed / duration); // ���� �� ��� (1.0 -> 0.0)
            gameStartPanel.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); // ���� �� ����
            yield return null; // ���� �����ӱ��� ���
        }

        // ���̵� �ƿ� �Ϸ� �� �г��� ��Ȱ��ȭ
        gameStartPanel.gameObject.SetActive(false);
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
