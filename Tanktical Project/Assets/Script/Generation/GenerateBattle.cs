using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBattle : MonoBehaviour
{
    public static GenerateBattle Instance { get; private set; }

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

    public void GenerateTerrain(List<GameObject> enemy)
    {
        Vector3 vector3 = transform.position;
        SelectionZone.Instance.transform.position = new Vector3(vector3.x, 0, vector3.z);
        SelectionZone.Instance.OnStartGenerate(enemy);
    }
}
