using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSkipButton : MonoBehaviour
{
    [SerializeField] private Button skipButton;

    public IntroTimeline introTimeline;

    private void Awake()
    {
        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ���(�ν����Ϳ��� ��ư ���� �� �ʿ����)
        skipButton.onClick.AddListener(OnClickSkipButton);
    }

    // ��ŵ ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void OnClickSkipButton()
    {
        Debug.Log("Skip button clicked");
        // ��Ʈ��Ÿ�Ӷ��� ��ũ��Ʈ�� �����ؼ� �� ��ȯ ��� ���� true�� ����
        introTimeline.AsyncOperation.allowSceneActivation = true;
    }
}
