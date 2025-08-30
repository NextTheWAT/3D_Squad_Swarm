using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance;

    public static CameraManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // 씬에 Manager가 없으면 에러를 발생시켜 문제를 알림
                Debug.LogError("CameraManager is not found in the scene.");
            }
            return _instance;
        }
    }

    public CinemachineVirtualCamera mainVirtualCamera;
    public CinemachineVirtualCamera stageSelectVirtualCamera;

    private void Awake()
    {
        // 싱글톤 패턴 초기화 (중복 로직 제거)
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    // 메인 카메라로 전환
    public void SetMainCamera()
    {
        mainVirtualCamera.Priority = 10;
        stageSelectVirtualCamera.Priority = 0;
    }

    // 스테이지 선택 카메라로 전환
    public void SetStageSelectVirtualCamera()
    {
        stageSelectVirtualCamera.Priority = 10; // 더 높은 우선순위로 전환
        mainVirtualCamera.Priority = 0;
    }
}