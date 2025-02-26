using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(ZoneSelectable))]
[CanEditMultipleObjects] // Permet d'agir sur plusieurs objets s�lectionn�s
public class DestroyADuplicate : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("D�truire les doubles"))
        {
            if (EditorUtility.DisplayDialog("Suppression des doublons",
                "�tes-vous s�r de vouloir supprimer les objets en double ?", "Oui", "Annuler"))
            {
                DestroyDuplicates();
            }
        }
    }

    private void DestroyDuplicates()
    {
        // R�cup�rer tous les objets s�lectionn�s dans l'�diteur
        ZoneSelectable[] selectedObjects = new ZoneSelectable[targets.Length];
        for (int i = 0; i < targets.Length; i++)
        {
            selectedObjects[i] = (ZoneSelectable)targets[i];
        }

        if (selectedObjects.Length == 0) return;

        // Trouver tous les objets de type ZoneSelectable dans la sc�ne
        ZoneSelectable[] allObjects = FindObjectsOfType<ZoneSelectable>();

        // Liste des objets � supprimer pour �viter des suppressions en boucle
        List<GameObject> objectsToDelete = new List<GameObject>();

        foreach (ZoneSelectable referenceObject in selectedObjects)
        {
            foreach (ZoneSelectable obj in allObjects)
            {
                // V�rifier si un autre objet a la m�me position et n'est pas dans la s�lection
                if (obj != referenceObject && obj.transform.position == referenceObject.transform.position)
                {
                    if (!objectsToDelete.Contains(obj.gameObject)) // �viter les suppressions multiples
                    {
                        objectsToDelete.Add(obj.gameObject);
                    }
                }
            }
        }

        // Supprimer les objets en dehors de la boucle de comparaison
        int deletedCount = 0;
        foreach (GameObject obj in objectsToDelete)
        {
            DestroyImmediate(obj);
            deletedCount++;
        }

        if (deletedCount > 0)
        {
            Debug.Log($"{deletedCount} objets en double ont �t� supprim�s.");
        }
        else
        {
            Debug.Log("Aucun objet en double trouv�.");
        }
    }
}
