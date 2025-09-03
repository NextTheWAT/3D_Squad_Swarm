using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Stage/StageConfig")]
public class StageConfig : ScriptableObject
{
    [Header("Scene")]
    public string sceneName;

    [Header("Spawn Rules")]
    public List<SpawnRule> spawns = new();

    [System.Serializable]
    public class SpawnRule
    {
        public string name;
        public GameObject prefab;
        public int initialCount = 0;
        public int maxCount = 10;
        public float respawnInterval = 1f;
        public StartCondition startCondition = StartCondition.Immediate;
        public float startDelay = 0f;
    }

    public enum StartCondition { Immediate, AfterSeconds }

    // ---------- 여기부터 NavMesh 기반 스폰 옵션 ----------
    [Header("NavMesh Spawn")]
    public bool spawnOnNavMesh = true;        // NavMesh 위로만 스폰
    public NavMeshData navMeshData;           // 스테이지 전용 NavMeshData 에셋
    public float navSampleMaxDistance = 2f;   // 샘플 반경
    public int navAreaMask = ~0;              // 모든 영역(~0) / 필요 시 마스크 지정

}
