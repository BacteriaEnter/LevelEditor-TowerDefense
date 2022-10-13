using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Effect/Rally",fileName ="Rally")]
public class Rally : Effect
{
    [SerializeField] Vector2Int[] checkPosition;
    [SerializeField] BuildingSO checkBuilding;
    [SerializeField] float multiplier;//周围每个学徒塔给予的冷却缩短倍率
    public override void AffectWhenEnter(Tower tower)
    {
        AffectRally(tower);
        EventManager.current.onTowerPlacedTriggerEffect+=tower.AffectEffectWhenTowerPlaced;
    }

    public override void AffectWhenTowerPlaced(Tower tower)
    {
        AffectRally(tower);
    }

    public override void AffectWhenExit(Tower tower)
    {
        EventManager.current.onTowerPlacedTriggerEffect -= tower.AffectEffectWhenTowerPlaced;
    }

    void AffectRally(Tower tower)
    {
        int count = 0;
        foreach (var pos in checkPosition)
        {
            Block block = LevelManager.Instance.GetBlockByPosition(pos+tower.towerPosition);
            if (block != null)
            {
                if (block.towerOnTile != null)
                {
                    if (block.towerOnTile.towerSO == checkBuilding)
                    {
                        count++;
                    }
                }   
            }
        }
        tower.currentInterval = tower.towerSO.baseInterval[tower.currentLevel] - multiplier * count;
    }
}
