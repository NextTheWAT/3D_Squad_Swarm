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

        // 버튼 클릭 이벤트에 함수 등록(인스펙터에서 버튼 연결 할 필요없음)
        returnButton.onClick.AddListener(OnClickReturnButton);
        nextStageButton.onClick.AddListener(OnClickNextStageButton);
        stage1Button.onClick.AddListener(() => OnClickStartStageButton(1));
        stage2Button.onClick.AddListener(() => OnClickStartStageButton(2));
        stage3Button.onClick.AddListener(() => OnClickStartStageButton(3));

        cameraManager = CameraManager.Instance;

        audioSource = GetComponent<AudioSource>();
    }

    // 인트로 화면으로 돌아가기
    public void OnClickReturnButton()
    {
        uiManager.SetIntro();

        cameraManager.SetMainCamera(); // 메인 카메라로 전환함수 호출
    }

    // 다음 Stage 선택버튼 불러오기
    public void OnClickNextStageButton()
    {
        // 다음 스테이지 버튼 클릭 시 애니메이션 재생
        if (animator != null)
        {
            animator.SetTrigger("NextStage");
        }
        else
        {
            Debug.LogWarning("Animator component not found.");
        }
    }

    // 스테이지 인덱스를 받는 단일 함수
    public void OnClickStartStageButton(int stageIndex)
    {
        StartCoroutine(DelayStageStart(stageIndex));
    }

    // 0초 지연하여 차 문열고 닫는 소리 재생 후 스테이지 시작
    private IEnumerator DelayStageStart(int stageIndex)
    {
        audioSource.PlayOneShot(audioSource.clip);
        
        yield return new WaitForSeconds(2.5f);
        animator.SetTrigger("StartStage");

        yield return new WaitForSeconds(2.0f);

        // 스테이지 시작 전, UI 매니저의 stageSelectCarObject 비활성화
        uiManager.stageSelectCarObject.SetActive(false);

        switch (stageIndex)
        {
            case 1:
                Debug.Log("스테이지 1 시작");
                // SceneManager.LoadScene("Stage1Scene");
                break;
            case 2:
                Debug.Log("스테이지 2 시작");
                // SceneManager.LoadScene("Stage2Scene");
                break;
            case 3:
                Debug.Log("스테이지 3 시작");
                // SceneManager.LoadScene("Stage3Scene");
                break;
        }
    }

    protected override UIState GetUIState()
    {
        return UIState.StageSelect;
    }
}