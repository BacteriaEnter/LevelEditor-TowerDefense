using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPointManager : MonoBehaviour
{
    public List<LevelPoint> LevelPoints = new List<LevelPoint>();
    public Dictionary<int, LevelPoint> LevelPointsDict = new Dictionary<int, LevelPoint>();
    private void Awake()
    {
        InitDic();
    }

    void InitDic()
    {
        foreach (var point in LevelPoints)
        {
            if (!LevelPointsDict.ContainsKey(point.levelIndex))
            {
                LevelPointsDict.Add(point.levelIndex, point);
            }
        }
    }

    public void InitLevelStatus(int levelIndex,bool isLocked,int starCount)
    {
        foreach (var point in LevelPoints)
        {
            if (point.levelIndex == levelIndex)
            {
                Debug.Log(levelIndex + "+" + isLocked);
                point.InitPoint(isLocked, starCount);
            }
        }
    }
}
