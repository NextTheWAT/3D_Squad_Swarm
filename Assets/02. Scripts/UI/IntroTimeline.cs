using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class IntroTimeline : MonoBehaviour
{
    private PlayableDirector director;

    void Awake()
    {
        director = GetComponent<PlayableDirector>();

        // 시작할 때 UI 상태를 None으로 설정해서 모든 UI를 비활성화
        UIManager.Instance.ChangeState(UIState.None);
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
        SceneManager.LoadScene(2); // Test : 타임업 게임오버씬 불러오기

        // UI매니저에 저장 된 스테이지 인덱스를 가져옴
        //int sceneNumber = UIManager.Instance.SelectedStageIndex;

        //switch (sceneNumber)
        //{
        //    case 1:
        //        Debug.Log("스테이지 1 시작");
        //        // SceneManager.LoadScene("Stage1Scene");
        //        break;
        //    case 2:
        //        Debug.Log("스테이지 2 시작");
        //        // SceneManager.LoadScene("Stage2Scene");
        //        break;
        //    case 3:
        //        Debug.Log("스테이지 3 시작");
        //        // SceneManager.LoadScene("Stage3Scene");
        //        break;
        //}
    }
}
