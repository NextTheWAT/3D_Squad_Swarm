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
                // ���� Manager�� ������ ������ �߻����� ������ �˸�
                Debug.LogError("CameraManager is not found in the scene.");
            }
            return _instance;
        }
    }

    public CinemachineVirtualCamera mainVirtualCamera;
    public CinemachineVirtualCamera stageSelectVirtualCamera;

    private void Awake()
    {
        // �̱��� ���� �ʱ�ȭ (�ߺ� ���� ����)
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