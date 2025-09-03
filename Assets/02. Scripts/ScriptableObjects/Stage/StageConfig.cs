using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage/StageConfig")]
public class StageConfig : ScriptableObject
{
    [Header("Scene")]
    public string sceneName; // Build Settings ��ϵ� �� �̸�

    // === ���� �߰�: ���� ��Ģ ===
    public enum StartCondition { Immediate, AfterSeconds, AfterInfectionPercent }

    [System.Serializable]
    public class SpawnRule
    {
        [Header("What")]
        public string name;                 // ��: "Human", "VIP", "Hunter"
        public GameObject prefab;         // ��ȯ�� ������

        [Header("How many")]
        public int initialCount = 0;      // ���� ��� ���� ��
        public int maxCount = 10;         // ���� �ִ� ���� ��
        public float respawnInterval = 1f;// �̸��� �� ä��� �ֱ�

        [Header("When")]
        public StartCondition startCondition = StartCondition.Immediate;
        public float startDelay = 0f;         // AfterSeconds ��

        [Header("Where")]
        public Transform[] spawnPoints;       // ���� ����Ʈ(������ randomArea ���)
        public Vector2 randomAreaSize = new Vector2(12f, 12f); // �߽�(0,0) ���� XZ �簢��
        public float fixedY = 0f;             // ���� ���� ��
    }

    [Header("Spawns")]
    public List<SpawnRule> spawns = new();
}
