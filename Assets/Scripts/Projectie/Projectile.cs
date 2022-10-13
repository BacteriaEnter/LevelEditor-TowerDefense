using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected float lifeTime = 5f;
    [SerializeField] protected float timer;
    [SerializeField] protected Enemy target;
    [SerializeField] protected Vector3 dir;
    [SerializeField] protected List<TowerSkillSO> skills;

    protected virtual void Move()
    {
        if (!target.isDead)
        {
            dir = target.transform.position - transform.position;

        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
    }

    protected void DestryProjectile()
    {
        skills = null;
        gameObject.SetActive(false);
    }

    public virtual void Init(int damage,Enemy enemy,List<TowerSkillSO> skillSO)
    {
        timer = lifeTime;
    }

    public virtual void CheckDistanceBetweenTarget(Enemy enemy)
    {
        if (Vector2.Distance(transform.position,enemy.transform.position)<=0.1f)
        {          
            enemy.TakeDamage(damage);
            if (skills != null)
            {
                foreach (var skill in skills)
                {
                    skill.AffectWhenAttackTrigger(enemy);
                }
            }
            target = null;
            DestryProjectile();
        }
    }
    

    protected void LifeTiming(float delta)
    {
        timer -= delta;
        if (timer <= 0)
        {
            DestryProjectile();
        }
    }
}
