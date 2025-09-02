using UnityEngine;

[RequireComponent(typeof(StatHandler))]
public class StatBinder : MonoBehaviour
{
    [SerializeField] private string unitIdOverride; // 비워두면 Tag 사용
    private StatHandler handler;

    private void Awake() => handler = GetComponent<StatHandler>();

    private void OnEnable()
    {
        StageManager.OnStageStarted += Handle;
        // 이미 스테이지가 시작돼 있다면 즉시 1회 적용
        if (StageManager.Instance != null && StageManager.Instance.Current != null)
            Handle(StageManager.Instance.Current);
    }

    private void OnDisable() => StageManager.OnStageStarted -= Handle;

    private void Handle(StageConfig cfg)
    {
        if (handler == null || cfg == null) return;
        string unitId = string.IsNullOrEmpty(unitIdOverride) ? gameObject.tag : unitIdOverride;
        var so = cfg.GetStatsFor(unitId);
        if (so != null) handler.ApplyFrom(so, clearBefore: true);
    }
}
