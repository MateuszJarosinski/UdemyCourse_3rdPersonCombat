using System.Collections;
using System.Collections.Generic;
using StateMachines.Player;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    void Start()
    {
        SwitchState(new PlayerTestState(this));
    }

}
