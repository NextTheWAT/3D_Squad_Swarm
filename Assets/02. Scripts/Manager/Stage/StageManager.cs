using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{
    public static event Action<StageConfig> OnStageStarted;
    public StageConfig Current { get; private set; }

    // 인스펙터에 드래그해 등록
    [Header("Registered Stages")]
    [SerializeField] private StageConfig stage1;
    [SerializeField] private StageConfig stage2;
    [SerializeField] private StageConfig stage3;

    protected override void Awake()
    {
        base.Awake(); // 싱글톤
        SceneManager.sceneLoaded += HandleLoaded;
        DontDestroyOnLoad(gameObject); // 부트스트랩 오브젝트라면 유지 권장
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        SceneManager.sceneLoaded -= HandleLoaded;
    }

    // ====== 외부에서 호출할 공개 API ======
    public void Stage1() => LoadStage(stage1);
    public void Stage2() => LoadStage(stage2);
    public void Stage3() => LoadStage(stage3);

    // (공통) 실제 로더
    public void LoadStage(StageConfig config)
    {
        if (config == null || string.IsNullOrEmpty(config.sceneName))
        {
            Debug.LogError("[StageManager] Invalid StageConfig/sceneName");
            return;
        }
        Current = config;
        SceneManager.LoadScene(config.sceneName); // 필요하면 Async로 변경
    }

    private void HandleLoaded(Scene _, LoadSceneMode __)
    {
        if (Current != null) OnStageStarted?.Invoke(Current);
    }
}
