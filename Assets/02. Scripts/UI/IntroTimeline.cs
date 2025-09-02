using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class IntroTimeline : MonoBehaviour
{
    private PlayableDirector director;

    // ���� �̸� �ε��ϴ� ���۷��̼�
    private AsyncOperation asyncOperation;

    public GameObject skipButton;

    void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        // ������ �� UI ���¸� None���� �����ؼ� ��� UI�� ��Ȱ��ȭ
        UIManager.Instance.ChangeState(UIState.None);

        // UI�Ŵ����� ���� �� �������� �ε����� �����ͼ� ������ ����
        int selectSceneNumber = UIManager.Instance.SelectedStageIndex;

        // ���� �� �񵿱� �ε� ����
        asyncOperation = SceneManager.LoadSceneAsync(selectSceneNumber);

        // �� �ε� �Ϸ� �� �ٷ� ��ȯ���� Ȯ���ϴ� ������ (false�� ����)
        asyncOperation.allowSceneActivation = false;

        // �ڷ�ƾ ����
        StartCoroutine(WaitForTimelineAndActivate());
    }

    void OnEnable()
    {
        // ��ũ��Ʈ�� Ȱ��ȭ�� �� �̺�Ʈ ���
        director.stopped += OnTimelineFinished;
    }

    void OnDisable()
    {
        // ��ũ��Ʈ�� ��Ȱ��ȭ�� �� �̺�Ʈ ����
        director.stopped -= OnTimelineFinished;
    }

    // 90%������ ���� ���� �ε��ϴ� �ڷ�ƾ
    private IEnumerator WaitForTimelineAndActivate()
    {
        // �� �ε��� 90% �Ϸ�� ������ ��ٸ�
        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }
    }

    // Ÿ�Ӷ����� ������ �� ȣ��Ǵ� �޼���
    private void OnTimelineFinished(PlayableDirector director)
    {
        // �� �ε��� 90% �Ϸ�Ǿ����� Ȯ��
        if (asyncOperation.progress >= 0.9f)
        {
            // �� ��ȯ ��� (true�� ����)
            asyncOperation.allowSceneActivation = true;
        }
    }
}
