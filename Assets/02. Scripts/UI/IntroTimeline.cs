using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class IntroTimeline : MonoBehaviour
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
    }

    private void Start()
    {
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
        Debug.Log(StageManager.Instance); // <- Null

        if (UIManager.Instance.SelectedStageIndex == 3) StageManager.Instance.Stage1();
        else if(UIManager.Instance.SelectedStageIndex == 4) StageManager.Instance.Stage2();
        else if(UIManager.Instance.SelectedStageIndex == 5) StageManager.Instance.Stage3();
    }
}
