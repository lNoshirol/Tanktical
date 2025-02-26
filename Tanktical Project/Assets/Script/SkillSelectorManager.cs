using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelectorManager : MonoBehaviour
{
    public static SkillSelectorManager Instance;

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
}
