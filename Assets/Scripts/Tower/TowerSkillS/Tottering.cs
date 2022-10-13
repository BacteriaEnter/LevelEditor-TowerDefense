using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Skill/Tottering", fileName = "Tottering")]
public class Tottering : TowerSkillSO
{
    [SerializeField] Effect slow;

    public override void AffectWhenCrossTheTrap(Enemy enemy)
    {
        enemy.AddTemporaryEffect(slow);
    }
}
