using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance;

    // �ν��Ͻ��� �������� ������Ƽ
    public static CameraManager Instance
    {
        get
        {
            // �ν��Ͻ��� ������ ������ ã��
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

    // ���� ī�޶��� �켱������ ���� Ȱ��ȭ
    public void SetMainCamera()
    {
        Debug.Log("SetMainCamera called");
        mainVirtualCamera.Priority = 10;

        stageSelectVirtualCamera.Priority = 0;
    }

    // �������� ���� ī�޶��� �켱������ ���� Ȱ��ȭ
    public void SetStageSelectVirtualCamera()
    {
        Debug.Log("SetStageSelectVirtualCamera called");
        stageSelectVirtualCamera.Priority = 10;
            
        mainVirtualCamera.Priority = 0;
    }
}