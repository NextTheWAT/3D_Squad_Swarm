using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawner : MonoBehaviour
{
    private StageConfig cfg;
    private readonly List<int> aliveCounts = new();
    private readonly List<Coroutine> loops = new();

    // 셧다운 감지
    private bool _shuttingDown = false;

    private void OnEnable()
    {
        _shuttingDown = false;
        StageManager.OnStageStarted += HandleStageStarted;

        if (StageManager.Instance != null && StageManager.Instance.Current != null)
            HandleStageStarted(StageManager.Instance.Current);
    }

    private void OnDisable()
    {
        // 셧다운 시작 표시 (이후 들어오는 onDespawn은 무시)
        _shuttingDown = true;

        StageManager.OnStageStarted -= HandleStageStarted;
        StopAllCoroutines();

        // 여기서 aliveCounts.Clear() / loops.Clear() 하지 마세요.
        // 다음 스테이지 시작 시 HandleStageStarted에서 초기화됩니다.
    }

    private void HandleStageStarted(StageConfig config)
    {
        if (config == null) return;
        cfg = config;

        StopAllCoroutines();
        loops.Clear();
        aliveCounts.Clear();

        for (int i = 0; i < cfg.spawns.Count; i++)
            aliveCounts.Add(0);

        for (int i = 0; i < cfg.spawns.Count; i++)
            loops.Add(StartCoroutine(RunRule(i)));
    }

    private IEnumerator RunRule(int i)
    {
        var rule = cfg.spawns[i];

        if (rule.startCondition == StageConfig.StartCondition.AfterSeconds && rule.startDelay > 0f)
            yield return new WaitForSeconds(rule.startDelay);

        for (int k = 0; k < rule.initialCount; k++)
        {
            SpawnOne(i);
            yield return null;
        }

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

        // 필요 시: 감염 보상/태그 세팅 등 여기에…

        // 증가
        aliveCounts[i]++;

        // 안전한 감소 람다 (인덱스 캡처 + 가드)
        int idx = i;
        var notifier = go.AddComponent<_DespawnNotifier>();
        notifier.onDespawn = () =>
        {
            if (_shuttingDown) return;                     // 정리 중이면 무시
            if (idx < 0 || idx >= aliveCounts.Count) return; // 범위 체크
            aliveCounts[idx] = Mathf.Max(0, aliveCounts[idx] - 1);
        };
    }

    private Vector3 GetSpawnPos(StageConfig.SpawnRule rule)
    {
        // 포인트 없으면 랜덤 영역 사용 (필요 시 포인트 로직 추가)
        float hx = rule.randomAreaSize.x * 0.5f;
        float hz = rule.randomAreaSize.y * 0.5f;
        float x = Random.Range(-hx, hx);
        float z = Random.Range(-hz, hz);
        return new Vector3(x, rule.fixedY, z);
    }

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