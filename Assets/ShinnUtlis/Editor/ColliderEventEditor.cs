using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Shinn;

[CustomEditor(typeof(ColliderEvent))]
public class ColliderEventEditor : Editor {

    private ColliderEvent script;
    private bool eventfold = false;

    public override void OnInspectorGUI()
    {
        script = (ColliderEvent) target;

        EditorGUILayout.Space();
        script.mytype = (ColliderEvent.type)EditorGUILayout.EnumPopup("Collider type", script.mytype);
        script.tagname = EditorGUILayout.TextField("Tag name", script.tagname);
        SelectType();


        EditorGUILayout.Space();
        eventfold = EditorGUILayout.Foldout(eventfold, "Events");
        if (eventfold)
        {

            EditorGUILayout.LabelField("-----------------------");

            script.EnableVoid = EditorGUILayout.Toggle("void events", script.EnableVoid);
            script.EnableBool = EditorGUILayout.Toggle("Bool events", script.EnableBool);
            script.EnableInt = EditorGUILayout.Toggle("int events", script.EnableInt);
            script.EnableFloat = EditorGUILayout.Toggle("float events", script.EnableFloat);
            script.EnableFloatArray = EditorGUILayout.Toggle("float array events", script.EnableFloatArray);
            script.EnableColor = EditorGUILayout.Toggle("color events", script.EnableColor);

            if (script.EnableBool)
            {
                SerializedProperty onCheck = serializedObject.FindProperty("boolevents");
                EditorGUILayout.PropertyField(onCheck);
                script.boolvalue = EditorGUILayout.Toggle("Input bool value", script.boolvalue);
            }

            if (script.EnableInt)
            {
                EditorGUILayout.Space();
                SerializedProperty onCheck = serializedObject.FindProperty("intevents");
                EditorGUILayout.PropertyField(onCheck);
                script.intvalue = EditorGUILayout.IntField("Input int value", script.intvalue);
            }

            if (script.EnableFloat)
            {
                EditorGUILayout.Space();
                SerializedProperty onCheck = serializedObject.FindProperty("floatevents");
                EditorGUILayout.PropertyField(onCheck);
                script.floatvalue = EditorGUILayout.FloatField("Input float value", script.floatvalue);
            }

            if (script.EnableFloatArray)
            {
                EditorGUILayout.Space();
                SerializedProperty onCheck = serializedObject.FindProperty("floatarratevents");
                EditorGUILayout.PropertyField(onCheck);

                SerializedProperty property = serializedObject.FindProperty("floatarrayvalue");
                EditorGUILayout.PropertyField(property, new GUIContent("Input floatarray value"), true);

            }

            if (script.EnableColor)
            {
                EditorGUILayout.Space();
                SerializedProperty onCheck = serializedObject.FindProperty("colorevents");
                EditorGUILayout.PropertyField(onCheck);
                script.colorvalue = EditorGUILayout.ColorField("Input color value", script.colorvalue);
            }

            if (script.EnableVoid)
            {
                EditorGUILayout.Space();
                SerializedProperty onCheck = serializedObject.FindProperty("voidevents");
                EditorGUILayout.PropertyField(onCheck);
            }

            if (GUI.changed)
                serializedObject.ApplyModifiedProperties();
        }

    }

    private void SelectType()
    {
        switch (script.mytype)
        {
            default:
                break;

            case ColliderEvent.type.OnTriggerEnter:
                script.mytype = ColliderEvent.type.OnTriggerEnter;
                break;
            case ColliderEvent.type.OnTriggerExit:
                script.mytype = ColliderEvent.type.OnTriggerExit;
                break;
            case ColliderEvent.type.OnTriggerStay:
                script.mytype = ColliderEvent.type.OnTriggerStay;
                break;


            case ColliderEvent.type.OnCollisionEnter:
                script.mytype = ColliderEvent.type.OnCollisionEnter;
                break;
            case ColliderEvent.type.OnCollisionEXit:
                script.mytype = ColliderEvent.type.OnCollisionEXit;
                break;
            case ColliderEvent.type.OnCollisionStay:
                script.mytype = ColliderEvent.type.OnCollisionStay;
                break;
        }
    }
}
