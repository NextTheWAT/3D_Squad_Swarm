using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class StageSceneManager : MonoBehaviour
{
    [System.Serializable]
    public struct StageGroup
    {
        public string key;            // ��: "Stage"
        public StageConfig[] stages;  // �� �׷쿡 ���� StageConfig��
    }

    [SerializeField] private StageGroup[] groups;
    [SerializeField] private string defaultKey = "Stage";

    private Dictionary<string, StageConfig[]> map;

    private void Awake()
    {
        map = new Dictionary<string, StageConfig[]>(groups?.Length ?? 0);
        if (groups == null) return;
        foreach (var g in groups)
        {
            if (!string.IsNullOrEmpty(g.key) && g.stages != null)
                map[g.key] = g.stages;
        }
    }

    // ��ư���� �ε����� �ѱ�� �뵵
    public void LoadByIndex(int index) => Load(defaultKey, index);

    // ��ư���� "Ű:�ε���" ���ڿ��� �ѱ�� �뵵 (��: "Stage:2")
    public void LoadByKeyAndIndexString(string keyAndIndex)
    {
        if (string.IsNullOrEmpty(keyAndIndex)) return;
        var parts = keyAndIndex.Split(':');
        if (parts.Length != 2 || !int.TryParse(parts[1], out var idx)) return;
        Load(parts[0], idx);
    }

    public void Load(string key, int index)
    {
        if (map == null || string.IsNullOrEmpty(key)) return;
        if (!map.TryGetValue(key, out var arr) || arr == null) return;
        if (index < 0 || index >= arr.Length) return;

        var cfg = arr[index];
        if (cfg == null) return;

        StageManager.Instance.LoadStage(cfg);
    }
}
