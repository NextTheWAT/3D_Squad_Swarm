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
        UIManager.Instance.SetPlayGame();

        ZombieManager.Instance.GlobalSpeedBoost = 0f; // 스테이지 시작시 좀비 속도 초기화
        PlayerManager.Instance.PlayerSpeedReset(); // 스테이지 시작시 플레이어 속도 초기화
    }

    private void HandleLoaded(Scene _, LoadSceneMode __)
    {
        if (Current != null) OnStageStarted?.Invoke(Current);
    }
    public void OnEnemyKilled(NPC npc)
    {
        if (npc == null) return;

        var sh = npc.GetComponent<StatHandler>();

        // StatHandler는 SO를 이미 적용/보관하고 있고 GetStat으로 꺼낼 수 있음
        float amount = (sh != null) ? sh.GetStat(StatType.InfectionPoint) : 0f;

        if (amount != 0f)
            UIManager.Instance?.getInfection(amount); // UIManager가 감염도/킬카운트 증가
    }
}
