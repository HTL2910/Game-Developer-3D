using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CrouchingState : State
{
    float playerSpeed;
    bool belowCeiling;
    bool crouchHeld;
    bool grounded;
    float gravityValue;
    Vector3 currentVelocity;
    public CrouchingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        character.animator.SetTrigger("Crouch");
        belowCeiling = false;
        crouchHeld=false;
        gravityVelocity.y = 0f;

        playerSpeed = character.crouchSpeed;
        character.controller.height = character.crouchColliderHeight;
        character.controller.center = new Vector3(0f, character.crouchColliderHeight / 2f, 0f);
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;
    }

    public override void Exit()
    {
        base.Exit();
        character.controller.height = character.normalColliderHeight;
        character.controller.center = new Vector3(0f, character.normalColliderHeight / 2f, 0f);
        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x, 0f, input.y);
        character.animator.SetTrigger("Move");

    }

    public override void HandleInput()
    {
        base.HandleInput();
        if(crouchAction.triggered&& !belowCeiling)
        {
            crouchHeld = true;
        }
        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0f, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized + 
            velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        character.animator.SetFloat("Speed", input.magnitude, character.speedDampTime, Time.deltaTime);
        if (crouchHeld)
        {
            stateMachine.ChangeState(character.standing);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        belowCeiling = CheckCollisionOverlap(character.transform.position +
            Vector3.up*character.normalColliderHeight);
        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
        if (grounded && gravityVelocity.y < 0f)
        {
            gravityVelocity.y = 0f;
        }
        currentVelocity = Vector3.Lerp(currentVelocity, velocity, character.velocityDampTime);
        character.controller.Move(currentVelocity * Time.deltaTime *
            playerSpeed + gravityVelocity * Time.deltaTime);
        if (velocity.magnitude > 0f)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation,
                Quaternion.LookRotation(velocity), character.rotationDampTime);
        }
    }
    public bool CheckCollisionOverlap(Vector3 targetPosition)
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        Vector3 direction = targetPosition - character.transform.position;
        if(Physics.Raycast(character.transform.position,direction,out hit, character.normalColliderHeight))
        {
            Debug.DrawRay(character.transform.position, direction * hit.distance, Color.yellow);
            return true;
        }
        else
        {
            Debug.DrawRay(character.transform.position, direction * character.normalColliderHeight, Color.white);
            return false;
        }
    }
}
