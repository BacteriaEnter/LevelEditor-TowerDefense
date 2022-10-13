using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Tower/RangerTower",fileName ="RangerTower")]
public class RangerTower : BuildingSO
{
    [SerializeField] GameObject projectilePrefab;
    protected override Enemy FindTargetInRange(List<Vector2Int> range)
    {
        return base.FindTargetInRange(range);
    }

    public override bool Attack(Tower tower,float minDamage,float maxDamage,DamageType damageType,List<Vector2Int> range)
    {
        List<Enemy> targets = FindTargetsInRange(range);
        if (targets.Count>0)
        {
            if (targets.Count<=tower.targetCount)
            {
                for (int i = 0; i < targets.Count; i++)
                {

                    Arrow_Projectile projectile = PoolManager.Release(projectilePrefab, tower.transform.position).GetComponent<Arrow_Projectile>();
                    int damage = Mathf.RoundToInt(Random.Range(minDamage, maxDamage));
                    projectile.Init(damage, targets[i], tower.skills_AttackTriggerList);
                }           
            }
            else
            {
                for (int i = 0; i < tower.targetCount; i++)
                {
                    Arrow_Projectile projectile = PoolManager.Release(projectilePrefab, tower.transform.position).GetComponent<Arrow_Projectile>();
                    int damage = Mathf.RoundToInt(Random.Range(minDamage, maxDamage));
                    projectile.Init(damage, targets[i],tower.skills_AttackTriggerList);
                }
            }
            return true;
        }
        return false;
    }

    public override bool CoolDown(ref float currentTimer,float currentInterval,float timeScale)
    {
        currentTimer -= Time.deltaTime*timeScale;
        if (currentTimer <= 0)
        {
            currentTimer = currentInterval;
            return true;
        }
        return false;
    }

  
}
