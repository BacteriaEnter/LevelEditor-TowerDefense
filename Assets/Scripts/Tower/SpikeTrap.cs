using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower/SpikeTrap", fileName = "SpikeTrap")]
public class SpikeTrap : BuildingSO
{

    protected override Enemy FindTargetInRange(List<Vector2Int> range)
    {
        return base.FindTargetInRange(range);
    }

    public override bool Attack(Tower tower, float minDamage, float maxDamage, DamageType damageType, List<Vector2Int> range)
    {
        List<Enemy> targets = FindTargetsInRange(range);
        if (targets.Count>0&&tower.currentLevel>=1)
        {
            Debug.Log("Attack");
            tower.anim.Play("Attack");
            int damage = Mathf.RoundToInt(Random.Range(minDamage, maxDamage));
            foreach (var target in targets)
            {
                target.TakeDamage(damage);
            }
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

    public override void TriggerSkillWhenEnemyCross(Tower tower, List<Vector2Int> range)
    {

        List<Enemy> targets=FindTargetsInRange(range);
        if (targets!=null)
        {
            foreach (var target in targets)
            {
                if (tower.skills_CrossTriggerList.Count > 0)
                {
                    foreach (var skill in tower.skills_CrossTriggerList)
                    {
                        skill.AffectWhenCrossTheTrap(target);
                    }
                }
            }    
        }
       
    }
}
