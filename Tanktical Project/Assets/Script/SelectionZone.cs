using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

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
    [SerializeField] private GameObject _group;
    [SerializeField] private LayerMask selectionLayer;
    public List<ZoneSelectable> allSelectableZones = new List<ZoneSelectable>();
    public List<ZoneSelectable> selectedZones = new List<ZoneSelectable>();
    public List<ZoneSelectable> EnemySpawn = new List<ZoneSelectable>();
    public List<ZoneSelectable> PlayerSpawn = new List<ZoneSelectable>();
    [SerializeField] private Vector3 boxsize;
    private bool _battle;

    [Header("Paramètres de l'arrivée du fond noir")]
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float timeToReachTarget = 3f;
    private float elapsedTime = 0f;

    /*private void Start()
    {
        OnStartGenerate();
    }*/

    public void OnStartGenerate()
    {
        _group.SetActive(false);
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
        yield return new WaitForSeconds(10);
        EnemySpawnZones();
    }

    public void EndBattle()
    {
        for (int i = 0; i < allSelectableZones.Count; i++)
        {
            allSelectableZones[i].EndBattle();
        }
        _battle = false;
        _group.SetActive(true);
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
            if (zone.transform.position.z > transform.position.z)
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
}
