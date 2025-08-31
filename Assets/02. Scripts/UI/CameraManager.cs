using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance;

    public static CameraManager Instance
    {
        get
        {
            // �ν��Ͻ��� �������� ������ ������ ã�ų� ���� ����
            if (_instance == null)
            {
                _instance = FindObjectOfType<CameraManager>();

                // ���� ������ ���� ���� ������Ʈ�� ����� ������Ʈ �߰�
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
        // �ν��Ͻ��� �̹� �����ϰ�, �� �ڽ��� �ƴ϶�� �ı�
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // �ν��Ͻ� �ʱ�ȭ �� �� ��ȯ �� �ı� ����
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ���� ī�޶�� ��ȯ
    public void SetMainCamera()
    {
        mainVirtualCamera.Priority = 10;
        stageSelectVirtualCamera.Priority = 0;
    }

    // �������� ���� ī�޶�� ��ȯ
    public void SetStageSelectVirtualCamera()
    {
        stageSelectVirtualCamera.Priority = 10; // �� ���� �켱������ ��ȯ
        mainVirtualCamera.Priority = 0;
    }
}