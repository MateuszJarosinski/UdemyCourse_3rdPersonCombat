using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

namespace StateMachines.Player
{
    public class PlayerFreeLookState : PlayerBaseState
    {
        private readonly int _freeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
        private readonly int _freeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

        private const float AnimatorDampTime = 0.1f;
        public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.InputReader.TargetEvent += OnTarget;
            stateMachine.Animator.Play(_freeLookBlendTreeHash);
        }
        public override void Tick(float deltaTime)
        {
            if (stateMachine.InputReader.IsAttacking)
            {
                stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
                return;
            }
            
            Vector3 movement = CalculateMovement();

            Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

            if (stateMachine.InputReader.MovementValue == Vector2.zero)
            {
                stateMachine.Animator.SetFloat(_freeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
                return;
            }

            stateMachine.Animator.SetFloat(_freeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
            FaceMovementDirection(movement, deltaTime);
        }
        public override void Exit()
        {
            stateMachine.InputReader.TargetEvent -= OnTarget;
        }

        private Vector3 CalculateMovement()
        {
            Vector3 forward = stateMachine.MainCameraTransform.forward;
            Vector3 right = stateMachine.MainCameraTransform.right;

            forward.y = 0f;
            right.y = 0f;
            
            forward.Normalize();
            right.Normalize();

            return forward * stateMachine.InputReader.MovementValue.y +
                   right * stateMachine.InputReader.MovementValue.x;
        }

        private void FaceMovementDirection(Vector3 movement, float deltaTime)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation,
                Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationDamping);
        }
        
        private void OnTarget()
        {
            if (!stateMachine.Targeter.SelectTarget()) { return; }
            
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }
}