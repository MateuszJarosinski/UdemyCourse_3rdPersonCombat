using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float drag = 0.3f;
    private Vector3 _impact;
    private float _verticalVelocity;

    private Vector3 _dampingVelocity;

    public Vector3 Movement => _impact + Vector3.up * _verticalVelocity;

    private void Update()
    {
        if (_verticalVelocity < 0f && characterController.isGrounded)
        {
            _verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, drag);

        if (agent != null)
        {
            if (_impact.sqrMagnitude < 0.2f * 0.2f)
            {
                agent.enabled = true;
            }
        }

    }

    public void AddForce(Vector3 force)
    {
        _impact += force;
        if (agent != null)
        {
            agent.enabled = false;
        }

    }
}
