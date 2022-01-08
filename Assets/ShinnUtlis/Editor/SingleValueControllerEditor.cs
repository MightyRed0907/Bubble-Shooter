using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using Shinn;

[CustomEditor(typeof(SingleValueController))]
public class SingleValueControllerEditor : Editor
{
    SingleValueController script;
    bool showItweenSettings = true;

    public override void OnInspectorGUI()
    {

        script = (SingleValueController)target;

        EditorGUILayout.Space();
        SerializedProperty onCheck = serializedObject.FindProperty("floatevents");
        EditorGUILayout.PropertyField(onCheck);
        
        script.type = (SingleValueController.Type)EditorGUILayout.EnumPopup("Type", script.type);
        SelectType();

        if (GUI.changed)
            serializedObject.ApplyModifiedProperties();
    }

    void SelectType()
    {
        switch (script.type)
        {
            default:
                itweenFuct();
                break;

            case SingleValueController.Type.Itween:
                itweenFuct();
                break;

            case SingleValueController.Type.PerlinNoise:
                perlinNoiseFuct();
                break;
        }

    }


    void perlinNoiseFuct()
    {
        showItweenSettings = EditorGUILayout.Foldout(showItweenSettings, "Perlin Noise");
        if (showItweenSettings)
        {
            EditorGUILayout.Space();
            script.basevalue = EditorGUILayout.FloatField("Base", script.basevalue);
            script.intensity = EditorGUILayout.FloatField("Intensity", script.intensity);
            script.noiseSpeed = EditorGUILayout.FloatField("Speed", script.noiseSpeed);
            script.startRand = EditorGUILayout.Toggle("Start RandomSeed", script.startRand);

            if (!script.startRand)
                script.randomseed = EditorGUILayout.Slider("RandomSeed", script.randomseed, 0, 1);

            script.stopTime = EditorGUILayout.FloatField("Stop time", script.stopTime);
        }
    }

    void itweenFuct()
    {
        showItweenSettings = EditorGUILayout.Foldout(showItweenSettings, "Itween");
        if (showItweenSettings)
        {
            EditorGUILayout.Space();

            script.valuerange = EditorGUILayout.Vector2Field("Range", script.valuerange);
            script.autoStart = EditorGUILayout.Toggle("AutoStart", script.autoStart);
            script.target = (GameObject)EditorGUILayout.ObjectField("Target", script.target, typeof(GameObject), true);
            script.time = EditorGUILayout.FloatField("Time", script.time);
            script.delay = EditorGUILayout.Slider("Delay", script.delay, 0, 60);
            script.ease = (iTween.EaseType)EditorGUILayout.EnumPopup("EaseType", script.ease);
            script.loop = (iTween.LoopType)EditorGUILayout.EnumPopup("LoopType", script.loop);
            script.ignoreTimeScalest = EditorGUILayout.Toggle("Ignore time scale", script.ignoreTimeScalest);

            EditorGUILayout.Space();
            script.complete = EditorGUILayout.Toggle("Complete event", script.complete);


            if (script.complete)
            {
                SerializedProperty onCheck2 = serializedObject.FindProperty("unityevent");
                EditorGUILayout.PropertyField(onCheck2);
            }

        }
    }
}
