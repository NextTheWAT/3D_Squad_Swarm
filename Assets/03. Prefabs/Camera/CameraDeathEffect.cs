using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraDeathEffect : MonoBehaviour
{
    public static CameraDeathEffect Instance { get; private set; }

    public CinemachineVirtualCamera virtualCamera;
    public float zoomInDistance = 3f;
    public float spinSpeed = 50f;
    public float zoomSpeed = 2f;

    private CinemachineFramingTransposer transposer;
    private Transform target;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (virtualCamera != null)
        {
            transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

            // 🔹 Find player by tag and set as camera follow/lookAt
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                virtualCamera.Follow = playerObj.transform;
                virtualCamera.LookAt = playerObj.transform;
                target = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("Player not found! Make sure your player has the 'Player' tag.");
            }
        }
    }

    public void PlayDeathCamera(Transform player)
    {
        target = player;
        StopAllCoroutines();
        StartCoroutine(DeathCameraRoutine());
    }

    private IEnumerator DeathCameraRoutine()
    {
        if (transposer == null || target == null) yield break;

        while (transposer.m_CameraDistance > zoomInDistance)
        {
            // Zoom in
            transposer.m_CameraDistance = Mathf.Lerp(
                transposer.m_CameraDistance,
                zoomInDistance,
                Time.deltaTime * zoomSpeed
            );

            // Spin around player
            virtualCamera.transform.RotateAround(target.position, Vector3.up, spinSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
