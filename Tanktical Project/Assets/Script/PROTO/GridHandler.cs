using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    public static GridHandler Instance;

    public ClickDetector ClickDetector;
    public GameObject CellPrefab;

    public List<GameObject> CellsList { get; private set; } = new List<GameObject>();

    [Header("Colors")]
    public Color BlankCellColor;
    public Color HighlightedCellColor;
    public Color MovementPreviewColor;
    public Color EnnemyOnCaseRangePreview;
    public Color AllyOnCaseRangePreview;
    public Color CaseInSkillRange;

    // System
    private Dictionary<Vector2, GameObject> _map = new();

    public List<Vector2> _newOffsets = new();
    [SerializeField] private List<Vector2> _oldOffsets = new();

    private bool _isShown = true;

    private Vector2 _lastPointedPos;
    private GameObject _currentPointedCell;
    private GameObject _lastPointedCell;

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

    private void Start()
    {
        for (int x = -12; x <= 12; x++)
        {

            for (int y = -12; y <= 12; y++)
            {
                GameObject newCell = Instantiate(CellPrefab, new Vector3(x, 1.51f, y), Quaternion.identity, this.transform);
                CellsList.Add(newCell);

                //newCell.AddComponent<tkt>();

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

        //// Cells to highlight
        //if (_oldOffsets.Count != 0)
        //{
        //    if (_currentPointedCell != _lastPointedCell)
        //    {
        //        for (int i = 0; i < _oldOffsets.Count; i++)
        //        {
        //            Vector2 offset = _oldOffsets[i];
        //            if (_map.ContainsKey(new Vector2(Mathf.RoundToInt(ClickDetector.Pos.x) + offset.x, Mathf.RoundToInt(ClickDetector.Pos.z) + offset.y))) break;
        //            _map[new Vector2(Mathf.RoundToInt(ClickDetector.Pos.x) + offset.x, Mathf.RoundToInt(ClickDetector.Pos.z) + offset.y)].TryGetComponent(out SpriteRenderer offsetSpriteRend);
        //            offsetSpriteRend.color = BlankCellColor;
        //            _oldOffsets.Remove(offset);
        //        }
        //    }
        //}

        //if (_newOffsets.Count != 0)
        //{
        //    foreach (Vector2 offset in _newOffsets)
        //    {
        //        // si _offsetsProcessed contient déjà l'offset OU s'il n'y a pas de case à cet endroit
        //        if (!_map.ContainsKey(new Vector2(Mathf.RoundToInt(ClickDetector.Pos.x) + offset.x, Mathf.RoundToInt(ClickDetector.Pos.z) + offset.y)) | _oldOffsets.Contains(offset)) return;
        //        _oldOffsets.Add(offset);
        //        print("added " + offset + " to offset processed");
        //        _map[new Vector2(Mathf.RoundToInt(ClickDetector.Pos.x) + offset.x, Mathf.RoundToInt(ClickDetector.Pos.z) + offset.y)].TryGetComponent(out SpriteRenderer offsetSpriteRend);
        //        offsetSpriteRend.color = MovementPreviewColor;
        //    }
        //}

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
