using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager current;
    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }

    public UnityAction<int> onEnemyReachGoal;//π÷ŒÔµ÷¥Ô÷’µ„

    public UnityAction onTowerPlacedTriggerEffect;

    public void EnemyReachGoal(int damage)
    {
        onEnemyReachGoal?.Invoke(damage);
    }

    public void TowerPlacedTriggerEffect()
    {
        onTowerPlacedTriggerEffect?.Invoke();
    }
}
