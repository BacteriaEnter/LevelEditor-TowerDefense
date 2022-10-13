using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSO : ScriptableObject
{
    public Sprite icon;
    public string towerName;
    public GameObject prefab;
    public Vector2Int[] range;
    [SerializeField] DamageType damageType;
    public DamageType DamageType { get { return damageType; } }
    [SerializeField] PlaceableType placeableType;
    public PlaceableType PlaceableType { get { return placeableType; } }

    //Status

    [Header("Level Status")]
    public List<int> minDamage;
    public List<int> maxDamage;
    public List<float> baseInterval;
    public List<int> cost;
    public int targetCount;
    public List<TowerSkillSO> towerSkills;
    //Check Enemy

    public virtual bool Attack(Tower tower,float minDamage, float maxDamage, DamageType damageType, List<Vector2Int> range)
    {
        return false;
    }

    protected virtual Enemy FindTargetInRange(List<Vector2Int> range)
    {
        List<Enemy> targets = new List<Enemy>();
        Enemy target=null;
        foreach (var item in range)
        {
            Block block=LevelManager.Instance.GetBlockByPosition(item);
            if (block!=null)
            {
                Enemy enemy = block.GetEnemy();
                if (enemy != null)
                {
                    targets.Add(enemy);
                }
            }
          
        }
        if (targets.Count>1)
        {
            SortTargets(targets, 0, targets.Count-1);
        }
        if (targets.Count>0)
        {
            target = targets[0];
        }
        return target;
    }

    protected virtual List<Enemy> FindTargetsInRange(List<Vector2Int> range)
    {
        List<Enemy> targets = new List<Enemy>();
        Enemy target = null;
        foreach (var item in range)
        {
            Block block = LevelManager.Instance.GetBlockByPosition(item);
            if (block != null)
            {
                Enemy enemy = block.GetEnemy();
                if (enemy != null)
                {
                    targets.Add(enemy);
                }
            }

        }
        if (targets.Count > 1)
        {
            SortTargets(targets, 0, targets.Count - 1);
        }

        return targets;
    }

    public virtual bool CoolDown(ref float coolDownTimer,float currentInterval, float timeScale)
    {
        return false;
    }



    private void SortTargets(List<Enemy> enemies, int start, int end)
    {
        if (start < end)
        {
            int mid = Paritition(enemies, start, end);
            SortTargets(enemies, start, mid - 1);
            SortTargets(enemies, mid + 1, end);
        }
    }


    int Paritition(List<Enemy> enemies, int start, int end)
    {
        Enemy pivot = enemies[end];
        int j = start;
        for (int i = start; i < end; i++)
        {
            float pos = Vector3.Dot(pivot.transform.forward, enemies[i].transform.position - pivot.transform.position);
            float angle = Mathf.Acos(pos);
            if (angle>=0&&angle<=90f)
            {
                Enemy temp = enemies[i];
                enemies[i] = enemies[j];
                enemies[j] = temp;
                j++;
            }
        }
        enemies[end] = enemies[j];
        enemies[j] = pivot;
        return j;
    }


    protected void TiggerSkillByAttack(Tower tower, Enemy enemy)
    {
        foreach (var skill in tower.skills_AttackTriggerList)
        {
            skill.AffectWhenAttackTrigger(enemy);
        }
    }
    public virtual void TriggerSkillWhenEnemyCross(Tower tower, List<Vector2Int> range)
    {

    }
}
