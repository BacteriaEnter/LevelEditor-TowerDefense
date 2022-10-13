using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/EnemyBattleChannel", fileName = "EnemyBattleChannel")]
public class EnemyBattleChannel : ScriptableObject
{
    public UnityAction<int> onEnemyDied;

    public void EnemyDied(int gold)
    {
        onEnemyDied?.Invoke(gold);
    }

}
