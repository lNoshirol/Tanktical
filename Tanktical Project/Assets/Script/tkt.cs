using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tkt : MonoBehaviour
{
    private void Update()
    {
        GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
    }
}
