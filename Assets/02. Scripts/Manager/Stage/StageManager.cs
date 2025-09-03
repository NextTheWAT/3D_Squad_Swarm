using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{
    public static event Action<StageConfig> OnStageStarted;
    public StageConfig Current { get; private set; }

    // �ν����Ϳ� �巡���� ���
    [Header("Registered Stages")]
    [SerializeField] private StageConfig stage1;
    [SerializeField] private StageConfig stage2;
    [SerializeField] private StageConfig stage3;

    protected override void Awake()
    {
        base.Awake(); // �̱���
        SceneManager.sceneLoaded += HandleLoaded;
        DontDestroyOnLoad(gameObject); // ��Ʈ��Ʈ�� ������Ʈ��� ���� ����
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        SceneManager.sceneLoaded -= HandleLoaded;
    }

    // ====== �ܺο��� ȣ���� ���� API ======
    public void Stage1() => LoadStage(stage1);
    public void Stage2() => LoadStage(stage2);
    public void Stage3() => LoadStage(stage3);

    // (����) ���� �δ�
    public void LoadStage(StageConfig config)
    {
        if (config == null || string.IsNullOrEmpty(config.sceneName))
        {
            Debug.LogError("[StageManager] Invalid StageConfig/sceneName");
            return;
        }
        Current = config;
        SceneManager.LoadScene(config.sceneName); // �ʿ��ϸ� Async�� ����
    }

    private void HandleLoaded(Scene _, LoadSceneMode __)
    {
        if (Current != null) OnStageStarted?.Invoke(Current);
    }
}
