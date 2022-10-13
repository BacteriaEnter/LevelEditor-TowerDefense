using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/Slow", fileName = "Slow")]
public class Slow : Effect
{
    [SerializeField] float speedDown;
    public override void AffectWhenEnter(Enemy enemy)
    {
        enemy.moveSpeed-=speedDown;
    }

    public override void AffectWhenExit(Enemy enemy)
    {
        enemy.moveSpeed = enemy.enemySO.Speed;
    }
}
