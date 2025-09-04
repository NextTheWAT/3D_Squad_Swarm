using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject playerPrefab;        // �÷��̾� ������
    public Transform spawnPoint;           // ���� ��ġ/ȸ��

    [Header("Camera Prefabs (instantiate only)")]
    public GameObject mainCameraPrefab;    // ���� ī�޶� ������
    public GameObject subCameraPrefab;     // ���� ī�޶� ������(������)

    [Header("Options")]
    public bool destroyExistingPlayer = true; // ���� Player ������ ����

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

        if (destroyExistingPlayer)
        {
            var existing = GameObject.FindGameObjectWithTag("Player");
            if (existing) Destroy(existing);
        }

        // 1) �÷��̾� ����
        playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        // 2) ī�޶� �����յ� �׳� ���� ����(�߰� ���� X)
        if (mainCameraPrefab != null)
            mainCamInstance = Instantiate(mainCameraPrefab);

        if (subCameraPrefab != null)
            subCamInstance = Instantiate(subCameraPrefab);
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
