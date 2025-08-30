using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button exitButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ���(�ν����Ϳ��� ��ư ���� �� �ʿ����)
        startButton.onClick.AddListener(OnClickStartButton);
        optionButton.onClick.AddListener(OnClickOptionButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    // ���� ����
    public void OnClickStartButton()
    {
        // ���Ӿ� �ε�
        // SceneManager.LoadScene("GameScene");
        Debug.Log("Start Button Clicked - Load Game Scene");
    }

    // �ɼ�â Ȱ��ȭ
    public void OnClickOptionButton()
    {
        // UI�Ŵ����� �ɼ�â Ȱ��ȭ �Լ� ȣ��
        uiManager.SetOption();
    }

    // ���� ����
    public void OnClickExitButton()
    {
        Application.Quit();
    }

    protected override UIState GetUIState()
    {
        return UIState.Intro;
    }
}