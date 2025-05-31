using UnityEditor;
using UnityEngine;

// Custom drawer for DeckEntry to show prefab name in the list
[CustomPropertyDrawer(typeof(DeckEntry))]
public class DeckEntryDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var prefabProp = property.FindPropertyRelative("cardPrefab");
        string prefabName = prefabProp.objectReferenceValue != null ? prefabProp.objectReferenceValue.name : "None";


        label = new GUIContent(prefabName);

        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}
