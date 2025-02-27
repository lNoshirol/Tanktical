using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;
using Unity.AI.Navigation;
using UnityEngine.AI;

public class SelectionZone : MonoBehaviour
{
    public static SelectionZone Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private GameObject _hideMap;
    [SerializeField] private LayerMask selectionLayer;
    public List<ZoneSelectable> allSelectableZones = new List<ZoneSelectable>();
    public List<ZoneSelectable> selectedZones = new List<ZoneSelectable>();
    public List<ZoneSelectable> EnemySpawn = new List<ZoneSelectable>();
    public List<ZoneSelectable> PlayerSpawn = new List<ZoneSelectable>();
    [SerializeField] private Vector3 boxsize;
    private bool _battle;
    [SerializeField] private GameObject _cam1;
    [SerializeField] private GameObject _cam2;
    [SerializeField] private GameObject _uiBattle;
    [SerializeField] private NavMeshSurface _surface;
    public List<GameObject> ListPlayer;
    public List<GameObject> ListEnemy;

    [Header("Paramètres de l'arrivée du fond noir")]
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float timeToReachTarget = 3f;
    private float elapsedTime = 0f;

    public void OnStartGenerate(List<GameObject> enemy)
    {
        ListEnemy.Clear();
        for (int i = 0; i < enemy.Count; i++)
        {
            ListEnemy.Add(enemy[i]);
        }
        _cam1.SetActive(false);
        _cam2.SetActive(true);
        _uiBattle.SetActive(true);
        _battle = true;
        DetectSelectableZones();
        ZoneSelectable closestZone = GetClosestZone();
        if (closestZone != null)
        {
            StartCoroutine(closestZone.ActivateSelectionDelay(0));
        }

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(6);
        _surface.BuildNavMesh();
        yield return new WaitForSeconds(4);
        EnemySpawnZones();
        PlayerSpawnZones();
        PlaceCharacters();
    }

    public void EndBattle()
    {
        for (int i = 0; i < allSelectableZones.Count; i++)
        {
            allSelectableZones[i].EndBattle();
        }

        _battle = false;
        _cam1.SetActive(true);
        _cam2.SetActive(false);
        _uiBattle.SetActive(false);
        allSelectableZones.Clear();
        selectedZones.Clear();
        EnemySpawn.Clear();
        _hideMap.transform.localPosition = new Vector3(0, -10, 0);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            EndBattle();
        }

        if (!_battle) return;

        if (elapsedTime < timeToReachTarget)
        {
            _hideMap.transform.position = Vector3.Lerp(_hideMap.transform.position, targetPosition, elapsedTime / timeToReachTarget);
            elapsedTime += Time.deltaTime;
        }
        else
        {
            _hideMap.transform.position = targetPosition;
        }
    }

    void DetectSelectableZones()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, boxsize, Quaternion.identity, selectionLayer);

        foreach (Collider col in hitColliders)
        {
            ZoneSelectable zone = col.GetComponent<ZoneSelectable>();
            if (zone != null && !allSelectableZones.Contains(zone))
            {
                allSelectableZones.Add(zone);
            }
        }
    }

    ZoneSelectable GetClosestZone()
    {
        ZoneSelectable closest = null;
        float minDistance = float.MaxValue;
        foreach (ZoneSelectable zone in allSelectableZones)
        {
            float distance = Vector3.Distance(transform.position, zone.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = zone;
            }
        }
        return closest;
    }

    public void AddToSelectedList(ZoneSelectable zone)
    {
        if (!selectedZones.Contains(zone))
        {
            selectedZones.Add(zone);
        }
    }

    private void EnemySpawnZones()
    {
        List<ZoneSelectable> validZones = new List<ZoneSelectable>();
        foreach (ZoneSelectable zone in selectedZones)
        {
            if (zone.transform.position.z > transform.position.z && !zone.IsRock)
            {
                validZones.Add(zone);
            }
        }
        validZones = validZones.OrderBy(x => Random.value).ToList();
        EnemySpawn.Clear();
        for (int i = 0; i < Mathf.Min(5, validZones.Count); i++)
        {
            EnemySpawn.Add(validZones[i]);
        }
    }

    private void PlayerSpawnZones()
    {
        List<ZoneSelectable> validZones = new List<ZoneSelectable>();
        foreach (ZoneSelectable zone in selectedZones)
        {
            if (zone.transform.position.z < transform.position.z && !zone.IsRock)
            {
                validZones.Add(zone);
            }
        }
        validZones = validZones.OrderBy(x => Random.value).ToList();
        PlayerSpawn.Clear();
        for (int i = 0; i < Mathf.Min(5, validZones.Count); i++)
        {
            PlayerSpawn.Add(validZones[i]);
        }
    }

    private void PlaceCharacters()
    {
        for (int i = 0; i < EnemySpawn.Count && i < ListEnemy.Count; i++)
        {
            PatrolAgent patrolAgent = ListEnemy[i].GetComponent<PatrolAgent>();
            if (patrolAgent != null)
            {
                patrolAgent.enabled = false;
            }
            MateFollow mateFollow = ListEnemy[i].GetComponent<MateFollow>();
            if (mateFollow != null)
            {
                mateFollow.enabled = false;
            }
            NavMeshAgent enemyAgent = ListEnemy[i].GetComponent<NavMeshAgent>();
            if (enemyAgent != null)
            {
                enemyAgent.Warp(EnemySpawn[i].transform.position + new Vector3(0, 1, 0));
            }
        }

        for (int i = 0; i < PlayerSpawn.Count && i < ListPlayer.Count; i++)
        {
            PlayerNavMeshController playerController = ListPlayer[i].GetComponent<PlayerNavMeshController>();
            if (playerController != null)
            {
                playerController.enabled = false;
            }
            MateFollow mateFollow = ListPlayer[i].GetComponent<MateFollow>();
            if (mateFollow != null)
            {
                mateFollow.enabled = false;
            }
            NavMeshAgent playerAgent = ListPlayer[i].GetComponent<NavMeshAgent>();
            if (playerAgent != null)
            {
                playerAgent.Warp(PlayerSpawn[i].transform.position + new Vector3(0, 1, 0));
            }
        }
    }

}
