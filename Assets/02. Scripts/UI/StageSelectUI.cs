using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUI : BaseUI
{
    [SerializeField] private Button returnButton;
    [SerializeField] private Button nextStageButton;
    [SerializeField] private Button stage1Button;
    [SerializeField] private Button stage2Button;
    [SerializeField] private Button stage3Button;

    private CameraManager cameraManager;

    public Animator animator;

    private AudioSource audioSource;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ���(�ν����Ϳ��� ��ư ���� �� �ʿ����)
        returnButton.onClick.AddListener(OnClickReturnButton);
        nextStageButton.onClick.AddListener(OnClickNextStageButton);
        stage1Button.onClick.AddListener(() => OnClickStartStageButton(1));
        stage2Button.onClick.AddListener(() => OnClickStartStageButton(2));
        stage3Button.onClick.AddListener(() => OnClickStartStageButton(3));

        cameraManager = CameraManager.Instance;

        audioSource = GetComponent<AudioSource>();
    }

    // ��Ʈ�� ȭ������ ���ư���
    public void OnClickReturnButton()
    {
        uiManager.SetIntro();

        cameraManager.SetMainCamera(); // ���� ī�޶�� ��ȯ�Լ� ȣ��
    }

    // ���� Stage ���ù�ư �ҷ�����
    public void OnClickNextStageButton()
    {
        // ���� �������� ��ư Ŭ�� �� �ִϸ��̼� ���
        if (animator != null)
        {
            animator.SetTrigger("NextStage");
        }
        else
        {
            Debug.LogWarning("Animator component not found.");
        }
    }

    // �������� �ε����� �޴� ���� �Լ�
    public void OnClickStartStageButton(int stageIndex)
    {
        StartCoroutine(DelayStageStart(stageIndex));
    }

    // 0�� �����Ͽ� �� ������ �ݴ� �Ҹ� ��� �� �������� ����
    private IEnumerator DelayStageStart(int stageIndex)
    {
        audioSource.PlayOneShot(audioSource.clip);
        
        yield return new WaitForSeconds(2.5f);
        animator.SetTrigger("StartStage");

        yield return new WaitForSeconds(2.0f);

        // �������� ���� ��, UI �Ŵ����� stageSelectCarObject ��Ȱ��ȭ
        uiManager.stageSelectCarObject.SetActive(false);

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