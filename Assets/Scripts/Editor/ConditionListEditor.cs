using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using ConditionList = GGJ.ConditionList;

[CustomEditor(typeof(ConditionList), true)]
public class ConditionListEditor : Editor
{

    public enum ConditionType
    {
        EventPassedCondition,
        TimeCondition,
        ResourceCondition,
        SpecialResourceCondition,
        ConditionList
    }

    ConditionType currentConditionType;

    ReorderableList reorderableList;
    //bool initialized = false;
    //void Initialize(SerializedProperty property)
    //{
    //    var prout = property.FindPropertyRelative("conditionList");
    //    reorderableList = ReorderableListUtility.CreateAutoLayout(prout);
    //    initialized = true;
    //}

    public void OnEnable()
    {
        try
        {
            var prout = serializedObject.FindProperty("conditionList");
            reorderableList = ReorderableListUtility.CreateAutoLayout(prout,  new string[] { "test" });
        }
        catch
        {
            // Do nothing
        }

    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //{
    //    if (!initialized )
    //    {
    //        Initialize(property);
    //    }
    //    else
    //    {
    //        reorderableList.DoLayoutList();

    //    }
    //    //base.OnGUI(position, property, label);
    //}

    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //{

    //    var pro = property.FindPropertyRelative("conditionList");
    //    Debug.Log(pro.isArray);
    //if (!initialized)
    //    Initialize(property);

    //EditorGUI.BeginProperty(position, label, property);

    //// Draw label
    //position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

    //// Calculate rects
    //var conditionTypeRect = new Rect(position.x, position.height - 10, position.width - 50, position.height);
    //var addButtonRect = new Rect(position.x + position.width - 40, position.height - 10, 40, position.height);
    //var list = property.FindPropertyRelative("conditions");

    //reorderableList.drawElementCallback =
    //    (Rect rect, int index, bool isActive, bool isFocused) => {

    //        EditorGUI.PropertyField(rect, list.GetArrayElementAtIndex(index));
    //    };
    //reorderableList.DoList(position);

    //currentConditionType = (ConditionType)EditorGUI.EnumPopup(conditionTypeRect, currentConditionType);
    //if(GUI.Button(addButtonRect, "Add"))
    //{
    //    AddCondition(property, currentConditionType);
    //}

    //EditorGUI.EndProperty();

    //if (EditorGUI.EndChangeCheck())
    //{
    //    property.serializedObject.ApplyModifiedProperties();
    //}
    //}

    //public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    //{
    //    return base.GetPropertyHeight(property, label);
    //}

    public void AddCondition(SerializedProperty property, ConditionType condition)
    {
        SerializedProperty listProperty = property.FindPropertyRelative("conditionList");
        //listProperty.InsertArrayElementAtIndex(listProperty.arraySize-1);
        //var currentConditionProperty = listProperty.GetArrayElementAtIndex(listProperty.arraySize - 1);
        //switch (condition)
        //{
        //    case ConditionType.EventPassedCondition:
        //        currentConditionProperty.objectReferenceValue = new EventPassedCondition();
        //        break;
        //    case ConditionType.TimeCondition:
        //        currentConditionProperty.objectReferenceValue = new TimeCondition();
        //        break;
        //    case ConditionType.ResourceCondition:
        //        currentConditionProperty.objectReferenceValue = new GameplayResourceCondition();
        //        break;
        //    case ConditionType.SpecialResourceCondition:
        //        currentConditionProperty.objectReferenceValue = new SpecialResourceCondition();
        //        break;
        //    case ConditionType.ConditionList:
        //        //currentConditionProperty.objectReferenceValue = new ConditionList();
        //        break;
        //    default:
        //        break;
        //}

        //currentConditionProperty.serializedObject.ApplyModifiedProperties();
    }
}
