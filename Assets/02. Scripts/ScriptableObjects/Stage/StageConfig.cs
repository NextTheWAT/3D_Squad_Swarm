using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage/StageConfig")]
public class StageConfig : ScriptableObject
{
    [Header("Scene")]
    public string sceneName; // Build Settings 등록된 씬 이름

    // === 새로 추가: 스폰 규칙 ===
    public enum StartCondition { Immediate, AfterSeconds, AfterInfectionPercent }

    [System.Serializable]
    public class SpawnRule
    {
        [Header("What")]
        public string name;                 // 예: "Human", "VIP", "Hunter"
        public GameObject prefab;         // 소환할 프리팹

        [Header("How many")]
        public int initialCount = 0;      // 시작 즉시 생성 수
        public int maxCount = 10;         // 동시 최대 존재 수
        public float respawnInterval = 1f;// 미만일 때 채우는 주기

        [Header("When")]
        public StartCondition startCondition = StartCondition.Immediate;
        public float startDelay = 0f;         // AfterSeconds 용

        [Header("Where")]
        public Transform[] spawnPoints;       // 지정 포인트(없으면 randomArea 사용)
        public Vector2 randomAreaSize = new Vector2(12f, 12f); // 중심(0,0) 기준 XZ 사각형
        public float fixedY = 0f;             // 높이 고정 값
    }

    [Header("Spawns")]
    public List<SpawnRule> spawns = new();
}
