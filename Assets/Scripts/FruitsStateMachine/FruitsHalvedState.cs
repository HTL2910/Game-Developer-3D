using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsHalvedState : FruitsState
{
    public FruitsHalvedState(Fruits fruit, FruitsStateMachine stateMachine) : base(fruit, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        fruits.ChangeModel(fruits.halved);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Input.GetKeyDown(KeyCode.S))
        {
            fruitsStateMachine.ChangeState(new FruitsSlicedState(fruits, fruitsStateMachine));
        }
    }
}
