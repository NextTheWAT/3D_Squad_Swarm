using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverSkipButton : MonoBehaviour
{
    [SerializeField] private Button skipButton;

    public GameOverTimeline GameOverCutScene_TimeUP;

    public Image fadePanel; // 페이드 효과 이미지

    private void Awake()
    {
        // 버튼 클릭 이벤트에 함수 등록(인스펙터에서 버튼 연결 할 필요없음)
        skipButton.onClick.AddListener(OnClickSkipButton);
    }

    // 스킵 버튼 클릭 시 호출되는 함수
    public void OnClickSkipButton()
    {
        Debug.Log("Skip button clicked");

        // 게임오버 UI를 먼저 활성화
        UIManager.Instance.SetGameOver();

        // 페이드 이미지를 명시적으로 비활성화
        if (fadePanel != null)
        {
            fadePanel.gameObject.SetActive(false);
        }

        // 인트로타임라인 스크립트에 접근해서 씬 전환 허용 값을 true로 설정
        GameOverCutScene_TimeUP.Director.Stop();
    }
}
