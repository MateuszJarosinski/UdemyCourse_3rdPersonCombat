using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    
    [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
    
    [field: SerializeField] public int AttackDamage { get; private set; }
    
    [field: SerializeField] public int AttackKnockback { get; private set; }
    
    [field: SerializeField] public Health Health { get; private set; }
    
    [field: SerializeField] public Target Target { get; private set; }
    
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    
    [field: SerializeField] public float MovementSpeed { get; private set; }

    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    
    [field: SerializeField] public float AttackRange { get; private set; }

    [field: SerializeField] public Health Player { get; private set; }

    
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        Agent.updatePosition = false;
        Agent.updateRotation = false;

        SwitchState(new EnemyIdleState(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
    }

    private void HandleTakeDamage()
    {
        SwitchState(new EnemyImpactState(this));
    }
    
    private void HandleDie()
    {
        SwitchState(new EnemyDeadState(this));
    }
}
