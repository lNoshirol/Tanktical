using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MateFollow : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform _transform;
    private bool _follow = true;

    void Update()
    {
        if (navMeshAgent != null && _follow)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            navMeshAgent.SetDestination(position);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        _follow = false;
        yield return new WaitForSeconds(0.5f);
        _follow = true;
    }
}
