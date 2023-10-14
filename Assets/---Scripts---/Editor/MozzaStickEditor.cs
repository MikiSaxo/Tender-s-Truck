using System;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditor(typeof(MozzaStick))]
public class MozzaStickEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MozzaStick circleFormation = (MozzaStick)target;

        if (GUILayout.Button("Generate Points"))
        {
            circleFormation.SpawnPoints();
        }
        if (GUILayout.Button("Reset Points"))
        {
            circleFormation.ResetPoints();
        }
        if (GUILayout.Button("Change Rotation"))
        {
            circleFormation.ChangeRotation();
        }
    }
}
