using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower/ApprenticeTower",fileName = "ApprenticeTower")]
public class ApprenticeTower : BuildingSO
{
    [SerializeField] GameObject projectilePrefab;
    protected override Enemy FindTargetInRange(List<Vector2Int> range)
    {
        return base.FindTargetInRange(range);
    }

    public override bool Attack(Tower tower,float minDamage, float maxDamage, DamageType damageType, List<Vector2Int> range)
    {
        Enemy target = FindTargetInRange(range);
        if (target)
        {
            ApprenticeProjectile projectile=PoolManager.Release(projectilePrefab,tower.transform.position).GetComponent<ApprenticeProjectile>();
            int damage=Mathf.RoundToInt(Random.Range(minDamage,maxDamage));
            projectile.Init(damage, target,null);
            return true;
        }
        return false;
    }

    public override bool CoolDown(ref float currentTimer, float currentInterval, float timeScale)
    {
        currentTimer -= Time.deltaTime * timeScale;
        if (currentTimer <= 0)
        {
            currentTimer = currentInterval;
            return true;
        }
        return false;
    }
}
