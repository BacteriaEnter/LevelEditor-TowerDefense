using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Skill/PoisonAttack", fileName = "PoisonAttack")]
public class PoisonAttack : TowerSkillSO
{
    [SerializeField] Effect poison;

    public override void AffectWhenAttackTrigger(Enemy enemy)
    {
        enemy.AddTemporaryEffect(poison);
    }
}
