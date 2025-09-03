using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // ✅

public class StageSpawner : MonoBehaviour
{
    private StageConfig cfg;
    private readonly List<int> aliveCounts = new();
    private readonly List<Coroutine> loops = new();

    // NavMeshDataInstance 핸들 (스테이지별 등록/해제)
    private NavMeshDataInstance _navInstance;
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
        _shuttingDown = true;
        StageManager.OnStageStarted -= HandleStageStarted;
        StopAllCoroutines();

        if (_navInstance.valid) _navInstance.Remove(); // 네브 해제
    }

    private void HandleStageStarted(StageConfig config)
    {
        if (config == null) return;
        cfg = config;

        // 스테이지 전용 NavMeshData 등록(있으면)
        if (_navInstance.valid) _navInstance.Remove();
        if (cfg.spawnOnNavMesh && cfg.navMeshData != null)
        {
            // NavMeshData의 transform(에셋 인스펙터에 보이는 Position/Rotation)을 그대로 사용
            _navInstance = NavMesh.AddNavMeshData(cfg.navMeshData,
                            cfg.navMeshData.position, cfg.navMeshData.rotation);
        }

        StopAllCoroutines();
        loops.Clear();
        aliveCounts.Clear();

        for (int i = 0; i < cfg.spawns.Count; i++) aliveCounts.Add(0);
        for (int i = 0; i < cfg.spawns.Count; i++) loops.Add(StartCoroutine(RunRule(i)));
    }

    private IEnumerator RunRule(int i)
    {
        var rule = cfg.spawns[i];

        if (rule.startCondition == StageConfig.StartCondition.AfterSeconds && rule.startDelay > 0f)
            yield return new WaitForSeconds(rule.startDelay);

        for (int k = 0; k < rule.initialCount; k++) { SpawnOne(i); yield return null; }

        while (true)
        {
            while (aliveCounts[i] < rule.maxCount)
            {
                SpawnOne(i);
                yield return (rule.respawnInterval > 0f)
                    ? new WaitForSeconds(rule.respawnInterval) : null;
            }
            yield return null;
        }
    }

    private void SpawnOne(int i)
    {
        var rule = cfg.spawns[i];
        if (rule.prefab == null) return;

        Vector3 pos = GetSpawnPosOnNavMesh(); // NavMesh 기반 위치
        var go = Instantiate(rule.prefab, pos, Quaternion.identity);

        aliveCounts[i]++;
        int idx = i;
        var notifier = go.AddComponent<_DespawnNotifier>();
        notifier.onDespawn = () =>
        {
            if (_shuttingDown) return;
            if (idx < 0 || idx >= aliveCounts.Count) return;
            aliveCounts[idx] = Mathf.Max(0, aliveCounts[idx] - 1);
        };
    }

    // ---------- NavMesh 위에서 안전하게 스폰 위치 구하기 ----------
    private Vector3 GetSpawnPosOnNavMesh()
    {
        // 1) NavMeshData가 있으면 그 bounds 안에서 랜덤 → SamplePosition
        if (cfg.spawnOnNavMesh && cfg.navMeshData != null)
        {
            var b = cfg.navMeshData.sourceBounds; // 에셋 인스펙터의 Source Bounds
            for (int attempt = 0; attempt < 12; attempt++)
            {
                var random = new Vector3(
                    Random.Range(b.min.x, b.max.x),
                    b.center.y,
                    Random.Range(b.min.z, b.max.z)
                );

                if (NavMesh.SamplePosition(random, out var hit,
                    cfg.navSampleMaxDistance, cfg.navAreaMask))
                {
                    return hit.position; // NavMesh 위 보장
                }
            }
        }

        // 2) (대안) 현재 씬 NavMesh에서 무조건 하나 뽑기: 삼각형 샘플
        if (cfg.spawnOnNavMesh)
        {
            var tri = NavMesh.CalculateTriangulation();
            if (tri.vertices != null && tri.indices != null && tri.indices.Length >= 3)
            {
                int triCount = tri.indices.Length / 3;
                int t = Random.Range(0, triCount) * 3;
                Vector3 a = tri.vertices[tri.indices[t + 0]];
                Vector3 b = tri.vertices[tri.indices[t + 1]];
                Vector3 c = tri.vertices[tri.indices[t + 2]];
                // 무작위 내부점
                float r1 = Random.value, r2 = Random.value;
                if (r1 + r2 > 1f) { r1 = 1f - r1; r2 = 1f - r2; }
                var p = a + (b - a) * r1 + (c - a) * r2;
                // 근처 다시 샘플(고저차 보정)
                if (NavMesh.SamplePosition(p, out var hit,
                    cfg.navSampleMaxDistance, cfg.navAreaMask))
                    return hit.position;
                return p; // 최후의 보정
            }
        }
        return Vector3.zero; // 실패
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
