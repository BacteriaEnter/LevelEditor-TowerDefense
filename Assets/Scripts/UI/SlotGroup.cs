using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGroup : MonoBehaviour
{
    [SerializeField] UIManager uIManager;
    [SerializeField] List<TowerSlot> slots;
    public void SelectTower(int i)
    {
        uIManager.SelectTower(i);
    }

    public void InitTowerSlot(List<BuildingSO> buildingSOList)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Sprite icon = null; 
            bool outOfIcon = true;
            if (i<buildingSOList.Count)
            {
                icon = buildingSOList[i].icon;
                outOfIcon = false;
            }
            slots[i].InitSlot(outOfIcon, icon);
        }
    }
}
