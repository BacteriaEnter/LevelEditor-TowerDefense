using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    Waypoint waypoint;

    public SavedWaveDetail waveDetail;
    public List<Enemy> enemies=new List<Enemy>();
    int spawnCount;
    int currentBoidsIndex;
    WaitForSeconds waitTimeBetweenUnitSpawns;
    WaitForSeconds waitTimeBetweenBoid;
    WaitForSeconds waitTimeBetweenWave;

    public int spawnPointIndex;

    private void Awake()
    {
        waypoint=GetComponent<Waypoint>();
    }

    private void Start()
    {
        if (!EnemyManager.Instance.spawnPoints.Contains(this))
        {
            EnemyManager.Instance.spawnPoints.Add(this);
        }

        GetWaveDetailFromEnemyManager();
        //UpdateWaitTime_UnitSpawn();
        //StartCoroutine(nameof(EnemySpawnCoroutine));
        UpdateWaitTime_WaveBetween();
        StartCoroutine(nameof(WaitNextWaveCoroutine));
        
    }

    IEnumerator EnemySpawnCoroutine()
    {
        var boid = waveDetail.boids[currentBoidsIndex];
        while (spawnCount < boid.enemyAmount)
        {
            yield return waitTimeBetweenUnitSpawns;
            SpawnEnemy(boid);
            spawnCount++;
        }
        spawnCount = 0;
        currentBoidsIndex++;
        //请求下一个Boid
        if (currentBoidsIndex <= waveDetail.boids.Count-1)
        {
            UpdateWaitTime_BoidBetween();
            UpdateWaitTime_UnitSpawn();
            StopCoroutine(nameof(EnemySpawnCoroutine));
            StartCoroutine(nameof(WaitNextBoidCoroutine));
        }
        else
        {
            currentBoidsIndex=0;
            //请求下一个Wave
            if (GetWaveDetailFromEnemyManager())
            {
                StopCoroutine(nameof(EnemySpawnCoroutine));
                UpdateWaitTime_WaveBetween();
                StartCoroutine(nameof(WaitNextWaveCoroutine));
            }
            else
            {
                StopCoroutine(nameof(EnemySpawnCoroutine));
            }

        }

    }

    IEnumerator WaveLagging()
    {
        yield return waitTimeBetweenWave;
    }
    IEnumerator WaitNextWaveCoroutine()
    {
        yield return waitTimeBetweenWave;
        UpdateWaitTime_UnitSpawn();
        StartCoroutine(nameof(EnemySpawnCoroutine));
        StopCoroutine(nameof(WaitNextWaveCoroutine));
    }


    IEnumerator WaitNextBoidCoroutine()
    {
        yield return waitTimeBetweenBoid;
        StartCoroutine(nameof(EnemySpawnCoroutine));
    }

    void SpawnEnemy(Boid boid)
    {
        Enemy unit = PoolManager.Release(boid.enemy).GetComponent<Enemy>();
        unit.Init(boid.movePreference, waypoint,this, boid.subwaypointIndex);
        enemies.Add(unit);
    }

    public void RemoveFromEnemiesList(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }

    /// <summary>
    /// 从敌人管理器处获取
    /// </summary>
    /// <returns>返回值决定当前关卡是否还剩下波次</returns>
    bool GetWaveDetailFromEnemyManager()
    {
        waveDetail = EnemyManager.Instance.GetWaveDetailFromEnemyManager();
        if (waveDetail != null)
            return true;
        else 
            return false;
    }

    void UpdateWaitTime_UnitSpawn()
    {
        waitTimeBetweenUnitSpawns = new WaitForSeconds(waveDetail.boids[currentBoidsIndex].timeBetweenUnit);
    }

    void UpdateWaitTime_BoidBetween()
    {
        waitTimeBetweenBoid = new WaitForSeconds(waveDetail.boids[currentBoidsIndex].timeBetweenBoid);
    }

    void UpdateWaitTime_WaveBetween()
    {
        waitTimeBetweenWave = new WaitForSeconds(waveDetail.timeBetweenWaveStart);
    }

    public int GetIndex(Enemy enemy)
    {
       return enemies.IndexOf(enemy);
    }
}
