using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    [SerializeField] private List<GameObject> _groupPlayer;
    void Start()
    {
        for (int i = 0; i < _groupPlayer.Count; i++)
        {
            if (SaveManager.Exists($"playerPosition_{i}"))
            {
                _groupPlayer[i].transform.position = SaveManager.GetVariable<Vector3>($"playerPosition_{i}");
            }
        }
    }

    public void Onsave()
    {
        for (int i = 0; i < _groupPlayer.Count; i++)
        {
            SaveManager.SetVariable($"playerPosition_{i}", _groupPlayer[i].transform.position);
        }
        SaveManager.Save("save.json");
    }
}
