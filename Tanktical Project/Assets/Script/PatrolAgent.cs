using UnityEngine;
using UnityEngine.AI;

public class PatrolAgent : MonoBehaviour
{
    [SerializeField] private PatrolPath patrolPath;
    private NavMeshAgent agent;
    private int currentNodeIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (patrolPath != null && patrolPath.PathNodes.Count > 0)
        {
            MoveToNextNode();
        }
    }

    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            NextNode();
        }
    }

    void MoveToNextNode()
    {
        if (patrolPath == null) return;

        Vector3 targetPosition = patrolPath.GetPositionOfPathNode(currentNodeIndex);
        if (targetPosition != Vector3.zero)
        {
            agent.SetDestination(targetPosition);
        }
    }

    void NextNode()
    {
        currentNodeIndex = (currentNodeIndex + 1) % patrolPath.PathNodes.Count;
        MoveToNextNode();
    }
}
