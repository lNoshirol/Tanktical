using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLaunchBattle : MonoBehaviour
{
    [SerializeField] private List<Entity> _entitiesInBattle = new();
    [SerializeField] private NavMeshAgent _navMeshAgent;
    private PlayerNavMeshController _playerMovement;
    private bool _actif = false;

    [SerializeField] private PlayerDetectorRadius _detectorRadius;
    [SerializeField] private float Radius = 10f;
    [SerializeField] private float visionAngle = 60f;

    private Coroutine _fightCoroutine;
    private FightStateManager _someFight;

    void Update()
    {
        if (_detectorRadius.DetectedPlayer != null && _fightCoroutine == null)
        {
            _detectorRadius.DetectedPlayer.TryGetComponent(out PlayerNavMeshController _playerMovement);
            
            if (_playerMovement != null)
            {
                _actif = true;
                _playerMovement.MovementActive = false;
                _playerMovement.StopMovement();
                _navMeshAgent.SetDestination(_detectorRadius.DetectedPlayer.transform.position);
                _fightCoroutine = StartCoroutine(Wait());
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
        FightStateManager fight = new(PlayerTeam.Instance.Team, _entitiesInBattle);
        fight.Init();
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
