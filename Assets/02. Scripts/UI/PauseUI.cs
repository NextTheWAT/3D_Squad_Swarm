using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : BaseUI
{
    [SerializeField] private Button returnButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button exitButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ���(�ν����Ϳ��� ��ư ���� �� �ʿ����)
        returnButton.onClick.AddListener(OnClickReturnButton);
        optionButton.onClick.AddListener(OnClickOptionButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    // �������� ���ư���
    public void OnClickReturnButton()
    {
        Debug.Log("Return Button Clicked");

        // ���ӸŴ����� �Ͻ����� ���� ���� �Լ� ȣ��
        GameManager.Instance.OnPause(false);

        // UI ���¸� �������� ����
        uiManager.ChangeState(UIState.Game);
    }

    // �ɼ�â Ȱ��ȭ
    public void OnClickOptionButton()
    {
        Debug.Log("Option Button Clicked");

        // UI�Ŵ����� �ɼ�â Ȱ��ȭ �Լ� ȣ��
        uiManager.SetOption();
    }

    // ���� ����
    public void OnClickExitButton()
    {
        Debug.Log("Exit Button Clicked");

        Application.Quit();
    }

    protected override UIState GetUIState()
    {
        return UIState.Pause;
    }
}
