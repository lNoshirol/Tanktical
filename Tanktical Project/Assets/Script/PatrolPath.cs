using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolPath : MonoBehaviour
{
    public List<NavMeshAgent> EnemiesToAssign = new List<NavMeshAgent>();
    public List<Transform> PathNodes = new List<Transform>();

    public float GetDistanceToNode(Vector3 origin, int nodeIndex)
    {
        if (!IsValidNode(nodeIndex))
        {
            return -1f;
        }

        return Vector3.Distance(PathNodes[nodeIndex].position, origin);
    }

    public Vector3 GetPositionOfPathNode(int nodeIndex)
    {
        if (!IsValidNode(nodeIndex))
        {
            return Vector3.zero;
        }

        return PathNodes[nodeIndex].position;
    }

    private bool IsValidNode(int index)
    {
        if (index >= 0 && index < PathNodes.Count && PathNodes[index] != null)
        {
            return true;
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        if (PathNodes.Count < 2)
        {
            return;
        }

        Gizmos.color = Color.cyan;

        for (int i = 0; i < PathNodes.Count; i++)
        {
            Transform current = PathNodes[i];
            Transform next = PathNodes[(i + 1) % PathNodes.Count];

            if (current != null && next != null)
            {
                Gizmos.DrawLine(current.position, next.position);
                Gizmos.DrawSphere(current.position, 0.1f);
            }
        }
    }
}
