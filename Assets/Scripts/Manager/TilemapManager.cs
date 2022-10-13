using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    [SerializeField] private Tilemap _LandMap, _WaypointsMap;
    [SerializeField] private int _levelIndex;
    [SerializeField] PromptSO promptSO;

#if UNITY_EDITOR
    public void SaveMap()
    {
        var  newLevel = ScriptableObject.CreateInstance<ScriptableLevel>();

        newLevel.LevelIndex = _levelIndex;
        newLevel.name = $"Level{_levelIndex}";
        newLevel.LandTiles = GetTilesFromMap(_LandMap).ToList();

        ScriptableObjectUtility.SaveLevelFile(newLevel);

        IEnumerable<SavedTile> GetTilesFromMap(Tilemap map)
        {
            foreach (var pos in map.cellBounds.allPositionsWithin)
            {
                if (map.HasTile(pos))
                {
                    var levelTile = map.GetTile<Tile>(pos);
                    yield return new SavedTile()
                    {
                        Position = pos,
                        tile = levelTile
                    };
                }
            }
        }
    }

    public static class ScriptableObjectUtility
    {
        public static void SaveLevelFile(ScriptableLevel level)
        {
            AssetDatabase.CreateAsset(level, $"Assets/Resources/Levels/{level.name}.asset");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

#endif
    public void ClearMap()
    {
        //too rough need judge object wether u want
        var maps = FindObjectsOfType<Tilemap>();
        foreach (var tilemap in maps)
        {
            tilemap.ClearAllTiles();
        }
    }



    public void LoadMap()
    {
        var level = Resources.Load<ScriptableLevel>($"Levels/Level{_levelIndex}");
        if (level == null)
        {
            Debug.LogError($"Level {_levelIndex} does not exist.");
            return;
        }

        ClearMap();
        foreach (var savedTile in level.LandTiles)
        {
            SetTile(_LandMap, savedTile);
        }
 
    }



    void SetTile(Tilemap map, SavedTile tile)
    {
        map.SetTile(tile.Position, tile.tile);
    }

    public void SaveBlocks()
    {
        var level = Resources.Load<ScriptableLevel>($"Levels/Level{_levelIndex}");
        if (level == null)
        {
            Debug.LogError($"Level {_levelIndex} does not exist.");
            return;
        }

        level.Blocks = GetBlocksFromMap(_LandMap).ToList();

        IEnumerable<SavedBlock> GetBlocksFromMap(Tilemap map)
        {
            foreach (var block in map.GetComponentsInChildren<Block>())
            {
                yield return new SavedBlock
                {
                    Position = block.transform.position,
                    isPlaceable = block.isPlaceable,
                    PlaceType = block.PlaceableType,
                    blockPrefab = Resources.Load<GameObject>($"Prefabs/Blocks/{block.gameObject.name}")
                };
            }
        }
    }

    public void ClearBlocks()
    {
        var blocks = FindObjectsOfType<Block>();
        foreach (var block in blocks)
        {
            DestroyImmediate(block.gameObject);
        }
    }

    public void LoadBlocks()
    {
        var level = Resources.Load<ScriptableLevel>($"Levels/Level{_levelIndex}");
        if (level == null)
        {
            Debug.LogError($"Level {_levelIndex} does not exist.");
            return;
        }
        ClearBlocks();
        foreach (var savedBlock in level.Blocks)
        {
            var block = Instantiate(savedBlock.blockPrefab).GetComponent<Block>();
            block.transform.SetParent(_LandMap.transform);
            block.Init(savedBlock.Position, savedBlock.isPlaceable, savedBlock.PlaceType, savedBlock.blockPrefab.name,promptSO);
        }
    }


    public void SaveWaypoints()
    {
        var level = Resources.Load<ScriptableLevel>($"Levels/Level{_levelIndex}");
        if (level == null)
        {
            Debug.LogError($"Level {_levelIndex} does not exist.");
            return;
        }

        level.WayPoints = GetWaypointFromMap(_WaypointsMap).ToList();
#if UNITY_EDITOR
        Debug.Log("----路径信息保存成功----");
#endif
        IEnumerable<SavedWayPoint> GetWaypointFromMap(Tilemap map)
        {

            foreach (var waypoint in map.GetComponentsInChildren<Waypoint>())
            {
                List<int> prevResult=new List<int>();
                if (waypoint.prevWayPoints!=null)
                {
                    foreach (var item in waypoint.prevWayPoints)
                    {
                        prevResult.Add(item.transform.GetSiblingIndex());
                    }                
                }
                else
                {
                    prevResult = null;
                }
                List<int> nextResult = new List<int>();
                if (waypoint.nextWayPoints != null)
                {
                    foreach (var item in waypoint.nextWayPoints)
                    {
                        nextResult.Add(item.transform.GetSiblingIndex());
                    }
                }
                else
                {
                    nextResult = null;
                }

                List<spOrientation> orientation = new List<spOrientation>();
                List<int> index_subwaypoints = new List<int>();
                List<bool> isTransferPoints = new List<bool> ();
                List<int> transferDestinations = new List<int>();      
                foreach (var item in waypoint.dic_subWaypoints)
                {
                    if (waypoint.dic_subWaypoints!=null)
                    {
                        
                        orientation.Add(item.Key);
                        index_subwaypoints.Add(item.Value);
                        isTransferPoints.Add(waypoint.dic_subWaypointsIndex[item.Value].isTransferPoint);
                        transferDestinations.Add(waypoint.dic_subWaypointsIndex[item.Value].transferDestination);
                    }
                }
                yield return new SavedWayPoint
                {
                    Position = waypoint.transform.position,
                    prevWayPointIndex_List = prevResult,
                    nextWayPointIndex_List = nextResult,
                    orient_subWaypoints = orientation,
                    index_subWaypoints=index_subwaypoints,
                    isTransferPoint = isTransferPoints,
                    transferDestination = transferDestinations,
                    multiBranch= waypoint.multiBranch,
                    isEnd = waypoint.isEnd,
                    isSpawnPoint=waypoint.isSpawnPoint,
                };
            }
        }
    }

    public void ClearWaypoints()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (var waypoint in waypoints)
        {
            DestroyImmediate(waypoint.gameObject);
        }
    }

    public void LoadWaypoints()
    {
        var level = Resources.Load<ScriptableLevel>($"Levels/Level{_levelIndex}");
        if (level == null)
        {
            Debug.LogError($"Level {_levelIndex} does not exist.");
            return;
        }
        ClearWaypoints();
        GameObject prefab = Resources.Load<GameObject>($"Prefabs/Waypoint/Waypoint");
        GameObject subWaypoint = Resources.Load<GameObject>($"Prefabs/Waypoint/SubWaypoint");
        List<Waypoint> waypoints = new List<Waypoint>();
        var savedWaypoints = level.WayPoints;
        for (int i = 0; i < savedWaypoints.Count; i++)
        {
            var waypoint = Instantiate(prefab).GetComponent<Waypoint>();
            waypoint.transform.SetParent(_WaypointsMap.transform);
            waypoint.gameObject.name = prefab.name;
            waypoints.Add(waypoint);
        }

        for (int i = 0; i < savedWaypoints.Count; i++)
        {
            waypoints[i].Init(savedWaypoints[i].Position, savedWaypoints[i].prevWayPointIndex_List,
                savedWaypoints[i].nextWayPointIndex_List, savedWaypoints[i].multiBranch,
                savedWaypoints[i].isEnd, savedWaypoints[i].isSpawnPoint, waypoints);
            for (int j = 0; j < savedWaypoints[i].index_subWaypoints.Count; j++)
            {
                var subWaypointEntity = Instantiate(subWaypoint).GetComponent<SubWaypoint>();       
                subWaypointEntity.Init(savedWaypoints[i].index_subWaypoints[j], savedWaypoints[i].isTransferPoint[j], savedWaypoints[i].transferDestination[j]);
                waypoints[i].AddSubwaypoint(savedWaypoints[i].orient_subWaypoints[j], savedWaypoints[i].index_subWaypoints[j]
                    , subWaypointEntity);
                subWaypointEntity.transform.SetParent(waypoints[i].transform);
            }
            waypoints[i].InitDic_subWaypointsIndex();
        }

    }

    public void LoadLevel(int level)
    {
        _levelIndex=level;
        LoadMap();
        LoadBlocks();
        LoadWaypoints();
    }
}
