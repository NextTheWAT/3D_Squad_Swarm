using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class IntroTimeline : MonoBehaviour
{
    private PlayableDirector director;

    // 씬을 미리 로드하는 오퍼레이션
    private AsyncOperation _asyncOperation;

    public AsyncOperation AsyncOperation
    {
        get { return _asyncOperation; }
        set { _asyncOperation = value; }
    }

    void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        // 시작할 때 UI 상태를 None으로 설정해서 모든 UI를 비활성화
        UIManager.Instance.ChangeState(UIState.None);

        // UI매니저에 저장 된 스테이지 인덱스를 가져와서 변수에 저장
        int selectSceneNumber = UIManager.Instance.SelectedStageIndex;

        // 다음 씬 비동기 로드 시작
        _asyncOperation = SceneManager.LoadSceneAsync(selectSceneNumber);

        // 씬 로딩 완료 후 바로 전환될지 확인하는 설정값 (false로 설정)
        _asyncOperation.allowSceneActivation = false;

        // 코루틴 시작
        StartCoroutine(WaitForTimelineAndActivate());
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

    // 90%까지만 다음 씬을 로딩하는 코루틴
    private IEnumerator WaitForTimelineAndActivate()
    {
        // 씬 로딩이 90% 완료될 때까지 기다림
        while (_asyncOperation.progress < 0.9f)
        {
            yield return null;
        }
    }

    // 타임라인이 끝났을 때 호출되는 메서드
    private void OnTimelineFinished(PlayableDirector director)
    {
        // 씬 로딩이 90% 완료되었는지 확인
        if (_asyncOperation.progress >= 0.9f)
        {
            // 씬 전환 허용 (true로 설정)
            _asyncOperation.allowSceneActivation = true;
        }
    }
}
