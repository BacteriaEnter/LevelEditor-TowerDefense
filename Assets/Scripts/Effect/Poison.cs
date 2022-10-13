using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Poison", fileName = "Poison")]
public class Poison : Effect
{
    [SerializeField] int poisonDamage;

    public override void AffectWhenEnter(Enemy enemy)
    {
        base.AffectWhenEnter(enemy);
    }

    public override void AffectByTime(Enemy enemy)
    {
        enemy.TakeDamage(poisonDamage);
    }
}
