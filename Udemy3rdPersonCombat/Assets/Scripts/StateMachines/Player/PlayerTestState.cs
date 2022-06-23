using UnityEngine;

namespace StateMachines.Player
{
    public class PlayerTestState : PlayerBaseState
    {
        private float _timer = 5.0f;
        public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
           Debug.Log("Enter");
        }

        public override void Tick(float deltaTime)
        {
            _timer -= deltaTime;
            Debug.Log(_timer);

            if (_timer <= 0)
            {
                stateMachine.SwitchState(new PlayerTestState(stateMachine));
            }
        }

        public override void Exit()
        {
            Debug.Log("Exit");
        }
    }
}