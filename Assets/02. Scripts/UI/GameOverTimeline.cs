using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameOverTimeline : MonoBehaviour
{
    private PlayableDirector _director;

    public PlayableDirector Director
    {
        get { return _director; }
        set { _director = value; }
    }

    void Awake()
    {
        _director = GetComponent<PlayableDirector>();

        // ������ �� UI ���¸� None���� �����ؼ� ��� UI�� ��Ȱ��ȭ
        UIManager.Instance.ChangeState(UIState.None);
    }

    void OnEnable()
    {
        // ��ũ��Ʈ�� Ȱ��ȭ�� �� �̺�Ʈ ���
        _director.stopped += OnTimelineFinished;
    }

    void OnDisable()
    {
        // ��ũ��Ʈ�� ��Ȱ��ȭ�� �� �̺�Ʈ ����
        _director.stopped -= OnTimelineFinished;
    }

    // Ÿ�Ӷ����� ������ �� ȣ��Ǵ� �޼���
    private void OnTimelineFinished(PlayableDirector director)
    {
        Debug.Log("GameOver Timeline Finished");
        // UIManager�� SetGameOver �޼��带 ȣ��
        UIManager.Instance.SetGameOver();
    }
}
