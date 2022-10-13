using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(TilemapManager))]
public class TilemapManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var script = (TilemapManager)target;

        if (GUILayout.Button("Save Map"))
        {
            script.SaveMap();
        }

        if (GUILayout.Button("Clear Map"))
        {
            script.ClearMap();
        }

        if (GUILayout.Button("Load Map"))
        {
            script.LoadMap();
        }

        if (GUILayout.Button("Save Blocks"))
        {
            script.SaveBlocks();
        }

        if (GUILayout.Button("Clear Blocks"))
        {
            script.ClearBlocks();
        }

        if (GUILayout.Button("Load Blocks"))
        {
            script.LoadBlocks();
        }

        if (GUILayout.Button("Save WayPpoints"))
        {
            script.SaveWaypoints();
        }

        if (GUILayout.Button("Clear WayPpoints"))
        {
            script.ClearWaypoints();
        }
        if (GUILayout.Button("Load WayPpoints"))
        {
            script.LoadWaypoints();
        }
    }
}
