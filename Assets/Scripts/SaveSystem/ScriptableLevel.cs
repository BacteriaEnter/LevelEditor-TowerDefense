using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScriptableLevel : ScriptableObject
{
    public int LevelIndex;
    public List<SavedTile> LandTiles;
    public List<SavedBlock> Blocks;
    public List<SavedWayPoint> WayPoints;
    public List<SavedWaveDetail> WaveDetails;
}
[Serializable]
public class SavedTile
{
    public Vector3Int Position;
    public Tile tile;
}
[Serializable]
public class SavedBlock
{
    public Vector3 Position;
    public bool isPlaceable;
    public PlaceableType PlaceType;
    public GameObject blockPrefab;
}
[Serializable]
public class SavedWayPoint
{
    public Vector3 Position;
    public List<int> prevWayPointIndex_List;
    public List<int> nextWayPointIndex_List;
    public List<spOrientation> orient_subWaypoints;
    public List<int> index_subWaypoints;
    public List<bool> isTransferPoint;
    public List<int> transferDestination;
    public bool multiBranch;
    public bool isEnd;
    public bool isSpawnPoint;
}
[Serializable]
public class SavedWaveDetail
{
    public float timeBetweenWaveStart;
    public int spawnPointIndex;
    public List<Boid> boids;
}

[Serializable]
public class Boid
{
    public GameObject enemy;
    public int enemyAmount;
    public int movePreference;
    public float timeBetweenUnit;
    public float timeBetweenBoid;
    [Range(0, 4)]
    public int subwaypointIndex;//0为随机选取子路径点
}

