using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName ="PlayerAction/SellAction",fileName ="SellAction")]
public class SellAction : PlayerAction
{
    [SerializeField] ButtonActionChannel buttonActionChannel;
    [SerializeField] LayerMask checkLayer;

    public override void Back(BattleManager battleManager)
    {
        throw new System.NotImplementedException();
    }

    public override void Enter(BattleManager battleManager)
    {
        if (battleManager.selectTower!=null)
        {
            battleManager.selectTower.ResetRangePromption();
            battleManager.selectTower = null;
            battleManager.selectTowerSO = null;
        }
     
        //Change Mouse Icon
    }

    public override void Exit(BattleManager battleManager, InputManager inputManager)
    {
        battleManager.ResetAction();
        Debug.Log("Exit Sell Action");
    }

    public override void Logic(BattleManager battleManager, InputManager inputManager, Vector2 mousePosition)
    {
        if (inputManager.cancel_Input)
        {
            buttonActionChannel.ExitSellActionByChannel();
            Exit(battleManager, inputManager);
        }
        else if (inputManager.confirmTap_input)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit, checkLayer))
            {
                if (hit.collider.CompareTag("Tower"))
                {
                    Tower tower = hit.collider.GetComponent<Tower>();
                    battleManager.GetGold(tower.SellTower());
                    battleManager.RemoveTower(tower);
                }
            }
        }
    }

    public override void Next(BattleManager battleManager)
    {
        throw new System.NotImplementedException();
    }

   
}
