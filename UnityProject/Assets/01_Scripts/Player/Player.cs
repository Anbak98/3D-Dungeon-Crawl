using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerCondition status;
    public PlayerController controller;
    public ItemData rootedItem;
    public Action addItem;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        status.OnAwake();
    }

    private void Start()
    {
        controller.OnStart();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (!Application.isFocused) return;
        controller.OnFixedUpdate();
    }

    private void LateUpdate()
    {
        if (!Application.isFocused) return;
        controller.OnLateUpdate();
    }
}
