using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsFullState : FruitsState
{
    public FruitsFullState(Fruits fruit, FruitsStateMachine stateMachine) : base(fruit, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        fruits.ChangeModel(fruits.full);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
      
        if (Input.GetKeyDown(KeyCode.H)) 
        {     
            fruitsStateMachine.ChangeState(new FruitsHalvedState(fruits, fruitsStateMachine));
        }
        else if (Input.GetKeyDown(KeyCode.R)) 
        {
            fruitsStateMachine.ChangeState(new FruitsRottenState(fruits, fruitsStateMachine));
        }
        else if (Input.GetKeyDown(KeyCode.S)) 
        {
            fruitsStateMachine.ChangeState(new FruitsSlicedState(fruits, fruitsStateMachine));
        }
        else if (Input.GetKeyDown(KeyCode.U)) 
        {
            fruitsStateMachine.ChangeState(new FruitsUnripeState(fruits, fruitsStateMachine));
        }
    }
}
