using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLaunchBattle : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    private PlayerNavMeshController _playerMovement;
    private bool _actif = false;

    [SerializeField] private float Radius = 10f;
    [SerializeField] private float visionAngle = 60f;

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player") && !_actif)
            {
                Vector3 directionToPlayer = (hitCollider.transform.position - transform.position).normalized;
                float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

                if (angleToPlayer < visionAngle / 2)
                {
                    if (!Physics.Raycast(transform.position, directionToPlayer, Radius))
                    {
                        _playerMovement = hitCollider.GetComponent<PlayerNavMeshController>();
                        if (_playerMovement != null)
                        {
                            _actif = true;
                            _playerMovement.MovementActive = false;
                            _playerMovement.StopMovement();
                            _navMeshAgent.SetDestination(hitCollider.transform.position);
                            StartCoroutine(Wait());
                        }
                    }
                }
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        LaunchBattle();
    }

    public void LaunchBattle()
    {
        GenerateBattle.Instance.GenerateTerrain();
        if (_playerMovement != null)
        {
            _playerMovement.MovementActive = true;
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Radius);

        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2, 0) * transform.forward * Radius;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle / 2, 0) * transform.forward * Radius;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
