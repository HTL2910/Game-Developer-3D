using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : State
{
    bool grounded;
    float gravityValue;
    float jumpHeight;
    float playerSpeed;
    Vector3 airVelocity;

    public JumpingState(Character _character,StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character= _character;
        stateMachine= _stateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        grounded = false;
        gravityValue=character.gravityValue;
        jumpHeight=character.jumpHeight;
        playerSpeed=character.playerSpeed;
        gravityVelocity.y = 0;

        character.animator.SetFloat("Speed", 0f);
        character.animator.SetTrigger("Jump");
        Jump();
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
        if (grounded)
        {
            stateMachine.ChangeState(character.landing);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        if (!grounded)
        {
            velocity = character.playerVelocity;
            airVelocity = new Vector3(input.x, 0f, input.y);
            velocity=velocity.x*character.cameraTransform.right.normalized+
                velocity.z*
                character.cameraTransform.forward.normalized;
            velocity.y = 0f;
            airVelocity=airVelocity.x*
                character.cameraTransform.right.normalized+
                airVelocity.z*
                character.cameraTransform.forward.normalized;
            airVelocity.y = 0f;
            character.controller.Move(gravityVelocity * Time.deltaTime +
                (airVelocity * character.airControl + velocity * (1 - character.airControl)) *
                playerSpeed * Time.deltaTime);
        }
        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
    }
    void Jump()
    {
        gravityVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }
}
