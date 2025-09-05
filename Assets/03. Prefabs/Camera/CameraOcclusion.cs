using UnityEngine;
using System.Collections.Generic;

public class CameraOcclusion : MonoBehaviour
{
    [Range(0f, 1f)]
    public float transparentAlpha = 0.3f; // How transparent the building gets
    public float detectionRadius = 5f;   // Radius of the fixed detection sphere
    public LayerMask buildingLayer;      // Layer for buildings

    private class RendererData
    {
        public MeshRenderer renderer;
        public Material[] originalMaterials;
    }

    private Dictionary<Transform, List<RendererData>> hiddenRenderers = new Dictionary<Transform, List<RendererData>>();

    private HashSet<Transform> currentlyDetected = new HashSet<Transform>();

    private void Update()
    {
        // Detect buildings in range using a fixed sphere
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, buildingLayer);
        HashSet<Transform> newDetected = new HashSet<Transform>();

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent<MeshRenderer>(out _)) continue;

            Transform parent = FindOcclusionRoot(hit.transform);
            if (parent == null) continue;

            newDetected.Add(parent);

            if (!currentlyDetected.Contains(parent))
                MakeTransparent(parent);
        }

        // Restore materials for objects no longer detected
        foreach (var parent in currentlyDetected)
        {
            if (!newDetected.Contains(parent))
                RestoreMaterials(parent);
        }

        currentlyDetected = newDetected;
    }

    private void MakeTransparent(Transform parent)
    {
        if (hiddenRenderers.ContainsKey(parent)) return;

        var renderers = new List<MeshRenderer>(parent.GetComponentsInChildren<MeshRenderer>());
        var rendererDataList = new List<RendererData>();

        foreach (var r in renderers)
        {
            if (r.gameObject.name.Contains("Sidewalk")) continue;

            RendererData data = new RendererData
            {
                renderer = r,
                originalMaterials = r.materials
            };
            rendererDataList.Add(data);

            Material[] newMats = new Material[r.materials.Length];
            for (int i = 0; i < r.materials.Length; i++)
            {
                Material m = new Material(r.materials[i]);
                MakeMaterialTransparent(m);
                newMats[i] = m;
            }
            r.materials = newMats;
        }

        if (rendererDataList.Count > 0)
            hiddenRenderers[parent] = rendererDataList;
    }

    private void RestoreMaterials(Transform parent)
    {
        if (!hiddenRenderers.ContainsKey(parent)) return;

        foreach (var data in hiddenRenderers[parent])
            data.renderer.materials = data.originalMaterials;

        hiddenRenderers.Remove(parent);
    }

    private void MakeMaterialTransparent(Material mat)
    {
        mat.SetFloat("_Mode", 3);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

        Color c = mat.color;
        c.a = transparentAlpha;
        mat.color = c;
    }

    private Transform FindOcclusionRoot(Transform child)
    {
        Transform current = child;

        while (current.parent != null)
        {
            if (current.parent.GetComponent<MeshRenderer>() == null &&
                current.parent.GetComponentsInChildren<MeshRenderer>().Length > 1)
            {
                return current.parent;
            }

            current = current.parent;
        }

        return child;
    }
}
