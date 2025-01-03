using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LandingState : State
{
    float timePassed;
    float landingTime;
    public LandingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        timePassed = 0f;
        character.animator.SetTrigger("Land");
        landingTime = 0.5f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (timePassed > landingTime)
        {
            character.animator.SetTrigger("Move");
            character.animator.SetFloat("Speed",3f);
            stateMachine.ChangeState(character.standing);
        }
        timePassed += Time.deltaTime;
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
