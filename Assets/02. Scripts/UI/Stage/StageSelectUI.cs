using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject human;

    private AudioSource audioSource;

    public Image panel; // ��ư Ŭ�� ���� �г� �̹���

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ���(�ν����Ϳ��� ��ư ���� �� �ʿ����)
        returnButton.onClick.AddListener(OnClickReturnButton);
        nextStageButton.onClick.AddListener(OnClickNextStageButton);
        stage1Button.onClick.AddListener(() => OnClickStartStageButton(3)); // 3�� ��(��������1)���� �̵�
        stage2Button.onClick.AddListener(() => OnClickStartStageButton(4)); // 4�� ��(��������2)���� �̵�
        stage3Button.onClick.AddListener(() => OnClickStartStageButton(5)); // 5�� ��(��������3)���� �̵�

        if (panel != null)
        {
            panel.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        // ó�� Ȱ��ȭ�� �� �г� ��Ȱ��ȭ
        panel.gameObject.SetActive(false);

        if (cameraManager == null)
        {
            cameraManager = CameraManager.Instance;
        }

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
        panel.gameObject.SetActive(true);
    }

    // 0�� �����Ͽ� �� ������ �ݴ� �Ҹ� ��� �� �������� ����
    private IEnumerator DelayStageStart(int stageIndex)
    {
        uiManager.SelectedStageIndex = stageIndex; // ���õ� �������� �ε����� ������ �����ص� -> ��Ʈ�ξ����� ����� ����


        audioSource.PlayOneShot(audioSource.clip);
        yield return new WaitForSeconds(0.5f);

        human.SetActive(true); // �ΰ� ������Ʈ Ȱ��ȭ
        
        yield return new WaitForSeconds(1.0f);
        
        human.SetActive(false); // �ΰ� ������Ʈ ��Ȱ��ȭ

        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("StartStage");

        yield return new WaitForSeconds(2.0f);

        // �������� ���� ��, UI �Ŵ����� stageSelectCarObject ��Ȱ��ȭ
        uiManager.stageSelectCarObject.SetActive(false);

        SceneManager.LoadScene(1); // 1�� ��(��Ʈ�ξ�)���� �̵�
    }

    protected override UIState GetUIState()
    {
        return UIState.StageSelect;
    }
}