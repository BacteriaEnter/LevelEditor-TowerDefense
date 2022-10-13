using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Range(0, 3)]
    public int movePreference=1;
    public Vector2Int currentBlockIn;//当前所在方格
    public Vector2Int additionalBlockIn;//当前所在方格

    [SerializeField] Waypoint targetWayPoint;
    [SerializeField] SpawnPoint spawnPoint;
    [SerializeField] Vector3 destination;
    [SerializeField] int subwaypointIn;
    [SerializeField] bool isGiant;
    [SerializeField] bool isLast;
    public bool isDead;

    [Header("Status")]
    public float moveSpeed;
    public int currentHealth;
    public int currentAmmor;
    public int currentResistance;

    [Header("Effect List")]
    public List<Effect> persistentEffects = new List<Effect>();
    public Dictionary<Effect, float> dic_effects = new Dictionary<Effect, float>();
    public Dictionary<Effect, float> dic_effectTimer = new Dictionary<Effect, float>();

    public EnemySO enemySO;

    [SerializeField] EnemyHealthBar enemyHealthBar;

    private void OnEnable()
    {

    }

    public void Init(int movePreference,Waypoint waypoint,SpawnPoint spawnPoint,int subwaypointIndex)
    {
        SetsubwayIndex(waypoint, subwaypointIndex);
        transform.position = waypoint.GetDestination(subwaypointIn,isGiant);
        this.movePreference = movePreference;   
        targetWayPoint = ChooseWaypointByMovePreference(waypoint);
        destination = targetWayPoint.GetDestination(subwaypointIn, isGiant);
        this.spawnPoint = spawnPoint;
        InitStatus();
        enemyHealthBar.Init(currentHealth);
    }



    private void Update()
    {
        float delta = Time.deltaTime;
        Move();
        Vector2Int currentBlock=Vector2Int.CeilToInt(transform.position);

        if (currentBlock != currentBlockIn)
        {
            RemoveFromCurrentBlockIn();
            if (isGiant)
            {
                Vector2Int additionalBlock = Vector2Int.FloorToInt(transform.position);
                additionalBlockIn = additionalBlock;
            }
            currentBlockIn = currentBlock;
            AddToCurrentBlockIn();
        }
        HandleEffectByTime(delta);
    }

    void Move()
    {
        CheckDistance();
        if (targetWayPoint!=null)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        }
    }

    Waypoint ChooseWaypointByMovePreference(Waypoint waypoint)
    {
        if (waypoint.nextWayPoints.Count ==1)
        {
            return waypoint.nextWayPoints[0];
        }
        if (movePreference == 0)
        {
            return waypoint.nextWayPoints[0];
        }
        else if (movePreference == 1)
        {
            if (waypoint.nextWayPoints.Count==2)
            {
                return  waypoint.nextWayPoints[0];
            }
            else
            {
                return  waypoint.nextWayPoints[1];
            }
        }
        else if(movePreference == 2)
        {
            if (waypoint.nextWayPoints.Count == 2)
            {
                return  waypoint.nextWayPoints[waypoint.nextWayPoints.Count-1];
            }
            else
            {
                return  waypoint.nextWayPoints[1];
            }
        }
        else if (movePreference == 3)
        {
            return waypoint.nextWayPoints[waypoint.nextWayPoints.Count - 1];
        }
        return null;
    }

    void CheckDistance()
    {
        if (Vector2.Distance(transform.position, destination) <=0.1f)
        {
            transform.position = destination;
            if (targetWayPoint.isEnd)
            {
                HandleReachedGoal();
                return;
            }
            Waypoint currentWaypoint = targetWayPoint;
            if (currentWaypoint.dic_subWaypointsIndex[subwaypointIn].isTransferPoint)
            {
                subwaypointIn = currentWaypoint.dic_subWaypointsIndex[subwaypointIn].transferDestination;
            }
            targetWayPoint = ChooseWaypointByMovePreference(targetWayPoint);
            destination = targetWayPoint.GetDestination( subwaypointIn, isGiant);
        }
    }
    /// <summary>
    /// 抵达终点
    /// </summary>
    void HandleReachedGoal()
    {
        RemoveFromCurrentBlockIn();
        gameObject.SetActive(false);
        //TAKE DAMAGE
        spawnPoint.RemoveFromEnemiesList(this);
        spawnPoint = null;
        targetWayPoint = null;
        EventManager.current.EnemyReachGoal(enemySO.Price);
    }

    /// <summary>
    /// 将自身怪物引用从当前方格中删除
    /// </summary>
    void RemoveFromCurrentBlockIn()
    {
        Block block=LevelManager.Instance.GetBlockByPosition(currentBlockIn);
        if (block != null)
        {
            block.RemoveEnemy(this);
        }
        if (isGiant)
        {
            Block block2 = LevelManager.Instance.GetBlockByPosition(additionalBlockIn);
            if (block2!=null)
            {
                block2.RemoveEnemy(this);
            }
        }
 
    }

    /// <summary>
    /// 将自身怪物引用添加到当前方格
    /// </summary>

    void AddToCurrentBlockIn()
    {
        Block block = LevelManager.Instance.GetBlockByPosition(currentBlockIn);

        if (block != null)
        {
            block.AddEnemy(this);
        }
        if (isGiant)
        {
            Block block2 = LevelManager.Instance.GetBlockByPosition(additionalBlockIn);
            if (block2!=null)
            {
                block2.AddEnemy(this);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth-=damage;

        if (currentHealth <= 0)
        {
            currentHealth=0;
            RemoveFromCurrentBlockIn();
            gameObject.SetActive(false);

            isDead = true;

            ClearAllEffects();
            if (isLast)
            {
                EnemyManager.Instance.lastDied = true;
      
            }
            enemySO.EnemyDied();
            if (spawnPoint!=null)
            {
                spawnPoint.RemoveFromEnemiesList(this);
                spawnPoint = null;
                targetWayPoint = null;
            }

        }
        enemyHealthBar.SetCurrentHealth(currentHealth);
    }

    void SetsubwayIndex(Waypoint waypoint,int subwaypointIndex)
    {
        if (subwaypointIndex==0)
        {
            int index = Random.Range(0, waypoint.dic_subWaypointsIndex.Count);
            subwaypointIn=waypoint.dic_subWaypointsIndex.ElementAt(index).Key;
        }
        else
        {
            subwaypointIn = subwaypointIndex;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void InitStatus()
    {
        currentHealth = enemySO.Health;
        currentAmmor = enemySO.Ammor;
        currentResistance = enemySO.Resistance;
        moveSpeed = enemySO.Speed;
        isDead = false;
    }

    public void AddTemporaryEffect(Effect effect)
    {
        if (!dic_effects.ContainsKey(effect))
        {
            dic_effects.Add(effect, effect.affectTime);
            dic_effectTimer.Add(effect, effect.affectInterval);
            effect.AffectWhenEnter(this);
        }
        else
        {
            dic_effects[effect] = effect.affectTime;
        }
    }

    public void AddPersistentEffect(Effect effect)
    {
        if (!persistentEffects.Contains(effect))
        {
            persistentEffects.Add(effect);
            effect.AffectWhenEnter(this);
        }
    }

    public void HandleEffectByTime(float delta)
    {
        if (isDead)
        {
            return;
        }
        for (int i = 0; i < dic_effects.Count; i++)
        {
            var kvp = dic_effects.ElementAt(i);
            dic_effects[kvp.Key] -= delta;
            dic_effectTimer[kvp.Key] -= delta;
            if (dic_effectTimer[kvp.Key] <= 0)
            {
                dic_effectTimer[kvp.Key] = kvp.Key.affectInterval;
                kvp.Key.AffectByTime(this);
            }
            if (dic_effectTimer.ContainsKey(kvp.Key))
            {
                if (dic_effects[kvp.Key] <= 0)
                {
                    kvp.Key.AffectWhenExit(this);
                    dic_effects.Remove(kvp.Key);
                    dic_effectTimer.Remove(kvp.Key);
                }
            }      
        }
    }

    void ClearAllEffects()
    {
        persistentEffects.Clear();
        dic_effects.Clear();
        dic_effectTimer.Clear();
    }
}
