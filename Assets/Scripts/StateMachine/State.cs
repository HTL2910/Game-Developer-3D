using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class State 
{
    public Character character;
    public StateMachine stateMachine;

    protected Vector3 gravityVelocity;
    protected Vector3 velocity;
    protected Vector2 input;

    public State(Character _character,StateMachine _stateMachine)
    {
        character=_character;
        stateMachine=_stateMachine;


    }
    public virtual void Enter()
    {
        Debug.Log("Enter State: " + this.ToString());

    }
    public virtual void HandleInput()
    {

    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicUpdate()
    {

    }
    public virtual void Exit()
    {

    }
}
