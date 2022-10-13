using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WayPointManagerWindow : EditorWindow
{
    [MenuItem("Tools/Waypoint Editor")]
    public static void Open()
    {
        GetWindow<WayPointManagerWindow>();
    }
    public Transform wayPointRoot;//路径点父节点
    public int branchCount = 0;
    public int MergePointCount = 0;
    public int branchReceiver = 1;
    public int MergePointReceiver = 1;
    public List<int> receiverCountList=new List<int>();
    public List<int> MergePointCountList = new List<int>();
    string helpBoxContent;//提示信息
    MessageType messageType = MessageType.None;//提示信息类型


    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);
        EditorGUILayout.PropertyField(obj.FindProperty("wayPointRoot"));
        EditorGUILayout.PropertyField(obj.FindProperty("branchCount"));
        EditorGUILayout.PropertyField(obj.FindProperty("MergePointCount"));
        EditorGUILayout.PropertyField(obj.FindProperty("branchReceiver"));
        EditorGUILayout.PropertyField(obj.FindProperty("MergePointReceiver"));
        EditorGUILayout.PropertyField(obj.FindProperty("receiverCountList"));
        EditorGUILayout.PropertyField(obj.FindProperty("MergePointCountList"));


        if (wayPointRoot == null)
        {
            EditorGUILayout.HelpBox("必须选择一个根节点", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
            EditorGUILayout.HelpBox(helpBoxContent, messageType);
        }
        obj.ApplyModifiedProperties();
    }

    private void DrawButtons()
    {
        if (GUILayout.Button("Connect To PrevWaypoint"))
        {
            ConnectToPrevWaypoint();
        }
        else if (GUILayout.Button("Create Branch"))
        {
            CreateBranch();
        }

        else if (GUILayout.Button("Create Branch Receiver"))
        {
            CreateBranchReceiver();
        }
        else if (GUILayout.Button("Connect All Receiver To Branch"))
        {
            ConnectAllReceiverToBranch();
        }
        else if (GUILayout.Button("Create Merge Point"))
        {
            CreateMergePoint();
        }
        else if (GUILayout.Button("Create Merge Point Receiver"))
        {
            CreateMergePointReceiver();
        }
        else if (GUILayout.Button("Connect All Receiver To MergePoint"))
        {
            ConnectAllReceiverToMergePoint();
        }
        else if (GUILayout.Button("Clear Branch Count"))
        {
            ClearBranchCount();
        }
        else if (GUILayout.Button("Clear Merge Point Count"))
        {
            ClearMergePointCount();
        }
        else if (GUILayout.Button("Connect Subwaypoint To Waypoints"))
        {
            ConnectSubwaypointToWaypoints();
        }
    }

    private void ConnectAllReceiverToBranch()
    {
        receiverCountList.Add(branchReceiver);
        for (int i = 1; i <= branchCount; i++)
        {
            string branchName = string.Format("WayPoint_Branch_{0}", i);
            var branch = wayPointRoot.Find(branchName).GetComponent<Waypoint>();
            for (int j = 1; j < receiverCountList[i-1]; j++)
            {
                string receiverName = string.Format("WayPoint_Branch_{0}_Receiver_{1}", i, j);
                var receiver = wayPointRoot.Find(receiverName).GetComponent<Waypoint>();
                branch.nextWayPoints.Add(receiver);
                receiver.prevWayPoints.Add(branch);
            }
        }


    }

    private void ConnectAllReceiverToMergePoint()
    {
        MergePointCountList.Add(MergePointReceiver);
        for (int i = 1; i <= MergePointCount; i++)
        {
            string MergePoinName = string.Format("WayPoint_MergePoint_{0}", i);
            var MergePoin = wayPointRoot.Find(MergePoinName).GetComponent<Waypoint>();
            for (int j = 1; j < MergePointCountList[i - 1]; j++)
            {
                string receiverName = string.Format("WayPoint_MergePoint_{0}_Receiver_{1}", i, j);
                var receiver = wayPointRoot.Find(receiverName).GetComponent<Waypoint>();
                MergePoin.prevWayPoints.Add(receiver);
                receiver.nextWayPoints.Add(MergePoin);
            }
        }


    }

    private void ConnectToPrevWaypoint()
    {
        GameObject waypointObject = wayPointRoot.GetChild(wayPointRoot.childCount - 1).gameObject;
        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        if (wayPointRoot.childCount >1)
        {
            waypoint.prevWayPoints.Add( wayPointRoot.GetChild(wayPointRoot.childCount - 2).GetComponent<Waypoint>());
            waypoint.prevWayPoints[0].nextWayPoints.Add(waypoint) ;
        }
        ResetHelpBox();
        Selection.activeGameObject = waypointObject;
    }
    /// <summary>
    /// 确立分支节点
    /// </summary>
    private void CreateBranch()
    {
        var select = Selection.activeGameObject;
        if (select != null&& select.transform.IsChildOf(wayPointRoot))
        {
            branchCount++;
            select.name = string.Format("WayPoint_Branch_{0}", branchCount);
            branchReceiver = 1;
            if (branchCount>1)
            {
                receiverCountList.Add(branchReceiver);
            }
            ResetHelpBox();
        }
        else
        {
            helpBoxContent = "请选择WaypointRoot下的一个子节点";
            messageType = MessageType.Warning;
        }
        ClearBranchCount();
    }

    private void CreateMergePoint()
    {
        var select = Selection.activeGameObject;
        if (select != null && select.transform.IsChildOf(wayPointRoot))
        {
            MergePointCount++;
            select.name = string.Format("WayPoint_MergePoint_{0}", MergePointCount);
            MergePointReceiver = 1;
            if (MergePointCount > 1)
            {
                MergePointCountList.Add(MergePointReceiver);
            }
            ResetHelpBox();
        }
        else
        {
            helpBoxContent = "请选择WaypointRoot下的一个子节点";
            messageType = MessageType.Warning;
        }
        ClearBranchCount();
    }

    private void CreateMergePointReceiver()
    {
        var select = Selection.activeGameObject;
        if (select != null && select.transform.IsChildOf(wayPointRoot))
        {
            select.name = string.Format("WayPoint_MergePoint_{0}_Receiver_{1}", MergePointCount, MergePointReceiver);
            MergePointReceiver++;
            ResetHelpBox();
        }
        else
        {
            helpBoxContent = "请选择WaypointRoot下的一个子节点";
            messageType = MessageType.Warning;
        }
    }
    /// <summary>
    /// 创建分支的接收节点
    /// </summary>
    private void CreateBranchReceiver()
    {
        var select = Selection.activeGameObject;
        if (select != null && select.transform.IsChildOf(wayPointRoot))
        {
            select.name = string.Format("WayPoint_Branch_{0}_Receiver_{1}", branchCount,branchReceiver);
            branchReceiver++;
            ResetHelpBox();
        }
        else
        {
            helpBoxContent = "请选择WaypointRoot下的一个子节点";
            messageType = MessageType.Warning;
        }
    }

    /// <summary>
    /// <para>重置分支计数</para>
    /// <para>重新绘制地图时使用</para>
    /// </summary>
    private void ClearBranchCount()
    {
        branchCount = 1;
        branchReceiver = 1;
        helpBoxContent = "已重置合并点计数";
        messageType = MessageType.Info;
    }

    private void ClearMergePointCount()
    {
        MergePointCount = 0;
        MergePointReceiver = 1;
        helpBoxContent = "已重置分支计数";
        messageType = MessageType.Info;
    }

    /// <summary>
    /// 重置HelpBox信息
    /// </summary>
    private void ResetHelpBox()
    {
        helpBoxContent = "";
        messageType = MessageType.None;
    }

    private void ConnectSubwaypointToWaypoints()
    {
        SubWaypoint[] subWaypoints=FindObjectsOfType<SubWaypoint>();
        Waypoint[] waypoints= wayPointRoot.GetComponentsInChildren<Waypoint>();
        foreach (SubWaypoint subway in subWaypoints)
        {
            Vector3 possiblePos1 = new Vector3(subway.transform.position.x - 0.5f, subway.transform.position.y - 0.5f);
            Vector3 possiblePos2 = new Vector3(subway.transform.position.x + 0.5f, subway.transform.position.y - 0.5f);
            Vector3 possiblePos3 = new Vector3(subway.transform.position.x + 0.5f, subway.transform.position.y + 0.5f);
            Vector3 possiblePos4 = new Vector3(subway.transform.position.x - 0.5f, subway.transform.position.y + 0.5f);
            bool isSet = false;
            foreach (var item in waypoints)
            {
                if (item.transform.position == possiblePos1)
                {
                    item.AddSubwaypointInEditor(spOrientation.TopRight,subway.Index, subway);
                    subway.transform.SetParent(item.transform);
                    isSet = true;
                    break;
                }
                else if (item.transform.position == possiblePos2)
                {
                    item.AddSubwaypointInEditor(spOrientation.TopLeft, subway.Index, subway);
                    subway.transform.SetParent(item.transform);
                    isSet = true;
                    break;
                }
                else if (item.transform.position == possiblePos3)
                {
                    item.AddSubwaypointInEditor(spOrientation.BottomLeft, subway.Index, subway);
                    subway.transform.SetParent(item.transform);
                    isSet = true;
                    break;
                }
                else if (item.transform.position == possiblePos4)
                {
                    item.AddSubwaypointInEditor(spOrientation.BottomRight, subway.Index, subway);
                    subway.transform.SetParent(item.transform);
                    isSet = true;
                    break;
                }
            
            }
#if UNITY_EDITOR
            if (!isSet)
            {
                Debug.LogError($"Can't find nearby waypoint Position:{subway.transform.position},Index:{subway.Index}");
            }
#endif
        }
    }
}
