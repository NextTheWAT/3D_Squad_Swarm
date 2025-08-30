using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUI : BaseUI
{
    [SerializeField] private Button returnButton;
    [SerializeField] private Button stage1Button;
    [SerializeField] private Button stage2Button;
    [SerializeField] private Button stage3Button;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ���(�ν����Ϳ��� ��ư ���� �� �ʿ����)
        returnButton.onClick.AddListener(OnClickReturnButton);
        stage1Button.onClick.AddListener(() => OnClickStartStageButton(1));
        stage2Button.onClick.AddListener(() => OnClickStartStageButton(2));
        stage3Button.onClick.AddListener(() => OnClickStartStageButton(3));
    }

    public void OnClickReturnButton()
    {
        uiManager.SetIntro();
    }

    // �������� �ε����� �޴� ���� �Լ�
    public void OnClickStartStageButton(int stageIndex)
    {
        switch (stageIndex)
        {
            case 1:
                Debug.Log("�������� 1 ����");
                // SceneManager.LoadScene("Stage1Scene");
                break;
            case 2:
                Debug.Log("�������� 2 ����");
                // SceneManager.LoadScene("Stage2Scene");
                break;
            case 3:
                Debug.Log("�������� 3 ����");
                // SceneManager.LoadScene("Stage3Scene");
                break;
        }
    }

    protected override UIState GetUIState()
    {
        return UIState.StageSelect;
    }
}