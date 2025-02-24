using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    public ClickDetector ClickDetector;
    public GameObject CellPrefab;

    [Header("Colors")]
    public Color BlankCellColor;
    public Color HighlightedCellColor;
    public Color MovementPreviewColor;

    // System
    private Dictionary<Vector2, GameObject> _map = new();

    public List<Vector2> _newOffsets = new();
    private List<Vector2> _oldOffsets = new();

    private bool _isShown = true;

    private Vector2 _lastPointedPos;
    private GameObject _currentPointedCell;
    private GameObject _lastPointedCell;

    private void Start()
    {
        for (int x = -12; x <= 12; x++)
        {

            for (int y = -12; y <= 12; y++)
            {
                GameObject newCell = Instantiate(CellPrefab, new Vector3(x, 1.51f, y), Quaternion.identity, this.transform);
                newCell.transform.Rotate(Vector3.right * 90);
                _map.Add(new Vector2(x, y), newCell);
                if (x == -12 | x == 12 | y == -12 | y == 12)
                {
                    newCell.TryGetComponent(out SpriteRenderer spriteRenderer);
                    spriteRenderer.enabled = false;
                }
            }
        }
        _currentPointedCell = _map[Vector2.zero];
        _lastPointedCell = _currentPointedCell;
    }

    private void Update()
    {
        Vector2 currentPointedPos = new Vector2(ClickDetector.Pos.x, ClickDetector.Pos.z);
        
        if (!(ClickDetector.Pos.x > 12 | ClickDetector.Pos.x < -12 | ClickDetector.Pos.z > 12 | ClickDetector.Pos.z < -12))
        {
            _currentPointedCell = _map[new Vector2(Mathf.RoundToInt(ClickDetector.Pos.x), Mathf.RoundToInt(ClickDetector.Pos.z))];
        }


        if (_currentPointedCell != _lastPointedCell && _currentPointedCell != null)
        {
            _lastPointedCell.TryGetComponent(out SpriteRenderer lastCellSpriteRend);
            lastCellSpriteRend.color = BlankCellColor;
            _lastPointedCell = _currentPointedCell;
            _currentPointedCell.TryGetComponent(out SpriteRenderer currentCellSpriteRend);
            currentCellSpriteRend.color = HighlightedCellColor;
        }


        if (_oldOffsets.Count > 0)
        {
            for (int i = 0; i < _oldOffsets.Count; i++)
            {
                Vector2 oldOffset = _oldOffsets[i];
                if (_map.ContainsKey(oldOffset))
                {
                    if (_map[oldOffset] == _currentPointedCell) break;
                    _map[oldOffset].TryGetComponent(out SpriteRenderer oldCellSpriteRend);
                    oldCellSpriteRend.color = BlankCellColor;
                    _oldOffsets.Remove(oldOffset);
                }
            }
        }

        if (_newOffsets.Count > 0)
        {
            for (int i = 0; i < _newOffsets.Count;i++)
            {
                Vector2 current = _newOffsets[i];
                Vector2 offset = new Vector2(Mathf.RoundToInt(currentPointedPos.x) + current.x, Mathf.RoundToInt(currentPointedPos.y) + current.y);
                //print(_map.ContainsKey(offset) + " | " + offset);
                if (_map.ContainsKey(offset))
                {
                    _map[offset].TryGetComponent(out SpriteRenderer newOffsetSpriteRend);
                    newOffsetSpriteRend.color = MovementPreviewColor;
                    _oldOffsets.Add(offset);
                }
            }
        }

        _lastPointedPos = currentPointedPos;
    }

    public void ShowOrHide()
    {
        foreach (GameObject cell in _map.Values)
        {
            cell.TryGetComponent(out SpriteRenderer cellSpriteR);
            cellSpriteR.enabled = !cellSpriteR.enabled;
        }
        _isShown = !_isShown;
    }
}
