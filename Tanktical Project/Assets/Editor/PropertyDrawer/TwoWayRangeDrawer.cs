using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TwoWayRangeAttribute))]
public class TwoWayRangeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        TwoWayRangeAttribute range = attribute as TwoWayRangeAttribute;

        if (property.propertyType == SerializedPropertyType.Vector2)
        {
            var value = property.vector2Value;
            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField(property.displayName, new [] {GUILayout.MaxWidth(125)});
            EditorGUILayout.FloatField(value.x, new []{GUILayout.MaxWidth(45)});
            EditorGUILayout.MinMaxSlider(ref value.x, ref value.y, range.Min, range.Max);
            EditorGUILayout.FloatField(value.y, new []{GUILayout.MaxWidth(45)});
            
            EditorGUILayout.EndHorizontal();
            
            property.vector2Value = value;
        }
    }
}
