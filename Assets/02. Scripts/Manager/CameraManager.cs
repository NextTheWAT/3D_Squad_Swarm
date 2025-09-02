using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance;

    // 인스턴스를 가져오는 프로퍼티
    public static CameraManager Instance
    {
        get
        {
            // 인스턴스가 없으면 씬에서 찾기
            if (_instance == null)
            {
                _instance = FindObjectOfType<CameraManager>();
            }
            return _instance;
        }
    }

    [SerializeField] private CinemachineVirtualCamera mainVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera stageSelectVirtualCamera;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        //mainVirtualCamera = transform.Find("Virtual Camera")?.GetComponent<CinemachineVirtualCamera>();
        //stageSelectVirtualCamera = transform.Find("Virtual Camera_StageSelect")?.GetComponent<CinemachineVirtualCamera>();
    }

    // 메인 카메라의 우선순위를 높여 활성화
    public void SetMainCamera()
    {
        Debug.Log("SetMainCamera called");
        mainVirtualCamera.Priority = 10;

        stageSelectVirtualCamera.Priority = 0;
    }

    // 스테이지 선택 카메라의 우선순위를 높여 활성화
    public void SetStageSelectVirtualCamera()
    {
        Debug.Log("SetStageSelectVirtualCamera called");
        stageSelectVirtualCamera.Priority = 10;
            
        mainVirtualCamera.Priority = 0;
    }
}