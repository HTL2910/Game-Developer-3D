using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SprintState : State
{
    float gravityValue;
    Vector3 currentVelocity;
    bool grounded;
    bool sprint;
    float playerSpeed;
    Vector3 cVelocity;
    public SprintState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        sprint = false;
        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0f;

        playerSpeed = character.sprintSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        velocity = new Vector3(input.x, 0f, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized +
            velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;
        if(Input.GetKeyDown(KeyCode.LeftShift) || input.sqrMagnitude == 0f)
        {
            sprint = false;
        }
        else
        {
            sprint = true;
        }
      
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (sprint)
        {
            character.animator.SetFloat("Speed", input.magnitude + 0.5f, character.speedDampTime, Time.deltaTime);


        }
        else
        {
            stateMachine.ChangeState(character.standing);
        }
   
    }


    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
        if(grounded && gravityVelocity.y<0f)
        {
            gravityVelocity.y = 0f;
        }
        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime);
        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);
        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation,
                Quaternion.LookRotation(velocity), character.rotationDampTime);
        }

    }
}
