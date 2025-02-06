using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerNavMeshController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private LayerMask clickableLayers;

    public void OnClickPerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayers))
            {
                if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas))
                {
                    navMeshAgent.SetDestination(navHit.position);
                }
            }
        }
    }
}
