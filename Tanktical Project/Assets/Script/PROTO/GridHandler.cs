using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    public ClickDetector ClickDetector;
    public GameObject CellPrefab;
    public Color BlankCell;
    public Color HighlightedCell;

    private Dictionary<Vector2, GameObject> _map = new();

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
            }
        }
        _currentPointedCell = _map[Vector2.zero];
        _lastPointedCell = _currentPointedCell;
    }

    private void Update()
    {
        if (ClickDetector.Pos.x > 12 | ClickDetector.Pos.x < -12 | ClickDetector.Pos.y > 12 | ClickDetector.Pos.y < -12) return;

        _currentPointedCell = _map[new Vector2(Mathf.RoundToInt(ClickDetector.Pos.x), Mathf.RoundToInt(ClickDetector.Pos.z))];
        if (_currentPointedCell != _lastPointedCell)
        {
            _lastPointedCell.TryGetComponent(out SpriteRenderer lastCellSpriteRend);
            lastCellSpriteRend.color = BlankCell;
            _lastPointedCell = _currentPointedCell;
            _currentPointedCell.TryGetComponent(out SpriteRenderer currentCellSpriteRend);
            currentCellSpriteRend.color = HighlightedCell;
        }
    }
}
