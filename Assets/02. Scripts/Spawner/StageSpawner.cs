using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawner : MonoBehaviour
{
    [Tooltip("인펙션 퍼센트 제공자(선택). AfterInfectionPercent 조건에서 사용")]
    [SerializeField] private MonoBehaviour progressSource; // IStageProgressProvider 구현체
    private IStageProgressProvider Progress => progressSource as IStageProgressProvider;

    private StageConfig cfg;
    private readonly List<int> aliveCounts = new();
    private readonly List<Coroutine> loops = new();

    private void OnEnable()
    {
        StageManager.OnStageStarted += HandleStageStarted;
        // 씬이 이미 로드된 상태라면 1회 반영
        if (StageManager.Instance != null && StageManager.Instance.Current != null)
            HandleStageStarted(StageManager.Instance.Current);
    }

    private void OnDisable()
    {
        StageManager.OnStageStarted -= HandleStageStarted;
        StopAllCoroutines();
        loops.Clear();
        aliveCounts.Clear();
    }

    private void HandleStageStarted(StageConfig config)
    {
        if (config == null) return;
        cfg = config;

        StopAllCoroutines();
        loops.Clear();
        aliveCounts.Clear();

        // 룰별 현재 생존 수 초기화
        for (int i = 0; i < cfg.spawns.Count; i++) aliveCounts.Add(0);

        // 룰별 루프 시작
        for (int i = 0; i < cfg.spawns.Count; i++)
            loops.Add(StartCoroutine(RunRule(i)));
    }

    private IEnumerator RunRule(int i)
    {
        var rule = cfg.spawns[i];

        // 시작 조건 대기
        switch (rule.startCondition)
        {
            case StageConfig.StartCondition.AfterSeconds:
                if (rule.startDelay > 0f) yield return new WaitForSeconds(rule.startDelay);
                break;
            case StageConfig.StartCondition.AfterInfectionPercent:
                yield return new WaitUntil(() =>
                    Progress != null && (Progress.InfectionPercent01 * 100f) >= rule.unlockInfectionPercent);
                break;
        }

        // 초기 스폰
        for (int k = 0; k < rule.initialCount; k++)
        {
            SpawnOne(i);
            yield return null;
        }

        // 유지 루프 (동시 수량 < max면 채움)
        while (true)
        {
            while (aliveCounts[i] < rule.maxCount)
            {
                SpawnOne(i);
                if (rule.respawnInterval > 0f) yield return new WaitForSeconds(rule.respawnInterval);
                else yield return null;
            }
            yield return null;
        }
    }

    private void SpawnOne(int i)
    {
        var rule = cfg.spawns[i];
        if (rule.prefab == null) return;

        Vector3 pos = GetSpawnPos(rule);
        var go = Instantiate(rule.prefab, pos, Quaternion.identity);

        //     스탯 주입 없음: 프리팹 내부 컴포넌트(예: Enemy/StatHandler)가
        //     스스로 붙어있는 ScriptableStats를 사용하도록 둡니다.

        // 라이프사이클 추적 (파괴/비활성 시 aliveCounts 감소)
        aliveCounts[i]++;
        var notifier = go.AddComponent<_DespawnNotifier>();
        notifier.onDespawn = () =>
        {
            aliveCounts[i] = Mathf.Max(0, aliveCounts[i] - 1);
        };
    }

    private Vector3 GetSpawnPos(StageConfig.SpawnRule rule)
    {
        if (rule.spawnPoints != null && rule.spawnPoints.Length > 0)
        {
            var p = rule.spawnPoints[Random.Range(0, rule.spawnPoints.Length)];
            return new Vector3(p.position.x, rule.fixedY, p.position.z);
        }
        // 랜덤 사각형(XZ), 중심(0,0)
        float hx = rule.randomAreaSize.x * 0.5f;
        float hz = rule.randomAreaSize.y * 0.5f;
        float x = Random.Range(-hx, hx);
        float z = Random.Range(-hz, hz);
        return new Vector3(x, rule.fixedY, z);
    }

    // 파괴/비활성 알림 전용
    private class _DespawnNotifier : MonoBehaviour
    {
        public System.Action onDespawn;
        private bool done;
        private void OnDisable() { TryNotify(); }
        private void OnDestroy() { TryNotify(); }
        private void TryNotify()
        {
            if (done) return;
            done = true;
            onDespawn?.Invoke();
        }
    }
}
