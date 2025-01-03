using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsSlicedState : FruitsState
{
    public FruitsSlicedState(Fruits fruit, FruitsStateMachine stateMachine) : base(fruit, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        fruits.ChangeModel(fruits.sliced); 
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
