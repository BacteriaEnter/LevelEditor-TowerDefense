using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy/EnemySO",fileName ="EnemySO")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private int ammor;
    [SerializeField] private int resistance;
    [SerializeField] private float speed;
    [SerializeField] private int bounty;//´ú¼Û
    [SerializeField] EnemyBattleChannel enemyBattleChannel;

    public int Health { get { return health; } }
    public int Price { get { return damage; } }
    public int Ammor { get { return ammor; } }
    public int Resistance { get { return resistance; } }

    public float Speed { get { return speed; } }

    public void EnemyDied()
    {
        enemyBattleChannel.EnemyDied(bounty);
    }
}
