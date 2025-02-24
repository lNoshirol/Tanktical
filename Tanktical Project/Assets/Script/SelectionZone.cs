using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    [SerializeField] private Vector3 boxsize;
    private bool _battle;

    [Header("Paramètres de l'arrivée du fond noir")]
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float timeToReachTarget = 3f;
    private float elapsedTime = 0f;

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
        _hideMap.transform.localPosition = new Vector3(0, -10, 0);
    }

    void Update()
    {
        //a enlever plus tard sert juste de test
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
        return allSelectableZones.OrderBy(zone => Vector3.Distance(transform.position, zone.transform.position)).FirstOrDefault();
    }

    public void AddToSelectedList(ZoneSelectable zone)
    {
        if (!selectedZones.Contains(zone))
        {
            selectedZones.Add(zone);
        }
    }
}
