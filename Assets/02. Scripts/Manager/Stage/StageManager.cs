using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{
    public static event Action<StageConfig> OnStageStarted;
    public StageConfig Current { get; private set; }

    protected override void Awake()
    {
        base.Awake(); // 싱글톤 인스턴스 확정
        SceneManager.sceneLoaded += HandleLoaded;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        SceneManager.sceneLoaded -= HandleLoaded;
    }

    public void LoadStage(StageConfig config)
    {
        if (config == null || string.IsNullOrEmpty(config.sceneName))
        {
            Debug.LogError("[StageManager] Invalid StageConfig/sceneName");
            return;
        }
        Current = config;
        SceneManager.LoadScene(config.sceneName); // (원하면 Async로 변경 가능)
    }

    private void HandleLoaded(Scene _, LoadSceneMode __)
    {
        if (Current != null) OnStageStarted?.Invoke(Current);
    }
}
