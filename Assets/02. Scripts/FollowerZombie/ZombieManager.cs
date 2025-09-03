using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance { get; private set; }

    private List<FollowerZombie> zombies = new List<FollowerZombie>();

    [Header("Camera")]
    public CinemachineVirtualCamera virtualCamera;
    public float baseDistance = 10f;
    public float distanceStep = 10f;
    public float maxDistance = 50f;
    public float lerpSpeed = 2f;

    private float targetDistance;
    private CinemachineFramingTransposer transposer;

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
            if (transposer != null)
            {
                transposer.m_CameraDistance = baseDistance;
                targetDistance = baseDistance;
            }
        }
    }

    private void Update()
    {
        if (transposer != null)
        {
            transposer.m_CameraDistance = Mathf.Lerp(
                transposer.m_CameraDistance,
                targetDistance,
                Time.deltaTime * lerpSpeed
            );
        }
    }

    public void RegisterZombie(FollowerZombie zombie)
    {
        if (!zombies.Contains(zombie))
        {
            zombies.Add(zombie);
            UpdateTargetDistance();
        }
    }

    public void UnregisterZombie(FollowerZombie zombie)
    {
        if (zombies.Contains(zombie))
        {
            zombies.Remove(zombie);
            UpdateTargetDistance();
        }
    }

    private void UpdateTargetDistance()
    {
        int zoomSteps = zombies.Count / 20;
        targetDistance = Mathf.Min(baseDistance + zoomSteps * distanceStep, maxDistance);
    }
}
