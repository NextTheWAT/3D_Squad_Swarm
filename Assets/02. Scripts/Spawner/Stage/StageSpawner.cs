using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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

        // 현재 활성 씬이 진짜 스테이지 씬일 때만 즉시 시작
        if (StageManager.Instance != null && StageManager.Instance.Current != null)
        {
            var cur = StageManager.Instance.Current;
            if (IsActiveSceneMyStage(cur))
                HandleStageStarted(cur);
            else
                HardStopAndCleanup(); // 혹시 남아있던 것들 정리
        }

    }

    private void OnDisable()
    {
        _shuttingDown = true;
        StageManager.OnStageStarted -= HandleStageStarted;
        StopAllCoroutines();

        if (_navInstance.valid) _navInstance.Remove(); // 네브 해제
    }

    // === 현재 활성 씬이 스테이지 씬인지 확인 ===
    private bool IsActiveSceneMyStage(StageConfig c)
    {
        if (c == null || string.IsNullOrEmpty(c.sceneName)) return false;
        return SceneManager.GetActiveScene().name == c.sceneName;
    }

    // === 홈씬 등에서 잘못 들어왔을 때 강제 정리 ===
    private void HardStopAndCleanup()
    {
        StopAllCoroutines();
        loops.Clear();
        aliveCounts.Clear();
        cfg = null;

        if (_navInstance.valid) _navInstance.Remove();
    }

    private void HandleStageStarted(StageConfig config)
    {
        // 현재 씬이 스테이지 씬이 아니면 아무것도 하지 않음(방어적 정리 포함)
        if (!IsActiveSceneMyStage(config))
        {
            HardStopAndCleanup();
            return;
        }

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
        // 혹시 중간에 씬이 바뀌면 즉시 종료
        if (!IsActiveSceneMyStage(cfg)) yield break;

        var rule = cfg.spawns[i];

        if (rule.startCondition == StageConfig.StartCondition.AfterSeconds && rule.startDelay > 0f)
            yield return new WaitForSeconds(rule.startDelay);

        for (int k = 0; k < rule.initialCount; k++) { SpawnOne(i); yield return null; }

        while (true)
        {
            if (!IsActiveSceneMyStage(cfg)) yield break; // 루프 중에도 가드
            while (aliveCounts[i] < rule.maxCount)
            {
                if (!IsActiveSceneMyStage(cfg)) yield break; // 추가 가드
                SpawnOne(i);
                yield return (rule.respawnInterval > 0f)
                    ? new WaitForSeconds(rule.respawnInterval) : null;
            }
            yield return null;
        }
    }

    private void SpawnOne(int i)
    {
        if (!IsActiveSceneMyStage(cfg)) return; // 스폰 시점 가드

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
        if (cfg != null && cfg.spawnOnNavMesh && cfg.navMeshData != null)
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
