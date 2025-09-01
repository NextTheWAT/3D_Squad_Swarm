using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameOverTimeline : MonoBehaviour
{
    private PlayableDirector director;

    void Awake()
    {
        director = GetComponent<PlayableDirector>();
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

    // Ÿ�Ӷ����� ������ �� ȣ��Ǵ� �޼���
    private void OnTimelineFinished(PlayableDirector director)
    {
        Debug.Log("GameOver Timeline Finished");
        // UIManager�� SetGameOver �޼��带 ȣ��
        UIManager.Instance.SetGameOver();
    }
}
