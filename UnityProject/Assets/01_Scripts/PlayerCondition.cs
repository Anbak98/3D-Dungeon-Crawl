using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamagable
{
    private PlayerStatus _statusInfo;

    [Header("Energy")]
    public int health;
    public int Health
    {
        get => health;
        set
        {
            health = value;
            OnHealthChanged?.Invoke();
        }
    }

    [Header("Movement")]
    public float moveSpeed;
    public float MoveSpeed
    {
        get => moveSpeed;
        set
        {
            moveSpeed = value;
            OnMoveSpeedChanged?.Invoke();
        }
    }

    public Action OnHealthChanged;
    public Action OnMoveSpeedChanged;

    // Start is called before the first frame update
    void Awake()
    {
        _statusInfo = ResourceLoadManager.Instance.PlayerStatus;
    }

    private void Start()
    {
        health = _statusInfo.health;
        moveSpeed = _statusInfo.moveSpeed;
    }
    public void TakePhysicalDamage(int damageAmount)
    {
        Health -= damageAmount;
    }
}