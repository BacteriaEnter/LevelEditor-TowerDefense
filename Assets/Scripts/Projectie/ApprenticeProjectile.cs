using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApprenticeProjectile : Projectile
{

    private void Update()
    {
        float delta = Time.deltaTime;
        Move();
        if (target!=null)
        {
            CheckDistanceBetweenTarget(target);
        }
        LifeTiming(delta);
    }
    protected override void Move()
    {
        base.Move();
        //if (target.isDead == false)
        //{
        //    Vector3 dir = target.transform.position - transform.position;
   
        //    transform.up = Vector3.Slerp(transform.up, dir,
        //            15 * Time.deltaTime);
        //}
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, speed * Time.deltaTime);
    }
    public override void Init(int damage,Enemy enemy,List<TowerSkillSO> skillSO)
    {
        base.Init(damage, enemy, skillSO);
        this.damage = damage;
        target = enemy;
        skills = skillSO;
    }
    public override void CheckDistanceBetweenTarget(Enemy enemy)
    {
        base.CheckDistanceBetweenTarget(enemy);
    }
}
