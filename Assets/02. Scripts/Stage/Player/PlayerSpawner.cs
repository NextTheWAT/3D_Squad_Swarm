using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject playerPrefab;        // 플레이어 프리팹
    public Transform spawnPoint;           // 스폰 위치/회전

    [Header("Camera Prefabs (instantiate only)")]
    public GameObject mainCameraPrefab;    // 메인 카메라 프리팹
    public GameObject subCameraPrefab;     // 서브 카메라 프리팹(있으면)

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

        // 1) 플레이어 스폰
        playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        // 2) 메인 카메라: 씬에 이미 있으면 그대로 사용, 없으면 프리팹 생성
        if (mainCameraPrefab != null && !MainCameraExistsInScene())
        {
            mainCamInstance = Instantiate(mainCameraPrefab);
        }

        // 3) 서브 카메라: 요청대로 그냥 프리팹 생성(추가 세팅 X)
        if (subCameraPrefab != null)
        {
            subCamInstance = Instantiate(subCameraPrefab);
        }
    }

    // 메인 카메라 존재 여부 판단
    private bool MainCameraExistsInScene()
    {
        // 활성 + MainCamera 태그
        if (Camera.main != null) return true;

        // 비활성까지 포함해서 MainCamera 태그 가진 카메라가 있는지 확인
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
