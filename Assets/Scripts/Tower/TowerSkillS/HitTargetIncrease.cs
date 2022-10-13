using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Tower/Skill/HitTargetIncrease", fileName = "HitTargetIncrease")]
public class HitTargetIncrease : TowerSkillSO
{
    [SerializeField] int increaseTargets;
    public override void AffectWhenLearned(Tower tower)
    {
        tower.targetCount+=increaseTargets;
    }
}
