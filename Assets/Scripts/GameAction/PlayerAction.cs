using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerAction : ScriptableObject
{
    public PlayerAction prevAction;
    public PlayerAction nextAction;

    public abstract void Enter(BattleManager battleManager);

    public abstract void Logic(BattleManager battleManager, InputManager inputManager, Vector2 mousePosition);

    public abstract void Next(BattleManager battleManager);

    public abstract void Back(BattleManager battleManager);

    public abstract void Exit(BattleManager battleManager,InputManager inputManager);



}
