using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsState 
{
    protected Fruits fruits;
    protected FruitsStateMachine fruitsStateMachine;

    public FruitsState(Fruits fruits, FruitsStateMachine fruitsStateMachine)
    {
        this.fruits = fruits;
        this.fruitsStateMachine = fruitsStateMachine;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void Exit() { }
}
