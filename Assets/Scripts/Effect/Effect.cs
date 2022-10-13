using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : ScriptableObject
{
    public bool isPersistent;
    public float affectTime;
    public float affectInterval;
    /// <summary>
    /// �����BUFFʱ����
    /// </summary>
    /// <param name="tower"></param>
    public virtual void AffectWhenEnter(Tower tower)
    {

    }

    /// <summary>
    /// �����BUFFʱ����
    /// </summary>
    /// <param name="enemy"></param>
    public virtual void AffectWhenEnter(Enemy enemy)
    {

    }
    /// <summary>
    /// ��BUFFʧЧʱ����
    /// </summary>
    /// <param name="enemy"></param>
    public virtual void AffectWhenExit(Enemy enemy)
    {

    }
    /// <summary>
    /// ��BUFFʧЧʱ����
    /// </summary>
    /// <param name="tower"></param>
    public virtual void AffectWhenExit(Tower tower)
    {

    }

    /// <summary>
    /// ���н�������ʱ����
    /// </summary>
    /// <param name="tower"></param>
    public virtual void AffectWhenTowerPlaced(Tower tower)
    {

    }

    /// <summary>
    /// ���ؼ���ʱ����
    /// </summary>
    /// <param name="tower"></param>
    public virtual void AffectWhenRecalculate(Tower tower)
    {

    }

    /// <summary>
    /// ÿ�����ʱ����
    /// </summary>
    /// <param name="enemy"></param>
    public virtual void AffectByTime(Enemy enemy)
    {

    }
}
