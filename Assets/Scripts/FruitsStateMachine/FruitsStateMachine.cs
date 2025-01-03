using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsStateMachine : MonoBehaviour
{

    public FruitsState currentState;


    public void Init(FruitsState state)
    {
        currentState = state;
        currentState.Enter();
    }
    public void ChangeState(FruitsState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
}
