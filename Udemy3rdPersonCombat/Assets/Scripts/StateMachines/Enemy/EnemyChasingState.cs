using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyChasingState : EnemyBaseState
{
    private readonly int _locomotionHash = Animator.StringToHash("Locomotion");
    private readonly int _speedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(_locomotionHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if(!IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        else if (IsInAttackRange())
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
        }

        MoveToPlayer(deltaTime);
        
        FacePlayer();

        stateMachine.Animator.SetFloat(_speedHash, 1f, AnimatorDampTime, deltaTime);
    }

    private bool IsInAttackRange()
    {
        if (stateMachine.Player.IsDead)
        {
            return false;
        }
        
        float playerDistanveSqr =
            (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDistanveSqr <= stateMachine.AttackRange * stateMachine.AttackRange;
    }

    private void MoveToPlayer(float deltaTime)
    {
        if (stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.destination = stateMachine.Player.transform.position;

            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }
        
        stateMachine.Agent.velocity = stateMachine.CharacterController.velocity;   
    }


    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }
}
