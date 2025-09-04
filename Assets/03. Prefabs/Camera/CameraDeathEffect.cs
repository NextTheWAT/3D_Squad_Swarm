using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraDeathEffect : MonoBehaviour
{
    public static CameraDeathEffect Instance { get; private set; }

    public CinemachineVirtualCamera virtualCamera;
    public float zoomInDistance = 3f;   // How close camera gets
    public float spinSpeed = 50f;       // Degrees per second
    public float zoomSpeed = 2f;        // How fast it zooms

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

        if (virtualCamera != null)
        {
            transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
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

        float startDistance = transposer.m_CameraDistance;

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
