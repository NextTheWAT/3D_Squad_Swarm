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

    public static event Action<StageConfig> OnStageStarted; // �߰�

    public void StartStage(StageConfig config)
    {
        if (!config) { Debug.LogError("..."); return; }
        Current = config;
        Debug.Log($"[StageManager] Start Stage: {config.stageName} (id={config.stageId})");

        // ApplyToScene(); // �� ���� ����ų� �ּ� ó��
        OnStageStarted?.Invoke(Current); // �̺�Ʈ ����
    }

    private void ApplyToScene()
    {
        // ���� ��� StatHandler�� ã�� Tag(=unitId) �������� �´� ScriptableStats ����
        var handlers = FindObjectsOfType<StatHandler>(includeInactive: true);
        foreach (var h in handlers)
        {
            string unitId = h.gameObject.tag;                 // �� �߰� ��ũ��Ʈ ���� Tag�� �ĺ�
            var stats = Current.GetStatsFor(unitId);
            if (stats != null)
            {
                h.ApplyFrom(stats, clearBefore: true);
            }
            else
            {
                // ���� ���� Ȯ�ο� �α�(����)
                // Debug.LogWarning($"No Stage stats for unitId '{unitId}' on {h.name}");
            }
        }
    }
}
