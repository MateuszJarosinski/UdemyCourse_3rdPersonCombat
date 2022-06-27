using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private Attack _attack;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackId) : base(stateMachine)
    {
        _attack = stateMachine.Attacks[attackId];
    }
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, 0.1f);
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {

    }
}
