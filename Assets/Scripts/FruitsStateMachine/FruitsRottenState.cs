using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsRottenState : FruitsState
{
    public FruitsRottenState(Fruits fruit, FruitsStateMachine stateMachine) : base(fruit, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        fruits.ChangeModel(fruits.rotten);
        fruits.StartCoroutine(fruits.DisableAndReactivate()); //
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }
}
