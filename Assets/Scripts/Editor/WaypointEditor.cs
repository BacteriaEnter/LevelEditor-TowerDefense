using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[InitializeOnLoad()]
public class WaypointEditor 
{
    [DrawGizmo(GizmoType.NonSelected|GizmoType.Selected|GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
    {
        if ((gizmoType & GizmoType.Selected)!=0)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.yellow * 0.5f;
        }
        Gizmos.DrawSphere(waypoint.transform.position, .1f);
        Gizmos.color = Color.white;
        if (waypoint.prevWayPoints != null)
        {
            Gizmos.color = Color.red;
            foreach (var prevWayPoint in waypoint.prevWayPoints)
            {
                Gizmos.DrawLine(waypoint.transform.position, prevWayPoint.transform.position);
            }

        }

    }
}
