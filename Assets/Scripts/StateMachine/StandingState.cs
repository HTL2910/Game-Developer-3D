using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : State
{
    float gravityValue;
    bool jump;
    Vector3 currentVelocity;
    bool grounded;
    bool sprint;
    bool move;
    float playerSpeed;
    Vector3 cVelocity;

    public StandingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character= _character;
        stateMachine = _stateMachine;
    }
        
    public override void Enter()
    {
        base.Enter();
        jump = false;
        sprint = false;
        input = Vector2.one;
        velocity = Vector3.zero;
        currentVelocity=Vector3.zero;
        gravityVelocity.y = 0f;

        playerSpeed = character.playerSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKeyDown(KeyCode.J))
        {
            jump = true;
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            move = true;
        }
        velocity=velocity.x*character.cameraTransform.right.normalized+
            velocity.z*character.cameraTransform.forward.normalized;
        velocity.y = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        character.animator.SetFloat("Speed", input.magnitude, character.speedDampTime, Time.deltaTime);
        if (sprint)
        {
            stateMachine.ChangeState(character.sprinting);
        }  
        if (move)
        {
            stateMachine.ChangeState(character.landing);
        }
        if (jump)
        {
            stateMachine.ChangeState(character.jumping);
        }
     
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;

        if(grounded && gravityVelocity.y < 0f)
        {
            gravityVelocity.y = 0f;
        }
        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime);
        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);
        if(velocity.sqrMagnitude>0f)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
        }
    }

    public override void Exit()
    {
        base.Exit();
        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x, 0f, input.y);
        if (velocity.sqrMagnitude > 0f)
        {
            character.transform.rotation = Quaternion.LookRotation(velocity);
        }


    }
}
