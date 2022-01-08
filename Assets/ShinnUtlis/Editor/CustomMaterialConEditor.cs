using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Shinn;

[CustomEditor(typeof(CustomMaterialCon))]
public class CustomMaterialConEditor : Editor {

    private CustomMaterialCon script;

    public override void OnInspectorGUI()
    {
        script = (CustomMaterialCon)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Custom value for material.");

        EditorGUILayout.Space();


        EditorGUIUtility.LookLikeInspector();
        SerializedProperty tps = serializedObject.FindProperty("render");
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(tps, true);

        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
        
        script.priority = EditorGUILayout.TextField("Property", script.priority);
        script.autoStart = EditorGUILayout.Toggle("Autostart", script.autoStart);        
        script.time = EditorGUILayout.FloatField("Time", script.time);
        script.delay = EditorGUILayout.Slider("Delay", script.delay, 0, 10);
        script.ease = (iTween.EaseType)EditorGUILayout.EnumPopup("EaseType", script.ease);
        script.loop = (iTween.LoopType)EditorGUILayout.EnumPopup("LoopType", script.loop);
        script.ignoreTimeScalest = EditorGUILayout.Toggle("Ignore time scale", script.ignoreTimeScalest);
        

        EditorGUILayout.Space();
        script.complete = EditorGUILayout.Toggle("Complete event", script.complete);


        if (script.complete)
        {
            SerializedProperty onCheck = serializedObject.FindProperty("unityevent");
            EditorGUIUtility.LookLikeControls();
            EditorGUILayout.PropertyField(onCheck);

            if (GUI.changed)
                serializedObject.ApplyModifiedProperties();
        }


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("-----------------------");
        
        script.type = (CustomMaterialCon.state)EditorGUILayout.EnumPopup("Simple Itween Fuction", script.type);
        SelectType();
    }

    private void SelectType()
    {
        switch (script.type)
        {
            default:
                break;

            case CustomMaterialCon.state.ColorControl:
                script.startColor = EditorGUILayout.ColorField("Start color", script.startColor);
                script.endColor = EditorGUILayout.ColorField("End color", script.endColor);
                break;


            case CustomMaterialCon.state.ValueControl:
                script.range = EditorGUILayout.Vector2Field("Range", script.range);
                break;
        }
    }
}
