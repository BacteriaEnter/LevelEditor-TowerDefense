using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerAction/SelectTower",fileName ="TowerSelect")]
public class TowerSelected : PlayerAction
{
    [SerializeField] SelectActionChannel selectActionChannel;
    Tower selectTower;
    public override void Back(BattleManager battleManager)
    {
        LevelManager.Instance.ResetRangePromption(selectTower.rangeInWorld);
        LevelManager.Instance.ResetSelectBlockPromption();
        Destroy(battleManager.selectTower.gameObject);
        battleManager.ResetTowerSelect();
        battleManager.ResetAction();
    }

    public override void Enter(BattleManager battleManager)
    {
        selectTower = battleManager.selectTower;
    }

    public override void Exit(BattleManager battleManager, InputManager inputManager)
    {
        battleManager.ResetTowerSelect();
        battleManager.ResetAction();
    }

    public override void Logic(BattleManager battleManager,InputManager inputManager,Vector2 mousePosition)
    {
    
        selectTower.transform.position = mousePosition;
        Vector2Int towerPosition = Vector2Int.CeilToInt(mousePosition);
        selectTower.PromptRange_Select(towerPosition);
        selectActionChannel.Prompt(selectTower,towerPosition, selectTower.rangeInWorld);

        //…Ë÷√À˛
        if (inputManager.rotateL_Input)
        {
            selectTower.RotateLeft();
        }
        else if (inputManager.rotateR_Input)
        {
            selectTower.RotateRight();
        }
    }

    public override void Next(BattleManager battleManager)
    {
        throw new System.NotImplementedException();
    }
}
