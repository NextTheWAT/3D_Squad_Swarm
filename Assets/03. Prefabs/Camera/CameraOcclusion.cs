using UnityEngine;
using System.Collections.Generic;

public class CameraOcclusion : MonoBehaviour
{
    [Range(0f, 1f)]
    public float transparentAlpha = 0.3f; // How transparent the building gets

    private class RendererData
    {
        public MeshRenderer renderer;
        public Material[] originalMaterials;
    }

    private Dictionary<Transform, List<RendererData>> hiddenRenderers = new Dictionary<Transform, List<RendererData>>();
    private Dictionary<Transform, int> parentOverlapCount = new Dictionary<Transform, int>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<MeshRenderer>(out _)) return;

        Transform parent = FindOcclusionRoot(other.transform);

        if (parent == null) return;

        // Increment overlap count
        if (!parentOverlapCount.ContainsKey(parent))
            parentOverlapCount[parent] = 0;
        parentOverlapCount[parent]++;

        // Only make transparent if it's the first overlap
        if (!hiddenRenderers.ContainsKey(parent))
        {
            var renderers = new List<MeshRenderer>(parent.GetComponentsInChildren<MeshRenderer>());
            var rendererDataList = new List<RendererData>();

            foreach (var r in renderers)
            {
                if (r.gameObject.name.Contains("Sidewalk"))
                    continue;

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
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<MeshRenderer>(out _)) return;

        Transform parent = FindOcclusionRoot(other.transform);

        if (parent == null) return;

        if (!parentOverlapCount.ContainsKey(parent)) return;

        // Decrement overlap count
        parentOverlapCount[parent]--;

        // Only restore materials when all child colliders have exited
        if (parentOverlapCount[parent] <= 0)
        {
            if (hiddenRenderers.ContainsKey(parent))
            {
                foreach (var data in hiddenRenderers[parent])
                    data.renderer.materials = data.originalMaterials;

                hiddenRenderers.Remove(parent);
            }

            parentOverlapCount.Remove(parent);
        }
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
