using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

// �̱��������� Home�������� ���
// ���ʸ� �̱��� Ŭ���� ������� ����
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
                Debug.LogWarning("CameraManager: ���� �̹� �ٸ� CameraManager �ν��Ͻ��� ����. �ߺ� �ν��Ͻ��� �ı�.");
                Destroy(gameObject);
                return;
            }
        }

        // �̱��� �ν��Ͻ� ����
        _instance = this;
    }

    // ���� ī�޶��� �켱������ ���� Ȱ��ȭ
    public void SetMainCamera()
    {
        mainVirtualCamera.Priority = 10;

        stageSelectVirtualCamera.Priority = 0;
    }

    // �������� ���� ī�޶��� �켱������ ���� Ȱ��ȭ
    public void SetStageSelectVirtualCamera()
    {
        stageSelectVirtualCamera.Priority = 10;
            
        mainVirtualCamera.Priority = 0;
    }
}