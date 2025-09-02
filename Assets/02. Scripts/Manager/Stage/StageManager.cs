using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{
    public static event Action<StageConfig> OnStageStarted;
    public StageConfig Current { get; private set; }

    protected override void Awake()
    {
        base.Awake(); // �̱��� �ν��Ͻ� Ȯ��
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
        SceneManager.LoadScene(config.sceneName); // (���ϸ� Async�� ���� ����)
    }

    private void HandleLoaded(Scene _, LoadSceneMode __)
    {
        if (Current != null) OnStageStarted?.Invoke(Current);
    }
}
