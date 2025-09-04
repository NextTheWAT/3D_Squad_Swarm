using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject playerPrefab;        // �÷��̾� ������
    public Transform spawnPoint;           // ���� ��ġ/ȸ��

    [Header("Camera Prefabs (instantiate only)")]
    public GameObject mainCameraPrefab;    // ���� ī�޶� ������
    public GameObject subCameraPrefab;     // ���� ī�޶� ������(������)

    private GameObject playerInstance;
    private GameObject mainCamInstance;
    private GameObject subCamInstance;

    void Start()
    {
        if (playerPrefab == null || spawnPoint == null)
        {
            Debug.LogError("[PlayerSpawner] Assign playerPrefab & spawnPoint.");
            return;
        }

        // 1) �÷��̾� ����
        playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        // 2) ���� ī�޶�: ���� �̹� ������ �״�� ���, ������ ������ ����
        if (mainCameraPrefab != null && !MainCameraExistsInScene())
        {
            mainCamInstance = Instantiate(mainCameraPrefab);
        }

        // 3) ���� ī�޶�: ��û��� �׳� ������ ����(�߰� ���� X)
        if (subCameraPrefab != null)
        {
            subCamInstance = Instantiate(subCameraPrefab);
        }
    }

    // ���� ī�޶� ���� ���� �Ǵ�
    private bool MainCameraExistsInScene()
    {
        // Ȱ�� + MainCamera �±�
        if (Camera.main != null) return true;

        // ��Ȱ������ �����ؼ� MainCamera �±� ���� ī�޶� �ִ��� Ȯ��
        var cams = FindObjectsOfType<Camera>(true);
        foreach (var cam in cams)
        {
            if (cam != null && cam.CompareTag("MainCamera"))
                return true;
        }
        return false;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (spawnPoint == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spawnPoint.position, 0.3f);
        Gizmos.DrawLine(spawnPoint.position, spawnPoint.position + spawnPoint.forward * 1.0f);
    }
#endif
}
