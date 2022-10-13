using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/SelectActionChannel",fileName = "SelectActionChannel")]
public class SelectActionChannel : ScriptableObject
{
    public UnityAction<Tower,Vector2Int,List<Vector2Int>>OnSelected;
    public UnityAction<Tower> OnTowerPlaced;


    public void Prompt(Tower tower,Vector2Int pos,List<Vector2Int> towerRange)
    {
        OnSelected?.Invoke(tower,pos, towerRange);
    }

    public void Place(Tower tower)
    {
        OnTowerPlaced?.Invoke(tower);
    }

}
