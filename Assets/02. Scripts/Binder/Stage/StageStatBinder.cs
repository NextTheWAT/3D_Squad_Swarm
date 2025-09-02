using UnityEngine;

[DisallowMultipleComponent]
public class StageStatBinder : MonoBehaviour
{
    [Tooltip("비워두면 GameObject.tag 를 unitId로 사용합니다.")]
    [SerializeField] private string unitIdOverride;

    private StatHandler handler;

    private void Awake()
    {
        handler = GetComponent<StatHandler>();
    }

    private void OnEnable()
    {
        StageManager.OnStageStarted += HandleStageStarted;   // StageManager가 이벤트를 쏘는 구조인 경우
    }

    private void OnDisable()
    {
        StageManager.OnStageStarted -= HandleStageStarted;
    }

    private void HandleStageStarted(StageConfig cfg)
    {
        if (handler == null || cfg == null) return;

        string unitId = string.IsNullOrEmpty(unitIdOverride) ? gameObject.tag : unitIdOverride;
        var so = cfg.GetStatsFor(unitId);    // StageConfig: unitId → ScriptableStats 매핑
        if (so != null)
        {
            handler.ApplyFrom(so, clearBefore: true);
        }
    }
}
