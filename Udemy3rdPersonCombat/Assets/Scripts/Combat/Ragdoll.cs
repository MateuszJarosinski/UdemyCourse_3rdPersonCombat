using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController _controller;

    private Collider[] _allColliders;

    private Rigidbody[] _allRigidbodies;
    void Start()
    {
        _allColliders = GetComponentsInChildren<Collider>(true);
        _allRigidbodies = GetComponentsInChildren<Rigidbody>(true);
        
        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool isRagdol)
    {
        foreach (Collider collider in _allColliders)
        {
            if (collider.gameObject.CompareTag("Ragdoll"))
            {
                collider.enabled = isRagdol;
            }
        }
        
        foreach (Rigidbody rigidbody in _allRigidbodies)
        {
            if (rigidbody.gameObject.CompareTag("Ragdoll"))
            {
                rigidbody.isKinematic = !isRagdol;
                rigidbody.useGravity = isRagdol;
            }
        }

        _controller.enabled = !isRagdol;
        animator.enabled = !isRagdol;
    }
}

