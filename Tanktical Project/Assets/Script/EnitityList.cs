using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnitityList : MonoBehaviour
{
    public List<GameObject> AllyList;
    public List<GameObject> EnnemyList;

    public static EnitityList instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
