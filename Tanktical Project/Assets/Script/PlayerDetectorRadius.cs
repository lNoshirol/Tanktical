using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectorRadius : MonoBehaviour
{
    public GameObject DetectedPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DetectedPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && DetectedPlayer != null)
        {
            DetectedPlayer = null;
        }
    }
}
