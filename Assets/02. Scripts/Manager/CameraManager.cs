using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

// 싱글톤이지만 Home씬에서만 사용
// 제너릭 싱글톤 클래스 사용하지 않음
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
        // 씬 내에 이미 인스턴스가 존재하면 현재 오브젝트 파괴
        if (_instance != null)
        {
            // 현재 인스턴스와 기존 인스턴스가 다르면 경고 로그와 함께 파괴
            if (_instance != this)
            {
                Debug.LogWarning("CameraManager: 씬에 이미 다른 CameraManager 인스턴스가 존재. 중복 인스턴스를 파괴.");
                Destroy(gameObject);
                return;
            }
        }

        // 싱글톤 인스턴스 설정
        _instance = this;
    }

    // 메인 카메라의 우선순위를 높여 활성화
    public void SetMainCamera()
    {
        mainVirtualCamera.Priority = 10;

        stageSelectVirtualCamera.Priority = 0;
    }

    // 스테이지 선택 카메라의 우선순위를 높여 활성화
    public void SetStageSelectVirtualCamera()
    {
        stageSelectVirtualCamera.Priority = 10;
            
        mainVirtualCamera.Priority = 0;
    }
}