using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Projectile : Projectile
{
    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        Move();
        if (target != null)
        {
            CheckDistanceBetweenTarget(target);
        }
        LifeTiming(delta);
    }
    protected override void Move()
    {
        if (target!=null)
        {
            if (!target.isDead)
            {
                dir = target.transform.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

               transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            }
            else
            {
                DestryProjectile();
            }
        }
        else
        {
            DestryProjectile();
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

    }

    public override void Init(int damage, Enemy enemy, List<TowerSkillSO> skillSO)
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
