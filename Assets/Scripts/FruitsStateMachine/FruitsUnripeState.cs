using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsUnripeState : FruitsState
{
    public FruitsUnripeState(Fruits fruit, FruitsStateMachine stateMachine) : base(fruit, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        fruits.ChangeModel(fruits.unripe);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Input.GetKeyDown(KeyCode.R))
        {
            fruitsStateMachine.ChangeState(new FruitsRottenState(fruits, fruitsStateMachine));
        }
    }
}
