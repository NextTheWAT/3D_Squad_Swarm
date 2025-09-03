using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAimer : MonoBehaviour
{
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public float maxDistance = 20f;
    void Awake()
    {
        if (lineRenderer != null)
            lineRenderer.enabled = false;
    }
    private void Update()
    {
        if (lineRenderer.enabled)
        {
            UpdateLine();
        }
    }

    public void EnableAim(bool enable)
    {
        lineRenderer.enabled = enable;
    }

    private void UpdateLine()
    {
        RaycastHit hit;
        Vector3 endPos;
        

        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, maxDistance))
        {
            endPos = hit.point;
        }
        else
        {
            endPos = firePoint.position + firePoint.forward * maxDistance;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, endPos);
    }
}