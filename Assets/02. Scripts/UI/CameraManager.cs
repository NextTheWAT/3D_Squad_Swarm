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
        // �� ���� �̹� �ν��Ͻ��� �����ϸ� ���� ������Ʈ �ı�
        if (_instance != null)
        {
            // ���� �ν��Ͻ��� ���� �ν��Ͻ��� �ٸ��� ��� �α׿� �Բ� �ı�
            if (_instance != this)
            {
                Debug.LogWarning("CameraManager: ���� �̹� �ٸ� CameraManager �ν��Ͻ��� �����մϴ�. �ߺ� �ν��Ͻ��� �ı��մϴ�.");
                Destroy(gameObject);
                return;
            }
        }

        // �̱��� �ν��Ͻ� ����
        _instance = this;

        // ī�޶� �Ҵ��� �����ߴ��� Ȯ���ϴ� ����� �α� �߰� (������)
        if (mainVirtualCamera == null)
        {
            Debug.LogError("mainVirtualCamera�� �ν����Ϳ� �Ҵ���� �ʾҽ��ϴ�.");
        }
        if (stageSelectVirtualCamera == null)
        {
            Debug.LogError("stageSelectVirtualCamera�� �ν����Ϳ� �Ҵ���� �ʾҽ��ϴ�.");
        }
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