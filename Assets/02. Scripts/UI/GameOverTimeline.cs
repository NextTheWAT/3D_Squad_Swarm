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
        // 스크립트가 활성화될 때 이벤트 등록
        director.stopped += OnTimelineFinished;
    }

    void OnDisable()
    {
        // 스크립트가 비활성화될 때 이벤트 해제
        director.stopped -= OnTimelineFinished;
    }

    // 타임라인이 끝났을 때 호출되는 메서드
    private void OnTimelineFinished(PlayableDirector director)
    {
        Debug.Log("GameOver Timeline Finished");
        // UIManager의 SetGameOver 메서드를 호출
        UIManager.Instance.SetGameOver();
    }
}
