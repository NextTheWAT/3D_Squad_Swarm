using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverSkipButton : MonoBehaviour
{
    [SerializeField] private Button skipButton;

    public GameOverTimeline GameOverCutScene_TimeUP;

    public Image fadePanel; // ���̵� ȿ�� �̹���

    private void Awake()
    {
        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ���(�ν����Ϳ��� ��ư ���� �� �ʿ����)
        skipButton.onClick.AddListener(OnClickSkipButton);
    }

    // ��ŵ ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void OnClickSkipButton()
    {
        Debug.Log("Skip button clicked");

        // ���ӿ��� UI�� ���� Ȱ��ȭ
        UIManager.Instance.SetGameOver();

        // ���̵� �̹����� ��������� ��Ȱ��ȭ
        if (fadePanel != null)
        {
            fadePanel.gameObject.SetActive(false);
        }

        // ��Ʈ��Ÿ�Ӷ��� ��ũ��Ʈ�� �����ؼ� �� ��ȯ ��� ���� true�� ����
        GameOverCutScene_TimeUP.Director.Stop();
    }
}
