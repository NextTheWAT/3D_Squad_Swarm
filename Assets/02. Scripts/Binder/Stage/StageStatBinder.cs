using UnityEngine;

[DisallowMultipleComponent]
public class StageStatBinder : MonoBehaviour
{
    [Tooltip("����θ� GameObject.tag �� unitId�� ����մϴ�.")]
    [SerializeField] private string unitIdOverride;

    private StatHandler handler;

    private void Awake()
    {
        handler = GetComponent<StatHandler>();
    }

    private void OnEnable()
    {
        StageManager.OnStageStarted += HandleStageStarted;   // StageManager�� �̺�Ʈ�� ��� ������ ���
    }

    private void OnDisable()
    {
        StageManager.OnStageStarted -= HandleStageStarted;
    }

    private void HandleStageStarted(StageConfig cfg)
    {
        if (handler == null || cfg == null) return;

        string unitId = string.IsNullOrEmpty(unitIdOverride) ? gameObject.tag : unitIdOverride;
        var so = cfg.GetStatsFor(unitId);    // StageConfig: unitId �� ScriptableStats ����
        if (so != null)
        {
            handler.ApplyFrom(so, clearBefore: true);
        }
    }
}
