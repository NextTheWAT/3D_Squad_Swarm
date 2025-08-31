using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance;

    public static CameraManager Instance
    {
        get
        {
            // 인스턴스가 존재하지 않으면 씬에서 찾거나 새로 생성
            if (_instance == null)
            {
                _instance = FindObjectOfType<CameraManager>();

                // 씬에 없으면 새로 게임 오브젝트를 만들어 컴포넌트 추가
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(CameraManager).Name);
                    _instance = singletonObject.AddComponent<CameraManager>();
                }
            }
            return _instance;
        }
    }

    public CinemachineVirtualCamera mainVirtualCamera;
    public CinemachineVirtualCamera stageSelectVirtualCamera;

    private void Awake()
    {
        // 인스턴스가 이미 존재하고, 나 자신이 아니라면 파괴
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 인스턴스 초기화 및 씬 전환 시 파괴 방지
        _instance = this;
        DontDestroyOnLoad(gameObject);
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