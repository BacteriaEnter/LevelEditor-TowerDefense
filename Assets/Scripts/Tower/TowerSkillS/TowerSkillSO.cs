using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSkillSO : ScriptableObject
{
    public string skillName;
    public int requestLevel;
    [TextArea]
    public string description;
    public Sprite icon;
    public bool attackPriority;
    public bool crossPriority;

    public virtual void AffectWhenLearned(Tower tower)
    {

    }

    public virtual void AffectWhenTimeTrigger(Tower tower)
    {

    }

    public virtual void AffectWhenAttackTrigger(Tower tower)
    {

    }

    public virtual void AffectWhenAttackTrigger(Enemy enemy)
    {

    }

    public virtual void AffectWhenCrossTheTrap(Enemy enemy)
    {

    }
}
