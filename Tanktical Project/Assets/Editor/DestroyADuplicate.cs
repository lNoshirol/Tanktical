using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(ZoneSelectable))]
[CanEditMultipleObjects] // Permet d'agir sur plusieurs objets sélectionnés
public class DestroyADuplicate : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Détruire les doubles"))
        {
            if (EditorUtility.DisplayDialog("Suppression des doublons",
                "Êtes-vous sûr de vouloir supprimer les objets en double ?", "Oui", "Annuler"))
            {
                DestroyDuplicates();
            }
        }
    }

    private void DestroyDuplicates()
    {
        // Récupérer tous les objets sélectionnés dans l'éditeur
        ZoneSelectable[] selectedObjects = new ZoneSelectable[targets.Length];
        for (int i = 0; i < targets.Length; i++)
        {
            selectedObjects[i] = (ZoneSelectable)targets[i];
        }

        if (selectedObjects.Length == 0) return;

        // Trouver tous les objets de type ZoneSelectable dans la scène
        ZoneSelectable[] allObjects = FindObjectsOfType<ZoneSelectable>();

        // Liste des objets à supprimer pour éviter des suppressions en boucle
        List<GameObject> objectsToDelete = new List<GameObject>();

        foreach (ZoneSelectable referenceObject in selectedObjects)
        {
            foreach (ZoneSelectable obj in allObjects)
            {
                // Vérifier si un autre objet a la même position et n'est pas dans la sélection
                if (obj != referenceObject && obj.transform.position == referenceObject.transform.position)
                {
                    if (!objectsToDelete.Contains(obj.gameObject)) // Éviter les suppressions multiples
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
            Debug.Log($"{deletedCount} objets en double ont été supprimés.");
        }
        else
        {
            Debug.Log("Aucun objet en double trouvé.");
        }
    }
}
