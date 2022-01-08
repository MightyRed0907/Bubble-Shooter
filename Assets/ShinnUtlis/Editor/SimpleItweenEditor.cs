using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using Shinn;

[CustomEditor(typeof(SimpleItween))]
public class SimpleItweenEditor : Editor {
    
    SimpleItween script;
    bool showItweenSettings = true;

    public override void OnInspectorGUI()
    {
        script = (SimpleItween)target;

        EditorGUILayout.Space();
        showItweenSettings = EditorGUILayout.Foldout(showItweenSettings, "Itween");
        if (showItweenSettings)
        {
           
            EditorGUILayout.LabelField("General setting.");

            EditorGUILayout.Space();
            script.target = (GameObject)EditorGUILayout.ObjectField("Target", script.target, typeof(GameObject), true);

            EditorGUILayout.Space();
            script.time = EditorGUILayout.FloatField("Time", script.time);
            script.delay = EditorGUILayout.Slider("Delay", script.delay, 0, 60);

            EditorGUILayout.Space();
            script.ease = (iTween.EaseType)EditorGUILayout.EnumPopup("EaseType", script.ease);
            script.loop = (iTween.LoopType)EditorGUILayout.EnumPopup("LoopType", script.loop);

            EditorGUILayout.Space();
            script.islocal = EditorGUILayout.Toggle("Is local", script.islocal);
            script.ignoreTimeScalest = EditorGUILayout.Toggle("Ignore time scale", script.ignoreTimeScalest);

            EditorGUILayout.Space();
            script.orienttopathst = EditorGUILayout.Toggle("Orien to path", script.orienttopathst);
            script.lookaheadValue = EditorGUILayout.Slider("Look ahead value", script.lookaheadValue, 0, 1);

            EditorGUILayout.Space();
            script.AutoStart = EditorGUILayout.Toggle("Auto start", script.AutoStart);

            EditorGUILayout.Space();
            script.startComplete = EditorGUILayout.Toggle("Complete event", script.startComplete);

            if (script.startComplete)
            {

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("-----------------------");

                script.EnableVoid = EditorGUILayout.Toggle("void events", script.EnableVoid);
                script.EnableBool = EditorGUILayout.Toggle("bool events", script.EnableBool);
                script.EnableInt = EditorGUILayout.Toggle("int events", script.EnableInt);
                script.EnableFloat = EditorGUILayout.Toggle("float events", script.EnableFloat);
                script.EnableFloatArray = EditorGUILayout.Toggle("float array events", script.EnableFloatArray);
                script.EnableVector3 = EditorGUILayout.Toggle("vector3 events", script.EnableVector3);
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

                if (script.EnableVector3)
                {
                    EditorGUILayout.Space();
                    SerializedProperty onCheck = serializedObject.FindProperty("vector3events");
                    EditorGUILayout.PropertyField(onCheck);

                    SerializedProperty property = serializedObject.FindProperty("vector3value");
                    EditorGUILayout.PropertyField(property, new GUIContent("Input vector3 value"), true);

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

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("-----------------------");
        script.mystate = (SimpleItween.state)EditorGUILayout.EnumPopup("Simple Itween Fuction", script.mystate);

        EditorGUILayout.Space();
        SelectType();


    }
  

    void SelectType()
    {
        switch (script.mystate)
        {

            case SimpleItween.state.colorTo:
                script.endColor = EditorGUILayout.ColorField("EndColor", script.endColor);
                break;

            case SimpleItween.state.moveTo:
                script.moveloc = (Transform) EditorGUILayout.ObjectField("Target loc", script.moveloc, typeof(Transform), true);
                break;

            case SimpleItween.state.rotationTo:
                script.rotvalue = EditorGUILayout.Vector3Field("Euler angles", script.rotvalue);
                break;

            case SimpleItween.state.scaleTo:
                script.scaleValue = EditorGUILayout.Vector3Field("Scale value", script.scaleValue);
                break;

            case SimpleItween.state.shakePosition:
                script.shakePos = EditorGUILayout.Vector3Field("Shake value", script.shakePos);
                break;

            case SimpleItween.state.punchPosition:
                script.punchPos = EditorGUILayout.Vector3Field("Punch value", script.punchPos);
                break;

            case SimpleItween.state.SP_fadeTo:
                script.fadeStart = EditorGUILayout.Slider("Sprite fade start", script.fadeStart, 0, 1);
                script.fadeEnd = EditorGUILayout.Slider("Sprite fade end", script.fadeEnd, 0, 1);
                break;

            case SimpleItween.state.rotationToAndMoveTo:
                script.moveloc = (Transform)EditorGUILayout.ObjectField("Target loc", script.moveloc, typeof(Transform), true);
                break;

        }
    }

}
