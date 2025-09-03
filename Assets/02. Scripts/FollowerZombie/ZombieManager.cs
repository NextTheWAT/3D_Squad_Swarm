using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance { get; private set; }

    private List<FollowerZombie> zombies = new List<FollowerZombie>();

    [Header("Camera")]
    public Cinemachine.CinemachineVirtualCamera virtualCamera;
    public float baseZoom = 5f;         
    public float zoomPerZombie = 0.5f;  
    public float maxZoom = 15f;         

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void RegisterZombie(FollowerZombie zombie)
    {
        if (!zombies.Contains(zombie))
        {
            zombies.Add(zombie);
            UpdateCameraZoom();
        }
    }

    public void UnregisterZombie(FollowerZombie zombie)
    {
        if (zombies.Contains(zombie))
        {
            zombies.Remove(zombie);
            UpdateCameraZoom();
        }
    }

    private void UpdateCameraZoom()
    {
        if (virtualCamera != null)
        {
            float targetZoom = baseZoom + zombies.Count * zoomPerZombie;
            targetZoom = Mathf.Min(targetZoom, maxZoom);
            virtualCamera.m_Lens.OrthographicSize = targetZoom;
        }
    }
}
