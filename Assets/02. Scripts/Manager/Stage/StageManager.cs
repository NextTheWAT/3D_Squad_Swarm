using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static event Action<StageConfig> OnStageStarted;

    [SerializeField] private StageConfig current; // 인스펙터에서 할당 or 코드로 교체

    public void StartStage(StageConfig config)
    {
        current = config;
        OnStageStarted?.Invoke(current);
    }

    public StageConfig Current => current;
}
