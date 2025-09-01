using UnityEngine;
using UnityEngine.AI;

public class NpcTest : MonoBehaviour
{
    public NavMeshAgent agent;
    public float patrolRange = 10f;

    private Vector3 destination;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
    }

    void Update()
    {
        // �������� ���� �����ϸ� ���ο� ���� ������ ����
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomPos = transform.position + new Vector3(
            Random.Range(-patrolRange, patrolRange),
            0,
            Random.Range(-patrolRange, patrolRange)
        );
        agent.SetDestination(randomPos);
    }
}
