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
        // 시작할 때 UI 상태를 None으로 설정해서 모든 UI를 비활성화
        UIManager.Instance.ChangeState(UIState.None);
    }

    void OnEnable()
    {
        // 스크립트가 활성화될 때 이벤트 등록
        _director.stopped += OnTimelineFinished;
    }

    void OnDisable()
    {
        // 스크립트가 비활성화될 때 이벤트 해제
        _director.stopped -= OnTimelineFinished;
    }

    // 타임라인이 끝났을 때 호출되는 메서드
    private void OnTimelineFinished(PlayableDirector director)
    {
        Debug.Log(StageManager.Instance); // <- Null

        if (UIManager.Instance.SelectedStageIndex == 3) StageManager.Instance.Stage1();
        else if(UIManager.Instance.SelectedStageIndex == 4) StageManager.Instance.Stage2();
        else if(UIManager.Instance.SelectedStageIndex == 5) StageManager.Instance.Stage3();
    }
}
