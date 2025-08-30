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
        // ���ӸŴ����� �Ͻ����� ���� ���� �Լ� ȣ��
        // GameManager.Instance.TogglePause();

        // UI ���¸� �������� ����
        uiManager.ChangeState(UIState.Game);
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
        return UIState.Pause;
    }
}
