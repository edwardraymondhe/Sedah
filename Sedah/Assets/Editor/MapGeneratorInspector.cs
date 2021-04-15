using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// It's a part of the Inspector for MapGenerator Editor in Unity
[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorInspector : Editor
{
    MapGenerator map;
    private void OnEnable() {
        map = (MapGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(Application.isPlaying)
        {
            if(GUILayout.Button("Initialization"))
            {
                map.Initialization();
            }

            if(GUILayout.Button("Repair map"))
            {
                map.TryRepair();
            }
        }
    }
}
