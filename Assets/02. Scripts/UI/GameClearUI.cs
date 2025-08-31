using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearUI : BaseUI
{
    [SerializeField] private Button backIntroButton;
    [SerializeField] private Button nextStageButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ���(�ν����Ϳ��� ��ư ���� �� �ʿ����)
        backIntroButton.onClick.AddListener(OnClickBackIntroButton);
        nextStageButton.onClick.AddListener(OnClickNextStageButton);
    }

    // ��Ʈ�ξ����� ���ư���
    public void OnClickBackIntroButton()
    {
        // ��Ʈ�ξ� �ٽ� �ε�
        SceneManager.LoadScene(0);

        Debug.Log("Back to Intro Button Clicked");
    }

    // ���� �������� ���Ӿ� ����
    public void OnClickNextStageButton()
    {
        // ���� ���Ӿ� �ε��� ��������
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // ���� �������� ���Ӿ� �ε�
        SceneManager.LoadScene(currentSceneIndex + 1);

        Debug.Log("NextStage Button Clicked");
    }

    protected override UIState GetUIState()
    {
        return UIState.GameClear;
    }
}