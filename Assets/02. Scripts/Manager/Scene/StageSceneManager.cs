using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class StageSceneManager : MonoBehaviour
{
    [System.Serializable]
    public struct StageGroup
    {
        public string key;            // 예: "Stage"
        public StageConfig[] stages;  // 이 그룹에 속한 StageConfig들
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

    // 버튼에서 인덱스만 넘기는 용도
    public void LoadByIndex(int index) => Load(defaultKey, index);

    // 버튼에서 "키:인덱스" 문자열로 넘기는 용도 (예: "Stage:2")
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
