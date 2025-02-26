using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class LevelDesigner : EditorWindow
{
    public int Width = 0;
    public int Length = 0;
    public int Space = 0;

    public int NumberWall = 0;
    public int NumberBreakableWall = 0;
    public int NumberBox = 0;

    public GameObject WallPrefab = null;
    public GameObject WallPrefab1 = null;
    public GameObject WallPrefab2 = null;
    public GameObject WallPrefab3 = null;
    public GameObject BreakableWallPrefab = null;
    public GameObject BreakableWallPrefab1 = null;
    public GameObject BreakableWallPrefab2 = null;
    public GameObject BreakableWallPrefab3 = null;
    public GameObject SpawnMapPrefab = null;
    public GameObject SpawnMapPrefab1 = null;
    public GameObject SpawnMapPrefab2 = null;
    public GameObject SpawnMapPrefab3 = null;
    public GameObject BoxPrefab = null;
    public GameObject BoxPrefab1 = null;
    public GameObject BoxPrefab2 = null;
    public GameObject BoxPrefab3 = null;

    private List<GameObject> _listBaseMap = new List<GameObject>();

    [MenuItem("Window/MyWindow/Level Designer")]
    public static void ShowWindow()
    {
        GetWindow<LevelDesigner>("Level Designer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Cr?e une base de niveau", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        Width = EditorGUILayout.IntField("Width", Width);
        Length = EditorGUILayout.IntField("Length", Length);
        Space = EditorGUILayout.IntField("Space", Space);
        EditorGUILayout.Space();

        NumberWall = EditorGUILayout.IntField("Rock Number", NumberWall);
        NumberBreakableWall = EditorGUILayout.IntField("Three Number", NumberBreakableWall);
        NumberBox = EditorGUILayout.IntField("Box Number", NumberBox);
        EditorGUILayout.Space();

        WallPrefab = EditorGUILayout.ObjectField("Rock Prefab", WallPrefab, typeof(GameObject), true) as GameObject;
        WallPrefab1 = EditorGUILayout.ObjectField("Rock Prefab", WallPrefab1, typeof(GameObject), true) as GameObject;
        WallPrefab2 = EditorGUILayout.ObjectField("Rock Prefab", WallPrefab2, typeof(GameObject), true) as GameObject;
        WallPrefab3 = EditorGUILayout.ObjectField("Rock Prefab", WallPrefab3, typeof(GameObject), true) as GameObject;
        BreakableWallPrefab = EditorGUILayout.ObjectField("Three Prefab", BreakableWallPrefab, typeof(GameObject), true) as GameObject;
        BreakableWallPrefab1 = EditorGUILayout.ObjectField("Three Prefab", BreakableWallPrefab1, typeof(GameObject), true) as GameObject;
        BreakableWallPrefab2 = EditorGUILayout.ObjectField("Three Prefab", BreakableWallPrefab2, typeof(GameObject), true) as GameObject;
        BreakableWallPrefab3 = EditorGUILayout.ObjectField("Three Prefab", BreakableWallPrefab3, typeof(GameObject), true) as GameObject;
        SpawnMapPrefab = EditorGUILayout.ObjectField("Ground Prefab", SpawnMapPrefab, typeof(GameObject), true) as GameObject;
        SpawnMapPrefab1 = EditorGUILayout.ObjectField("Ground Prefab", SpawnMapPrefab1, typeof(GameObject), true) as GameObject;
        SpawnMapPrefab2 = EditorGUILayout.ObjectField("Ground Prefab", SpawnMapPrefab2, typeof(GameObject), true) as GameObject;
        SpawnMapPrefab3 = EditorGUILayout.ObjectField("Ground Prefab", SpawnMapPrefab3, typeof(GameObject), true) as GameObject;
        BoxPrefab = EditorGUILayout.ObjectField("Box Prefab", BoxPrefab, typeof(GameObject), true) as GameObject;
        BoxPrefab1 = EditorGUILayout.ObjectField("Box Prefab", BoxPrefab1, typeof(GameObject), true) as GameObject;
        BoxPrefab2 = EditorGUILayout.ObjectField("Box Prefab", BoxPrefab2, typeof(GameObject), true) as GameObject;
        BoxPrefab3 = EditorGUILayout.ObjectField("Box Prefab", BoxPrefab3, typeof(GameObject), true) as GameObject;
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Generate New Map"))
        {
            GenerateGrid();
            _listBaseMap.Clear();
        }
    }

    private void GenerateGrid()
    {
        CleanNullReferences(); // Avant de commencer, nettoyez les r?f?rences nulles.

        if (WallPrefab == null || SpawnMapPrefab == null)
        {
            Debug.LogError("Wall Prefab or Spawn Map Prefab is not assigned!");
            return;
        }

        GameObject parentObject = new GameObject("GeneratedGrid");

        for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Length; z++)
            {
                Vector3 position = new Vector3(x * Space, 0, z * Space);
                GameObject instance = null;
                int random = Random.Range(0, 4);
                switch (random)
                {
                    case 0:
                        instance = Instantiate(SpawnMapPrefab, position, Quaternion.identity);
                        break;
                    case 1:
                        instance = Instantiate(SpawnMapPrefab1, position, Quaternion.identity);
                        break;
                    case 2:
                        instance = Instantiate(SpawnMapPrefab2, position, Quaternion.identity);
                        break;
                    case 3:
                        instance = Instantiate(SpawnMapPrefab3, position, Quaternion.identity);
                        break;
                }
                instance.transform.parent = parentObject.transform;
                _listBaseMap.Add(instance); // Ajouter ? la liste.
            }
        }

        //GenerateWalls(parentObject);

        for (int i = 0; i < NumberBox; i++)
        {
            CleanNullReferences(); // V?rifiez la liste avant chaque it?ration.

            int choose = Random.Range(0, _listBaseMap.Count);
            GameObject instance = null;
            int random = Random.Range(0, 4);
            switch (random)
            {
                case 0:
                    instance = Instantiate(BoxPrefab);
                    break;
                case 1:
                    instance = Instantiate(BoxPrefab1);
                    break;
                case 2:
                    instance = Instantiate(BoxPrefab2);
                    break;
                case 3:
                    instance = Instantiate(BoxPrefab3);
                    break;
            }
            instance.transform.position = _listBaseMap[choose].transform.position;
            instance.transform.parent = parentObject.transform;
            DestroyImmediate(_listBaseMap[choose]);
            _listBaseMap.RemoveAt(choose);
        }

        for (int i = 0; i < NumberWall; i++)
        {
            CleanNullReferences(); // V?rifiez la liste avant chaque it?ration.

            int choose = Random.Range(0, _listBaseMap.Count);
            GameObject instance = null;
            int random = Random.Range(0, 4);
            switch (random)
            {
                case 0:
                    instance = Instantiate(WallPrefab);
                    break;
                case 1:
                    instance = Instantiate(WallPrefab1);
                    break;
                case 2:
                    instance = Instantiate(WallPrefab2);
                    break;
                case 3:
                    instance = Instantiate(WallPrefab3);
                    break;
            }
            instance.transform.position = _listBaseMap[choose].transform.position;
            instance.transform.parent = parentObject.transform;
            DestroyImmediate(_listBaseMap[choose]);
            _listBaseMap.RemoveAt(choose);
        }

        for (int i = 0; i < NumberBreakableWall; i++)
        {
            CleanNullReferences(); // V?rifiez la liste avant chaque it?ration.

            int choose = Random.Range(0, _listBaseMap.Count);
            GameObject instance = null;
            int random = Random.Range(0, 4);
            switch (random)
            {
                case 0:
                    instance = Instantiate(BreakableWallPrefab);
                    break;
                case 1:
                    instance = Instantiate(BreakableWallPrefab1);
                    break;
                case 2:
                    instance = Instantiate(BreakableWallPrefab2);
                    break;
                case 3:
                    instance = Instantiate(BreakableWallPrefab3);
                    break;
            }
            instance.transform.position = _listBaseMap[choose].transform.position;
            instance.transform.parent = parentObject.transform;
        }

        CleanNullReferences();
        Debug.Log("Grid and walls generated successfully!");
    }


    private void GenerateWalls(GameObject parentObject)
    {
        HashSet<Vector3> wallPositions = new HashSet<Vector3>();

        // G?n?ration des murs du haut et du bas
        for (int x = 0; x < Width; x++)
        {
            Vector3 topPosition = new Vector3(x * Space, 0, 0);
            wallPositions.Add(topPosition);
            GameObject topWall = Instantiate(WallPrefab, topPosition, Quaternion.identity);
            topWall.transform.parent = parentObject.transform;
            OnDestroyate(topPosition);

            Vector3 bottomPosition = new Vector3(x * Space, 0, (Length - 1) * Space);
            wallPositions.Add(bottomPosition);
            GameObject bottomWall = Instantiate(WallPrefab, bottomPosition, Quaternion.identity);
            bottomWall.transform.parent = parentObject.transform;
            OnDestroyate(bottomPosition);
        }

        // G?n?ration des murs des c?t?s gauche et droit
        for (int z = 0; z < Length; z++)
        {
            Vector3 leftPosition = new Vector3(0, 0, z * Space);
            wallPositions.Add(leftPosition);
            GameObject leftWall = Instantiate(WallPrefab, leftPosition, Quaternion.identity);
            leftWall.transform.parent = parentObject.transform;
            OnDestroyate(leftPosition);

            Vector3 rightPosition = new Vector3((Width - 1) * Space, 0, z * Space);
            wallPositions.Add(rightPosition);
            GameObject rightWall = Instantiate(WallPrefab, rightPosition, Quaternion.identity);
            rightWall.transform.parent = parentObject.transform;
            OnDestroyate(rightPosition);
        }
    }

    private void OnDestroyate(Vector3 position)
    {
        for (int i = _listBaseMap.Count - 1; i >= 0; i--) // Boucle invers?e
        {
            if (_listBaseMap[i] != null && _listBaseMap[i].transform.position == position) // V?rifier si l'objet est valide
            {
                DestroyImmediate(_listBaseMap[i]); // Supprimer imm?diatement l'objet
                _listBaseMap.RemoveAt(i); // Retirer de la liste
                CleanNullReferences();
            }
        }
    }

    private void CleanNullReferences()
    {
        _listBaseMap.RemoveAll(item => item = null);
    }
}