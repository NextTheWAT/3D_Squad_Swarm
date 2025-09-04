using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject playerPrefab;   // 플레이어 프리팹
    public Transform spawnPoint;      // 맵에 배치한 스폰 포인트(Transform)

    [Header("Options")]
    public bool destroyExistingPlayer = true; // 기존 Player 있으면 제거

    private GameObject current;

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

        current = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

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
