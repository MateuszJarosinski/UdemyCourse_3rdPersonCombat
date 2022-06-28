using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int _health;

    private bool _isInvunerable;

    public bool IsDead => _health == 0;
    
    public event Action OnTakeDamage;
    public event Action OnDie;


    private void Start()
    {
        _health = maxHealth;
    }

    public void DealDamage(int damage)
    {
        if (_health == 0)
        {
            return;
        }

        if (_isInvunerable)
        {
            return;
        }

        _health = Mathf.Max(_health - damage, 0);
        
        OnTakeDamage?.Invoke();

        if (_health == 0)
        {
            OnDie?.Invoke();
        }
        
        Debug.Log(_health);
    }

    public void SetInvunerable(bool isInvunerable)
    {
        this._isInvunerable = isInvunerable;
    }
    
}
