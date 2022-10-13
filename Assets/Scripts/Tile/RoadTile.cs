using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTile : Block
{
    public bool isEnemyOnThisTile;
    public List<EnemyManager> enemiesOnThisTile;
    public void EnemyEnterThisTile(EnemyManager enemy)
    {
        if (!enemiesOnThisTile.Contains(enemy))
        {
            enemiesOnThisTile.Add(enemy);
        }
    }

    public void EnemyLeaveThisTile(EnemyManager enemy)
    {
        if (!enemiesOnThisTile.Contains(enemy))
        {
            enemiesOnThisTile.Remove(enemy);
        }
    }
}
