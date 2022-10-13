using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PlayerAction/SelectTowerInWord", fileName = "SelectTowerInWord")]
public class SelectTowerInWord : PlayerAction
{
    Tower selectTower;
    [SerializeField] ButtonActionChannel channel;
    public override void Back(BattleManager battleManager)
    {
        battleManager.ResetTowerSelect();
        battleManager.ResetAction();
        selectTower.ResetRangePromption();
        selectTower = null;
        battleManager.UpdateUnitInfo(selectTower);
    }

    public override void Enter(BattleManager battleManager)
    {
        selectTower = battleManager.selectTower;
        battleManager.UpdateUnitInfo(selectTower);
        selectTower.SelectedInWorld();
        bool upgradeAvaliable=selectTower.CheckUpgradeAvailable();
        if (upgradeAvaliable)
        {
            channel.UpgradeStatusEnable();
        }
        else
        {
            channel.UpgradeStatusDisable();
        }
    }

    public override void Exit(BattleManager battleManager, InputManager inputManager)
    {
        Back(battleManager);
        channel.UpgradeStatusDisable();
    }

    public override void Logic(BattleManager battleManager, InputManager inputManager, Vector2 mousePosition)
    {
        if (selectTower !=battleManager.selectTower)
        {
            selectTower.ResetRangePromption();
            selectTower = battleManager.selectTower;
            battleManager.UpdateUnitInfo(selectTower);
            selectTower.SelectedInWorld();
        }
    }

    public override void Next(BattleManager battleManager)
    {
        throw new System.NotImplementedException();
    }


    public void CheckTowerUpgradeAvailble()
    {
        bool upgradeAvaliable = selectTower.CheckUpgradeAvailable();
        if (upgradeAvaliable)
        {
            channel.UpgradeStatusEnable();
        }
        else
        {
            channel.UpgradeStatusDisable();
        }
    }

}
