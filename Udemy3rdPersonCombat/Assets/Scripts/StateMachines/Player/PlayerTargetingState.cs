using System.Collections;
using System.Collections.Generic;
using StateMachines.Player;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int _targetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    
    private readonly int _targetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int _targetingRightHash = Animator.StringToHash("TargetingRight");
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.CancelEvent += OnCancel;
        stateMachine.Animator.Play(_targetingBlendTreeHash);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);
        
        UpdateAnimator(deltaTime);
        
        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCancel;
    }

    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();
        
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
    
    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
            
        return movement;
    }

    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y == 0)
        {
            stateMachine.Animator.SetFloat(_targetingForwardHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(_targetingForwardHash, value, 0.1f, deltaTime);
        }
        
        if (stateMachine.InputReader.MovementValue.x == 0)
        {
            stateMachine.Animator.SetFloat(_targetingRightHash, 0,  0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(_targetingRightHash, value,  0.1f, deltaTime);
        }
    }
}
