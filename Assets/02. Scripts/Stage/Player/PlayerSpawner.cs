using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject playerPrefab;        // 플레이어 프리팹
    public Transform spawnPoint;           // 스폰 위치/회전

    [Header("Camera Prefabs (instantiate only)")]
    public GameObject mainCameraPrefab;    // 메인 카메라 프리팹
    public GameObject subCameraPrefab;     // 서브 카메라 프리팹(있으면)

    [Header("Options")]
    public bool destroyExistingPlayer = true; // 기존 Player 있으면 제거

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

        // 1) 플레이어 스폰
        playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        // 2) 카메라 프리팹들 그냥 같이 생성(추가 세팅 X)
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
