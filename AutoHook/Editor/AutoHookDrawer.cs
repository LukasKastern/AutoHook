#if UNITY_EDITOR

using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomPropertyDrawer(typeof(AutoHook))]
public class AutoHookDrawer : PropertyDrawer
{ 
    private static string GetPropertyType(SerializedProperty property)
    {
        var type = property.type;
        var match = Regex.Match(type, @"PPtr<\$(.*?)>");
        if (match.Success)
            type = match.Groups[1].Value;
        return type;
    }
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var serializedObject = property.serializedObject.targetObject as MonoBehaviour;

        if (serializedObject == null)
        {
            Debug.LogWarning("Auto Hooking only works on classes that derive from MonoBehaviour");
            return;
        }
        
        var desiredPropertyType = GetPropertyType(property);

        var component = serializedObject.GetComponent(desiredPropertyType);

        if (component == null)
        {
            EditorGUI.ObjectField(position, property, label);

            return;
        }

        if (property.objectReferenceValue != component)
        {
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
            
        property.objectReferenceValue = component; 

        EditorGUI.LabelField(position, label, new GUIContent("Auto Hooked"));
    }
}

#endif