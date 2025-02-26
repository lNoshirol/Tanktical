using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSelectable : MonoBehaviour
{
    public Vector3 startPos;
    public bool IsSelected = false;
    public bool IsRock = false;
    private List<ZoneSelectable> adjacentZones = new List<ZoneSelectable>();

    [Header("Paramètres de sélection")]
    [SerializeField] private float selectionDelay = 0.1f; // Temps avant l'activation d'une zone adjacente
    [SerializeField] private AnimationCurve Curve; // Courbe d'animation pour overshoot
    [SerializeField] private float Height = 1f; // Hauteur du déplacement
    [SerializeField] private float moveTime = 0.5f; // Durée du mouvement

    void Start()
    {
        startPos = transform.position;
        DetectAdjacentZones();
    }

    void DetectAdjacentZones()
    {
        if (IsRock) return;

        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };

        foreach (Vector3 dir in directions)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, 1.2f))
            {
                ZoneSelectable adjacentZone = hit.collider.GetComponent<ZoneSelectable>();
                if (adjacentZone != null && !adjacentZones.Contains(adjacentZone))
                {
                    adjacentZones.Add(adjacentZone);
                    Debug.DrawLine(transform.position, adjacentZone.transform.position, Color.green, 2f);
                }
            }
        }

        Debug.Log($"{gameObject.name} a détecté {adjacentZones.Count} zones adjacentes.");
    }

    public IEnumerator ActivateSelectionDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!IsSelected && SelectionZone.Instance.allSelectableZones.Contains(this))
        {
            IsSelected = true;
            SelectionZone.Instance.AddToSelectedList(this);
            StartCoroutine(MoveUp());

            float newDelay = delay + selectionDelay;

            foreach (ZoneSelectable zone in adjacentZones)
            {
                if (!zone.IsSelected)
                {
                    StartCoroutine(zone.ActivateSelectionDelay(newDelay));
                }
            }
        }
    }

    IEnumerator MoveUp()
    {
        Vector3 targetPos = startPos + new Vector3(0, Height, 0); // Position cible

        float elapsedTime = 0;

        while (elapsedTime < moveTime)
        {
            float curveValue = Curve.Evaluate(elapsedTime / moveTime);
            float height = Mathf.LerpUnclamped(0, Height, curveValue); // Utilisation de LerpUnclamped pour gérer l'overshoot
            transform.position = startPos + new Vector3(0, height, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos; // Assurer que l'objet finit bien à la position correcte
    }

    public void EndBattle()
    {
        transform.position = startPos;
    }
}
