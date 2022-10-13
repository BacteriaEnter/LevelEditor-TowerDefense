using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> nextWayPoints = new List<Waypoint>();
    public List<Waypoint> prevWayPoints = new List<Waypoint>();
    [Header("标识")]
    public bool multiBranch;//多分支
    public bool isEnd;
    public bool isSpawnPoint;
    public Dictionary<spOrientation, int> dic_subWaypoints= new Dictionary<spOrientation, int>();
    public Dictionary<int, SubWaypoint> dic_subWaypointsIndex= new Dictionary<int, SubWaypoint>();//副路径点与index相连的词典

    [SerializeField] List<int> index_subWaypoints = new List<int>();
    [SerializeField] List<SubWaypoint> subWaypoints = new List<SubWaypoint>();

    public void Init(Vector3 position,
        List<int> prevIndexs,
        List<int> nextIndexs,
        bool multiBranch,
        bool isEnd,
        bool isSpawnPoint,
        List<Waypoint>waypoints)
    {
        transform.position = position;
        if (prevIndexs!=null)
        {
            foreach (var index in prevIndexs)
            {
                prevWayPoints.Add(waypoints[index]);
            }
        }
        if (nextIndexs!=null)
        {
            foreach (var index in nextIndexs)
            {
                nextWayPoints.Add(waypoints[index]);
            }

        }
        this.multiBranch = multiBranch;
        this.isEnd = isEnd;
        this.isSpawnPoint = isSpawnPoint;
    }

    private void Awake()
    {
#if UNITY_EDITOR
        InitDic_subWaypointsIndex();
#endif
    }

    private void Start()
    {
        if (isSpawnPoint)
        {

            gameObject.AddComponent<SpawnPoint>();
        }
    }
 

    public void AddSubwaypointInEditor(spOrientation orientation,int index, SubWaypoint subWaypoint)
    {

        if (!dic_subWaypoints.ContainsKey(orientation))
        {
            dic_subWaypoints.Add(orientation, index);
            dic_subWaypointsIndex.Add(index, subWaypoint);
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError($"This subwaypoint already exsist index: {index}");
#endif
        }
    }

    public void AddSubwaypoint(spOrientation orientation,int  index, SubWaypoint subWaypoint)
    {
        if (!index_subWaypoints.Contains(index))
        {
            index_subWaypoints.Add(index);
            subWaypoints.Add(subWaypoint);
            subWaypoint.transform.position=SetPositionByOrientation(orientation);
        }
        else 
        {
#if UNITY_EDITOR
            Debug.LogError($"This subwaypoint already exsist index: {index}");
#endif
        }
    }

    private Vector3 SetPositionByOrientation(spOrientation orientation)
    {
        if (orientation == spOrientation.TopRight)
        {
            return new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f);
        }
        else if (orientation == spOrientation.TopLeft)
        {
            return new Vector3(transform.position.x - 0.5f, transform.position.y + 0.5f);
        }
        else if (orientation == spOrientation.BottomLeft)
        {
            return new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f);
        }
        else 
        {
            return new Vector3(transform.position.x + 0.5f, transform.position.y - 0.5f);
        }

    }

    public Vector3 GetDestination(int index,bool isGiant)
    {
        if (isGiant)
        {
            return transform.position;
        }
        Vector3 destination = Vector3.zero;

        if (dic_subWaypointsIndex.TryGetValue(index,out SubWaypoint subwaypoint))
        {
            destination = subwaypoint.transform.position;
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError($"Wrong index:{index}");
#endif
        }
        return destination;
    }

    public void InitDic_subWaypointsIndex()
    {
        for (int i = 0; i < subWaypoints.Count; i++)
        {
            if (!dic_subWaypointsIndex.ContainsKey(index_subWaypoints[i]))
            {
                dic_subWaypointsIndex.Add(index_subWaypoints[i], subWaypoints[i]);
            }       
        }
    }
}
