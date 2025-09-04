using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject playerPrefab;   // �÷��̾� ������
    public Transform spawnPoint;      // �ʿ� ��ġ�� ���� ����Ʈ(Transform)

    [Header("Options")]
    public bool destroyExistingPlayer = true; // ���� Player ������ ����

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
