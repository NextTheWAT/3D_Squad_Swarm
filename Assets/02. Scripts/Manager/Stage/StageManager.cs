using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("Stage Presets")]
    [SerializeField] private StageConfig stage1;
    [SerializeField] private StageConfig stage2;
    [SerializeField] private StageConfig stage3;

    public StageConfig Current { get; private set; }

    public void StartStage1() => StartStage(stage1);
    public void StartStage2() => StartStage(stage2);
    public void StartStage3() => StartStage(stage3);

    public static event Action<StageConfig> OnStageStarted; // 추가

    public void StartStage(StageConfig config)
    {
        if (!config) { Debug.LogError("..."); return; }
        Current = config;
        Debug.Log($"[StageManager] Start Stage: {config.stageName} (id={config.stageId})");

        // ApplyToScene(); // 이 줄은 지우거나 주석 처리
        OnStageStarted?.Invoke(Current); // 이벤트 발행
    }

    private void ApplyToScene()
    {
        // 씬의 모든 StatHandler를 찾아 Tag(=unitId) 기준으로 맞는 ScriptableStats 적용
        var handlers = FindObjectsOfType<StatHandler>(includeInactive: true);
        foreach (var h in handlers)
        {
            string unitId = h.gameObject.tag;                 // ★ 추가 스크립트 없이 Tag로 식별
            var stats = Current.GetStatsFor(unitId);
            if (stats != null)
            {
                h.ApplyFrom(stats, clearBefore: true);
            }
            else
            {
                // 매핑 누락 확인용 로그(선택)
                // Debug.LogWarning($"No Stage stats for unitId '{unitId}' on {h.name}");
            }
        }
    }
}
