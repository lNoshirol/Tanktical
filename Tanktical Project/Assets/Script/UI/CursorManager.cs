using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private GameObject _cursor;
    [SerializeField] private Vector2 _offset;

    private Camera _camera;

    void Awake()
    {
        _camera = Camera.main;
        Cursor.visible = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        _cursor.transform.position = Input.mousePosition - (Vector3)_offset;
    }
}
