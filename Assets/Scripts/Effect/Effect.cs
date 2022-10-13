using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : ScriptableObject
{
    public bool isPersistent;
    public float affectTime;
    public float affectInterval;
    /// <summary>
    /// 当获得BUFF时触发
    /// </summary>
    /// <param name="tower"></param>
    public virtual void AffectWhenEnter(Tower tower)
    {

    }

    /// <summary>
    /// 当获得BUFF时触发
    /// </summary>
    /// <param name="enemy"></param>
    public virtual void AffectWhenEnter(Enemy enemy)
    {

    }
    /// <summary>
    /// 当BUFF失效时触发
    /// </summary>
    /// <param name="enemy"></param>
    public virtual void AffectWhenExit(Enemy enemy)
    {

    }
    /// <summary>
    /// 当BUFF失效时触发
    /// </summary>
    /// <param name="tower"></param>
    public virtual void AffectWhenExit(Tower tower)
    {

    }

    /// <summary>
    /// 当有建筑放置时触发
    /// </summary>
    /// <param name="tower"></param>
    public virtual void AffectWhenTowerPlaced(Tower tower)
    {

    }

    /// <summary>
    /// 当重计算时触发
    /// </summary>
    /// <param name="tower"></param>
    public virtual void AffectWhenRecalculate(Tower tower)
    {

    }

    /// <summary>
    /// 每个间隔时触发
    /// </summary>
    /// <param name="enemy"></param>
    public virtual void AffectByTime(Enemy enemy)
    {

    }
}
