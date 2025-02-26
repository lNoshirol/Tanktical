using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VFXTester))]
public class VFXTesterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        VFXTester tester = (VFXTester)target;

        if (EditorApplication.isPlaying)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Test ALL VFX"))
            {
                tester.OnTest();
            }
            
            if (GUILayout.Button("Cancel All VFX"))
            {
                tester.OnCancel();
            }
            EditorGUILayout.EndHorizontal();
            
            for(int i = 0; i<tester.TestInfos.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Test VFX: " + tester.TestInfos[i].Sequence.GetType().Name))
                {
                    tester.OnTest(i);
                }
                
                if (GUILayout.Button("Cancel VFX: " + tester.TestInfos[i].Sequence.GetType().Name))
                {
                    tester.OnCancel(i);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
