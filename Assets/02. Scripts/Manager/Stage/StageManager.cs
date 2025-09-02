using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static event Action<StageConfig> OnStageStarted;

    [SerializeField] private StageConfig current; // �ν����Ϳ��� �Ҵ� or �ڵ�� ��ü

    public void StartStage(StageConfig config)
    {
        current = config;
        OnStageStarted?.Invoke(current);
    }

    public StageConfig Current => current;
}
